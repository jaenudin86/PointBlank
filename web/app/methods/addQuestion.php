<?php
/*
	Добавление вопроса
*/

$mail = checkParam($_POST["login"]);
$login = checkParam($_POST["login"]);

// utils
function checkParam($param)
{
	$formatted = $param;
	$formatted = trim($formatted);
	$formatted = stripslashes($formatted);
	$formatted = htmlspecialchars($formatted);
	
	return $formatted;
}
?>