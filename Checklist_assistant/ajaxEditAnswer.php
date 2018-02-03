<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

session_start();

$id = $_POST['id'];
$editedAnswer = $_POST['edited_answer'];
//echo "\n\n".$comment."/".$id;

require_once "configuration.php";
$safelyEditedAnswer=mysqli_real_escape_string($connectify,$editedAnswer);
//echo $id; 
//$x = mysql_result(mysql_query($connectify,"SELECT * FROM $tablename WHERE `sequence` = $id "),0,"context");
if( ! mysqli_query($connectify,"UPDATE responses SET `context`='$safelyEditedAnswer' WHERE `sequence` = $id "))
   echo "\n\n".$editedAnswer." /-> ".$id."\nSorry,\nEdit Unsuccessful :( ";

//.. else echo "This edit was a total success";
?>
