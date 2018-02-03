<?php 
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

session_start();
if(!isset($_SESSION['me'])) echo "<script>window.location = 'frontpage.php'</script>"; //if not valid user kick him to login page
if(!isset($_POST['newQuestionField'])) echo "<script>window.location = '../index0.php'</script>"; //if manually called, i.e not via POST method then kick the attempt
if($_POST['block_name'] == "" || $_POST['newQuestionField'] == "" ) { echo "<div style='color: red;' >You know what, this kind of incomplete entries are unacceptable, <br/> I really need a meaningful *Block ID* along with a *Question*  :( </div>"; return; } // activates when blockID less NewQuestions are detected..

require_once '../configuration.php';
//print_r($_POST);
$blockID = $_POST['block_name'];
$newQuestion = $_POST['newQuestionField'];

$safeNQ=mysqli_real_escape_string($connectify,$newQuestion);

if(mysqli_query($connectify, "INSERT INTO question VALUES('','$blockID','$safeNQ')"))
echo  $newQuestion;
