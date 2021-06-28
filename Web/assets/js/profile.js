function EditProfile()
{
    alert('The profile edit form is still in construction, please wait moment');
}

function UpdateProfileView(fullname){
    $(".employee-name").text(fullname);
}

function UpdateProfileView(){
    try{
        $.ajax({
            method: 'POST',
            url: 'php/profile.php',
            success: function(response) {
                let myObj = JSON.parse(response);
                console.log(myObj);

                if (myObj.error != "")
                {
                    alert(myObj.error);
                    return false;
                }
                $(".employee-name").text(myObj[0].fullname);
                $(".department").text(myObj[0].department_name);
                $(".birthdate").text(myObj[0].birthdate);
                $(".contract").text(myObj[0].type);
            }, 
            error: function(jqXHR, textStatus, errorThrown) {
                alert("Error = " + jqXHR.status + ", status = " + textStatus + ", " +
                    "error thrown: " + errorThrown);
            }
        });
        return false;
    }
    catch(error) {
        alert(error);
        return false;
    }
    
}