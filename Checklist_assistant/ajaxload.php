<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

session_start();
?>
<!doctype HTML>
<meta charset="UTF-8" />
<head>
	<title>
		Full Conversation 
	</title>
	<div align="right">
		
		<a href="*/.." title="My Checklist" align="right"><img src="images/home1.png" style="height:30px; width:25px;" /></a><i>
		<a href='#↓' title="Go to Bottom of page" onClick="document.f1.thought.focus();">
			<img src="images/down.jpg" style="height:20px; width:15px;" /></a>

	</div>

<link rel="stylesheet" href="jquery-ui.css" />

<script src="jquery-1.9.1.min.js"> </script>  <!//src=source>

<script src="jquery-ui.js"> </script>

<style> 
.hidden {display: none;}

</style> 


</head>
<body>


<?php 
 $qref_=$_SESSION['qref'];
 $currentThread=$_SESSION['working_thread'];
require_once 'configuration.php';

///<--- module getThreadID ---> ////////
$threadIdArray = mysqli_query($connectify,"SELECT thread_id FROM about_threads WHERE thread_name = '$currentThread' ");    //GET the THREAD ID
$threadIdRow = mysqli_fetch_array($threadIdArray);
$threadId=$threadIdRow[0];
$_SESSION['working_thread_id']=$threadId; ////// <===== setting up a new session variable to put the Working-Thread-ID directly.. :P
require 'qMan/allAdmins.php';
if ( in_array($_SESSION['me'] , $admins, true )) ////////////////////////////////////////////////////////////////////////////////////////////////////////
{ ///////// ONLY ADMINS have the PRiVILeGE to EXECUTE THIS BLOCK Since it's has a DELETE BUTTON, which might be a destructive tool for REGULAR USERS ////
 ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	$result = mysqli_query($connectify,"SELECT * FROM responses,about_threads,question WHERE ref=qref AND thread_ref=thread_id AND thread_ref='$threadId' ORDER BY t_stamp");
	$questionNumber = 1;
	while($value = mysqli_fetch_array($result)){
	//foreach ($data as $key => $value) {
		//echo $value['sequence'].'   	::  '.$value['context'].'<a class="delete">◄</a>'.'<br/>';
		echo '<div id="Qspace1" class="QspaceStyle"><b><small>&nbsp&nbsp'.$questionNumber.'&nbsp►'
		.$value['qcontext'].'</b> </small></div>'                   //<--The question field
		.'<div class="answerStyle"> <i><style>a:link {color:#4E388D;}a:hover {color:#088FDD;}</style>'
		.'<a href="#" class="del" title="Delete This Entry" rel="'.$value['sequence'].'">█</a>&nbsp;&nbsp'  //<----- The delete link
		//.'<a name="#" class="edit" title="Edit Entry" rel="'.$value['sequence'].'">E▄</a>'     //<----- The BOGUS edit link :P
		.'<b><div3 id="'.$value['sequence'].'" title="click to Modify, click away to save" >'.$value['context']   // <------------- the REAL user response (EDITABLE) 
 		.'</div3></div></b></i><small><div class="commentStyle"><div1 id="'.$value['sequence'].'" title="Click to Comment, Click away to save" >'
		.$value['comment'].' .</b></div1></div>' //<--corresponding futureUserResponse aka comment field
		.'<div id="yellowfield" align="right" class="timestampStyle">'.$value['t_stamp'].'</small><i></div>'; //<--The timestamp field
		//.'<br/>';//.'<hr>';
	$questionNumber++;
	}
	$lastquestion = mysqli_query($connectify,"SELECT qcontext FROM question WHERE `ref`=$qref_");
	while($value2 = mysqli_fetch_array($lastquestion)){
		echo '<b><div id="Qspace" class="QspaceStyle"><small> &nbsp&nbsp&nbsp►'
		.$value2['qcontext'].'</b> </small></div><a name="↓"> </a> </div>'; // <!--<<<------the bottom Anchor---- -->
	}
}
else /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
{  /////// FOR NON-ADMIN USERS /////////////////////////////// LESS POWER MAKES YOU LESS RESPONSIbLE ;) :) ;)  //////////////
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	$result = mysqli_query($connectify,"SELECT * FROM responses,about_threads,question WHERE ref=qref AND thread_ref=thread_id AND thread_ref='$threadId' ORDER BY t_stamp");
	$questionNumber = 1;
	while($value = mysqli_fetch_array($result)){
	//foreach ($data as $key => $value) {
		//echo $value['sequence'].'   	::  '.$value['context'].'<a class="delete">◄</a>'.'<br/>';
		echo '<div id="Qspace1" class="QspaceStyle"><b><small>&nbsp&nbsp'.$questionNumber.'&nbsp►'
		.$value['qcontext'].'</b> </small></div>'                   //<--The question field
		.'<div class="answerStyle"> <i><style>a:link {color:#4E388D;}a:hover {color:#088FDD;}</style>'
		.'  &nbsp;&nbsp'  //<----- The disabled delete link
		.'<b><div3 id="'.$value['sequence'].'" title="click to Modify, click away to save" >'.$value['context']   // <------------- the REAL user response (EDITABLE)... 
		.'</div3></div></b></i><small><div class="commentStyle"><div1 id="'.$value['sequence'].'" title="Click to Comment, Click away to save" >'
		.$value['comment'].' .</b></div1></div>' //<--corresponding futureUserResponse aka comment field
		.'<div id="yellowfield" align="right" class="timestampStyle">'.$value['t_stamp'].'</small><i></div>'; //<--The timestamp field
		//.'<br/>';//.'<hr>';
	$questionNumber++;
	}

	$lastquestion=mysqli_query($connectify,"SELECT qcontext FROM question WHERE `ref`=$qref_");
	while($value2 = mysqli_fetch_array($lastquestion)){
		echo '<b><div id="Qspace" class="QspaceStyle"><small> &nbsp&nbsp&nbsp►'
		.$value2['qcontext'].'</b> </small><a name="↓"> </a> </div>'; // <!--<<<------the bottom Anchor---- -->
	}

}
//echo '<pre>';
//print_r($data);

