const ThongtinBG = require('../models/TTBG');
const LoaiBoardGame = require('../models/LOAIBG');
const Game = require('../models/Game');
const { render } = require('node-sass');

class SiteController {

  index(req, res, next) {
 // Lấy danh sách board game và loại board game    
 
    ThongtinBG.find().exec()
    .then( (ThongtinBGs) => {
      LoaiBoardGame.find({}).exec()
        .then((LoaiBoardGames) => {
          LoaiBoardGames = LoaiBoardGames.map(LoaiBG => LoaiBG.toObject());
            ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
              res.render('home.handlebars', {
                LoaiBoardGames: LoaiBoardGames,
                ThongtinBGs: ThongtinBGs
              });
        })
        .catch(next);
    })
    .catch(next);
  }

      // get /search/MaLBG
      searchbox(req,res,next) {
        var value = req.params._id;
            ThongtinBG.find({MaLBG : value }).exec()
            .then(ThongtinBGs => {
                ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
                res.render('boardgame/search.handlebars', {ThongtinBGs : ThongtinBGs })
            })
        .catch(next);
    }

// [POST] /phone
     phone(req,res) {
      const customerNumber = req.body.phone;
      const bgId = req.body.bgId; 
      Game.findOneAndUpdate(
        { MaTTBG: bgId, TinhTrangMuon: 'Chưa được thuê', TinhTrangBG: 'Tốt' },
        { DatHang: customerNumber, TinhTrangMuon: 'Đang giữ hàng' },
        { new: true }
      )
        .exec()
        .then(updatedGame => {
          if (updatedGame) {
            // Board game tồn tại trong Game và đã được cập nhật thành công
            // kiểm tra và cập nhật ThongtinBG
            return ThongtinBG.findOneAndUpdate({ _id: bgId }, { $inc: { SoLuong: -1 } }, { new: true }).exec();
            // $inc: { SoLuong: -1 } để giảm giá trị SoLuong đi 1 đơn vị
          } else {
            // Board game không tồn tại trong Game
            //Đặt giá trị SoLuong trong ThongtinBG thành 0
            return ThongtinBG.findOneAndUpdate({ _id: bgId }, { SoLuong: 0 }, { new: true }).exec();
          }
        })
        .catch(error => {
          // Xử lý lỗi nếu có
          res.status(500).json({ error: error.message });
        })

      .then(updatedThongtinBG => {
        if (updatedThongtinBG) {
          var expirationDate = new Date();
                        expirationDate.setMinutes(expirationDate.getMinutes() + 1);
                        console.log(expirationDate)// Cộng thêm 24 giờ
          Game.find({TinhTrangMuon : 'Đang giữ hàng', DatHang : customerNumber }).exec()
            .then(Games => {
              if (new Date() >= expirationDate ) {
                // Reset giá trị DatHang thành rỗng và TinhTrangMuon thành 'Chưa được thuê'
                Games.DatHang = '';
                Games.TinhTrangMuon = 'Chưa được thuê';
                // Lưu lại ThongtinBG đã được cập nhật
                Games.save();
                
              return ThongtinBG.findOneAndUpdate({ _id: bgId }, { SoLuong: 1 }, { new: true }).exec();
              }
              
            })
            .catch(error => {
              // Xử lý lỗi nếu có
              res.status(500).json({ error: error.message });
            });
        }
      })
      
      .catch(error => {
        // Xử lý lỗi nếu có
        res.status(500).json({ error: error.message });
      });
}
}

module.exports = new SiteController();

