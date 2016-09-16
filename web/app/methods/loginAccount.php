<?php
/*
	Авторизация
*/
session_start();
include("../config.php");

$login = checkParam($_POST["login"]);
$password = checkParam($_POST["password"]);
$captcha = checkParam($_POST["captcha"]);

if($captcha != $_SESSION["captcha"])
{
	header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/error?t=captcha', true, 301 );
}

if($login != null & $password != null)
{
	$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
	$check = $mysqli->query("SELECT * FROM `accounts` WHERE `Login`='". $login ."'");
	$check = $check->fetch_array();
	
	if($check['Login'])
	{
		if($check['Password'] == $password)
		{
			$_SESSION["login"] = $login;
			header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/index', true, 301 );
		}
		else
		{
			header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/error?t=login_password', true, 301 );
		}
	}
	else
	{
		header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/error?t=login_unk', true, 301 );
	}
}
else
{
	header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/error?t=login_params', true, 301 );
}

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