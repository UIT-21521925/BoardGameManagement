const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug);
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;
const Decimal = Schema.Types.Decimal128;

const TTBG = new Schema ({
    MATTBG: {type :ObjectId},
    TENBG: {type: String},
    SONGUOICHOI : {type: Number, min: 16, index : true},
    DOTUOI : {type : Number, min: 16},
    TRIGIA: { type: Decimal},
    GIATHUE: { type: Decimal},
    SOLUONG : {type : Number},
    HINHANH : { type: String },
    MALBG: {type: Object}
});

module.exports = mongoose.model('ThongTinBG', TTBG);