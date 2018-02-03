<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

  
  	session_start();
	require_once 'configuration.php';

if(isset($_SESSION['working_thread'])){
  	$threadname = $_SESSION['working_thread'];
  	$oldqref = $_SESSION['qref'];
  	mysqli_query($connectify, "UPDATE about_threads SET `last_qref`= '$oldqref' WHERE `thread_name`='$threadname' ");

  	$_SESSION['qref']=1;
}
	



	$currentThread = $_POST['postedThread']; /// ChangeTrack to a different Thread
	$_SESSION['working_thread']=$currentThread;
	
	if($result=mysqli_query($connectify,"SELECT last_qref FROM about_threads WHERE `thread_name`='$currentThread' ")) {
		$row=mysqli_fetch_array($result);
		$_SESSION['qref']=$row['last_qref'];
	}
	else echo "No qref data is found :(";

//	echo "The thread ' $currentThread ' has been successfully loaded   :)";
	// header("location:index.php");


?>
