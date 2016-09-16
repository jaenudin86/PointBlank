<?php
/*
	Админка
*/
session_start();

$function = $_GET["function"];

include('templates/head.tmpl.php');
include('templates/header.tmpl.php');

if($function == "create_post")
{
	include('templates/admin/create_post.tmpl.php');
}

include('templates/footer.tmpl.php');

?>