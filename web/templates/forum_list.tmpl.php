<?php
$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
$forums = $mysqli->query('SELECT * FROM `forum_list`;');
?>
</div>
<div class="container">
	<div id="block-quicktabs-1" class="block block-quicktabs">
		<h3>Форум</h3>
	</div>
	<table border="1">
		<tbody>
			<tr>
				<td>Название раздела</td>
				<td style="text-align: right;">Последнее сообщение</td>
			</tr>
		<?php
		while ($forum = $forums->fetch_assoc())
		{
			?>
			<tr>
				<td>
					<img src="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/storage/images/forum/<?php echo $forum['url'] ?>.png"/>
					<a style="vertical-align: 150%;" href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/forum?f=<?php echo $forum['id'] ?>"><h6><?php echo $forum['title'] ?></h6></a>
					<p style="margin-top: -24px; margin-left: 50px;"><?php echo $forum['info'] ?></p>
				</td>
				
				<?
					$last_message = $mysqli->query('SELECT * FROM `forum_last_messages` WHERE `forumID`="'. $forum["id"] .'";');
					$last_message = $last_message->fetch_array();
					
					if(!$last_message['threadID'])
					{
						?>
						<td style="text-align: right;width: 260px;">Нет сообщений</td>
						<?
					}
					else
					{
						$getThread = $mysqli->query("SELECT * FROM `forum_threads` WHERE `id`='". $last_message['threadID'] ."'");
						$getThread = $getThread->fetch_array();
						
						$last_user = $mysqli->query('SELECT * FROM `players` WHERE `AccountID`="'. $getThread["authorID"] .'";');
						$last_user = $last_user->fetch_array();
						
						?>
						<td style="text-align: right;width: 260px;">Последнее: <a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/forum?t=<?php echo $getThread['id'] ?>"><?php echo $getThread["title"]; ?></a>
							<br>От: <a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/profile?id=<?php echo $last_user['AccountID']; ?>"><? echo $last_user['Name']; ?></a>
							<br>Когда: <a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/forum?t=<?php echo $getThread['id'] ?>"><?php echo $getThread["date"]; ?></a>
						</td>
						<?
					}
				?>
			</tr>
			<?
		}
		?>
		</tbody>
	</table>
</div>
<? mysqli_close($mysqli); ?>