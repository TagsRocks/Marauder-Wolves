<?php
// Send variables for the MySQL database class.
//Change User,PW and yourDBname
$database = mysql_connect('localhost', 'tutorial', 'tutorialpassword') or die('Could not connect: ' . mysql_error());
mysql_select_db('tutorial') or die('Could not select database');

$query = "SELECT * FROM `scores` ORDER by `score` DESC LIMIT 100";
$result = mysql_query($query) or die('Query failed: ' . mysql_error());

$num_results = mysql_num_rows($result);


for($i = 0; $i < $num_results; $i++)
{
    $row = mysql_fetch_array($result);
    echo $row['name'] . ";" . $row['score'] . ";";
}

/*
 * ORIGINAL:
for($i = 0; $i < $num_results; $i++)
{
    $row = mysql_fetch_array($result);
    echo $row['name'] . "\t" . $row['score'] . "\n";
}
______

$encode = array();
$i = 1;
while($row = mysql_fetch_assoc($result)) {
    $encode[$row['id']][] = $row['score'];
    $encode[$row['id']][] = $row['name'];
   // $encode[$row['id']][$i] = $row['name'];
    $i++;
}

echo json_encode($encode);
*/
?>