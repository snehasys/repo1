<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/


require_once 'configuration.php';


$result = mysqli_query($connectify,"SELECT * FROM question");

while ($value = mysqli_fetch_array($result))
{
	echo $value['qref'].'  ::  '. $value['qcontext'].'<br/>';
}

?>
