<?php
$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
$threads = $mysqli->query("SELECT * FROM `forum_threads` WHERE `forumID`='". $forumID ."' ORDER BY `id` DESC LIMIT 10;");

$threadInfo = $mysqli->query("SELECT * FROM `forum_list` WHERE `id`=". $forumID);
$threadInfo = $threadInfo->fetch_array();

?>
</div>
	<div class="container">
		<div id="block-quicktabs-1" class="block block-quicktabs">
		<h6><a href="/forum">Форум</a> > <? echo $threadInfo['title']; ?></h6>
			<div style="float:right;" class="friends__modal__buttons">
				<a href="/forum?act=create" class="btn btn_profile iblock">Создать тему</a> 
			</div>
		</div>
		<table border="1">
			<tbody>
				<tr>
					<td>Название темы</td>
					<td style="text-align: right;width: 260px;">Последнее сообщение</td>
				</tr>
				<!-- thread -->
				<?
				while ($thread = $threads->fetch_assoc())
				{
					
					$creator = $mysqli->query("SELECT * FROM `players` WHERE `AccountID`='". $thread['authorID'] ."';");
					$creator = $creator->fetch_array();
					
					$last_message_thread = $mysqli->query("SELECT * FROM `forum_replyes` WHERE `threadID`='". $thread['id'] ."' ORDER BY `id` DESC LIMIT 1;");
					$last_message_thread = $last_message_thread->fetch_array();
					
					$last_message_creator = $mysqli->query("SELECT * FROM `players` WHERE `AccountID`='". $last_message_thread['authorID'] ."';");
					$last_message_creator = $last_message_creator->fetch_array();
					
					?>
					<tr>
						<td>
							<a style="vertical-align: 150%;" href="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>"><h6><? echo $thread["title"]; ?></h6></a>
							<p style="margin-top: -22px;"><a href="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>/profile?id=<? echo $creator['AccountID']; ?>"><? echo $creator['Name']; ?></a>, <? echo $thread["date"]; ?></p>
						</td>
						<td style="text-align: right;width: 260px;">От: <a href="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>/profile?id=<? echo $last_message_creator['AccountID']; ?>"><?php echo $last_message_creator['Name']; ?></a>
						<br>Когда: <a href=""><? echo $last_message_thread['date']; ?></a>
						</td>
					</tr>
					<?
				}
				?>
				<!-- /thread -->
			</tbody>
		</table>
	</div>
<?php mysqli_close($mysqli); ?>