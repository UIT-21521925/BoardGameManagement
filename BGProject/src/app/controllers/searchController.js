
// const LOAIBG = require('../models/LOAIBG');
// const ThongtinBG = require('../models/TTBG')
// class searchController {
//     // get /home/tenbg
//     searchbox(req,res,next) {
//         var value = req.params._id
//       LOAIBG.findOne({_id: value}).exec()
//         .then((LoaiBoardGame)=> {
//             LoaiBoardGame = LoaiBoardGame.map(LoaiBG => LoaiBG.toObject());
//             ThongtinBG.find({MaLBG : value })
//             .then(ThongtinBGs => {
             
//                 ThongtinBGs = ThongtinBGs.map(ThongtinBG => ThongtinBG.toObject());
//                 res.render('boardgame/search.handlebars', {LoaiBoardGame: LoaiBoardGame,ThongtinBGs : ThongtinBGs })
//             })
//             .catch(next);
//         })
//         .catch(next);
//     }
// }


// module.exports = new searchController();