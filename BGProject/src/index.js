
const express = require('express');
const morgan = require('morgan');
const handlebars = require('express-handlebars');
const path = require('path');
const app = express();
const port = 3000;

app.use(express.static(path.join(__dirname,'public')))
// connect to DB
const db = require('./config/db');
db.connect();

// Routers
const route = require('./routes');
// middleware

app.use(express.urlencoded({ extended: true }));
app.use(express.json());

// http logger
app.use(morgan('combined'));
// 

// template engine
app.engine('handlebars', handlebars.engine({
  defaultLayout: 'main',
  helpers: {
    ifCond: handlebars.helpers.ifCond    // Thêm đoạn này để đăng ký helper ifCond
  }
})); 
// Định nghĩa view engine
app.set('view engine', 'handlebars');
app.set('views', path.join(__dirname, 'resources/views'));


// page
// app.get('/', (req, res) => {
//   res.render('home.handlebars');
// })


// app.get('/product', (req, res) => {
//   res.render('product.handlebars');
// })

// rout init
route(app);

app.listen(port, () => {
  console.log(`Example app listening on port at http://localhost:${port}`)
})