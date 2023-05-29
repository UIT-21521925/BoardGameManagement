const mongoose = require('mongoose');
const slug = require('mongoose-slug-generator');
mongoose.Promise = require('bluebird');

mongoose.plugin(slug)
const Schema = mongoose.Schema;

const ObjectId = Schema.Types.ObjectId;

const DONHANG = new Schema ({
    MADH : ObjectId,
    MACTDH : Object,
    MABG : Object
});

module.exports = mongoose.model('DONHANG', DONHANG);