<?php
/*
	Вывод новостей
*/

$p = $_GET["p"];// страница
$p = trim($p);
$p = stripslashes($p);
$p = htmlspecialchars($p);
$p = preg_replace("/[^0-9]/", '', $p);

if(strlen($p) > 3)
{
	?>
	<meta http-equiv="refresh" content="0;URL=<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/news" />
	<?
}

$d = $p - 1;

if($p)
{
	$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
	$posts = $mysqli->query('SELECT * FROM `news` ORDER BY `id` DESC LIMIT '. $d .'0, '. $p .'0;');
	mysqli_close($mysqli);
}
else
{
	$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
	$posts = $mysqli->query('SELECT * FROM `news` ORDER BY `id` DESC LIMIT 10;');
	mysqli_close($mysqli);
}

?>
</div>
	<div class="container">
		<div class="news__list__head">
			<h1 class="heading">Новости</h1>
		</div>
		<div class="">
			<div class="news">
				<div class="news__list">
				<?
					while ($post = $posts->fetch_assoc())
					{	
						?>
						<div class="news__list__item">
							<a href="post?p=<?php echo $post["url"]; ?>">
								<?
									if($post["type"] == "news")
									{
										?>
										<div class="news__picture fleft img_63">
											<img src="<?php echo $post["image"]; ?>" />
											<span class="news__picture__label common news_label_63">Общее</span>
										</div>
										<?
									}
									if($post["type"] == "important")
									{
										?>
										<div class="news__picture fleft img_25">
										<img src="<?php echo $post["image"]; ?>" />
											<span class="news__picture__label important news_label_25">Важное</span>
										</div>
										<?
									}
									if($post["type"] == "ivent")
									{
										?>
										<div class="news__picture fleft img_25">
										<img src="<?php echo $post["image"]; ?>" />
											<span class="news__picture__label contest news_label_15">Ивент</span>
										</div>
										<?
									}
								?>
							</a>
							<div class="news__content fright">
								<p>
									<a href="post?p=<?php echo $post["url"]; ?>" class="news__title"><?php echo $post["title"]; ?></a>
								</p>
								<p class="news__lead"><?php echo $post["cat"]; ?></p>
								<div class="news__info">
									<span class="news__info__date fleft"><?php echo $post["date"]; ?></span>
									<a href="post?p=<?php echo $post["url"]; ?>" class="news__info__readmore fright">Подробнее</a>
								</div>
							</div>
						</div>
						<?
					}
				?>
				<div class="item-list">
					<ul class="pager">
						<li class="pager-next">
						<?php
							if($p)
							{
								$next = $p + 1;
								$past = $p - 1;
							}
							else
							{
								$next = 2;
								$past = null;
							}
						?>
							<?
								if($past != null)
								{
									?>
										<a href="/news?p=<?php echo $past; ?>" title="На следующую страницу" class="active">‹ предыдущая</a>
									<?
								}
							?>
							<a href="/news?p=<?php echo $next; ?>" title="На следующую страницу" class="active">следующая ›</a>
						</li>
					</ul>
				</div>
				</div>
			</div>
		</div>				
	</div>
