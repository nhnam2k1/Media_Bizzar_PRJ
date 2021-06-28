<?php
require_once("Config.php");

class ScheduleModel extends Config{
    private $pdo;
    private $employeeScheduleTable;
    private $employeeTable;
    private $scheduleTable;
    private $sessionTable;
    
    public function __construct(){
        $this->employeeScheduleTable = "employee_Schedule";
        $this->employeeTable = "employee";
        $this->scheduleTable = "schedule";
        $this->sessionTable = "session";
        $this->pdo = $this->GetConnection();
    }

    public function GetShiftsFromDatabase($StartDate, $EndDate, $employeeID){
        try{
            $sql = "SELECT Date, Session, FirstName, LastName, Fullname
                    FROM(
                        SELECT Schedule.id AS ScheduleID, Schedule.date AS Date, Session.time_of_day AS Session, 
                                Employee.first_name AS FirstName, Employee.last_name AS LastName
                        FROM ((($this->employeeScheduleTable AS EmployeeSchedule
                        INNER JOIN $this->scheduleTable AS Schedule ON EmployeeSchedule.schedule_id = Schedule.id)
                        INNER JOIN $this->employeeTable AS Employee ON EmployeeSchedule.employee_id = Employee.id)
                        INNER JOIN $this->sessionTable  AS Session  ON Schedule.session_id = Session.id)
                        WHERE Schedule.date BETWEEN :startDate AND :endDate
                        ORDER BY Schedule.date, Session.id
                    )   AS ShiftsInMonthAllEmployees
                    CROSS JOIN (
                        SELECT concat(first_name,' ', last_name) AS Fullname
                        FROM $this->employeeTable WHERE id = :employeeID
                    ) AS TemporaryEmployeeName
                    WHERE ScheduleID IN 
                    (
                        SELECT Schedule.id
                        FROM ($this->employeeScheduleTable AS EmployeeSchedule
                        INNER JOIN $this->scheduleTable AS Schedule ON EmployeeSchedule.schedule_id = Schedule.id)
                        WHERE (Schedule.date BETWEEN :startDate AND :endDate) AND (EmployeeSchedule.employee_id = :employeeID) 
                    );";

            $stmt = $this->pdo->prepare($sql);
            $stmt->bindParam(":startDate", $StartDate);
            $stmt->bindParam(":endDate"  , $EndDate);
            $stmt->bindParam(":employeeID",$employeeID);
            $stmt->execute();

            $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
            return $result;
        }
        catch(Exception $e){
            throw $e;
        }
    }
}
?>