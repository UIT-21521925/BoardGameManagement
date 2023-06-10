
const siteRouter = require('./siteRouter')
const bgRouter = require('./bgRouter');

function route(app) {
    // app.get('/search',siteRouter),
    app.use('/home', bgRouter),
    app.use('/',siteRouter)

}
module.exports = route;