
const ThongtinBG = require('../models/TTBG')
const LoaiBoardGame = require('../models/LOAIBG')
class BgController {
    index(req, res, next) {
      // LoaiBoardGame.find({}).exec()
      //   .then((LOAIBGs) => {
      //     res.render('product.handlebars', {LOAIBGs})
      //   })
      //   .catch(next);
      
               
      ThongtinBG.find({}).exec()
      .then((ThongtinBGs) => {
        res.render('product.handlebars', {ThongtinBGs})
      })
      .catch(next);
    } 

    // get / slug
    // show(req,res) {
    //   res.send('DETAIL');
    // }
}


module.exports = new BgController();