const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug)
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;
const Decimal = Schema.Types.Decimal128;


const BIENBAN = new Schema ({
    MABB: ObjectId,
    LYDO: String, 
    SOTIENTHU: Decimal,
    MADH : Object,
    creatAt: {type: Date},
    
});

module.exports = mongoose.model('UUDAI',UUDAI);