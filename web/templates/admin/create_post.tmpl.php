</div>
<div class="container">
	<div class="innerpage" id="textcontent">
		<div id="node" class="textpage">
			<h1 class="textpage__main_header">Новый пост</h1>
			<div class="js-module">
				<p>Обычная строка пишется через тег &lt;p&gt;</p>
				<p>Ссылка с помощью &lt;a href="#"&gt; &lt;/a&gt; </p>
				<p>Жирный текстс помощью &lt;strong&gt; &lt;/strong&gt; </p>
				<p>Заголовок с помощью &lt;h4&gt;</p>
				<p>Пункт с помощью &lt;span style="font-size:16px;"&gt;&lt;strong&gt;1. Пункт&lt;/strong&gt;&lt;/span&gt; </p>
				<p>Перечисление с помощью &lt;ul&gt; &lt;li&gt;пункт&lt;/li&gt; &lt;/ul&gt; </p>
				<p>Разделяющая черта с помощью &lt;hr&gt; </p>
				<hr>
				<form action="app/methods/addPost.php" method="post">
					<h2>Создание поста</h2>
					<label>Название поста</label>
					<input name="title" type="text" style="width:725px; height:25px;"/>
					<br>
					<br>
					<label>ЧПУ URL</label>
					<input name="url" type="text" style="width:725px; height:25px;"/>
					<br>
					<br>
					<label>Короткое описание поста(кат)</label>
					<input name="cat" type="text" style="width:725px; height:25px;"/>
					<br>
					<br>
					<label>Ссылка на превью-изображение</label>
					<input name="image" type="text" style="width:725px; height:25px;"/>
					<br>
					<br>
					<label>Тип поста</label>
					<br>
					<select size="3" multiple name="type" style="width:300px; height:100px;">
						<option selected value="news">Общее</option>
						<option value="ivent">Ивент</option>
						<option value="important">Важное</option>
					</select>
					<br>
					<br>
					<label>Текст поста</label>
					<div id="editor">
						<textarea id="input" name="post" rows="30" cols="101"></textarea>
					</div>
				
					<hr>
					<h2>Превью</h2>
				
					<div id="preview" class="wmd-preview">
					</div>
				
				
					<!-- jQuery and Syntax Highlight -->
					<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
					<script type="text/javascript" src="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>/storage/js/jquery.syntaxhighlighter.min.js"></script>

					<!-- WMD -->
					<script type="text/javascript" src="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>/storage/js/showdown.js"></script>
					<script type="text/javascript" src="<? echo $config["main"]["protocol"]; ?><? echo $config["main"]["url"]; ?>/storage/js/wmd.js"></script>
    
    
					<!-- jQuery listener for syntax highlight -->
					<script type="text/javascript">
					$(document).ready(function() {
					$("#highlightCode").click(function(){
						$.SyntaxHighlighter.init({
						'lineNumbers': false,
						'debug': true
						});
					});
					new WMD("input", "toolbar", { preview: "preview" });
					});
					</script>
					<button class="btn btn_type2 btn_dwld js-btn_dwld"/>Опубликовать</button>
				</form>
				
				<!--
				<div class="color_block block_thesis_orange_text rtecenter">
					<a href="https://pvp.mail.ru/tournament/126/">
						<span style="color:#ffffff;">Кнопка</span>
					</a>
				</div>-->
			</div>
		</div>
	</div>
</div>
</div>