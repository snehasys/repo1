<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

  
  	session_start();
	require_once 'configuration.php';
	

if(isset($_SESSION['working_thread'])){  //// if there is an existing thread you are working on, then first save that thread's ongoing Qreference number
  	$threadName = $_SESSION['working_thread'];
  	$oldqref = $_SESSION['qref'];
  	mysqli_query($connectify,"UPDATE about_threads SET `last_qref`= '$oldqref' WHERE `thread_name`='$threadName' ");
}


//	$newThread = 
$newThread = $_POST['threadRequest'];

	$pattern0=$threadName."→".$newThread; // making 't_$threadName_blah' kinda string for appending infront of asked newThreadnames/subThreadNames e.g. 'blah'


	if(
		mysqli_query($connectify, "INSERT INTO `about_threads` VALUES ('', '$pattern0', '1')")
	  )	{
		$_SESSION['working_thread']=$pattern0; // <------------ SET the currentThread as this new thread..
		$_SESSION['qref']=1;
		 echo "New Thread: ' $pattern0 ' created successfully. :D\n Hope you enjoy your new session...";
		return;
	}
	else echo "Sorry, I'm getting errors,\n--------------------------\n\n** Maybe \" $pattern0 \" already exists **\nOR\n**You are probably using whitespaces/special characters/empty Fields ** ";

