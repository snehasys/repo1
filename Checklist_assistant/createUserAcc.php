<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

session_start();

$usr=$_POST['newUname'];
$usrMail=$_POST['newMail'];
$usrPwd=$_POST['newPwd'];
$retypedPwd=$_POST['retypePwd'];

//echo "I am at this page, thankyou \n".$usr."/".$usrMail."/".$usrPwd;
//////// password fields mismatch check ///////////////
if($usrPwd!=$retypedPwd){
	echo "Given passwords do not match with each other.\n\n ** Please enter again carefully **";
	return;
}

//THE TASKS:: retype the password, existing user or not? check string-length password; all in this php side


require_once "configuration.php";
/////////// User Name Availability Check ///////////
$temp=mysqli_query($connectify,"SELECT COUNT(*) AS exist FROM login_details WHERE `uname`='$usr' ");
$userImpossible=mysqli_fetch_array($temp);
if($userImpossible['exist'])
{ 
	echo $userImpossible['exist'].") The User-name: '".$usr."' is already taken\n\n\t** Please try another **";
	return;
}
/////////////// password length Check /////////////
$pwdLen=mb_strlen($usrPwd);
if ($pwdLen<6||$pwdLen>20){
	echo "Sorry, the chosen ".$pwdLen." length password is unacceptable.\n\n Select between 6-20 characters.. ";
	return;
}
////////////// since ALL GOOD(so far), so SEND THE USER-given-DATA TO MYSQL and TRY TO CREATE HIS ACCOUNT :P /////////
if(mysqli_query($connectify,"INSERT INTO login_details VALUES ('$usr', '$usrPwd' , '$usrMail' )"))
{
	echo "You just created your account successfully with the following credentials :\n\n User   : '".$usr
	."'\n Email : '".$usrMail
	."'\n\n     *** Memorize your password and/or keep it safe ***";
	$_SESSION['me']=$usr;// setting the SESSION ME variable
	echo "\nsession.ME==".$_SESSION['me']."\n\n **Kindly REFRESH this PAGE**";

	return;
}

echo "Exception catched, \n Fatal error";



?>
