<?php
Class InputValidation{
    public function __construct(){

    }
    public function CleanData($data){
        $data = str_replace('<script>', '', $data); 
        $data = str_replace('</script>', '', $data);
        $data = trim($data);
        $data = stripslashes($data);
        $data = htmlspecialchars($data);
        return $data;
    }
}
?>