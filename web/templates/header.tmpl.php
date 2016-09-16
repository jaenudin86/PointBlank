<body>
	<div class="layout node">
		<div class="layout__wrapper">
			
			<header class="header">
				<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/index" class="header__logo vulcano"></a>
				<div class="header__buttons">
				<?php
				
					if(!$_SESSION['login'])
					{
						?>
							<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/register" class="btn btn_type1 btn_reg js-btn_reg">регистрация</a>
							<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/download" class="btn btn_type2 btn_dwld js-btn_dwld">скачать игру</a>
						<?
					}
					else
					{
						?>
							<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/donate" class="btn btn_type1 btn_reg js-btn_reg">пополнить счёт</a>
							<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/forum" class="btn btn_type2 btn_dwld js-btn_dwld">форум</a>
						<?
					}
				
				?>
				</div>
			</header>
			
			<div class="layout__shadow">
				<div class="layout__top">
					<nav class="navigation fleft">
						<ul class="navigation__list">
							<li class="navigation__item">
								<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/news" class="navigation__item__mainlink">Новости</a>
							</li>
							<li class="navigation__item">
								<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/guide" class="navigation__item__mainlink">Об игре</a>
							</li>
							<li class="navigation__item">
								<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/top" class="navigation__item__mainlink">Рейтинги</a>
							</li>
							<li class="navigation__item">
								<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/download" class="navigation__item__mainlink">Скачать</a>
							</li>
							<li class="navigation__item">
								<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/media" class="navigation__item__mainlink">Медиа</a>
							</li>
							<li class="navigation__item navigation__item-parent">
								<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/forum" class="navigation__item__mainlink">Общение</a>
								<ul class="navigation__sublist">
									<li class="navigation__sublist__item">
										<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/forum" >Форум</a>
									</li>
									<li class="navigation__sublist__item">
										<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/social">Социальные сети</a>
									</li>
								</ul>
							</li>
							<li class="navigation__item">
								<a href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/support" class="navigation__item__mainlink">Помощь</a>
							</li>
						</ul>
					</nav>
					<div class="auth fright" id="block-user-1">
					<?php
					if(!$_SESSION['login'])
					{
						?>
						<!-- Неавторизован -->
						<div class="auth__account ">
							<a class="auth__login tcenter" href="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/login">Войти</a>
						</div>
						<?
					}
					else
					{
						?>
						<!-- Авторизован -->
						<div class="auth__account">
							<div class="auth-login js-auth-login">
								<div class="auth__user_pic">
									<a href="/profile" title="<?php echo $_SESSION['login']; ?>">
										<img src="<?php echo $config["main"]["protocol"]; ?><?php echo $config["main"]["url"]; ?>/storage/images/avatar.jpg" width="53" alt="">
									</a>
								</div>
								<div class="auth__user_name"><a href="/profile" title="<?php echo $_SESSION['login']; ?>"><span class="auth__title"><?php echo $_SESSION['login']; ?></span></a></div>
							</div>
							<div class="auth__dropdown js-auth_dropdown" style="">
								<ul class="auth__nav">
									<li class="auth__nav__item">
										<a href="/profile">Личный кабинет</a>
									</li>
									<li class="auth__nav__item">
										<a href="/friends">Пригласить друга</a>
									</li>
									<li class="auth__nav__item">
										<a href="/logout">Выйти</a>
									</li>	
								</ul>
							</div>
						</div>
						<!-- /Авторизован -->
						<?
					}
					?>
					</div>