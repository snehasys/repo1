<?php 
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

session_start();
//if(!isset($_POST['whatever_blah'])) echo "<script>window.location = '../index0.php'</script>"; //if manually called, i.e not via POST method then kick the attempt
$id=$_POST['id'];
$editedQuestion=$_POST['editedQuestion'];

require '../configuration.php';

$safe_editedQuestion=mysqli_real_escape_string($connectify,$editedQuestion);
if(!mysqli_query($connectify,"UPDATE question SET `qcontext`='$safe_editedQuestion' WHERE `ref` = $id "))
	echo "\n\n".$comment." /-> ".$id."\nSorry,\n That edit was totally Unsuccessful :( ";


?>
