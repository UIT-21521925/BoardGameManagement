const ThongtinBG = require('../models/TTBG')
const LoaiBoardGame = require('../models/LOAIBG');



class SiteController {

  index(req, res, next) {
 // Lấy danh sách board game và loại board game    
 
    ThongtinBG.find().exec()
    .then( (ThongtinBGs) => {
      LoaiBoardGame.find({}).exec()
        .then((LoaiBoardGames) => {
          console.log(req.query.q)
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

//   searchHandler(req, res, next) {
//     ThongtinBG.find({MaLBG : req.params._id}).exec()
//         .then(ThongtinBGs => {
//             var filteredThongtinBGs = [];
//             ThongtinBGs.forEach(ThongtinBG => {
//                 if (ThongtinBG.MaLBG === value) {
//                     filteredThongtinBGs.push(ThongtinBG.toObject());
//                 }
//             });
//             res.render('home.handlebars', { ThongtinBGs: filteredThongtinBGs });
//         })
//         .catch(next);
// }
  
}
module.exports = new SiteController();

