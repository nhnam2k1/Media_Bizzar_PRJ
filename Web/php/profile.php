<?php
require_once('Model/ProfileModel.php');
require_once('InputValidation.php');
session_start();

if (isset($_SESSION['loggedin']) && $_SESSION['loggedin'] == true){

}
else{
    header('location: index.html');
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    try {
        $inputValidation = new InputValidation();
        $profileModel = new ProfileModel();
        $id = $_SESSION["ID"];

        $result = $profileModel->GetEmployeeProfile($id);

        if ($result == null){
            http_response_code(403);
            echo "Something wrong with the database";
            return;
        }
        $result['error'] = "";
        http_response_code(200);
        echo json_encode($result);
    }
    catch(Exception $e) {
        http_response_code(403);
        $error['error'] = $e->GetMessage();
        echo json_encode($error);
    }
}
?>