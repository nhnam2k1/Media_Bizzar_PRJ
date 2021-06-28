<?php
require_once('Model/availabilityModel.php');
session_start();

const DaysOfWeek = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
const Sessions = ["Morning", "Afternoon", "Evening"];

try {
    $id = $_SESSION["ID"];
    $availabilityModel = new AvailabilityModel();

    if ($_SERVER["REQUEST_METHOD"] == "POST")
    {
        $availabilityTable = [];

        foreach (DaysOfWeek as $key => $value)
        {
            $val = $_POST[$value];
            $availabilityTable[$value] = $val;
        }

        $temp = $availabilityModel->Get($id);

        if ($temp == null)
        {
            $availabilityModel->Create($id, $availabilityTable);
        }
        else
        {
            $availabilityModel->Update($id, $availabilityTable);
        }

        $result['error'] = "";
        http_response_code(200);
        echo json_encode($result);
    }
    else if ($_SERVER["REQUEST_METHOD"] == "GET")
    {
        $data = $availabilityModel->Get($id);

        if ($data == null)
        {
            $data = [];
            foreach (DaysOfWeek as $key => $value)
            {
                $data[$value] = 0;
            }
        }

        $result['data'] = $data;
        $result['error'] = "";
        http_response_code(200);
        echo json_encode($result);
    }
}
catch(Exception $e){
    http_response_code(403);
    $error['error'] = $e->GetMessage();
    echo json_encode($error);
}
?>