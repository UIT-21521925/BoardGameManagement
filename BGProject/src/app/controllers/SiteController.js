const ThongtinBG = require('../models/TTBG');
const LoaiBoardGame = require('../models/LOAIBG');
const Game = require('../models/Game');
const { render } = require('node-sass');
const { query } = require('express');

class SiteController {

index(req, res, next) {
  ThongtinBG.find().exec()
    .then((ThongtinBGs) => {
      LoaiBoardGame.find({}).exec()
      .then(LoaiBoardGames => {
        ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
        const promises = ThongtinBGs.map((ThongtinBG) => {
          return Game.countDocuments({
              MaTTBG: ThongtinBG._id,
              TinhTrangMuon: { $in: ["Đang thuê", "Đang giữ hàng"] }
          }).exec()
      });
      Promise.all(promises)
          .then((counts) => {
              const processedBoardGames = ThongtinBGs.map((ThongtinBG, index) => {
                  const tinhTrang = counts[index] < ThongtinBG.SoLuong ? 1 : 0;
                  return {
                      _id: ThongtinBG._id,
                      TenBoardGame: ThongtinBG.TenBoardGame,
                      GiaThue: ThongtinBG.GiaThue,
                      HinhAnh : ThongtinBG.HinhAnh,
                      tinhTrang: tinhTrang
                  };
              });
            LoaiBoardGames = LoaiBoardGames.map(LoaiBG => LoaiBG.toObject());
              res.render('home.handlebars',{LoaiBoardGames: LoaiBoardGames, boardGames: processedBoardGames, ThongtinBGs : ThongtinBGs });
          })
          .catch((error) => {
              console.log('Lỗi:', error);
              res.render('error');
          });
      })
      .catch(next);
    })
    .catch(next);
}
      // get /search/MaLBG
    searchbox(req,res,next) {
        var value = req.params._id;
        var keyName = req.body.name;
        LoaiBoardGame.find({TenLBG: keyName}).exec()
          .then(LoaiBoardGames => {
            ThongtinBG.find({MaLBG : value }).exec()
            .then(ThongtinBGs => {
                ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
                res.render('boardgame/search.handlebars', {ThongtinBGs : ThongtinBGs })
            })
            .catch(next);
      })
          .catch(next);
     
    }
    //post /search/q
    search(req, res) {
      var keyName = req.body.q;
      ThongtinBG.find({TenBoardGame : {$regex :keyName}}).exec()
        .then( ThongtinBG => {
          ThongtinBG = ThongtinBG.map(ThongtinBG => ThongtinBG.toObject());
          res.render('boardgame/search.handlebars', {ThongtinBGs : ThongtinBG })
        })

    }

// [POST] /phone
phone(req, res) {
  const customerNumber = req.body.phone;
  const bgId = req.body.bgId;

  Game.findOneAndUpdate(
    {
      MaTTBG: bgId,
      TinhTrangMuon: 'Chưa được thuê',
      TinhTrangBG: 'Tốt',
    },
    {
      TinhTrangMuon: 'Đang giữ hàng',
      DatHang: customerNumber,
      NgayGiuHang: new Date(),
    }
  ).exec()
    .then((game) => {
      if (!game) {
        // Board game không đáp ứng các điều kiện để cập nhật trạng thái
        // Thực hiện các hành động khác tùy thuộc vào logic của bạn
        res.status(500).json({ error: 'Không thể cập nhật trạng thái' });
      } else {
        // Lưu thành công, render lại trang home
        res.redirect('/');
        setTimeout(() => {
          Game.findOneAndUpdate(
            {
              MaTTBG: bgId,
              TinhTrangMuon: 'Đang giữ hàng',
            },
            {
              TinhTrangMuon: 'Chưa được thuê',
              DatHang: "",
            }
          ).exec()
            .then(() => {
              // Cập nhật thành công
              console.log('Tình trạng mượn đã được cập nhật.');
            })
            .catch((error) => {
              console.log('Lỗi khi cập nhật tình trạng mượn:', error);
            });
        }, 30 * 20000);
      }
    })
    .catch((error) => {
      // Xử lý lỗi nếu có
      res.status(500).json({ error: error.message });
    });
  }
}

module.exports = new SiteController();

