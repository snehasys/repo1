  <?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/
session_start();
  print_r($_POST);

require '../configuration.php';

$b_id=$_POST['block_id'];
$n_priorities=$_POST['new_priorities'];

/*
$priority_array=explode("/", $n_priorities);

	$counter=0; 
while($priority_array[$counter]){
 $checked=0;
 $real_block_arr=mysqli_query($connectify,"SELECT `qblockid` FROM `qmatrix`");
	while($real_blocks=mysqli_fetch_array($real_block_arr)){

		if ($priority_array['$counter'] === $real_blocks[0]) $checked=1;
	}
  if ( ! $checked) { echo "this priority matrix either contains fictional BlockIDs or is in wrong format.."; return; }
}
*/
// if (! preg_match("[\d\,]*", $n_priorities)) echo "Wrong pattern detected"; ////<---------- doesn't work :P

if(mysqli_query($connectify,"UPDATE `qmatrix` SET `nextblockpriority`='$n_priorities' WHERE qblockid= $b_id ")) echo "***The edit was Successful*** make sure you have putted only valid BlockIDs and slashes there.. ";

else echo "Your edit was unsuccessful.. :( ";
