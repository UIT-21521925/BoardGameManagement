
const ThongtinBG = require('../models/TTBG');
const Game = require('../models/Game');
class BgController {
    // get /home/tenbg
    show(req,res,next) {
      ThongtinBG.findOne({TenBoardGame: req.params.TenBoardGame}).exec()
        .then((boardgames)=> {
            // boardgames = boardgames.map(boardgame => boardgame.toObject());
            res.render('boardgame/show.handlebars', {boardgames: boardgames.toObject()})
        })
        .catch(next);
    }

    //[POST] /home/phonebg
   

}


module.exports = new BgController();