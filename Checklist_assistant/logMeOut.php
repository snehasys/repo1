<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/


session_start();
$threadName=$_SESSION['working_thread'];
$qref=$_SESSION['qref'];

require_once 'configuration.php';
mysqli_query($connectify,"UPDATE about_threads SET `last_qref`='$qref' WHERE `thread_name` = '$threadName' ");
header("location:index.php");

session_destroy();

?>
