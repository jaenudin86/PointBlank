<?php
/*
	Входная точка приложения
*/
session_start();

include("app/config.php");

$page = $_GET["page"];
$page = trim($page);
$page = stripslashes($page);
$page = htmlspecialchars($page);

if(!$page)
{
	header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/index', true, 301 );
}

if($page == "launcher-page"){ include("app/pages/launcher.php"); exit(); }// work

if($page == "index"){ include("app/pages/main.php"); exit(); }// work
if($page == "news"){ include("app/pages/news.php"); exit(); }// work
if($page == "post"){ include("app/pages/post.php"); exit(); }// work
if($page == "guide"){ include("app/pages/guide.php"); exit(); }// work 
if($page == "top"){ include("app/pages/top.php"); exit(); }// work
if($page == "download"){ include("app/pages/download.php"); exit(); }// work
if($page == "media"){ include("app/pages/media.php"); exit(); }// work
if($page == "social"){ include("app/pages/social.php"); exit(); }// work
if($page == "forum/"){ include("app/pages/forum.php"); exit(); }// ?
if($page == "forum"){ include("app/pages/forum.php"); exit(); }// ?
if($page == "support"){ include("app/pages/support.php"); exit(); }
if($page == "register"){ include("app/pages/register.php"); exit(); }// work
if($page == "login"){ include("app/pages/login.php"); exit(); }
if($page == "logout"){ include("app/pages/logout.php"); exit(); }
if($page == "profile"){ include("app/pages/profile.php"); exit(); }
if($page == "ref"){ include("app/pages/ref.php"); exit(); }

if($page == "admin"){ include("app/pages/admin.php"); exit(); }// ?

if($page == "404"){ include("app/pages/404.php"); exit(); }// work
if($page == "error"){ include("app/pages/error.php"); exit(); }

if($page == "success"){ include("app/pages/success.php"); exit(); }
if($page == "activate"){ include("app/pages/activate.php"); exit(); }

?>