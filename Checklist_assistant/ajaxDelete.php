<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/
session_start();
require 'configuration.php';

$id = $_POST['id'];
//$id--;
//echo $id;
//$x = mysql_result(mysql_query($connectify,"SELECT * FROM $tablename WHERE `sequence` = $id "),0,"context");
if(mysqli_query($connectify,"DELETE FROM responses WHERE `sequence` = $id ")) echo "   __Delete successful";

else echo "This delete was UNSUCCESSFUL :( ";

?> 


