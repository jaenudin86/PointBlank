</div>
	<div class="container">
		<div id="block-quicktabs-1" class="block block-quicktabs">
			<h3>Авторизация</h3>
		</div>
		<div class="form">
			<div class="tab-content">
				<div id="login">
					<form action="app/methods/loginAccount.php" method="post">
						<div class="field-wrap">
							<label>Ваш логин*</label>
							<input name="login" type="text" style="width:270px;" />
						</div>
						<br>
						<div class="field-wrap">
							<label>Ваш пароль*</label>
							<input name="password" type="password" style="width:270px;" />
						</div>
						<br>
						<div class="field-wrap">
							<label>Код с картинки*</label>
							<br>
							<img src="app/utils/captcha.php" alt="защитный код">
							<br>
							<input name="captcha" type="text" style="width:270px;" />
						</div>
						<br>
						<button class="btn btn_type2 btn_dwld js-btn_dwld"/>Вход</button>
					</form>
				</div>
			</div>
		</div>
	</div>