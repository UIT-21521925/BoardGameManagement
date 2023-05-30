const ThongtinBG = require('../models/TTBG')
const Boardgame = require('../models/BG');

class SiteController {
  index(req, res, next) {
    ThongtinBG.find({}).exec()
    .then( (ThongtinBGs) => {
      res.render('home.handlebars', {ThongtinBGs});
    })
    .catch(next);
  }
}

module.exports = new SiteController();