const ThongtinBG = require('../models/TTBG')
const LoaiBoardGame = require('../models/LOAIBG');


class SiteController {

  index(req, res, next) {

    // POST [] hien thi danh sach board game, loai boardgame
    ThongtinBG.find({ })

    .then( (ThongtinBGs) => {

      LoaiBoardGame.find({}).exec()

        .then((LoaiBoardGames) => {

          LoaiBoardGames = LoaiBoardGames.map(LoaiBG => LoaiBG.toObject());

          ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
          
          res.render('home.handlebars', {
            LoaiBoardGames: LoaiBoardGames,
            ThongtinBGs: ThongtinBGs,
          });
        })
        .catch(next);
    })
    .catch(next);
  }

}
  

module.exports = new SiteController();

