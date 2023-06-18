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

// [POST] /phone
     phone(req,res) {
      const customerNumber = req.body.phone;
      const bgId = req.body.bgId; 
      Game.findOneAndUpdate(
        { MaTTBG: bgId, TinhTrangMuon: 'Chưa được thuê', TinhTrangBG: 'Tốt' },
        { DatHang: customerNumber, TinhTrangMuon: 'Đang giữ hàng' },
        { new: true }
      ).exec()

      .then(updatedThongtinBG => {
        if (updatedThongtinBG) {
          const expirationDate = new Date();
          expirationDate.setHours(expirationDate.getHours() + 24); // Cộng thêm 24 giờ

        } else {
          res.status(500).json({ error: error.message });
        }
      })
      
      .catch(error => {
        // Xử lý lỗi nếu có
        res.status(500).json({ error: error.message });
      });
}
}

module.exports = new SiteController();

