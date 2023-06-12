const express = require('express')
const router = express.Router();

const siteController = require('../app/controllers/SiteController');



// router.post('/store', siteController.store);
router.get('/:_id', siteController.searchbox);
router.get('/', siteController.index);


module.exports = router;