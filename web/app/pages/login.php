<?php
/*
	login page
*/
session_start();

if($_SESSION['login'] != null)
{
	header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/profile', true, 301 );
	exit();
}

include('templates/head.tmpl.php');
include('templates/header.tmpl.php');
include('templates/login.tmpl.php');
include('templates/right_block.tmpl.php');
include('templates/pre_footer.tmpl.php');
include('templates/footer.tmpl.php');

?>