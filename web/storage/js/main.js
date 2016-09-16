// Billing
var active = "payments";
function setactive(paymentType){
	if (active == paymentType){
		return true;
	}
	else if(paymentType == 'sms'){
		$('.payments').hide();
		$('.sms').show();
	}
	else{
		$('.sms').hide();
		$('.payments').show();
	}
	active = paymentType;
	return true;
}

function clickTab(e) {

	var profileNavItem = $('.profile__tabs__item'),
			billingType = $('.js_billing_type'),
			type = $(e).data('tab');


	if (!$(e).hasClass('active')){
		profileNavItem.removeClass('active');
		billingType.hide();

		$(e).addClass('active')
		setactive(type)

		return false;
	}
}

// Dynamic init selectbox
function initSelectbox(){
	$('.js-selectbox').selectBox().bind('open', function(e){
		if ($(this).find("option").length > 10) {
			$('.selectBox-dropdown-menu').addClass("simplebar");
			$('.selectBox-dropdown-menu').simplebar();
		}
	});
	$('.js-selectbox').selectBox().bind('close', function(e){
		$('.selectBox-dropdown-menu').removeClass("simplebar");
	});
}

$(function(){

	// Spoiler
	$('spoiler').each(function(index, val) {
		var spoilerTitle = $(this).attr('title'),
				spoilerHtml = $(this).html();

		$(this).replaceWith('<div class="spoiler js-spoiler"><div class="spoiler__title js-spoiler__link">'+spoilerTitle+'</div><div class="spoiler__content js-spoiler__content">'+spoilerHtml+'</div></div>');

		$('a[rel="lightbox[field_image][]"]').on("click", function(e){
			e.preventDefault();
			Lightbox.start(this, false, false, false, false);
		});
	});

	// Spoiler_v2
	$('.spoiler_block').each(function(index, val) {
		var spoilerTitle = $(this).find('.spoiler_title').html(),
				spoilerHtml = $(this).find('.spoiler_content').html();

		$(this).replaceWith('<div class="spoiler js-spoiler"><div class="spoiler__title js-spoiler__link">'+spoilerTitle+'</div><div class="spoiler__content js-spoiler__content">'+spoilerHtml+'</div></div>');

		$('a[rel="lightbox[field_image][]"]').on("click", function(e){
			e.preventDefault();
			Lightbox.start(this, false, false, false, false);
		});
	});

	// Friends service
	$(document).on('click', '.js-friendgift', function(event) {
		console.log(11);
		$(this).toggleClass('active');
		$(this).parent().find('.js-friendgift_list').slideToggle(300, function() {
			//Stuff to do *after* the animation takes place
		})
	});

});

$(document).on('click', '.js-spoiler__link', function(event) {
	event.preventDefault();
	$(this).parents('.js-spoiler').find('.js-spoiler__content').toggleClass('spoil_opened');
	$(this).parents('.js-spoiler').toggleClass('opened');
});

// Login form
$(document).on('click', '.js-auth-login', function(event) {
	event.preventDefault();
	$(this).find('.auth__title').toggleClass('opened');
	$('.js-auth_dropdown').stop().slideToggle('fast');
});
$(document).on('click', function(e) {

	if ($(e.target).hasClass('auth__account') || $(e.target).parents().hasClass('auth__account') ) {
		return true;
	} else {
		$('.js-auth-login').find('.auth__title').removeClass('opened');
		$('.js-auth_dropdown').stop().slideUp('fast');
	}
});
// ------

// ------
function set_calendar_link(obj) {
		var new_link = $(obj).val();
		$(obj).next().attr('href', new_link);
}


// Social page screens
// $('.js-social-page').ready(function() {

// 	$(document).on('mouseover', '.js-social-item', function() {
// 		var screenId = $(this).data('screen'),
// 				link = $(this).attr('href');

// 		$(this).addClass('active').siblings().removeClass('active');
// 		$('.js-social-screens').find(screenId).addClass('active').siblings().removeClass('active');
// 		$('.js-socialpage_link').attr('href', link);
// 	});

