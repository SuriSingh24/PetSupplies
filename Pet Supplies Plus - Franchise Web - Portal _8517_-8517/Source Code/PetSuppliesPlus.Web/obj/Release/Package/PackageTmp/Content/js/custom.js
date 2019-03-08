$(document).ready(function(){
	$(".checkbox").each(function(){
		if($(this).find("input").is(":checked")){
			$(this).addClass("checked");
		}
	});
	$(".checkbox").click(function(){
		$(this).toggleClass("checked");
	});
	$('#tablularData').stacktable({myClass:'mobile-table'});
	$(".mainMenuMobile").click(function(){
		$(".dashboard_menu").slideToggle();
	})
	
	$(".slideMenuMobile").click(function(){
		$("section.wrapper nav").toggleClass("open");
	});

	$("section.wrapper > nav > ul > li.parent").click(function(){
		$(this).children("ul").slideToggle();
	});
	$("section.wrapper > nav").css("min-height", $("section.wrapper").height());

	$(".related_content").css("min-height", ($("section.wrapper").height() - 75));

	$("input.datepicker").datepicker().on('show', function(e){
		var datepickerHeight = $("div.datepicker-dropdown").height();
		if(($(this).offset().top + $(this).height() +datepickerHeight) > $('body').height()){
			$("div.datepicker-dropdown").css('top',  ($(this).offset().top - $(this).height() - 10 - $("div.datepicker-dropdown").height()));	
		}else{
			$("div.datepicker-dropdown").css('top',  ($(this).offset().top + 10 + $(this).height()));	
		}
	});

	$(".responsive-calendar").responsiveCalendar({
		time: '2016-03',
		events: {
		}
    });		
})

