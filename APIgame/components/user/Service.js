const userModel = require('./Model');

const login = async (username, password) => {
    try {
        let user = await userModel.findOne({ username });
        if (user) {
            if(password == user.password){
                return user;
            }
            return false;
        }
    } catch (error) {
        console.log('đn k đc ', error);
    }
    return false;
}

const register = async (username, password) => {
    try {
        
        let user = await userModel.findOne({ username });
        if (user){
            console.log('email đã được đăng kí');
            return false;
        };
        user = new userModel({ username, password, positionX: "", positionY: "", positionZ: "" });
        await user.save();
        return true;
    } catch (error) {
        console.log('đk k đc', error);
    }
    return false;
}

const savePosition = async (username, positionX, positionY, positionZ) => {
    try {
        let user = await userModel.findOne({ username });
        if (user) {
            user.positionX = positionX;
            user.positionY = positionY;
            user.positionZ = positionZ;
            await user.save();
            return user;
        }
        return false;
    } catch (error) {
        console.log('save lỗi ', error);
    }
    return false;
}

const saveScore = async (username, score) => {
    try {
        let user = await userModel.findOne({ username });
        if (user) {
            user.score = score;
            await user.save();
            return user;
        }
        return false;
    } catch (error) {
        console.log('save score lỗi ', error);
    }
    return false;
}

module.exports = {login, register, savePosition, saveScore};