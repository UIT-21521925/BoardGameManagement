const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug)
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;


const KHACHHANG = new Schema ({
    MAKH : ObjectId,
    TENKH: String,
    NGSINH : {type :Date, min: 16},
    DIACHI: String,
    EMAIL: String,
    SDT : Int16Array,
    TICHDIEM : {type: Int16Array, min: 0}
});

module.exports = mongoose.model('KHACHHANG',KHACHHANG);