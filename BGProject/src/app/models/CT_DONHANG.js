const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug)
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;
const Decimal = Schema.Types.Decimal128;

const CT_DONHANG = new Schema ({
    MACTDH : ObjectId,
    NGAYTHUE: Date,
    TRANGTHAI: String,
    NGAYTRA : Date,
    TONGTIEN: Decimal,
    MAKH : Object
});

module.exports = mongoose.model('CT_DONHANG', CT_DONHANG);