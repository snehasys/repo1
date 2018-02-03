
<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/


session_start();
 if(isset($_SESSION['me'])) echo "<script> window.location.replace ('*/..') </script>";


?>

<!doctype HTML>
<meta charset="UTF-8" />
<title>Checklist Login Page</title>
<h5 style='color: #ffffed;' title='&#67;&#114;&#101;&#100;&#105;&#116;&#115;&#33;'>&#32;&#68;&#101;&#115;&#105;&#103;&#110;&#101;&#114;&#32;&#67;&#104;&#101;&#99;&#107;&#108;&#105;&#115;&#116;&#32;&#118;&#49;&#46;&#51;&#44;&#32;&#169;&#32;&#32;&#50;&#48;&#49;&#51;&#32;&#115;&#110;&#101;&#104;&#97;&#115;&#121;&#115;&#44;<br/>&#68;&#101;&#118;&#101;&#108;&#111;&#112;&#101;&#100;&#32;&#98;&#121;&#32;&#83;&#110;&#101;&#104;&#97;&#115;&#121;&#115;&#174;&#44;&#32;&#83;&#112;&#111;&#110;&#115;&#111;&#114;&#101;&#100;&#32;&#98;&#121;&#32;&#73;&#68;&#67;&#45;&#73;&#73;&#84;&#32;&#66;&#111;&#109;&#98;&#97;&#121;</h5>
<div id='loading'>
<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<img src="images/ajax-loader2.gif" style="height: 20px;width: 20px;"/><img src="images/ajax-loader2.gif" style="height: 20px;width: 20px;"/>

</div>
<body>

<style>
body { font-size: 82.5%; }
label, input { display:block; }
input.text { margin-bottom:12px; width:95%; padding: .4em; }
fieldset { padding:0; border:0; margin-top:25px; }
h1 { font-size: 1.2em; margin: .6em 0; }
div#users-contain { width: 350px; margin: 20px 0; }
div#users-contain table { margin: 1em 0; border-collapse: collapse; width: 100%; }
div#users-contain table td, div#users-contain table th { border: 1px solid #eee; padding: .6em 10px; text-align: left; }
.ui-dialog .ui-state-error { padding: .3em; }
.validateTips { border: 1px solid transparent; padding: 0.3em; }

.hidden {display: none;}
.QspaceStyle {color:dark; border: 2px solid white;  background-color: #BBBBFF; font-family: sans-serif; padding: .3em;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px;}
.loginStyle { color: grey; border: 2px solid green; background-color: #dfd; padding: .3em;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px;}

</style>
<link rel="stylesheet" href="jquery-ui.css" />
<script src="jquery-1.9.1.min.js"> </script>

<script src="jquery-ui.js"> </script>

<script>

  $(document).ready(function(){

  	$("#loading").delay(1200).hide(0);

  	$(function(){

  		 $("#login-fields").dialog({autoOpen : true,
  		 draggable: false,  height : 490, width : 390, modal : false, position : "topleft" ,
  		 show : "fold", hide : "fold"});

  		 $("#signup-fields").dialog({autoOpen : false,
  		 draggable: true,  height : 352, width : 370, modal : true, position : ['center',150] ,
  		 show : "clip", hide : "blind"}); // puff, bounce, drop, highlight, blind, size, fold, explode,pulsate,shake,



  	});

  $("#f1").submit( function()
  {     
    $.post('user_login.php',$('#f1').serialize(), function(data){
		alert(data);
		$('#login-fields').dialog( "close" );
		window.location.replace ('index.php').delay(5000);

       });
    return false;
   });

  $("#f2").submit( function()
  {     
    $.post('createUserAcc.php',$('#f2').serialize(), function(data){
		alert(data);
		$('#signup-fields').dialog( "close" );				
       });
    return false;
   });


  });


</script>
<div id="login-fields" title="Authenticate Yourself" class="hidden">
	<img src="images/Checklist_assistant_logo.png"> <!style="height:300px; width:250px;" >

  <form id="f1"> <!action="user_login.php" method="post">
	<br/><center>
	
	<input type="text" name="uname" placeholder="User Name" title='Your User Name'/>
	
	<input type="password" name="pwd" placeholder="Password" title='Your Password'> 
	<input type="submit" class='loginStyle' value="   Login..  " title='Click to Login'/>
	<small> New User? Well, just click <a href="#" onClick="$( '#signup-fields' ).dialog( 'open' );">here</a> <!--If some problem arises add "$('#signup-fields').show();" in the onClick() module.. :D -->
  		<!center>
  </form>
</div>

<!----////////////////  popped signup dialogue \\\\\\\\\\\\\\-------->
<div id="signup-fields" title="Registration" class="hidden" >
	<form id="f2">
	 <center>
	<img src="images/Checklist_assistant.jpg">
		<br/><br/>
		<label>user</label>
		<input type="text" name="newUname" placeholder="New_Username"  
		pattern="[A-Za-z0-9_$]+" title="Preferred unique username, (no special characters allowed but '_'and'$')" />  <! anything but an apostrophe:: pattern = "^((?!').)*$"  >
		<label>mail-id</label>115
		<input type="text" name="newMail" placeholder="Your E-mail" 
		pattern="^[_A-Za-z0-9-]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})$" title="Email: you@yourdomain.com"	/>		
		<label>passwords</label>
		<input type="password" name="newPwd" placeholder="PassWord" title='Type in a password'/>
		<input type="password" name="retypePwd" placeholder="Type it again" title='Retype that Password' />
		
		<input type="submit" class="QspaceStyle" value=" Sign Up  "/>
		<small>*You know the rules! All fields are required</small>
	</form>
	</center>
</div>

</body>
</HTML>


