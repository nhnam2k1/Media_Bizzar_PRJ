<?php
require_once('Model/ScheduleModel.php');
session_start();

try {
    $scheduleModel = new ScheduleModel();

    if ($_SERVER["REQUEST_METHOD"] == "POST") {
        $employeeID = $_SESSION['ID'];

        $monthView = $_POST['Month'];
        $yearView  = $_POST['Year'];

        $dt = $yearView.'-'.$monthView.'-12';
        $firstDayInMonth = date("Y-m-01", strtotime($dt));
        $lastDayInMonth  = date("Y-m-t", strtotime($dt));

        $rawData = $scheduleModel->GetShiftsFromDatabase($firstDayInMonth, $lastDayInMonth, $employeeID);
        $compressData = [];

        foreach ($rawData as $raw) {
            $date = $raw['Date'];
            $session = $raw['Session'];
            $name = $raw['FirstName'].' '.$raw['LastName'];

            if (!isset($compressData[$date][$session])) {
                $compressData[$date][$session] = [];
            }
            array_push($compressData[$date][$session], $name);
        }
        $result['error'] = "";
        $result['shifts'] = $compressData;
        echo json_encode($result);
    }
}
catch(Exception $e){
    $error['error'] = $e->GetMessage();
    echo json_encode($error);
}
?>