// });
// ------

// Mainpage top slider
$('.js-promo').ready(function() {
	var mySwiper = new Swiper ('.js-promo', {
		loop: true,
		pagination: '.swiper-pagination',
		paginationClickable: true,
		spaceBetween: 10,
		autoplay: 4000,
		// autoplayDisableOnInteraction: false
	})
});
// ------

// Mainpage slideshow
$('.js-home-slideshow').ready(function() {
	if ($('.js-home-slideshow').length > 0) {
		var mediaData,
				mediaHome,
				mediaType,
				mediaSettings;

		$.ajax({
			url: '/dynamic/media/?a=mainjson',
			type: 'GET',
			async: false,
			cache: false,
			dataType: 'json',
		})
		.done(function(data) {
			mediaData = data;
		});

		mediaSettings = {
			loop: true,
			autoplay: 4000,
			width: 747,
			spaceBetween: 10,
			autoplayDisableOnInteraction: false,
			keyboardControl: true,
		},

		mediaHome = new Swiper ('.js-home-slideshow', mediaSettings);


		$('.js-media-tab').on('click', function(e) {
			e.preventDefault();

			var mediaItem = '';
			mediaType = $(this).data('type');

			removeVideoSlide();
			$(this).addClass('active').siblings().removeClass('active');

			mediaHome.removeAllSlides();
			$.each(mediaData[mediaType], function(index, val) {
				if(mediaType == 'video'){
					mediaItem += '<div class="swiper-slide js-mainvideo" data-video="'+this.video+'"><img src="http://img.youtube.com/vi/'+this.video+'/maxresdefault.jpg" width="748" height="420"><div class="icon-play"></div></div>';
				} else{
					mediaItem += '<div class="swiper-slide"><img src="'+this.big_image+'" width="748" height="420"></div>';
				}
			});

			mediaHome.appendSlide(mediaItem);
			mediaHome.startAutoplay();
		});

		$('.js-media-tab').eq(0).click();

		$('.js-home-slideshow').parent().append('<span class="swiper-button-prev js-prevslide"></span> <span class="swiper-button-next js-nextslide"></span>');

		$(document).on('click', '.js-prevslide', function(event) {
			event.preventDefault();
			mediaHome.slidePrev();
			if(mediaType == 'video'){
				removeVideoSlide();
				mediaHome.startAutoplay();
			}
		});

		$(document).on('click', '.js-nextslide', function(event) {
			event.preventDefault();
			mediaHome.slideNext();
			if(mediaType == 'video'){
				removeVideoSlide();
				mediaHome.startAutoplay();
			}
		});

		$(document).on('click', '.js-mainvideo', function(event) {
			var videoId = $(this).data('video');
			$(this).addClass('video-during').html('<iframe width="748" height="421" src="https://www.youtube.com/embed/'+videoId+'?rel=0&autoplay=1" frameborder="0" allowfullscreen></iframe>');
			mediaHome.stopAutoplay();
		})
	};
});


removeVideoSlide = function() {
	if ($('.video-during').length > 0) {
		$('.video-during').html('<img src="http://img.youtube.com/vi/'+$('.video-during').data('video')+'/maxresdefault.jpg" width="748" height="420"><div class="fotorama__video-play"></div>').removeClass('video-during');
	}
}
// ------

// Popup
$(document).on('click', '.js-popup-close', function(event) {
	event.preventDefault();
	$(this).parents('.js-popup-overlay').fadeOut();
});

$(document).on('click', '.js-popup-overlay', function(e) {
	e.preventDefault();
	if ( $(e.target).hasClass('js-popup-overlay') ){
		$('.js-popup-close').click();
	}
});

$(document).keyup(function(e) {
	if (e.keyCode == 27) {
		$('.js-popup-close').click();
	}
});
// ------


