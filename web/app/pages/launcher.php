<?php
/* Статус серверов */
$ApiStatus = @fsockopen ($config["launcher"]["api_server"],$config["launcher"]["api_port"],$errno, $errstr, 1);
$LoginStatus = @fsockopen ($config["launcher"]["login_server"],$config["launcher"]["login_port"],$errno, $errstr, 1);
$GameStatus = @fsockopen ($config["launcher"]["game_server"],$config["launcher"]["game_port"],$errno, $errstr, 1);
$BattleStatus = @fsockopen ($config["launcher"]["battle_server"],$config["launcher"]["battle_port"],$errno, $errstr, 1);

//SELECT * FROM `news` ORDER BY `id` DESC LIMIT 10;
$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
$posts = $mysqli->query('SELECT * FROM `news` ORDER BY `id` DESC LIMIT 4;');
mysqli_close($mysqli);

?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
<link rel="shortcut icon" href="/media/images/favicon.ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1251" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
	<script type="text/javascript" src="storage/js/jquery.js?v=17"></script>
    <script type="text/javascript" src="storage/js/jquery.idtabs.min.js?v=18"></script>
	<link rel="stylesheet" type="text/css" href="storage/css/launcher/style.css?v=63" />
<style>
.hide_class{
  display: none;
  visibility: hidden;
}
</style>
	<base target="_blank" />
    <title>Новости</title>




</head>
<body>
<div id="bg">
<div id="banner_prof" class="hide_class"></div>

	<div id="news-title"><a href="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>/news">НОВОСТИ</a></div>
	<div id="news-gradient"></div>
	<ul id="news">
	<?
	while ($post = $posts->fetch_assoc())
	{
		?>
		<li>
			<div class="date"><? echo $post["date"]; ?></div>
			<div class="content">
				<a href="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>/post?p=<?php echo $post["url"]; ?>"><? echo $post["title"]; ?></a>
			</div>
		</li>
		<?
	}
	?>
	</ul>
<a id="donate" href="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>/donate"></a>


<div id="server_status">
	<span style="color:#FFFFFF;font-family:Arial;font-size:12px;">API Server: </span>
	<?
	if($ApiStatus)
		{
			?>
			<span style="color:#00FF7F;font-family:Arial;font-size:12px;">ON</span>
			<?
		}
		else
		{
			?>
			<span style="color:#FF0000;font-family:Arial;font-size:12px;">OFF</span>
			<?
		}?>
		<span style="color:#FFFFFF;font-family:Arial;font-size:12px;">&nbsp;&nbsp; Login Server: </span>
		<?
		if($LoginStatus)
		{
			?>
			<span style="color:#00FF7F;font-family:Arial;font-size:12px;">ON</span>
			<?
		}
		else
		{
			?>
			<span style="color:#FF0000;font-family:Arial;font-size:12px;">OFF</span>
			<?
		}
		?>
		<span style="color:#FFFFFF;font-family:Arial;font-size:12px;">&nbsp;&nbsp; GameServer: </span>
		<?
		if($GameStatus)
		{
			?>
			<span style="color:#00FF7F;font-family:Arial;font-size:12px;">ON</span>
			<?
		}
		else
		{
			?>
			<span style="color:#FF0000;font-family:Arial;font-size:12px;">OFF</span>
			<?
		}
		?>
		<span style="color:#FFFFFF;font-family:Arial;font-size:12px;">&nbsp;&nbsp; BattleServer: </span>
		<?
		if($BattleStatus)
		{
			?>
			<span style="color:#00FF7F;font-family:Arial;font-size:12px;">ON</span>
			<?
		}
		else
		{
			?>
			<span style="color:#FF0000;font-family:Arial;font-size:12px;">OFF</span>
			<?
		}?>
	</div>



</body>
</html>