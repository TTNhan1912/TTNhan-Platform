
// bắt lỗi đăng ký
const checkRegister = (req, res, next) => {
    const { username, password } = req.body;
    if ( !username || !password ) {
        return res.status(400).json({ error: true, 
            message: 'Vui lòng nhập đầy đủ thông tin' });
    } else {
        let regexEmail = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
        if (!regexEmail.test(username)) {
            return res.status(400).json({ error: true, 
                message: 'Email không hợp lệ' });
        }
        
        return next();
    }
}

module.exports = { checkRegister };