// Giftshop click
$(document).on('click', '.submitgifts', function(){
		var form = $('.js-gifts');

		var data = form.serializeArray();
		$.ajax({
				url: form.attr('action'),
				data: data,
				type: 'POST',
				async: false,
				cache: false,
				dataType: 'text',
				success: function(data) {
						$('#mr_block_giftshop').html(data);
						construct__gifts();
				},
				fail: function() {
				}
		});
		return false;
});
$(document).on('click', '.cancelgifts', function(){
		$.ajax({
				url: $(this).attr('href'),
				data: {},
				type: 'GET',
				async: false,
				cache: false,
				dataType: 'text',
				success: function(data) {
						console.log(data)
						$('#mr_block_giftshop').html(data);
						construct__gifts();
				},
				fail: function() {
				}
		});
		return false;
});

$(document).on('click', '.gifts_list .ref_weapon', function(){
		if ($(this).hasClass("selected")) {
				$(this).removeClass('selected');
				$(this).find("input[name='items[]']").val("");
		} else{
				$(this).addClass('selected');
				$(this).find("input[name='items[]']").val($(this).attr('itemid'));
		};
});

/* click */

$(document).on('click', '.main_gift_block .rank_wrapper .list_1 label', function(){
		if (!$(this).hasClass("disabled")) {
				if ($(this).hasClass("selected")) {
						$(this).removeClass('selected');
				} else{
						$(".main_gift_block .rank_wrapper .list_1 label").removeClass("selected");
						$(this).addClass('selected');
				};
		};
});
$(document).on('click', '.main_gift_block .rank_wrapper .list_2 label', function(){
		if ($(this).hasClass("selected")) {
				//$(this).removeClass('selected');
		} else{
				$(".main_gift_block .rank_wrapper .list_2 label").removeClass("selected");
				$(this).addClass('selected');
		};
});
// ------

// PTS submit
$(document).on('click', '.js-pts_submit', function(){
	var form = $('.js-pts_form');

	var data = form.serializeArray();
	$.ajax({
		url: form.attr('action'),
		data: data,
		type: 'POST',
		async: false,
		cache: false,
		dataType: 'text',
		success: function(data) {
			$('#main__pts').html(data);
		},
		fail: function() {
		}
	});
	return false;
});
// ------

// Friends click
$(document).on('click', '.js-friends_item', function(event) {
	$('.js-friends_item').removeClass('active');
	$('.js-friends_modal').hide();

	$(this).addClass('active');
	$(this).parent().find('.js-friends_modal').show();
});

$(document).on('click', '.js-friends_close', function(event) {
	event.preventDefault();
	$('.js-friends_modal').hide();
	$('.js-friends_item').removeClass('active');
});
// ------

friendsSearch = function(friendsData) {
	var friendsNames = [],
			$searchInput = $(".js-friends_search");

	$searchInput.data('oldVal', $searchInput.val());

	$searchInput.bind("propertychange change click keyup input paste blur focus", function(event){
		if ($searchInput.data('oldVal') != $searchInput.val()) {
			showFriendsResult($searchInput.val());
			$searchInput.data('oldVal', $searchInput.val());
		}
	});

	if (friendsData.friends != false) {
		$.each(friendsData.friends, function(index, val) {
			var friend = friendsData.friends[index].first_name + ' ' + friendsData.friends[index].last_name;
			friendsNames.push(friend);
		});

		$searchInput.autocomplete({
			source: friendsNames,
			select: function( event, ui ) {
			showFriendsResult(ui.item.value);
			}
		});
	}

}

showFriendsResult = function (string) {
	$('.friends__social__item').hide();
	$('.friends__social__item').each(function(){
		if ($(this).data('username').indexOf(string) > -1) {
			$(this).show();
		};
	});
}

// Tooltips
$(document).on('mouseover', '.js-tooltip', function(event) {
	var tooltipText = $(this).data('title'),
			tooltipType = $(this).data('type'),
			tooltipContent;

	switch(tooltipType) {
		case 'text':
			tooltipContent = '<span class="tooltip__content">'+tooltipText+'</span>';
			break;
		case 'image':
			tooltipContent = '<span class="tooltip__content"><img src="'+tooltipText+'" /></span>';
			break;
	}
	$(this).prepend(tooltipContent);
});
$(document).on('mouseout', '.js-tooltip', function(event) {
	$('.tooltip__content').remove();
});
// ------

