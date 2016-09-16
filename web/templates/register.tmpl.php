</div>
	<div class="container">
		<div id="block-quicktabs-1" class="block block-quicktabs">
			<h3>Регистрация</h3>
		</div>
		<div class="form">
			<div class="tab-content">
				<div id="login">
					<a>Поля, помеченные звездочкой(*) обязательны к заполнению</a>
					<br>
					<br>
					<form action="app/methods/createAccount.php" method="post">
						<div class="field-wrap">
							<label>Ваш e-mail адрес<a>*</a></label>
							<input name="mail" type="text" style="width:270px;"/>
						</div>
						<br>
						<div class="field-wrap">
							<label>Ваш логин<a>*</a></label>
							<input name="login" type="text" style="width:270px;" />
						</div>
						<br>
						<div class="field-wrap">
							<label>Ваш никнейм на сервере<a>*</a></label>
							<input name="nickname" type="text" style="width:270px;" />
						</div>
						<br>
						<div class="field-wrap">
							<label>Ваш пароль<a>*</a></label>
							<input name="password" type="password" style="width:270px;" />
						</div>
						<br>
						<div class="field-wrap">
							<label>Реферальный код (необязательно)</label>
							<input name="referal" type="text" style="width:270px;" />
						</div>
						<br>
						<div class="field-wrap">
							<label>Код с картинки<a>*</a></label>
							<br>
							<img src="app/utils/captcha.php" alt="защитный код">
							<br>
							<input name="captcha" type="text" style="width:270px;" />
						</div>
						<br>
						<button class="btn btn_type2 btn_dwld js-btn_dwld"/>Регистрация</button>
					</form>
				</div>
			</div>
		</div>
	</div>