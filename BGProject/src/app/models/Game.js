const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug);
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;
const Decimal = Schema.Types.Decimal128;


const Game = new Schema ({
    MaTTBG: {type: ObjectId},
    TinhTrangBG: {type: String},
    TinhTrangMuon : { type: String },
    DatHang : {type: String},
    NgayDatHang: { type: Date,default : Date.now()} 
}, { versionKey: false })

module.exports = mongoose.model('Game', Game, 'Game');