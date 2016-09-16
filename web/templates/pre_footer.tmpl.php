<div class="clear"></div>
<div class="socials js-socials-main">
	<?
	if($_SESSION['login'])
	{
		?>
			<div class="title">присоединяйся</div>
			<div class="socials__list">
				<a href="https://vk.com/oznetwork" class="socials__item vk" target="_blank">ВКонтакте</a>
				<a href="https://twitter.com/pointblankpw" class="socials__item tw" target="_blank">Twitter</a>
			</div>
		<?
	}
	else
	{
		?>
		<a href="/register" class="btn btn_type1 btn_reg">Зарегистрироваться</a>
		<?
	}
	?>
</div>