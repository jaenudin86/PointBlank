<?php

if($page == "index"){ $pageTitle = "Главная"; }
if($page == "news"){ $pageTitle = "Новости"; }
if($page == "post"){ $pageTitle = $post["title"]; }
if($page == "guide"){ $pageTitle = "Как начать играть"; }
if($page == "top"){ $pageTitle = "Топ игроков"; }
if($page == "download"){ $pageTitle = "Скачать"; }
if($page == "media"){ $pageTitle = "Медиа"; }
if($page == "social"){ $pageTitle = "Социальные сети"; }
if($page == "forum"){ $pageTitle = $forumTitle; }
if($page == "support"){ $pageTitle = "Помощь"; }
if($page == "register"){ $pageTitle = "Регистрация"; }
if($page == "login"){ $pageTitle = "Авторизация";}
if($page == "profile"){ $pageTitle = "Мой профиль"; }
if($page == "ref"){ $pageTitle = "Реферальная система"; }

if($page == "admin"){ $pageTitle = "Админ-панель"; }

if($page == "404"){ $pageTitle = "Ошибка"; }

if($page == "forum"){ $pageTitle = "Форум"; }
if($page == "forum/"){ $pageTitle = "Форум"; }

?>
<!doctype html>
<html xml:lang="ru">
<meta http-equiv="content-type" content="text/html;charset=utf-8" />

<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title><?php echo $pageTitle; ?> | <?php echo $config["main"]["name"]; ?></title>
	<meta name="description" content="Point Blank Private Server">
	<meta name="keywords" content="Point,Blank,PB,Private,Server,Приватный,Сервер,Пиратский,Поинт,Бланк">
	
	<link rel="image_src" href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/storage/images/static/social_img_wide.png">
	<meta property="og:image" content="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/storage/images/static/social_img_wide.png" />
	<meta property="og:title" content="Point Blank | Private Server">
	<meta property="og:description" content="First Private Server Point Blank - PointBlank.PW">
	<meta property="og:type" content="game">
	<meta property="og:url" content="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>">
	
	<link type="text/css" rel="stylesheet" media="all" href="/storage/css/style.css" />
	<link rel="stylesheet" href="/storage/css/slider.css">
	
	<script src="http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
	
	<script type="text/javascript" src="storage/js/slider.js"></script>
	<script type="text/javascript" src="storage/js/main.js"></script>
	
</head>