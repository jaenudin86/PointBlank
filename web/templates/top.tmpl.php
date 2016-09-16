<?php
$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
$players = $mysqli->query("SELECT * FROM `players` ORDER BY `Exp` DESC LIMIT 50;");
?>
</div>
<div class="container">
	<div class="innerpage" id="textcontent">
		<div id="node" class="node">
			<div id="mr_block_top">
				<div class="heading">Рейтинги</div>
				<div class="ladders rank">
					<div class="ladder_content" style="display: block;">
						<ul>
						<?php
						$place = 1;
						while ($player = $players->fetch_assoc())
						{
							?>
							<li class="rank__row rank_<?php echo $place; ?>">
								<div class="position fleft"><?php echo $place; ?></div>
								<div class="icon_rank fleft">
									<img src="storage/images/ranks/<?php echo $player['Rank']; ?>.gif" style="vertical-align: -11%;">
								</div>
								<div class="nickname fleft"><?php echo $player['Name']; ?></div>
								<?
								
									if($player['ClanID'] == 0)
									{
										?>
										<div class="clanname fleft">Не состоит в клане</div>
										<?
									}
									else
									{
										$clan = $mysqli->query("SELECT * FROM `clans` WHERE `ClanID`='". $player['ClanID'] ."'");
										$clan = $clan->fetch_array();
										?>
										<div class="clanname fleft"><?php echo $clan["Name"]; ?></div>
										<?
									}
								
								?>
							</li>
							<?
						$place++;
						}
						?>
						</ul>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<?php mysqli_close($mysqli); ?>