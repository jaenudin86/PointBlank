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
				
				$getPlayer = $mysqli->query("SELECT * FROM `players` WHERE `AccountID`='". $getAccount['AccountID'] ."'");
				$getPlayer = $getPlayer->fetch_array();
				
				// создание инвентаря
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '100003004', '0', '3', '100')");
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '200004006', '0', '3', '100')");
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '300005003', '0', '3', '100')");
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '400006001', '0', '3', '100')");
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '601002003', '1', '3', '100')");
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '702001001', '2', '3', '100')");
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '803007001', '3', '3', '100')");
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '904007002', '4', '3', '100')");
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '1001001005', '5', '3', '100')");
				$mysqli->query("INSERT INTO `items` (`ObjectID`, `OwnerID`, `ItemID`, `Slot`, `Type`, `Count`) VALUES ('', '". $getPlayer['PlayerID'] ."', '1001001006', '6', '3', '100')");
				// создание дефолт конфигов
				$mysqli->query("INSERT INTO `players_config` (`PlayerID`, `Config`, `Blood`, `Visibility`, `Mao`, `Audio1`, `Audio2`, `AudioEnable`, `MouseSensitivity`, `Vision`) VALUES ('". $getPlayer['PlayerID'] ."', '55', '1', '2', '0', '30', '100', '2', '70', '80')");
				// создание экипировки
				$mysqli->query("INSERT INTO `players_equip` (`PlayerID`, `WeaponPrimary`, `WeaponSecondary`, `WeaponMelee`, `WeaponThrownNormal`, `WeaponThrownSpecial`, `CharRed`, `CharBlue`, `CharHelmet`, `CharDino`, `CharBeret`) VALUES ('". $getPlayer['PlayerID'] ."', '100003004', '601002003', '702001001', '803007001', '904007002', '1001001005', '1001001006', '1102003001', '0', '0')");
				// создание медалей
				$mysqli->query("INSERT INTO `players_medals` (`PlayerID`, `Ribbons`, `Badges`, `Medals`, `MasterMedals`) VALUES ('". $getPlayer['PlayerID'] ."', '0', '0', '0', '0')");
				// создание квеста
				$mysqli->query("INSERT INTO `players_quests` (`PlayerID`, `MissionID`, `CardID`, `Card1_1`, `Card1_2`, `Card1_3`, `Card1_4`, `Card2_1`, `Card2_2`, `Card2_3`, `Card2_4`, `Card3_1`, `Card3_2`, `Card3_3`, `Card3_4`, `Card4_1`, `Card4_2`, `Card4_3`, `Card4_4`, `Card5_1`, `Card5_2`, `Card5_3`, `Card5_4`, `Card6_1`, `Card6_2`, `Card6_3`, `Card6_4`, `Card7_1`, `Card7_2`, `Card7_3`, `Card7_4`, `Card8_1`, `Card8_2`, `Card8_3`, `Card8_4`, `Card9_1`, `Card9_2`, `Card9_3`, `Card9_4`, `Card10_1`, `Card10_2`, `Card10_3`, `Card10_4`, `LastRewardEXP`, `LastRewardCredits`, `RepeatCount`) VALUES ('". $getPlayer['PlayerID'] ."', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0')");
				// создание статистики
				$mysqli->query("INSERT INTO `players_stats` (`PlayerID`, `Fights`, `Wins`, `Losts`, `Kills`, `Headshots`, `Deaths`, `Escapes`, `SeasonFights`, `SeasonWins`, `SeasonLosts`, `SeasonKills`, `SeasonHeadshots`, `SeasonDeaths`, `SeasonEscapes`) VALUES ('". $getPlayer['PlayerID'] ."', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0')");
				// создание званий(перок)
				$mysqli->query("INSERT INTO `players_titles` (`PlayerID`, `SlotCount`, `TitleEquiped1`, `TitleEquiped2`, `TitleEquiped3`, `titlePos1`, `titlePos2`, `titlePos3`, `titlePos4`, `titlePos5`, `titlePos6`, `title1`, `title2`, `title3`, `title4`, `title5`, `title6`, `title7`, `title8`, `title9`, `title10`, `title11`, `title12`, `title13`, `title14`, `title15`, `title16`, `title17`, `title18`, `title19`, `title20`, `title21`, `title22`, `title23`, `title24`, `title25`, `title26`, `title27`, `title28`, `title29`, `title30`, `title31`, `title32`, `title33`, `title34`, `title35`, `title36`, `title37`, `title38`, `title39`, `title40`, `title41`, `title42`, `title43`, `title44`) VALUES ('". $getPlayer['PlayerID'] ."', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0')");
				
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