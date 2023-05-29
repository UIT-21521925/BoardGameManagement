
const siteRouter = require('./siteRouter')
const bgRouter = require('./bgRouter');

function route(app) {
    app.use('/product', bgRouter),
    app.use('/',siteRouter)

}
module.exports = route;