
<?php 
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

session_start();
if(!isset($_SESSION['me'])) echo "<script>window.location = 'frontpage.php'</script>"; //if not valid user kick him to login page
if(!isset($_SESSION['qref'])) $_SESSION['qref']=1; //start questioning from the beginning

if(!isset($_SESSION['working_thread']) || $_SESSION['me']==$_SESSION['working_thread'] ) echo "<script>window.location = 'index0.php'</script>"; // if working on no thread jump to threadlist

$currentThread=$_SESSION['working_thread'];

 $currentUserName=$_SESSION['me'];
// echo $currentUserName;
$_SESSION['prevEntry']="NONE";
?>

<style>

.hidden {display: none;}
.linkStyle {color:grey; border: 2px solid grey; background-color: #fff;font-family: sans-serif; padding: .3em;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px;}

.QspaceStyle {color:dark; border: 2px solid white;  background-color: #BBBBFF; font-family: sans-serif; padding: .3em;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px;}
.answerStyle {border-top: 1px solid grey; background-color: #eee; padding: 0.8em; padding-left: 0.1em; font-size: 100%;-webkit-border-radius:3px;-moz-border-radius:3px;-ms-border-radius:3px;-o-border-radius:3px;border-radius:13px;}
.commentStyle {background-color: #ffff9f;-webkit-border-radius:3px;-moz-border-radius:3px;-ms-border-radius:3px;-o-border-radius:3px;border-radius:13px;}
.timestampStyle {background-color: #ffffbf;}
.skipperClass {color: #fff; background-color: brown;-webkit-border-radius:13px;-moz-border-radius:13px;-ms-border-radius:13px;-o-border-radius:13px;border-radius:13px;}
</style>

<!doctype>
<html>
<head> 
<meta charset="UTF-8" />
  <title> <?php echo "Thread:: $currentThread"; ?> </title> 
  <! |||||||||_______________________>
          <a name='↑'></a> <!ASCII24 Anchor > 
                <meta name="viewport" content="width=device-width, initial-scale=1.0" />

<link rel="stylesheet" href="jquery-ui.css" />
                
                <div class="top-container" style="border-bottom-width: 1px;">
                <body onLoad="document.f1.thought.focus();">
                <table border="0" cellpadding="0" cellspacing="0"  width="100%" align="center">
                   <tr>
                      <td>
                              <table border="0" cellspacing="0" cellpadding="3" width="100%">
                                <tr style="background-color: #FAFFF0"><!#088fff">
                                   <td align="left" width=100%>
                                        <!pre means preformatted>
                                          <i><small><strong>
                                            <small>
                                            
                                            <a href='index0.php' class="linkStyle" title="Create/Browse your Threads" > My Threads </a>
                                            &nbsp;&nbsp;&nbsp;
                                            <a href='#↓' name='bott' class="linkStyle" title="i.e. Bottom of page" onClick="document.f1.thought.focus();">Goto End of this Thread</a>
                                            &nbsp;<B>       |</B> &nbsp;&nbsp;
                                            <a href='ajaxload.php' class="linkStyle" title="i.e. full Thread" >See Full Conversation </a>
                                            &nbsp;&nbsp;
                                            <?php require 'qMan/allAdmins.php'; if ( in_array($_SESSION['me'] , $admins, true )) echo "| &nbsp; <a href='qMan/' title='I want to Edit QuestionBank' class='linkStyle' style='border: 4px solid brown;background-color: #008800; color: white'>Wow! SuperUser Detected</a> &nbsp;| ";?>
                                            &nbsp;&nbsp;
                                            <a href='images/..' title="refresh now"><img src='images/refresh-animated.gif' /></a>
                                            &nbsp;<B>       |</B> &nbsp;&nbsp;
                                            <button id="about" title="about-Checklist">What am I?</button>
                                            <td align="right"> <i><b>
                                              <small>
                                            <a href='logMeOut.php' name='logout' title="I'm done, Log Me Out" class='QspaceStyle' style='border: 3px solid white;background-color: #088fff;color: white'>←Logout▬</a>&nbsp;&nbsp;
                                            </b></i></small></td>
                                            
                                          </strong></small></i>
                                         </font>
                                   </td>
                                                  <!--td align="right"> </td> <br/-->
                                </tr>
                              </table>
                                          <h1 style="color:#fff; background-color: #088fff;border: 4px solid black;"> <!#088fff>
                                       <img src="images/tick.jpg" style="height:30px; width:25px;" /> <?php echo "$currentUserName 's Checklist ( $currentThread )";?></h1>
                       </td>
                   </tr>
                </table>
            </div>
                <! |||||||||_______________________>

  <script src="jquery-1.9.1.min.js"> </script>  <!//src=source>
  
<script src="jquery-ui.js"> </script> <!>


<script>
 var skipcount = 0;
 var skiptext = "<div2 class='skipperClass'><blink><b> :&nbsp;&nbsp; SKIPPED by user &nbsp;&nbsp;</b></blink></div2>";
 var urlpaste = "<img src= ' pasteURL ' />";

//<h1> Sorry, Javascript/J-query Support Required, See your browser privileges :( <h1>
  //<!-- Begin J-Query body

 $(document).ready(function() { 
//    document.documentElement.style.overflow = 'hidden'; // disables the vertical scrollbar, doesn't work for IE*

    $("#messages").load('ajaxload.php');
    $("#loading").delay(1200).hide(0);
    

    $("#talking").submit( function(){
     
    $.post('ajaxPost.php',$('#talking').serialize(), function(data){
          //alert(data);
          ($('#messages').append('<div>'+data+'</div>'));
          
          $('html, body').animate({
            scrollTop: $(".posting").offset().top
          }, 2500);
          
          document.getElementById('clearfield').click();
          document.f1.thought.focus();
      });

      //$("#id").scrollTop($("#id").scrollTop() + 100);
      //$('html, body').animate({scrollBottom: $("#page").offset().top}, 2000);

          return false;
    });

  }); // End Scripting -->
  </script>
</head>

<!center>
<!______dISPLAY Hist__________>
<div id="messages">  </div>

<!______Posting__________>
<div class="posting" id='posting_id'>    <!style="height:10%; position:relative; bottom:0px;">
<form id="talking" name='f1'> <! onsubmit="window.scrollBy(0, 200); return true" >
<label> <!Mess> </label>
 <!textarea rows="1" cols="50" placeholder="Thought" title="Free your mind and tell" name='thought' > </textarea> 
 
<!--animate :P--> 
 <div id="loading"> &nbsp&nbsp <img src="images/ajax-loader1.gif" style="height:13px; width:170px;"/> fetching</div>   <!-- loading anime-->
 <!///////////// TEXTBOX //////////////>
 <input type='text' size="50" title="Free your mind and tell" maxlength="500" id='thought_id' name="thought"  placeholder="Current Thought" onkeyup="if (event.keyCode == 13)  document.f1.put.click();"></textarea>
 <!pattern="[^.!?\s][^.!?]*(?:[.!?](?![']?\s|$)[^.!?]*)*[.!?]?[']?(?=\s|$)"> <!pattern="[A-Za-z]"/> 
 <!///////////// PUT/SKIP/CLEAR buttons ////////////>
 <input type="submit"  name='put' title="Click me when you are done" value="Put" style="position:relative;"/>
 <!--///// this botton doesn't work /////// -->

 <input type="button" id="skipper"  value="Skip" onClick="skipcount++;document.f1.thought.value= skipcount+':'+skiptext; document.f1.put.click();" title="Click me, if this question isn't applicable." />
 <input type="reset" id="clearfield" value="clear" onClick="document.f1.thought.focus();" title="Flush that Box" />
 <input type="button" class="QspaceStyle" value="put a picture!" style="border: 2px solid green;background-color: white;" title="How to?: Replace the **paste_Image_URL_here ** with your image URL"
             onMouseDown=" getElementById('thought_id').value += urlpaste; document.f1.thought.focus(); " />
 
 <a href='#↑' id="gotop" title="Go to Top of page" ><!onClick= "$("html, body").animate({ scrollTop: $(document).height() }, "slow");"> <img src="images/top.jpg" style="height:30px; width:30px;" /> </a>
 <a name='↓'> </a> </div> <!--<<<------the bottom Anchor---- -->
</form>
</div>
 <div id="footer" class="footer" > <p> &nbsp <! the adjustment bureau >
</div>

<!/center>
</body>
</html>
