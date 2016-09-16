<?php
/*
	Пост в блоге
*/
session_start();

$postUrl = $_GET["p"];
$postUrl = trim($postUrl);
$postUrl = stripslashes($postUrl);
$postUrl = htmlspecialchars($postUrl);

if(!$postUrl)
{
	header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/index', true, 301 );
}
else
{
	$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
	$post = $mysqli->query('SELECT * FROM `news` WHERE `url`="'. $postUrl .'";');
	$post = $post->fetch_array();
	mysqli_close($mysqli);
	
	if(!$post['title'])
	{
		header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/index', true, 301 );
	}
}

include('templates/head.tmpl.php');
include('templates/header.tmpl.php');
include('templates/post.tmpl.php');
include('templates/right_block.tmpl.php');
include('templates/pre_footer.tmpl.php');
include('templates/footer.tmpl.php');
?>