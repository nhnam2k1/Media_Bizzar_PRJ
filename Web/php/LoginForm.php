<?php
require_once('Model/LoginModel.php');
require_once('InputValidation.php');
session_start();

if (isset($_SESSION['loggedin']) && $_SESSION['loggedin'] == true){

}
else{
    header('location: index.html');
}

if ($_SERVER["REQUEST_METHOD"] == "POST") 
{
    try
    {
        $inputValidation = new InputValidation();
        $loginModel = new LoginModel();
        $username = $_POST['Username'];
        $password = $_POST['Password'];

        $username = $inputValidation->CleanData($username);
        $password = $inputValidation->CleanData($password);
        $result = $loginModel->GetLoginFromDatabase($username, $password);

        if ($result == null){
            http_response_code(403);
            echo "Wrong username or password";
            return;
        }
        
        $_SESSION['ID'] = $result['id'];
        $_SESSION['loggedin']  = true;
        $result['error'] = "";

        http_response_code(200);
        echo json_encode($result);
    }
    catch(Exception $e)
    {
        http_response_code(403);
        $error['error'] = $e->GetMessage();
        echo json_encode($error);
    }
}
?>