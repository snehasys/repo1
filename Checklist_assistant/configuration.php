<?php
/**
* @author snehasys <snehasysmail@googlemail.com>
*/

$hostName='localhost';
$user='snehasys';
$password='snehas';
$DBname='dcl';

// Create connection
$connectify=mysqli_connect( $hostName , $user , $password , $DBname );

// Check connection
if (mysqli_connect_errno($connectify))
  {
  echo "Failed to connect to MySQL: " . mysqli_connect_error();
  }


/*
$con = mysql_connect("localhost","snehasys","snehas","dcl");
if (!$con)
  {
  die('Could not connect: ' . mysql_error());
  }

mysql_query("USE test",$con);


/*
$config =array();
$config['host'] = 'localhost';
$config['user'] = 'root';
$config['pass'] = '';
$config['table'] = 'test';

$config =array();
$config['host'] = 'localhost';
$config['user'] = 'snehasys';
$config['pass'] = 'snehas';
$config['table'] = 'test';


include 'mysqli.class.php';
$DB = new DB($config);
*/

?>
