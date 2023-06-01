
const ThongtinBG = require('../models/TTBG')
const LoaiBoardGame = require('../models/LOAIBG')
class BgController {
    // get /home/tenbg
    show(req,res,next) {
      ThongtinBG.findOne({TenBoardGame: req.params.TenBoardGame})

        .then((boardgame)=> res.render('boardgame/show.handlebars', {boardgame}))
        .catch(next);
    }
}


module.exports = new BgController();