?>

<script>
<!--// Begin scripting
function divClicked() {  ////////////////////////////////////////////--->>> helps put a comment
    var divHtml = $(this).html(); //capture the div1's content

    $('html, body').animate({ scrollTop: $(this).offset().top-50}, 1000);
    var sequence = $(this).attr('id');
    
    var editableText = $("<textarea rows='1' cols='40' title='Click away to save' onFocus='this.select()' id='"+sequence+"' />");
    editableText.val(divHtml);
    $(this).replaceWith(editableText); //replaceWith is a very handy command, monsieur .. :D
    editableText.focus();
    // setup the blur event for this new textarea
    editableText.blur(editableTextBlurred);
}

function editableTextBlurred() {  ////////////////////////////////////////////--->>> helps put a comment
    var id3=$(this).attr('id');
    var html = $(this).val();
	
	$.post("ajaxCommentPost.php",{'id' : id3,'comment' : html}, function(data){
		if(data)	alert(data);
			//document.write(id);
				//alert(item);
			});
     
    var viewableText = $("<div1 id='"+id3+"' >");
    viewableText.html(html);
    $(this).replaceWith(viewableText);
    // setup the click event for this new div
    viewableText.click(divClicked);
}
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
function divClicked3() {  ////////////////////////////////////////////--->>> helps edit an Answer, a.k.a. Response
    var divHtml = $(this).html(); //capture the div3's content

    $('html, body').animate({ scrollTop: $(this).offset().top-250}, 1400);
    var sequence = $(this).attr('id');
    
    var editableText = $("<textarea rows='1' cols='60' title='Click away to save'  id='"+sequence+"' />");
    editableText.val(divHtml);
    $(this).replaceWith(editableText);
    editableText.focus();
    // setup the blur event for this new textarea
    editableText.blur(editableTextBlurred3);
}

