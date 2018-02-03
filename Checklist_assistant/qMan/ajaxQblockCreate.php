<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/


//new block create ajax page
session_start();
if(!isset($_SESSION['me'])) echo "<script>window.location = '../frontpage.php'</script>"; //if not valid user kick him to login page
  /////////////////////// $colors=array('red', 'yellow', 'blue');  $_SESSION['color']=$colors;
///////////////////////

require 'allAdmins.php'; ///////// ONLY ADMINS ARE ALLOWED IN THIS PAGE /////////////
if (!in_array($_SESSION['me'] , $admins, true )) { /// ADMIN CHECK...
	echo "<h2 style='color: RED;' > <br/><br/>**  Unauthorized access DENIED **<h2>";
	return ;
}

require '../configuration.php';
if ( mysqli_query($connectify,"INSERT INTO qmatrix VALUES ('','')" )) echo "**Cheers! New Question Block Created :) ";
else 	echo "** Creation ERROR ** ";

?> 	 		

