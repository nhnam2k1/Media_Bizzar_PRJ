<?php

require_once("Config.php");

class LoginModel extends Config{
    private string $employeeTable; 
    private $pdo;

    public function __construct(){
        $this->employeeTable = "employee";
        $this->pdo = $this->GetConnection();
    }

    public function GetLoginFromDatabase(string $username, string $password){
        $sql = "SELECT id FROM $this->employeeTable 
                WHERE username = :username AND password = :password;";
                
        $stmt = $this->pdo->prepare($sql);
        $stmt->bindParam(':username', $username);
        $stmt->bindParam(':password', $password);
        $stmt->execute();

        $result = null;
        if ($stmt->rowCount() > 0)
        {
            $result = $stmt->fetch(PDO::FETCH_ASSOC);
        }
        return $result;
    }
}
?>