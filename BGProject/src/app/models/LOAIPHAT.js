const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug)
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;
const Decimal = Schema.Types.Decimal128;


const LOAIPHAT = new Schema({
    MALP : {type: ObjectId},
    TENLP: {type: String, default:"Khong co ten"},
    SOTIENPHAT: {type: Decimal, default:10000}
});

module.exports = mongoose.model('LOAIPHAT',LOAIPHAT);