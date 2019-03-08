// JavaScript Document

 $(document).ready(function () {
	 
 	$(".ts-sidebar-menu li a").each(function () {
 		if ($(this).next().length > 0) {
 			$(this).addClass("parent");
 		};
 	})
 	var menux = $('.ts-sidebar-menu li a.parent');
 	
 	$('.more').click(function () {
 		$(this).parent('li').toggleClass('open');
 	});
	$('.parent').click(function (e) {
		e.preventDefault();
 		$(this).parent('li').toggleClass('open');
 	});
 	$('.menu-btn').click(function () {
 		$('nav.ts-sidebar').toggleClass('menu-open');
 	});
	 

	
 });