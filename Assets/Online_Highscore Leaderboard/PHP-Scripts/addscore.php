<?php
//Change User and PW
$db = mysql_connect('localhost', 'tutorial', 'tutorialpassword') or die('Could not connect: ' . mysql_error());
//Change DBname
mysql_select_db('tutorial') or die('Could not select database');

// Strings must be escaped to prevent SQL injection attack.
$name = mysql_real_escape_string($_GET['name'], $db);
$score = mysql_real_escape_string($_GET['score'], $db);
$uniqueID = mysql_real_escape_string($_GET['uniqueID'], $db);
$hash = $_GET['hash'];

$secretKey="123456789"; # Change this value to match the value stored in the client javascript below

$real_hash = md5($name . $score . $secretKey);
if($real_hash == $hash) {
    // Send variables for the MySQL database class.
   // $query = REPLACE INTO scores( '$uniqueID', '$name', '$score' );
    //REPLACE INTO scores( id, name, score, uniqueID ) VALUES ( 1, 'Testname', 100, 'A1' )
    $query = "replace into scores values ('$uniqueID', '$name', '$score');";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());
}
?>