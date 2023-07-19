const ThongtinBG = require('../models/TTBG');
const LoaiBoardGame = require('../models/LOAIBG');
const Game = require('../models/Game');
const { render } = require('node-sass');
const { query } = require('express');

class SiteController {

  index(req, res, next) {
    ThongtinBG.find({}).exec()
      .then((ThongtinBGs) => {
        LoaiBoardGame.find({}).exec()
          .then(LoaiBoardGames => {
            ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
            const promises = ThongtinBGs.map( ThongtinBG => {
              return Game.find({ MaTTBG: ThongtinBG._id }).exec()
                .then((games) => {
                  let countIndex = 0;
                  for (const game of games) {
                    if ((game.TinhTrangBG !== "Hỏng" && (game.TinhTrangMuon == "Đang giữ hàng" || game.TinhTrangMuon == "Đang thuê")) || (game.TinhTrangBG == "Hỏng" && game.TinhTrangMuon == "Chưa được thuê")) {
                      countIndex += 1; // Tăng số lượng nếu có tình trạng mượn là "Đang giữ hàng" hoặc "Đang thuê"
                    }
                  }
                  return countIndex;
                });
            });
  
            Promise.all(promises)
              .then((counts) => {
                const processedBoardGames = ThongtinBGs.map((ThongtinBG, index) => {
                  const tinhTrang = counts[index] < ThongtinBG.SoLuong ? 1 : 0;
                  return {
                    _id: ThongtinBG._id,
                    TenBoardGame: ThongtinBG.TenBoardGame,
                    GiaThue: ThongtinBG.GiaThue,
                    HinhAnh: ThongtinBG.HinhAnh,
                    tinhTrang: tinhTrang
                  };
                });
  
                LoaiBoardGames = LoaiBoardGames.map(LoaiBG => LoaiBG.toObject());
                res.render('home.handlebars', {
                  LoaiBoardGames: LoaiBoardGames,
                  boardGames: processedBoardGames,
                  ThongtinBGs: ThongtinBGs
                });
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
                const promises = ThongtinBGs.map( ThongtinBG => {
                  return Game.find({ MaTTBG: ThongtinBG._id }).exec()
                    .then((games) => {
                      let countIndex = 0;
                      for (const game of games) {
                        if ((game.TinhTrangBG !== "Hỏng" && (game.TinhTrangMuon == "Đang giữ hàng" || game.TinhTrangMuon == "Đang thuê")) || (game.TinhTrangBG == "Hỏng" && game.TinhTrangMuon == "Chưa được thuê")) {
                          countIndex += 1; // Tăng số lượng nếu có tình trạng mượn là "Đang giữ hàng" hoặc "Đang thuê"
                        }
                      }
                      return countIndex;
                    });
                });
                Promise.all(promises)
                .then((counts) => {
                  const processedBoardGames = ThongtinBGs.map((ThongtinBG, index) => {
                    const tinhTrang = counts[index] < ThongtinBG.SoLuong ? 1 : 0;
                    return {
                      _id: ThongtinBG._id,
                      TenBoardGame: ThongtinBG.TenBoardGame,
                      GiaThue: ThongtinBG.GiaThue,
                      HinhAnh: ThongtinBG.HinhAnh,
                      tinhTrang: tinhTrang
                    };
                  });
                  res.render('boardgame/search.handlebars', {
                    LoaiBoardGames: LoaiBoardGames,
                    boardGames: processedBoardGames,
                    ThongtinBGs: ThongtinBGs
                  });
                })
            })
            .catch(next);
      })
          .catch(next);
     
    }
    //post /search/q
    search(req, res,next) {
      var keyName = req.body.q;
      ThongtinBG.find({TenBoardGame : {$regex :keyName , $options : 'i'}}).exec()
      .then(ThongtinBGs => {
        ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
        const promises = ThongtinBGs.map( ThongtinBG => {
          return Game.find({ MaTTBG: ThongtinBG._id }).exec()
            .then((games) => {
              let countIndex = 0;
              for (const game of games) {
                if ((game.TinhTrangBG !== "Hỏng" && (game.TinhTrangMuon == "Đang giữ hàng" || game.TinhTrangMuon == "Đang thuê")) || (game.TinhTrangBG == "Hỏng" && game.TinhTrangMuon == "Chưa được thuê")) {
                  countIndex += 1; // Tăng số lượng nếu có tình trạng mượn là "Đang giữ hàng" hoặc "Đang thuê"
                }
              }
              return countIndex;
            });
        });
        Promise.all(promises)
        .then((counts) => {
          const processedBoardGames = ThongtinBGs.map((ThongtinBG, index) => {
            const tinhTrang = counts[index] < ThongtinBG.SoLuong ? 1 : 0;
            return {
              _id: ThongtinBG._id,
              TenBoardGame: ThongtinBG.TenBoardGame,
              GiaThue: ThongtinBG.GiaThue,
              HinhAnh: ThongtinBG.HinhAnh,
              tinhTrang: tinhTrang
            };
          });
          res.render('boardgame/search.handlebars', {
            boardGames: processedBoardGames,
            ThongtinBGs: ThongtinBGs
          });
        })
    })
        .catch((error) => {
          // Xử lý lỗi nếu có
          res.status(500).json({ error: error.message });
        });

    }

// [POST] /phone
phone(req, res) {
  const customerNumber = req.body.phone;
  const bgId = req.body.bgId;

  Game.findOneAndUpdate(
    {
      MaTTBG: bgId,
      TinhTrangMuon: 'Chưa được thuê',
      TinhTrangBG: { $in: ["Tốt", "Trầy xước"] },
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
      //  res.redirect('/');
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
        }, 40000);
      }
    })
    .catch((error) => {
      // Xử lý lỗi nếu có
      res.status(500).json({ error: error.message });
    });
  }
}

module.exports = new SiteController();

