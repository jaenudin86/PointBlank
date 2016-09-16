<?php
/*
	forum
*/
session_start();

$forumID = $_GET["f"];
$threadID = $_GET["t"];
$act = $_GET["act"];

include('templates/head.tmpl.php');
include('templates/header.tmpl.php');

if($forumID == null & $threadID == null & $act == null)
{
	include('templates/forum_list.tmpl.php');
}
if($forumID != null)
{
	include('templates/forum_threads.tmpl.php');
}
if($threadID != null)
{
	include('templates/forum_thread.tmpl.php');
}
if($act == 'create')
{
	include('templates/forum_create_thread.tmpl.php');
}

include('templates/right_block.tmpl.php');
include('templates/pre_footer.tmpl.php');
include('templates/footer.tmpl.php');

?>