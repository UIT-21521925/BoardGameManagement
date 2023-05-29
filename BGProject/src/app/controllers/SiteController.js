const TTBG = require('../models/TTBG');
const Boardgame = require('../models/BG');

class SiteController {
  index(req, res, next) {
    let results =[]
    Boardgame.find({}).exec()
      .then(boardgames => {
        const boardgameIds = boardgames.map(boardgame => boardgame._id);
        let datas = []
        TTBG.find({ _id: { $in: boardgameIds } }).exec()
          .then(async (ThongtinBGs) => {
            for (const boardgame of boardgames) {
              ThongtinBGs.find(async (detail) => {if (detail._id.toString() === boardgame._id.toString()) {
                await new Promise((resolve) => setTimeout(resolve, 500));
                datas.push({
                  detail,boardgame
                })
              }});
            };
            await new Promise((resolve) => setTimeout(resolve, 1000));
            res.render('home.handlebars', { datas : datas });
          })
          .catch(next);
      })
      .catch(next);
  }
}

module.exports = new SiteController();