class ValidateInput{
    constructor(){
        this.usernameRegex = /^[a-zA-Z0-9]+$/;
        this.passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$/;
        this.nameRegex = /^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$/;
    }

    ValidatePassword(password){
        if (password.length < 8){
            throw "The password should have at least 8 characters";
        }

        if (!password.match(this.passwordRegex)){
            throw "The password should contain at least one numeric digit, " +
                  "one uppercase and one lowercase letter, length between 6 to 20 characters";
        }
    }

    ValidateUsername(username){
        if (username.length < 8){
            throw "The username should have at least 8 characters";
        }

        if(!username.match(this.usernameRegex)){
            throw "The username should contain alphabets and digits";
        }
    }

    ValidateName(name){
        if (!name.match(this.nameRegex)){
            throw "The name should only contatin alpahabets";
        }
    }

    ValidateConfirmPasswrod(password, confirmPassword){
        if (password != confirmPassword){
            throw "The confirm password should be the same as password";
        }
    }
}