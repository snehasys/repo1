<?php  
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

/**
  * THis page just authenticates a valid existing user
  */

session_start();

require_once 'configuration.php';

$uname=$_POST['uname'];
$pwd=$_POST['pwd'];   // TODO: Revisit here to implement hashing of pwd. // Also update the DB accordingly. (salting required??)
                     //       Sending real passwords via POST operation is a txetbook security threat

//$mail=$_POST['userMail'];

//$num=mysql_result(mysql_query("SELECT COUNT(*) AS count FROM login_details WHERE `uname`='$uname' AND `pwd`='$pwd' "),0,"count");
$query=mysqli_query($connectify,"SELECT COUNT(*) AS countExists FROM login_details WHERE `uname`='$uname' AND `pwd`='$pwd' "); // TODO: (do salting here with the hashed pwd). AlsoRemember to hash+salt while registering newUser
$userValid=mysqli_fetch_array($query);
if(! $userValid['countExists']){
	echo "** Are you kidding me?\n\n← These are Incorrect Login Credentials !";
	return;
}
else{
	$_SESSION['me']=$uname;// setting the SESSION ME variable
	echo $userValid['countExists'].". Welcome ".$uname."  :) ";
	//header("location:index.php");
	//return;
}
//echo " Total Valid user of ".$uname."/".$pwd." is: ".$userValid['countExists'];


?>
