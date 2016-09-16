<?php
session_start();
$_SESSION['login'] = null;
header( 'Location: '. $config["main"]["protocol"] .''. $config["main"]["url"] .'/index', true, 301 );
?>