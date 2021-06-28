<?php
require_once("Config.php");

class AvailabilityModel extends Config{
    private $pdo;
    private $availabilityTable;
    
    const DaysOfWeek = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

    public function __construct(){
        $this->availabilityTable = "employee_availability";
        $this->pdo = $this->GetConnection();
    }

    public function Get($id) {
        $strDaysInWeek = implode(", ", DaysOfWeek);
        $sql = "SELECT {$strDaysInWeek} FROM $this->availabilityTable WHERE ID = :id;";

        $stmt = $this->pdo->prepare($sql);
        $stmt->bindParam(":id", $id);
        $stmt->execute();
        $result = null;

        if ($stmt->rowCount() > 0)
        {
            $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        }
        
        return $result;
    }

    public function Update($id, $newAvailability){
        $convert = array();
        foreach($newAvailability as $key => $value)
        {
            $clause = "{$key} = {$value}";
            $convert[] = $clause;
        }
        $clause = implode(", ", $convert);
        $sql = "UPDATE $this->availabilityTable SET {$clause} WHERE ID = :id;";

        $stmt = $this->pdo->prepare($sql);
        $stmt->bindParam(":id", $id);
        $stmt->execute();

        return ($stmt->rowCount() > 0);
    }

    public function Create($id, $newAvailability){
        $columns = array();  $values  = array();

        $columns[] = "ID";   $values[] = $id;

        foreach($newAvailability as $key => $value) {
            $columns[] = $key;
            $values[] = $value;
        }

        $strCols = implode(", ", $columns);
        $strVals = implode(", ", $values);
        $sql = "INSERT INTO $this->availabilityTable ({$strCols}) VALUES({$strVals});";

        $stmt = $this->pdo->prepare($sql);
        $stmt->execute();

        return ($stmt->rowCount() > 0);
    }
}
?>