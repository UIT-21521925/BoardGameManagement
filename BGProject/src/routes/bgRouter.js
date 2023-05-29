const express = require('express');
const router = express.Router();
const siteController = require('../app/controllers/SiteController');
const bgController = require('../app/controllers/BgController');

router.use('/product', bgController.index);
router.use('/', bgController.index);

module.exports = router;