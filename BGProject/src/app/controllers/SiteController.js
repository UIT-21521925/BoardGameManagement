const ThongtinBG = require('../models/TTBG');
const LoaiBoardGame = require('../models/LOAIBG');
const Game = require('../models/Game');
const { render } = require('node-sass');

class SiteController {

//   index(req, res, next) {
    
//  // Lấy danh sách board game và loại board game    
//         ThongtinBG.find().exec()
//         .then( (ThongtinBGs) => {
//           const tinhTrangOptions = ["Đang thuê", "Đang giữ hàng"];
//           Game.countDocuments({MaTTBG: ThongtinBG._id, TinhTrangMuon: { $in: tinhTrangOptions }}).exec()
//           .then(count => {
//               const tinhtrang = count < ThongtinBGs.SoLuong ? 1 : 0; // So sánh số lượng game đã được thuê/giữ hàng với số lượng tài liệu game
//               LoaiBoardGame.find({}).exec()
//               .then (LoaiBoardGames => {
//                 LoaiBoardGames = LoaiBoardGames.map(LoaiBG => LoaiBG.toObject());
//                 ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
//                   res.render('home.handlebars', {
//                     LoaiBoardGames: LoaiBoardGames,
//                     ThongtinBGs: ThongtinBGs,
//                     tinhtrang :  tinhtrang
//                   });
//               })
            
//           })
//           .catch(next);
//         })
//         .catch(next);
       
// }
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
            ThongtinBG.find({MaLBG : value }).exec()
            .then(ThongtinBGs => {
                ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
                res.render('boardgame/search.handlebars', {ThongtinBGs : ThongtinBGs })
            })
        .catch(next);
    }

// [POST] /phone
phone(req, res) {
  const customerNumber = req.body.phone;
  const bgId = req.body.bgId;
// quỷ bg
//   Game.findOneAndUpdate(
//     { MaTTBG: bgId, TinhTrangMuon: 'Chưa được thuê', TinhTrangBG: 'Tốt' },
//     { DatHang: customerNumber, TinhTrangMuon: 'Đang giữ hàng', NgayDatHang: new Date()},
//     { new: true }
//   ).exec()
//     .then(updatedGame => {
//       if (updatedGame) {
//         // Board game exists in Game and successfully updated
//         return ThongtinBG.findOneAndUpdate({ _id: bgId }, { $inc: { SoLuong: -1 } }, { new: true }).exec()
//           .then(updatedThongtinBG => {
//             if (!updatedThongtinBG) {
//               // Board game does not exist in ThongtinBG or unable to update
//               throw new Error('Không thể cập nhật thông tin board game.');
//             }

//             const createAt = updatedGame.createAt;
//             createAt.setMinutes(createAt.getMinutes() + 2); 

//             const expirationDate = createAt.toLocaleString("en-US", { timeZone: "Asia/Jakarta" });
//             console.log(expirationDate)// Add 2 minutes to createAt

//             const currentDateTime = new Date().toLocaleString("en-US", {timeZone: "Asia/Jakarta"});
//             console.log(currentDateTime )
            
//             if (currentDateTime > expirationDate) {
//               return Game.findOneAndUpdate(
//                 { MaTTBG: bgId, TinhTrangMuon: 'Đang giữ hàng' },
//                 { TinhTrangMuon: 'Chưa được thuê', DatHang: '', createAt: currentDateTime }
//               ).exec()
//                 .then(() => {
//                   return ThongtinBG.findOneAndUpdate(
//                     { _id: bgId },
//                     { $inc: { SoLuong: 1 } },
//                     { new: true }
//                   ).exec();
//                 })
//                 .catch(error => {
//                   // Handle error if any
//                   res.status(500).json({ error: error.message });
//                 });
//             }
//           })
//           .catch(error => {
//             // Handle error if any
//             res.status(500).json({ error: error.message });
//           });
//       } else {
//         // Board game does not exist in Game
//         return ThongtinBG.findOneAndUpdate({ _id: bgId }, { SoLuong: 0 }, { new: true }).exec();
//       }
//     })
//     .then(updatedThongtinBG => {
//       if (updatedThongtinBG) {
//         res.redirect('/');
//       }
//       // //  thành công
//       // res.redirect('/');
//     })
//     .catch(error => {
//       // Handle error if any
//       res.status(500).json({ error: error.message });
//     });
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
      }, 30 * 1000);
    }
  })
  .catch((error) => {
    // Xử lý lỗi nếu có
    res.status(500).json({ error: error.message });
  });
}
}

module.exports = new SiteController();

