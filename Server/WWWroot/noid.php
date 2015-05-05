<?php if (isset($_GET['clientver'])){
}
else{
header("Location: index1.php"); 
exit;
	}
?>

<?php
include("globalconfig.php"); 
?>
<html>

<head><title>noid</title></head>

<body>


	<pre>
<?php 
	//echo "Simple Package Manager Version: ";
	//echo "$Version";
	//echo "<br/>";
	//print_r($_GET);
	if ($_GET['clientver'] < $Version){
	echo "The client Version is out of date";
		//return to the client PERFORM UPDATE 
	} 	
	else {
		if ($_GET['clientver'] == $Version){
		//We are good to go
		echo "We are on a valid version number";
		}
		else {
		echo "Invalid Client Version Number";
		}
	}
	
?>

	</pre>
</body>
</html>