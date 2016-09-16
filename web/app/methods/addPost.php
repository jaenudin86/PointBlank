<?php
/*
	Добавление поста
*/
include("../config.php");

if($_POST["title"] != null & $_POST["url"] != null & $_POST["cat"] != null & $_POST["image"] != null & $_POST["type"] != null & $_POST["post"] != null)
{
	$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
	$mysqli->query("INSERT INTO `news`(`id`, `url`, `title`, `cat`, `post`, `image`, `type`, `date`) VALUES ('','". $_POST['url'] ."','". $_POST['title'] ."','". $_POST['cat'] ."','". $_POST['post'] ."','". $_POST['image'] ."','". $_POST['type'] ."','". date('j').".". date('m').".". date('Y') ."');");
	
	header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/post?p='. $_POST['url'] , true, 301 );
}

?>