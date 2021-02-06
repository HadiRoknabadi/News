/*
	1. Fixed header
	2. Main slideshow
	3. Site search
	4. Owl Carousel
	5. Video popup
	6. Counter
	7. Contact form
	8. Back to top
	9. Custom
*/


jQuery(function ($) {
	"use strict";


	/* ----------------------------------------------------------- */
	/*  Fixed header
	/* ----------------------------------------------------------- 


	/* ----------------------------------------------------------- */
	/*  Mobile Menu
	/* ----------------------------------------------------------- */
	
	jQuery(".nav.navbar-nav li a").on("click", function () {
		if( jQuery(this).closest('li').hasClass('dropdown') || jQuery(this).closest('li').find('ul').length > 0 ){
			if( jQuery('.navbar-toggle').is(':hidden') || jQuery(this).closest('li').hasClass('mega-dropdown') ){
				location.href = jQuery(this).attr('href');
			}
			return false;
		}
	});
	
	jQuery(".nav.navbar-nav li a").on("click", function () {
		if( (jQuery(this).closest('li').hasClass('dropdown') || jQuery(this).closest('li').find('ul').length > 0) && !jQuery(this).closest('li').hasClass('mega-dropdown') && jQuery('.navbar-toggle').is(':visible') ){
			jQuery(this).closest("li").find("> ul").slideToggle();
			jQuery(this).find('i').toggleClass("fa-angle-down fa-angle-up");
			jQuery(this).toggleClass('opened');
		}
	});

	$('.nav-tabs[data-toggle="tab-hover"] > li > a').hover(function () {
		$(this).tab('show');
	});


	/* ----------------------------------------------------------- */
	/*  Main slideshow
	/* ----------------------------------------------------------- */

	$('#main-slide').carousel({
		pause: true,
		interval: 100000,
	});


	/* ----------------------------------------------------------- */
	/*  Site search
	/* ----------------------------------------------------------- */



	$('.nav-search').on('click', function () {
		$('.search-block').fadeIn(350);
	});

	$('.search-close').on('click', function () {
		$('.search-block').fadeOut(350);
	});



	/* ----------------------------------------------------------- */
	/*  Owl Carousel
	/* ----------------------------------------------------------- */

	//Trending slide

	$(".trending-slide").owlCarousel({

		rtl: true,
		loop: true,
		animateIn: 'fadeIn',
		autoplay: true,
		autoplayTimeout: 3000,
		autoplayHoverPause: true,
		nav: true,
		margin: 30,
		dots: false,
		mouseDrag: false,
		slideSpeed: 500,
		navText: ["<i class='fa fa-angle-right'></i>", "<i class='fa fa-angle-left'></i>"],
		items: 1,
		responsive: {
			0: {
				items: 1
			},
			600: {
				items: 1
			}
		}

	});


	//Featured slide

	$(".featured-slider").owlCarousel({

		rtl: true,
		loop: true,
		autoplay: true,
		autoplayTimeout: 5000,
		autoplayHoverPause: true,
		autoplaySpeed: 800,
		margin: 0,
		dots: false,
		mouseDrag: true,
		touchDrag: true,
		slideSpeed: 500,
		nav: true,
		navText: ["<i class='fa fa-angle-right'></i>", "<i class='fa fa-angle-left'></i>"],
		navSpeed: 600,
		items: 1,
		responsive: {
			0: {
				items: 1
			},
			600: {
				items: 1
			}
		}

	});
	
	$(window).resize(function(){
		$(".featured-slider").trigger('refresh.owl.carousel');
	});

	//Latest news slide

	$(".latest-news-slide").owlCarousel({

		rtl: true,
		loop: false,
		animateIn: 'fadeInLeft',
		autoplay: false,
		autoplayHoverPause: true,
		nav: true,
		margin: 30,
		dots: false,
		mouseDrag: false,
		slideSpeed: 500,
		navText: ["<i class='fa fa-angle-right'></i>", "<i class='fa fa-angle-left'></i>"],
		items: 3,
		responsive: {
			0: {
				items: 1
			},
			600: {
				items: 3
			}
		}

	});

	//Latest news slide


	//Latest news slide

	$(".more-news-slide").owlCarousel({

		rtl: true,
		loop: false,
		autoplay: false,
		autoplayHoverPause: true,
		nav: false,
		margin: 30,
		dots: true,
		mouseDrag: false,
		slideSpeed: 500,
		navText: ["<i class='fa fa-angle-right'></i>", "<i class='fa fa-angle-left'></i>"],
		items: 1,
		responsive: {
			0: {
				items: 1
			},
			600: {
				items: 1
			}
		}

	});

	$(".post-slide").owlCarousel({

		rtl: true,
		loop: true,
		animateOut: 'fadeOut',
		autoplay: false,
		autoplayHoverPause: true,
		nav: true,
		margin: 30,
		dots: false,
		mouseDrag: false,
		slideSpeed: 500,
		navText: ["<i class='fa fa-angle-right'></i>", "<i class='fa fa-angle-left'></i>"],
		items: 1,
		responsive: {
			0: {
				items: 1
			},
			600: {
				items: 1
			}
		}

	});



	/* ----------------------------------------------------------- */
	/*  Popup
	/* ----------------------------------------------------------- */
	$(document).ready(function () {

		$(".gallery-popup").colorbox({
			rel: 'gallery-popup',
			transition: "fade",
			innerHeight: "500"
		});

		$(".popup").colorbox({
			iframe: true,
			innerWidth: 600,
			innerHeight: 400
		});

	});



	/* ----------------------------------------------------------- */
	/*  Counter
	/* ----------------------------------------------------------- */

	$('.counterUp').counterUp({
		delay: 10,
		time: 1000
	});



	/* ----------------------------------------------------------- */
	/*  Contact form
	/* ----------------------------------------------------------- */

	$('#contact-form').submit(function () {

		var $form = $(this),
			$error = $form.find('.error-container'),
			action = $form.attr('action');

		$error.slideUp(750, function () {
			$error.hide();

			var $name = $form.find('.form-control-name'),
				$email = $form.find('.form-control-email'),
				$subject = $form.find('.form-control-subject'),
				$message = $form.find('.form-control-message');

			$.post(action, {
					name: $name.val(),
					email: $email.val(),
					subject: $subject.val(),
					message: $message.val()
				},
				function (data) {
					$error.html(data);
					$error.slideDown('slow');

					if (data.match('success') != null) {
						$name.val('');
						$email.val('');
						$subject.val('');
						$message.val('');
					}
				}
			);

		});

		return false;

	});


	/* ----------------------------------------------------------- */
	/*  Back to top
	/* ----------------------------------------------------------- */

	$(window).scroll(function () {
		if ($(this).scrollTop() > 50) {
			$('#back-to-top').fadeIn();
		} else {
			$('#back-to-top').fadeOut();
		}
	});

	// scroll body to 0px on click
	$('#back-to-top').on('click', function () {
		$('#back-to-top').tooltip('hide');
		$('body,html').animate({
			scrollTop: 0
		}, 800);
		return false;
	});

	$('#back-to-top').tooltip('hide');


	/* ----------------------------------------------------------- */
	/*  Custom
	/* ----------------------------------------------------------- */

	
});