function editableTextBlurred3() {  ////////////////////////////////////////////--->>> helps edit an Answer, a.k.a. Response
    var id3=$(this).attr('id');
    var html = $(this).val();
	
	$.post("ajaxEditAnswer.php",{'id' : id3,'edited_answer' : html}, function(data){
		if(data)	alert(data);
			//document.write(id);
				//alert(item);
			});
     
    var viewableText = $("<div3 id='"+id3+"' >");
    viewableText.html(html);
    $(this).replaceWith(viewableText);
    // setup the click event for this new div
    viewableText.click(divClicked3);
}
///~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 $(document).ready(function()
  {
	$("div1").click(divClicked);  /////////////////////////////////////////--->>> helps put a comment

	$("div3").click(divClicked3);  /////////////////////////////////////////--->>> helps edit an Answer, a.k.a. Response

//	$('html, body').animate({   scrollTop: $("#gotop").offset().top }, 3000);

	$('.del').click(function(){
			var item = $(this).parent();
			var id = $(this).attr('rel');
			$.post("ajaxDelete.php",{'id' : id}, function(data){
			//	alert(data);
			//document.write(id);
				//alert(item);
				$(item).hide();
				$('html, body').animate({
		            scrollTop: $(".del").offset().top   
		          }, 1000);

			});
	});


$(function(){ 

//"--------------------------------------------------"///// THE INFO PAGE
 $("#dialog-about").dialog({autoOpen : false,
	 draggable: true,  height : 540, width : 490, modal : true, position : ['top',35] ,
	// puff, bounce, drop, highlight, blind, size, fold, explode,pulsate,shake,
	 show : "fold", hide : "drop",
	 buttons:
	 {
	 	"Get it? Now go back to what you were doing..": function(){
	 		$(this).dialog("close");
	 	}
	 }
	
	 });

/*
$('.edit').click(function(){
		var item = $(this).parent();
		id1 = $(this).attr('rel');
		$( "#dialog-about" ).dialog( "open" );

});
 */
//==================================================
	$( "#about" ).button().click(function() {
		$( "#dialog-about" ).dialog( "open" );
  	 });

  });
	
}); 
// End Scripting -->
</script>

<div id="dialog-about" title="Well, I am your Checklist.. 	" class='hidden'>
	<small>
		<p> 
 Often one faces a situation /challenge in design, where it is difficult to <u> identify their problems</u> with the existing products, except those which are apparent and obvious. This failure converts the creative task of problem solving into the routine exercise of solving mechanically framed design problems. Even if the important problem is identified and solved, it is not <b> unusual</b> to find that when the design is almost finalized, one gets ideas about the other problems, which could have been solved <b>together</b>, without adding to <u>design time and cost</u>.
    </p>
    <p>This checklist of yours, is an <b>aid</b> to explore the problem from many sides. The list is general in nature and has been worked out on the basis of experience of developing products in wide range of fields, from the consumer appliances to industrial products.
</p> <p>
 In fact <u><b>in no case all the questions will be relevant.</b></u> Look for your possible goals in designing and pick up the questions from the collection. Edit them to suit the type of problems you are handling. Questions asked and replied with an open mind can give you <b>CLUE</b>'s to the <u>valid and rational change</u> that you were <b>looking for</b>.
</p> <p>
The checklist is by no means, the end of your problem explorations, but only a <b>beginning</b>. To get an effective solution, you must explore the effective way, of looking at the problem, and spot the problem that has eluded notice so far. This transforms the routine task of collecting data into a <b>creative search</b> for a different way of <u>looking at the problem situation</u>. Effective solutions will emerge only if the <b>problems are framed correctly</b>.
		</p>
</small>
</div>
</body>
</HTML>
