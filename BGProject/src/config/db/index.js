const mongoose = require('mongoose');

async function connect () {
    try {
        await mongoose.connect('mongodb://127.0.0.1:27017/BoardGame', {
            useNewUrlParser: true,
            useUnifiedTopology: true
        });
        console.log('success');
    }  catch (error) {
        console.log('failure!');
    }
}
// mongodb://127.0.0.1:27017/BG

module.exports = { connect };