<?php

require_once("Config.php");

class ProfileModel extends Config{
    private string $employeeTable; 
    private string $departmentTable;
    private string $contractTable;
    private $pdo;

    public function __construct(){
        $this->employeeTable = "employee";
        $this->departmentTable = "department";
        $this->contractTable = "contract";
        $this->pdo = $this->GetConnection();
    }

    public function GetEmployeeProfile($id){
        try {
            $sql = "SELECT concat(emp.first_name,' ', emp.last_name) AS fullname, emp.email, emp.address, emp.city, emp.country, 
                           emp.phone_number, emp.wage, emp.gender, department.title AS department_name, emp.birthdate, contract.type  
                    FROM (($this->employeeTable AS emp 
                    INNER JOIN $this->departmentTable AS department ON emp.department_id = department.id) 
                    INNER JOIN $this->contractTable AS contract ON emp.contract_id = contract.id) 
                    WHERE emp.id = :id;";

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
        catch(Exception $e){
            throw $e;
        } 
    }
}
?>