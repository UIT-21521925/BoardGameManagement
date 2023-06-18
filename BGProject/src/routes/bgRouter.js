const express = require('express');
const router = express.Router();

const bgController = require('../app/controllers/BgController');

router.use('/:TenBoardGame', bgController.show);


module.exports = router;