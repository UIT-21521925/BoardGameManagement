const express = require('express')
const router = express.Router();

const siteController = require('../app/controllers/SiteController');


router.post('/phone', siteController.phone);
router.get('/search/:_id', siteController.searchbox);
router.get('/', siteController.index);


module.exports = router;