function checkNYMaraphon(task_index) {
	$.ajax({
		url: '/dynamic/minigames/?g=marathon_ny&a=info',
		type: 'GET',
		async: true,
		dataType: 'json'
	})
	.done(function(data) {
		var response = data;
		$.each(data.tasks, function(index, val) {
			if(val.id == task_index && val.status == 0 && response.token) {
				$.ajax({
					url: '/dynamic/minigames/?g=marathon_ny&a=run_task&task_id='+task_index+'&token='+response.token,
					type: 'GET',
					async: true,
					dataType: 'json'
				})
				.done(function(data) {
				});
			}
		});
	});
};

// Add new year nichosi
// function getNichosi() {
// 	if (getCookie('nichosi') > 0 || !getCookie('nichosi')) {
//
// 		$('body').append('<div class="nichosi"></div>');
// 		$('.nichosi').animate({bottom:0}, 350, function(){
// 			$(document).on('click', '.nichosi', function() {
// 				if (!getCookie('nichosi') && authUser) {
// 					document.cookie = "nichosi=4; expires=Mon, 18 Jan 2016 15:00:00 GMT; domain=wf.mail.ru; path=/";
// 				}
// 				window.location = "https://wf.mail.ru/news/1000654.html";
// 			});
// 		})
//
// 	}
// };
//
// function checkNichosi() {
// 	if ( getCookie('nichosi') && getCookie('nichosi') > 0 ) {
// 		var newCount = getCookie('nichosi') - 1;
// 		document.cookie = "nichosi="+newCount+"; expires=Mon, 18 Jan 2016 15:00:00 GMT; domain=wf.mail.ru; path=/";
// 	}
// };
//
// $(function(){
// 	var rand = 1 - 0.5 + Math.random() * (10 - 1 + 1)
// 	rand = Math.round(rand);
// 	if (rand > 3){
// 		setTimeout(getNichosi, 1000);
// 		checkNichosi();
// 	}
// });

getCookie = function(name) {
	var cookie = " " + document.cookie;
	var search = " " + name + "=";
	var setStr = null;
	var offset = 0;
	var end = 0;
	if (cookie.length > 0) {
		offset = cookie.indexOf(search);
		if (offset != -1) {
			offset += search.length;
			end = cookie.indexOf(";", offset)
			if (end == -1) {
				end = cookie.length;
			}
			setStr = unescape(cookie.substring(offset, end));
		}
	}
	return(setStr);
};


// Remove services from list /billing/payment
$(document).on('click', '.js-service_remove', function(event) {
	var removeLink = $(this).data('link');
	var serviceType = $(this).data('service');
	var params = (serviceType == 'rename')? {'dismiss_char': 1} : {};

	var serviceName = '';


	$('.js-service_name').text('');
	$('.js-service_modal').addClass('off');

	switch (serviceType) {
		case 'rename':
			serviceName = "переименование персонажа";
			break;

		case 'giftshop':
			serviceName = "подарки";
			break;
	}

	$('.js-service_name').text(serviceName);
	$('.js-service_modal').removeClass('off');

	$('.js-close_modal').click(function(e){
		e.preventDefault();
		$('.js-service_name').text('');
		$('.js-service_modal').addClass('off');
	});

	$('.js-service_process').click(function(e){
		e.preventDefault();
		$.ajax({
			url: removeLink,
			type: 'POST',
			dataType: 'json',
			data: params
		})
		.done(function(data) {
			console.log(data);
			if (data.status) {
				window.location.reload();
			}
		});
	});

});


// Dynamic load script
function loadScript ( url, callback ) {
	callback = (typeof callback != 'undefined') ? callback : {};

	$.ajax({
		type: "GET",
		url: url,
		success: callback,
		dataType: "script",
		cache: true
	});
}

$(document).on('click', '.btn_download', function() {
  GMR.showSimpleDownloadHint();
});
