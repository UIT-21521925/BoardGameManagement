
const siteRouter = require('./siteRouter')
const bgRouter = require('./bgRouter');
// const searchRouter = require('./searchRouter');
function route(app) {
    app.use('/search',siteRouter),
    app.use('/home', bgRouter),
    app.use('/',siteRouter)

}
module.exports = route;