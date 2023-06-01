const ThongtinBG = require('../models/TTBG')
const Boardgame = require('../models/BG');
const handlebars = require('handlebars');
const LoaiBoardGame = require('../models/LOAIBG')

class SiteController {

  index(req, res, next) {
    ThongtinBG.find({}).exec()
    .then( (ThongtinBGs) => {
      LoaiBoardGame.find({}).exec()
        .then((LoaiBoardGames) => {
          LoaiBoardGames = LoaiBoardGames.map(LoaiBG => LoaiBG.toObject());
          ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
          res.render('home.handlebars', {LoaiBoardGames:LoaiBoardGames,ThongtinBGs: ThongtinBGs});
          // res.render('home.handlebars', {ThongtinBGs});
        })
        .catch(next);
    })
    .catch(next);
  }
}

module.exports = new SiteController();