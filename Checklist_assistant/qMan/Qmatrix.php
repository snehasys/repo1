<?php  
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

/**   Summary:
  *  This page is constructing the question matrix 
  *  TODO: more work required on the Acyclic Graph mapping of the questions, 
  *        and the datastructure behind it. Increase the question depths.
  *        Try to introduce weightage of each path.
  *
  */
session_start();
if(!isset($_SESSION['me'])) echo "<script>window.location = '../frontpage.php'</script>"; //if not valid user kick him to login page
print_r($_SESSION);
require 'allAdmins.php'; ///////// ONLY ADMINS ARE ALLOWED IN THIS PAGE /////////////
if (! in_array($_SESSION['me'] , $admins, true )) {
	echo "<h2 style='color: RED;' > <br/><br/>**  This is Unauthorized, Access DENIED **<h2>";
	return;
}


require '../configuration.php';
  echo "<table border='1' class='curvedEdges'>
	<th>blockID</th><th title='In descending Order, seperated by slashes / .'>Next Block Priority</th>";
  $tableRows2Darray=mysqli_query($connectify,"SELECT * FROM `qmatrix`");
  while ($tableRows = mysqli_fetch_array($tableRows2Darray ) )
   {
	echo "<tr><td rel='readOnly' style='color: #555'>". $tableRows['qblockid']."</td><b><td rel='". $tableRows['qblockid'] ."'title='In descending Order, seperated by slashes / .' style='background-color: green;'>".$tableRows['nextblockpriority']."</td></b></tr>";
  }

 echo "</table>";

?>
<style type="text/css">

table.curvedEdges { border:10px solid Brown;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px; }
table.curvedEdges th { color: white;border-bottom:1px dotted black;  border-color: #666666; background-color: #800; padding:10px; }
table.curvedEdges td { color: white;border-bottom:1px dotted white; background-color: #ffffdf;  padding:2px; }

.logoutStyle {color:dark; border: 4px solid white;  background-color: #BBBBFF; font-family: sans-serif; padding: .3em;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px;}
</style>

  <script src="../jquery-1.9.1.min.js"> </script>  <!//src=source>
  

<script>
function tdClicked() {  ////////////////////////////////////////////--->>> helps edit an Answer, a.k.a. Response
	if ($(this).attr('rel') == 'readOnly') return;
    var divHtml = $(this).html(); //capture the div3's content

    var QblockID = $(this).attr('rel');
    
    var editableText = $("<input type='text' size='16' title='** BE Very CAREFUL, Do NOT PUT anything but valid BlockIDs and slashes** Put data,In descending Order, seperated only by slashes / . Then Click away to save'  rel='"+QblockID+"' />");

    editableText.val(divHtml);
    $(this).replaceWith(editableText);
    editableText.focus();
    // setup the blur event for this new textbox
    editableText.blur(editableTextBlurred);
}

function editableTextBlurred() {  ////////////////////////////////////////////--->>> helps edit an Answer, a.k.a. Response
    var html = $(this).val();
    var id4 =$(this).attr('rel');

	
	$.post("ajaxEditQmatrix.php",{'block_id' : id4 ,'new_priorities' : html}, function(data){
		if(data)	alert(data);
			//document.write(id);
				//alert(item);
			});
  
    var viewableText = $("<td style='background-color: #f22;' >");
    viewableText.html(html);
    $(this).replaceWith(viewableText);
    // setup the click event for this new div
    viewableText.click(tdClicked);
}
///~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	$(document).ready(function(){

		$("td").click(tdClicked);  /////////////////////////////////////////--->>> helps editing a Block priority
		$('#newBlockCreate').submit(function(){					
		    $.post('ajaxQblockCreate.php',$('#newBlockCreate').serialize(), function(data){
		    	if(data) alert(data);
		    });
		 return false; 
		});

	});

</script>
<div id='newBlockCreate'>
<form name='f1'>
	<input type='submit' value='create'/>
</form>
</div>


