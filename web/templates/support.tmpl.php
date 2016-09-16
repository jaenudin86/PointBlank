</div>
	<div class="container">
		<div class="news__list__head">
			<h1 class="heading">Задать вопрос</h1>
		</div>
		<div class="form">
			<div class="tab-content">
				<div id="support">
					<form action="app/methods/addQuestion.php" method="post">
						<div class="field-wrap">
							<label>Ваш E-Mail</label>
							<input name="mail" type="text" style="width:270px;" />
						</div>
						<br>
						<div class="field-wrap">
							<label>Опишите вашу проблему</label>
							<textarea id="input" name="question" rows="30" style="height: 300px;" cols="101"></textarea>
						</div>
						<br>
						<div class="field-wrap">
							<label>Код с картинки</label>
							<br>
							<img src="app/utils/captcha.php" alt="защитный код">
							<br>
							<input name="captcha" type="text" style="width:270px;" />
						</div>
						<br>
						<button class="btn btn_type2 btn_dwld js-btn_dwld"/>Задать вопрос</button>
					</form>
				</div>
			</div>
		</div>
	</div>