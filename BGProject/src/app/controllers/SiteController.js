const ThongtinBG = require('../models/TTBG')
const LoaiBoardGame = require('../models/LOAIBG');
const { render } = require('node-sass');



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

  // search(req, res, next ) {
  //   ThongtinBG.find({}).exec()
  //   .then (ThongtinBGs => {
  //     res.render('home.handlebars', {ThongtinBGs})
  //   })
  //   .catch(next);

  // }
  // store(req, res, next ) {
  //   res.send('OKE')
  // }
  
}
module.exports = new SiteController();

