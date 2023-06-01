const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug);
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;
const Decimal = Schema.Types.Decimal128;

const TTBG = new Schema ({
    MATTBG: {type :ObjectId},
    TenBoardGame: {type: String},
    SoNguoiChoi : {type: Number, min: 16, index : true},
    DoTuoi : {type : Number, min: 16},
    TriGia: { type: Decimal},
    GiaThue: { type: Decimal},
    SoLuong: {type : Number},
    HinhAnh : { type: String },
    MALBG: {type: Object}
});

module.exports = mongoose.model('ThongTinBG', TTBG);