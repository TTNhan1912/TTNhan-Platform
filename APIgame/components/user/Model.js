const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const ObjectId = Schema.ObjectId;

const userSchema = new Schema({
    id: { type: ObjectId }, // khóa chính
    username: { type: String, unique: true, require: true }, // unique là k đc trùng
    password: { type: String, require: true },
    notification: {type: String},
    // otp: {type: String},
    score: {type: Number},
    positionX: { type: String},
    positionY: { type: String},
    positionZ: { type: String}
});

module.exports = mongoose.models.user || mongoose.model('user', userSchema);