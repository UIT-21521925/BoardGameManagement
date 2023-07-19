
const express = require('express');
const morgan = require('morgan');
const handlebars = require('express-handlebars');
const path = require('path');
const app = express();
const helpers = require('./app/components/helpers')
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
// app.use(morgan('combined'));
// 

// template engine
const hbs = handlebars.create({ defaultLayout: 'main' });
hbs.handlebars.registerHelper('isEqual', helpers.isEqual);
app.engine('handlebars', hbs.engine);


// Định nghĩa view engine
app.set('view engine', 'handlebars');
app.set('views', path.join(__dirname, 'resources/views'));

// rout init
route(app);

app.listen(port, () => {
  console.log(`Example app listening on port at http://localhost:${port}`)
})