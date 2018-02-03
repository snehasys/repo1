<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

session_start();
print_r($_SESSION);
if(!isset($_SESSION['me'])) echo "<script>window.location = '../frontpage.php'</script>"; //if not valid user kick him to login page

require 'allAdmins.php'; ///////// ONLY ADMINS ARE ALLOWED IN THIS PAGE /////////////
if (! in_array($_SESSION['me'] , $admins, true )) {
	echo "<h2 style='color: RED;' > <br/><br/>**  This is Unauthorized, Access DENIED **<h2>";
	return;
}
?>

<!doctype>
<HTML>
	<meta charset='utf-8'/>
	<title>Interact With Questions</title>
	<head>
	<body>
		<h1 style="color: #AABBCC; "><center>Backbone Questions</center><hr/></h1>
		
<script src="../jquery-1.9.1.min.js"> </script>  <!//src=source>

	</head>


	<style>
	.QspaceStyle {color:DarkBlue; border: 3px solid white;background-color: #BBCCDD; padding: 5px; padding: .3em;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px;}
	</style>


<script>
function divClicked() {  ////////////////////////////////////////////--->>> helps edit a question
    var divHtml = $(this).html(); //capture the div2's content

    $('html, body').animate({ scrollTop: $(this).offset().top-250}, 1000);
    var sequence = $(this).attr('id');

    var editableText = $("<input type='text' size='63' maxlength='200' title='Click away to save' id='"+sequence+"' />");
    editableText.val(divHtml);
    $(this).replaceWith(editableText); //replaceWith is a very handy command, monsieur .. :D
    editableText.focus();
    // setup the blur event for this new textarea
    editableText.blur(editableTextBlurred);
}

function editableTextBlurred() {  ////////////////////////////////////////////--->>> helps edit a question
	var id2=$(this).attr('id');
    var html = $(this).val();
	
	$.post("ajaxQedit.php",{'id' : id2,'editedQuestion' : html}, function(data){
		if(data)	alert(data);
			//document.write(id2);
	});     
    var viewableText = $("<div2 id='"+id2+"' >");
    viewableText.html(html);
    $(this).replaceWith(viewableText);
    // setup the click event for this new div
    viewableText.click(divClicked);
}

function check(stuff) {
	var selectedBlockFromDropdown=stuff.options[stuff.selectedIndex].value;
	document.getElementById('block_id').value=selectedBlockFromDropdown;
}


 $(document).ready(function() {

	$("div2").click(divClicked);  /////////////////////////////////////////--->>> helps edit a question

	$("#form1").submit(function() { 

		$.post('ajaxQpost.php',$('#form1').serialize(), function(data){
			($('#appended').append("<div title='Refresh this page to edit this question' class='QspaceStyle'><b>"+data+"</div>"));

	          $('html, body').animate({
	            scrollTop: $("#appended").offset().top
	          }, 3500);
			 document.getElementById('nQF').select();
//	          alert(data);
	         
		});

	  return false; 
	});



});
</script>


<?php
require '../configuration.php';
$result = mysqli_query($connectify,'SELECT * FROM question order by qblock,ref');

	while($line = mysqli_fetch_array($result)){ ///// <--------------- shows all the questions....
		echo '<div id="Qspace" class="QspaceStyle">'.$line['ref'].'→ <div2 id="'.$line['ref'].'" >'.$line['qcontext'].'</div2></div>';
	}
//	$i=$i+1;
//----------------------------------------------------------------------
	echo "<div id='appended' style='color: RED'> </div>";
	$query = mysqli_query($connectify,"SELECT DISTINCT qblockid FROM qmatrix "); // Run your query

echo '<i></small><b><div style="color:RED;">Select your block within all existing blocks :-->> </i></small>
<select id="list" name="dropdown" value="b_id" onChange="check(this);" >
 <option value="">Select</option>'; // Opens the drop down box
// Loop through the query results, outputing the options one by one

while ($row = mysqli_fetch_array($query)) { /// populating the dropdown... 
   echo "<option value='".$row[0]."' >".$row[0]."</option>";
}

echo '</select>';// Close your drop down box
?>

	<form id="form1" name='f1'>
			<label>Block ID</label>
		<input type='text' size="1" id='block_id' name='block_name' title='Numeric Question block ID' placeholder=' # #' readonly> 
												<!value='<?php $max = mysqli_query($connectify,"SELECT max(ref) as maxid FROM question ");	 $max1=mysqli_fetch_array($max); echo $max1['maxid'];?>'/>
		<input type='text' id='nQF' name='newQuestionField' size="50" placeholder='new_question' title="Insert a New Question with a relevant blockID "/>
		<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<input type='submit' value='  Put  '/>
		<input type='reset' value='Clear' id='clearfield'/>
	</form>
	</div>





	</body>
