const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug);
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;
const Decimal = Schema.Types.Decimal128;


const TTBG = new Schema ({
    TenBoardGame: {type: String},
    SoNguoiChoi : {type: Number, min: 16, index : true},
    DoTuoi : {type : Number, min: 16},
    TriGia: { type: Number},
    GiaThue: { type: Number},
    SoLuong: {type : Number},
    HinhAnh : { type: String },
    LuatChoi : { type: String },
    ThoiGianChoi : { type: Number },
    MaLBG: {type: String},
    slug : {type : String, slug : 'TenBoardGame', unique : true}
});

module.exports = mongoose.model('ThongTinBG', TTBG, 'BoardGame');