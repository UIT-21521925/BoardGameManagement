// const ThongtinBG = require('../models/TTBG')
// const Boardgame = require('../models/BG');
// const handlebars = require('handlebars');
// const LoaiBoardGame = require('../models/LOAIBG')

// class SiteController {

//   index(req, res, next) {

    
//     ThongtinBG.find({}).exec()

//     .then( (ThongtinBGs) => {

//       LoaiBoardGame.find({}).exec()

//         .then((LoaiBoardGames) => {

//           LoaiBoardGames = LoaiBoardGames.map(LoaiBG => LoaiBG.toObject());

//           ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());

//           const filteredThongtinBGs = ThongtinBGs.filter((item) => {
//             return item.LOAIBG._id === req.query.value;
//           });
//           res.render('home.handlebars', {LoaiBoardGames : LoaiBoardGames, ThongtinBGs :ThongtinBGs ,  filters: filteredThongtinBGs});

//           // res.render('home.handlebars', {ThongtinBGs});
//         })
//         .catch(next);
//     })
//     .catch(next);
//   }

  
// }

// module.exports = new SiteController();
const ThongtinBG = require('../models/TTBG');
const Boardgame = require('../models/BG');
const handlebars = require('handlebars');
const LoaiBoardGame = require('../models/LOAIBG');

class SiteController {
  index(req, res, next) {
    ThongtinBG.find({})
      .exec()
      .then((ThongtinBGs) => {
        LoaiBoardGame.find({})
          .exec()
          .then((LoaiBoardGames) => {
            LoaiBoardGames = LoaiBoardGames.map((LoaiBG) => LoaiBG.toObject());
            ThongtinBGs = ThongtinBGs.map((ThongtinBG) => ThongtinBG.toObject());

            const filteredThongtinBGs = ThongtinBGs.filter((item) => {
              return item.LOAIBG._id.toString() === req.query.value;
            });

            res.render('home.handlebars', {
              LoaiBoardGames: LoaiBoardGames,
              ThongtinBGs: ThongtinBGs,
              filters: filteredThongtinBGs,
              selectedCategory: req.query.value || '',
            });
          })
          .catch(next);
      })
      .catch(next);
  }
}

module.exports = new SiteController();
