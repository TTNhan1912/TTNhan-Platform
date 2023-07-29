
const userService = require('./Service');

const login = async (username, password) => {
    try {
        return await userService.login(username, password);
    } catch (error) {
        console.log(error);
    }
    return null;
}

const register = async (username, password) => {
    try {
        return await userService.register(username, password);
    } catch (error) {
        console.log('User controller failed to register', error);
    }
    return false;
}

const savePosition = async (username, positionX, positionY, positionZ) => {
    try {
        return await userService.savePosition(username, positionX, positionY, positionZ);
    } catch (error) {
        console.log('User controller failed to register', error);
    }
    return false;
}

const saveScore = async (username, score) => {
    try {
        return await userService.saveScore(username, score);
    } catch (error) {
        console.log('User controller failed to register', error);
    }
    return false;
}



module.exports = { login, register, savePosition, saveScore };