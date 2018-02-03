<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/
session_start();

$id = $_POST['id'];
$comment = $_POST['comment'];
//echo "\n\n".$comment."/".$id;

require_once "configuration.php";
$safeComment=mysqli_real_escape_string($connectify,$comment);
//$id--;
//echo $id;
//$x = mysql_result(mysql_query($connectify,"SELECT * FROM $tablename WHERE `sequence` = $id "),0,"context");
if( ! mysqli_query($connectify,"UPDATE responses SET `comment`='$safeComment' WHERE `sequence` = $id "))
   echo "\n\n".$comment." /-> ".$id."\nSorry,\nEdit Unsuccessful :( ";

?>
