const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug)
const Schema = mongoose.Schema;
const ObjectId = Schema.Types.ObjectId


const BG = new Schema ({
    TinhTrangBG : String,
    TinhTrangMuon : String,
    TTBG: Object
})

module.exports = mongoose.model('BG', BG);