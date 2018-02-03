
<!doctype>
<html>
<meta charset="UTF-8" />
<center>
<head>
	<title>All Threads</title>
<div align='right'> <small><i><b>
<a href='logMeOut.php' title='Log Me Out' class='logoutStyle' style='border: 3px solid white;background-color: #088fff;color: white'>←Logout▬</a>&nbsp;</b></i></div>
<a href='*/..' title='Go top Checklist Page'><img src='images/Checklist_assistant_home.jpg'/></a><br/><br/><br/>
<div id='bunchOfThreads' >
		

<button id='fork'>Create new rooted Thread</button>
<b><br/><br/> Or <br/>Browse any </b>
<link rel="stylesheet" href="jquery-ui.css" />
 
  <?php   ////______________________________________________//////////////////////////
         //// THIS is the THREAD selection/creation PAGE.. //////////////////////////
        ////______________________________________________//////////////////////////
session_start();
print_r($_SESSION);
if(!isset($_SESSION['me'])) echo "<script>window.location = 'frontpage.php'</script>"; //if Invalid user, kick him/her to login page
$currentUserName=$_SESSION['me'];
$pattern0=$currentUserName."_";
$pattern=$pattern0."%"; // cooking up the wildcase search pattern for populating thread files
//echo $pattern0;

require 'qMan/allAdmins.php';
if ( in_array($_SESSION['me'] , $admins, true )) $pattern="%" ; ///////////// GIVES ADMINs THE PRIVILEGE to SNOOP into ALL USER's CHECKLISTS

include 'configuration.php';

 $result=mysqli_query($connectify,"SELECT `thread_name` FROM about_threads WHERE `thread_name` LIKE '$pattern' ORDER BY `thread_name`");

////THE TABLE >>>>>>>>
 echo "<table border='1' id='table1' rel='".$_SESSION['me']."' class='curvedEdges'> <tr><th> $currentUserName 's Threads </th></tr><tr><td>";
 //$columnName="Tables_in_".$DBname." (".$pattern.")"; echo "\n".$columnName; //all this trouble for naming as "Tables_in_test (res%)" :P
$prevrow="EMPTY"; 
while($row = mysqli_fetch_row($result)){
	$isSubset=strpos($row[0], $prevrow );
	echo "<ul>";
	if($isSubset=== false) echo "<hr/></ul><li>";
	else echo "<li>";
	echo "<a href='#' class='threads' rel='".$row[0]."'  > ".$row[0]."</a>";
//
	if($isSubset=== false) {
		echo " <input type='button' id='".$row[0]."' class='subThreadButtons' value='Create Sub Thread'/>";
		$prevrow=$row[0];
	}
	else {echo "</li></ul>";}
}

echo "</td></tr></table>";

echo "</div>";


/*
 mysqli_query($connectify,"DROP TABLE ".$tablename.'_bak');
 mysqli_query($connectify,"RENAME TABLE $tablename TO ".$tablename.'_bak');
 mysqli_query($connectify, "CREATE TABLE $tablename ( sequence INT(5) NOT NULL AUTO_INCREMENT
													 , context VARCHAR(300) , comment VARCHAR(300)
													  , t_stamp DATETIME , qref INT(4)
													 , primary key(sequence), foreign key(qref) references question(ref) )");

 echo $tablename.' table created/recreated successfully, hope you do know what you are doing...';
 */
 //$DB->Query(" CREATE TABLE response1 ( sequence INT(5) NOT NULL AUTO_INCREMENT , context VARCHAR(50), primary key(sequence) );");
?>
<!-- CSS goes in the document HEAD  -->
<style type="text/css">

table.curvedEdges { border:10px solid DodgerBlue;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px; }
table.curvedEdges th { border-bottom:1px dotted black;  border-color: #666666; background-color: #bbbbff; padding:10px; }
table.curvedEdges td { color: green;border-bottom:1px dotted white; background-color: #ffffdf;  padding:2px; }

.logoutStyle {color:dark; border: 4px solid white;  background-color: #BBBBFF; font-family: sans-serif; padding: .3em;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px;}
</style>

<script src="jquery-1.9.1.min.js"> </script>  <!//src=source>
<script src="jquery-ui.js"> </script> <!the ui  >

<script>
$(document).ready(function() { 
	$('#bunchOfThreads').draggable();

	$('.subThreadButtons').click(function(){
		var newThread2=$(this).attr('id');

		$.post('threadSelect.php',{'postedThread' : newThread2 }, function(data){
   		if(data) alert(data);
 //  		window.location.replace('*/..');
   	    });
		$( "#newThread" ).dialog( "open" );
		getElementById('preThreadName').value= "newThread2";
	});
	

	$('.threads').click(function(){
//   threadSelect(){
   	var thread=$(this).attr('rel');
   	$.post('threadSelect.php',{'postedThread' : thread }, function(data){
   		document.write("<h1>Loaded Successfully</h1>");
   		if(data) alert(data);
   		window.location.replace('*/..').delay(6000);
    });
  });

	$("#newThread").submit(function(){
		$.post('newThreadCreate.php',$('#newThread').serialize() ,function(data){
			alert(data);
			window.location.replace("*/..");
		});
		return false;

	});

$(function(){

//"--------------------------------------------------"///// THE INFO PAGE
 $("#newThread").dialog({autoOpen : false,
	 draggable: true,  height : 190, width : 410, modal : true, position : ['top',250] ,
	 show : "size", hide : "size"}); // puff, bounce, drop, highlight, blind, size, fold, explode,pulsate,shake,


//==================================================
	$( "#fork" ).button().click(function() {
		var newThread2=$('#table1').attr('rel');

		$.post('threadSelect.php',{'postedThread' : newThread2 }, function(data){
   		if(data) alert(data);
 //  		window.location.replace('*/..');
   	    });
		$( "#newThread" ).dialog( "open" );
  	 });

  });


});

</script>


<body>

<div id="NewThread" title='Create thread' >
	<form id='newThread' name='f1'>
		<input type='text' size='25' maxlength='20' id='preThreadName' placeholder='New Thread Name' name='threadRequest' pattern="[A-Za-z0-9_$]+" title='no special characters(but _ $) or spaces are allowed as ThreadNames'/> <!// the valid thread pattern   >
		<input type='submit' value='GO' title='Create'/> 
		<input type='reset' value='<' title='Flush the Box' />
		<img src='images/Checklist_assistant.jpg'/><br/>
		<! Soon in here the radio buttons are  coming :D >
	</form>
</div>
	

<br/></br>
&nbsp;&nbsp; /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
</body>
</html>
 
