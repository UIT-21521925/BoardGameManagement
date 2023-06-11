
const LOAIBG = require('../models/LOAIBG');
const ThongtinBG = require('../models/TTBG')
class searchController {
    // get /home/tenbg
    searchbox(req,res,next) {
      LOAIBG.findOne({_id: req.params._id}).exec()
        .then((LoaiBoardGame)=> {
            // boardgames = boardgames.map(boardgame => boardgame.toObject());
            res.render('boardgame/search.handlebars', {LoaiBoardGame: LoaiBoardGame.toObject()})
        })
        .catch(next);
    }
}


module.exports = new searchController();