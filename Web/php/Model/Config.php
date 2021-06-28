<?php
class Config 
{
    public function GetConnection()
    {
        try 
        {
            $pdo = new PDO("mysql:host=studmysql01.fhict.local;dbname=dbi429506", 
                            "dbi429506", 
                            "bangbang56");
    
            $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            return $pdo;
        }
        catch(PDOException $e)
        {
            die("ERROR: Could not connect. " . $e->getMessage());
        }
    }
}
?>