$(document).ready(function(){
    let loginForm = $("#login-form");
    let validateInput = new ValidateInput();

    $("#login-form").submit(function(){
        let values = $(this).serializeArray();
        let username = values[0].value;
        let password = values[1].value;

        try{
            validateInput.ValidateUsername(username);
            validateInput.ValidatePassword(password);

            $.ajax({
                method: 'POST',
                url: 'php/loginForm.php',
                data:{
                    'Username' : username,
                    'Password' : password
                },
                success: function(response) {
                    let myObj = JSON.parse(response);
                    console.log(myObj);

                    if (myObj.error != "")
                    {
                        alert(myObj.error);
                        return false;
                    }
                    else
                    {
                        alert("You are successful login");
                        window.location.replace("schedule.html"); 
                        return true;
                    }
                }, 
                error: function(jqXHR, textStatus, errorThrown) {
                    alert("Error = " + jqXHR.status + ", status = " + textStatus + ", " +
                        "error thrown: " + errorThrown);
                }
            });
            return false;
        }
        catch(error)
        {
            alert(error);
            return false;
        }
    });
});