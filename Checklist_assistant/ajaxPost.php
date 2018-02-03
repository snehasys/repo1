<link rel="stylesheet" href="jquery-ui.css" />

<script src="jquery-1.9.1.min.js"> </script>  <!//src=source>

<script src="jquery-ui.js"> </script>

	<style>
.QspaceStyle {color:dark; border: 2px solid white; background-color: #BBBBFF; font-family: sans-serif; padding: .3em;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px;}
.answerStyle {border-top: 1px solid grey; background-color: #eee; padding: 0.8em; padding-left: 0.1em; font-size: 100%;-webkit-border-radius:3px;-moz-border-radius:3px;-ms-border-radius:3px;-o-border-radius:3px;border-radius:13px;}
.commentStyle {background-color: #ffff9f;border}
.timestampStyle {background-color: #ffffbf;}
    </style>

<?php 
session_start(); 
if(!isset($_SESSION['working_thread'])) echo "<script>window.location = 'qMan/index.php'</script>"; //if manually called, i.e not via POST method then kick that attempt

$currentThread=$_SESSION['working_thread'];
require_once 'configuration.php';
//print_r($_POST);
//echo $_POST['fetch_qref'];
//$qref= $_POST['fetch_qref'];
$qref_=$_SESSION['qref'];
$threadId=$_SESSION['working_thread_id'];

$reply= $_POST['thought'];

$messy=mysqli_real_escape_string($connectify,$reply);
//if($messy=="") $messy="< - - Skipped - - >";

if( $messy!=$_SESSION['prevEntry'] && $messy != "" ) //// Get into this block if skipped via #skipper button or NotEqual to previous entry/// AND restrict NULLs
{
//	echo ''.$qref_.': </u><br/>';
	$qref_=$qref_+1;
	$ques=mysqli_query($connectify,"SELECT qcontext FROM question WHERE `ref`=$qref_"); 

	$qref_adjusted=$qref_-1; //<------------------ fixit \/ fixed ;D
	if(mysqli_query($connectify,"INSERT INTO responses VALUES ('','$threadId', '$messy','  **Leave your comments here**', now(), '$qref_adjusted')"))
	{
//		$currentTime=date(' Y-m-d H:i:s');
		$currentTime=myDateTime();
		$_SESSION['qref']="$qref_";
		$ques_1 = mysqli_fetch_array($ques);
//		while($ques_1 = mysqli_fetch_array($ques)){ //apparently useless, since just one time execution of the following "echo" command is required.. bleh bleh! :P
		echo '<div class="answerStyle" title="You are not supposed to edit thoughts while on active thread, BUT, if you are so desperate, you may refresh this page"><i>'
		.'  &nbsp;&nbsp;<b>'.$reply
		.'</b></div><div class="commentStyle" title="You are not supposed to put comments while on active thread, and if you are so determined, you may refresh this page" > ** </div>'
		.'<div align="right" class="timestampStyle"><small></i>'.$currentTime.'</small><i></div>'
		.'<div id="Qspace" class="QspaceStyle" title="Answer My Question, if it is worth of your time :)"><b><small> &nbsp&nbsp&nbsp►'
		.$ques_1['qcontext'].'</small><b></div>';

	mysqli_query($connectify,"UPDATE about_threads SET `last_qref`='$qref_' WHERE `thread_id` = '$threadId' "); // saving private Ryan :P

//		}
		$_SESSION['prevEntry']=$messy; ///Saving for next reference, so that no two consecutive entries be the same, thus preventing multiple key hits.. :)

	}
}



////////////////////////////////////// the Date_module ////////////////////////////////
function myDateTime(){  //time.php :)
	//echo "The Time ";
$d=25;
//while($d>0){
$today= date('Y-m-d');
$hour= date ('H')+5; //print $hour.":::";
$min= date('i')+30;
$sec=date('s');
if ($min>59){
	$min-=60; //min=min-60
	$hour+=1;
}
if ($hour>24) {
	$hour-=24;
}
if ($hour>12)
	{ $hour-=12;
		if($hour==12) return $today."&nbsp".$hour.":".$min.":".$sec." am"."\n"; //case when hour==24
		else
		return $today."&nbsp".$hour.":".$min.":".$sec." pm"."\n";
	}
else {
	if($hour==12) return $today."&nbsp".$hour.":".$min.":".$sec." pm"."\n";//case when hour==12
	else
	return $today."&nbsp".$hour.":".$min.":".$sec." am"."\n";
	}
}
///////////////////////////
?>
<body onLoad="document.f1.thought.focus();">



