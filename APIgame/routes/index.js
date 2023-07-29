var express = require('express');
var router = express.Router();

const userController = require('../components/user/Controller');
const {checkRegister} = require('../components/Validation');

// http://localhost:3000/login
// đăng nhập
router.post('/login', async (req, res, next) => {
  try {
    const { username, password } = req.body;
    const user = await userController.login(username, password);
    if (user) {
      return res.status(200).json({ status: 1, notification: "đăng nhập thành công", username: user.username, score: user.score, positionX: user.positionX, positionY: user.positionY, positionZ: user.positionZ });
    }
    return res.status(400).json({ status: 0, notification: "đăng nhập không thành công",  });
  } catch (error) {
    console.log(error);
    return res.status(500).json({ status: 0, notification: "đăng nhập không thành công",  });
  }
});

// http://localhost:3000/register
// đăng kí
router.post('/register',[checkRegister], async (req, res, next) => {
  try {
    const { username, password } = req.body;
    const user = await userController.register(username, password);
    if (user) {
      return res.status(200).json({ status: 1, notification: 'đăng kí thành công' });
    }
    return res.status(400).json({ status: 0, notification: 'đăng kí thất bại' });
  } catch (error) {
    console.log(error);
    return res.status(500).json({ status: 0, notification: 'đăng kí thất bại' });
  }
});

// http://localhost:3000/savePosition
// đăng kí
router.post('/savePosition', async (req, res, next) => {
  try {
    const { username, positionX, positionY, positionZ } = req.body;
    const user = await userController.savePosition(username, positionX, positionY, positionZ);
    if (user) {
      return res.status(200).json({ status: 1, notification: 'lưu thành công' });
    }
    return res.status(400).json({ status: 0, notification: 'lưu thất bại' });
  } catch (error) {
    console.log(error);
    return res.status(500).json({ status: 0, notification: 'lưu thất bại' });
  }
});

// http://localhost:3000/saveScore
// đăng kí
router.post('/saveScore', async (req, res, next) => {
  try {
    const { username, score } = req.body;
    const user = await userController.saveScore(username, score);
    if (user) {
      return res.status(200).json({ status: 1, notification: 'lưu thành công' });
    }
    return res.status(400).json({ status: 0, notification: 'lưu thất bại' });
  } catch (error) {
    console.log(error);
    return res.status(500).json({ status: 0, notification: 'lưu thất bại' });
  }
});



module.exports = router;
