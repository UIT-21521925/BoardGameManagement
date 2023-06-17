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
      const bgId = req.body.bgId; //
      Game.findOne({ DatHang: customerNumber }).exec()
      .then(result => {
        if (!result) {
          // Số điện thoại chưa tồn tại trong cơ sở dữ liệu, lưu số điện thoại mới
            let newCustomerNumber = new Game({ MaTTBG : bgId, DatHang: customerNumber, TinhTrangThue : 'Đang giữ chỗ', TinhTrangBG:'Good' });
            return newCustomerNumber.save();
        } 
        ThongtinBG.findOneAndUpdate({_id : bgId },{ $inc: { SoLuong: -1}, }).exec()
      })
      .then(() => {
          return ThongtinBG.findOneAndUpdate({_id : bgId} ,{$inc: { SoLuong: -1}, },{ new: true }).exec()
      })
      .then(updatedThongtinBG => {
        if (updatedThongtinBG) {
          // const expirationDate = new Date();
          // expirationDate.setHours(expirationDate.getHours() + 24); // Cộng thêm 24 giờ
          // // hien thi tinh trang board game
          // res.locals.successmessage = true;
          //  res.cookie('successmessage', true); 
   //   res.render('/home.handlebars', { expirationDate,successmessage : true, ThongtinBG });
          res.redirect('/')
        } else {
          // Không tìm thấy ThongtinBG với ID tương ứng
          res.status(404).json({ error: 'Không tìm thấy ThongtinBG' });
        }
      })
      
      .catch(error => {
        // Xử lý lỗi nếu có
        res.status(500).json({ error: error.message });
      });


      Game.findByIdAndRemove(bgId).exec()
      .then(deletedThongtinBG => {
        if (deletedThongtinBG) {
          // Cập nhật số lượng của ThongtinBG
          return ThongtinBG.findOneAndUpdate({ _id: bgId }, { $inc: { SoLuong: 1 } }, { new: true }).exec();
        } else {
          res.status(404).json({ error: 'Không tìm thấy ThongtinBG' });
        }
      })
      .then(updatedThongtinBG => {
        if (updatedThongtinBG) {
          res.redirect('/');
        } else {
          res.status(404).json({ error: 'Không tìm thấy ThongtinBG' });
        }
      })
      .catch(error => {
        res.status(500).json({ error: error.message });
      });
    }
  

}

module.exports = new SiteController();

