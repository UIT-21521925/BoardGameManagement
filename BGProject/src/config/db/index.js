const mongoose = require('mongoose');

async function connect () {
    try {
        await mongoose.connect('mongodb+srv://cnpm:Thuydiem29@cluster0.2jmsamm.mongodb.net/BoardGame', {
            useNewUrlParser: true,
            useUnifiedTopology: true
        });
        console.log('success');
    }  catch (error) {
        console.log('failure!');
    }
}

module.exports = { connect };