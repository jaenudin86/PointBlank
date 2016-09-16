				<div id="block-views-Banners-block_2" class="block block-views">
					<div id="slider">
						<ul>
							<li>
								<a href="/post?p=first-post">
									<img src="storage/images/slider/1.jpg">
								</a>
							</li>
							<li>
								<a href="/post?p=testers-three">
									<img src="storage/images/slider/2.jpg">
								</a>
							</li>
						</ul>  
					</div>	
				</div>
			</div>
			<div class="container">
					<div id="block-quicktabs-1" class="block block-quicktabs">
						<h3>Новости</h3>
					</div>
					<div class="">
						<div class="news">
							<div class="news__list">
							<!-- POST -->
							<?php
								//SELECT * FROM `news` ORDER BY `id` DESC LIMIT 10;
								$mysqli = mysqli_connect($config['database']['server'], $config['database']['user'], $config['database']['password'], $config['database']['name']);
								$posts = $mysqli->query('SELECT * FROM `news` ORDER BY `id` DESC LIMIT 6;');
								mysqli_close($mysqli);
								
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
								<!-- /POST -->
							</div>
							<a href="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>/news" class="material_more_link">Все новости</a>
						</div>
					</div>
					<div id="block-block-19" class="block block-block">
					<div class="banners_home">
						<div class="banners_home__item">
							<a class="banners_home__pic" href="/top"><img alt="" src="storage/images/quick/banner1.png"></a> <a class="banners_home__title" href="/top">Рейтинги</a>
							<p>Рейтинг игроков по рангу.<br>
							Посмотри их все!</p>
						</div>
						<div class="banners_home__item">
							<a class="banners_home__pic" href="/guide"><img alt="" src="storage/images/quick/banner2.png"></a> <a class="banners_home__title" href="/guide">Первые шаги</a>
							<p>Смотри обучающее видео<br>
							и смело приступай к игре.</p>
						</div>
						<div class="banners_home__item">
							<a class="banners_home__pic" href="/ref"><img alt="" src="storage/images/quick/banner3.png"></a> <a class="banners_home__title" href="/ref">Приводи друзей</a>
							<p>За достижения каждого<br>
							приглашенного соратника ты<br>
							будешь получать подарки!</p>
						</div>
					</div>
				</div>
			</div>