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
        const value = req.params._id;
            ThongtinBG.find({MaLBG : value })
            .then(ThongtinBGs => {
                ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
                res.render('boardgame/search.handlebars', {ThongtinBGs : ThongtinBGs })
            })
        .catch(next);
    }

    // regrister(req,res,next) {
    //   res.send(r)
    // }

    phone(req,res) {
      const customerNumber = req.body.phone;
      const bgId = req.body.bgId; //
      Game.findOne({ DatHang: customerNumber }).exec()
      .then(result => {

        if (!result) {
          // Số điện thoại chưa tồn tại trong cơ sở dữ liệu, lưu số điện thoại mới
          let newCustomerNumber = new Game({ DatHang: customerNumber });
          return newCustomerNumber.save();
        } 
        ThongtinBG.findOneAndUpdate({_id : bgId , SoLuong: -1}).exec()
        // res.send(bgId)
      })
      .then(() => {
          return ThongtinBG.findOneAndUpdate({_id : bgId} ,{$inc: { SoLuong: 1}, },{ new: true }).exec()
      })
      .then(updatedThongtinBG => {
        if (updatedThongtinBG) {
          // Cập nhật thành công số lượng của ThongtinBG
          res.json({ message: 'Cập nhật số lượng thành công' });
        } else {
          // Không tìm thấy ThongtinBG với ID tương ứng
          res.status(404).json({ error: 'Không tìm thấy ThongtinBG' });
        }
      })
      .catch(error => {
        // Xử lý lỗi nếu có
        res.status(500).json({ error: error.message });
      });

    }

}

module.exports = new SiteController();

