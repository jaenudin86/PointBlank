<?php
/*
	Создание аккаунта
*/
session_start();
include("../config.php");

$mail = checkParam($_POST['mail']);
$login = checkParam($_POST['login']);
$nickname = checkParam($_POST['nickname']);
$password = checkParam($_POST['password']);
$referal = checkParam($_POST['referal']);
$captcha = checkParam($_POST['captcha']);

if($mail != null & $login != null & $nickname != null & $password != null & $captcha != null)
{
	$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
	$check = $mysqli->query("SELECT * FROM `accounts` WHERE `Mail`='". $mail ."'");
	$check = $check->fetch_array();
	
	if(!$check['Login'])
	{
		$check2 = $mysqli->query("SELECT * FROM `accounts` WHERE `Login`='". $login ."'");
		$check2 = $check2->fetch_array();
		
		if(!$check2['Login'])
		{
			$check3 = $mysqli->query("SELECT * FROM `players` WHERE `Name`='". $nickname ."'");
			$check3 = $check3->fetch_array();
			
			if(!$check3['Name'])
			{
				$hash = md5($login ."". rand(1, 50000) ."". $mail);
				
				$createAccount = $mysqli->query("INSERT INTO `accounts`(`AccountID`, `Login`, `Password`, `Status`, `Referals`, `Mail`, `Hash`) VALUES ('','". $login ."','". $password ."','0','0','". $mail ."','". $hash ."');");
				
				$getAccount = $mysqli->query("SELECT * FROM `accounts` WHERE `Login`='". $login ."'");
				$getAccount = $getAccount->fetch_array();
				
				$createPlayer = $mysqli->query("INSERT INTO `players`(`PlayerID`, `AccountID`, `Name`, `Rank`, `PC_Cafe`, `Emblem`, `Exp`, `GP`, `Money`, `ClanID`) VALUES ('','". $getAccount['AccountID'] ."','". $nickname ."','0','0','0','0','0','0','0');");
				
				//Замените настройки на нужные.
				$mail_to = $mail; //Вам потребуется указать здесь Ваш настоящий почтовый ящик, куда должно будет прийти письмо.
				$type = 'plain'; //Можно поменять на html; plain означает: будет присылаться чистый текст.
				$charset = 'UTF-8';

				include('../utils/mail.php');
				
				$name = 'no-reply@pointblank.pw';
				$phone = null;
				$message = wordwrap($message, 70, "Для активации перейдите по ссылке : https://pointblank.pw/activate?account=". $hash);
				$replyto = $mail;
				$headers = "To: \"Administrator\" <$mail_to>\r\n".
							  "From: \"$replyto\" <$mail_from>\r\n".
							  "Reply-To: $replyto\r\n".
							  "Content-Type: text/$type; charset=\"$charset\"\r\n";
				$sended = smtpmail($mail_to, $name, $message, $headers);
				
				header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/success?t=register', true, 301 );
			}
			else
			{
				header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/error?t=reg_nickname', true, 301 );
			}
		}
		else
		{
			header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/error?t=reg_login', true, 301 );
		}
	}
	else
	{
		header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/error?t=reg_mail', true, 301 );
	}
}
else
{
	header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/error?t=reg_params', true, 301 );
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