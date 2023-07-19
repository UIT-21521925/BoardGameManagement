const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug)
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;

const LOAIBG = new Schema ({
    _id : ObjectId,
    TenLBG : String
});

module.exports = mongoose.model('LOAIBG',LOAIBG,'LoaiBG');