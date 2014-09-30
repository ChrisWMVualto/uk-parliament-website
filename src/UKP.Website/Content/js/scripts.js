$(document).ready(function() {


  ////////////////////////////////////////////
  //popover
  ////////////////////////////////////////////
  $('.popover-markup>.trigger').popover({
      html: true,
      title: function () {
          return $(this).parent().find('.head').html();
      },
      content: function () {
          return $(this).parent().find('.content').html();
      }
  });


  ////////////////////////////////////////////
  //search select param
  ////////////////////////////////////////////
  if ($(".select-search-param").length) {
    $('.select-search-param').selectpicker({
        style: 'btn-search-select form-control',
        size: 4
    });
  }


  ////////////////////////////////////////////
  //remove tag
  ////////////////////////////////////////////
  $(".btn-tag").click(function(){
    $(this).remove();
  });

  ////////////////////////////////////////////
  //bootstrap checkbox
  ////////////////////////////////////////////
  if ($(".checkbox").length) {
    $(".checkbox").checkbox({
       buttonStyle: 'btn-checkbox',
       buttonStyleChecked: 'btn-checkbox',
       checkedClass: 'fa fa-tick fa-2x',
       uncheckedClass: 'fa fa-tick fa-2x fa-dark',
       constructorCallback: null,
       defaultState: false,
       defaultEnabled: true,
       checked: false,
       enabled: true
    });
  }

  ////////////////////////////////////////////
  //player share & info pane close btn's
  ////////////////////////////////////////////

  $( ".close-info" ).click(function() {
     $( "#info" ).removeClass("active");
     $( "#info-tab" ).removeClass("active");
  });

  $( ".close-share" ).click(function() {
     $( "#share" ).removeClass("active");
     $( "#share-tab" ).removeClass("active");
  });

  ////////////////////////////////////////////
  //home-tabs joined to responsive select
  ////////////////////////////////////////////
  $.each($('select.select-home-tabs').children(), function () {
      if ($(this).attr('data-url') == window.location.href) {
          $(this).attr('selected', 'selected');

          if ($(".select-home-tabs").length) {
              $('.select-home-tabs').selectpicker({
                  style: 'btn-home-select',
                  size: 4
              });
          }
      }
  });
  
  $('select.select-home-tabs').on('change', function() {
      window.location = $(this).children(':selected').attr('data-url');
  });

  ////////////////////////////////////////////
  //audio active toggle
  ////////////////////////////////////////////
  $( ".audio-toggle" ).click(function() {
    $( this ).toggleClass( "active" );
  });

  ////////////////////////////////////////////
  //home-recently archived na btns
  ////////////////////////////////////////////
  $('#recentNextCommons').click(function(){
    $('#recentlyArchivedCommons').slickNext();
     return false;
  });

  $('#recentPrevCommons').click(function(){
    $('#recentlyArchivedCommons').slickPrev();
     return false;
  });

  $('#recentNextLords').click(function(){
    $('#recentlyArchivedLords').slickNext();
    return false;
  });

  $('#recentPrevLords').click(function(){
    $('#recentlyArchivedLords').slickPrev();
    return false;
  });

  $('#recentNextCommittees').click(function(){
    $('#recentlyArchivedCommittees').slickNext();
    return false;
  });

  $('#recentPrevCommittees').click(function(){
    $('#recentlyArchivedCommittees').slickPrev();
    return false;
  });

  ////////////////////////////////////////////
  //epg-slimscroll details alert stack area
  ////////////////////////////////////////////
  if ($(".stack").length) {
    $('.stack').slimScroll({
        railVisible: true,
        railColor: '#ffffff',
        railOpacity: 0.3,
        color: '#ffffff',
        size: '12px',
        height: 'auto'
    });
  }

  /////////////////////////////////////////////
  //click false
  /////////////////////////////////////////////
  $('.info, .audio-toggle, .close-pane').click(function (event) {
    event.preventDefault();
    // or use return false;
  });

  ////////////////////////////////////////////
  //epg-datepicker
  ////////////////////////////////////////////
  if ($(".date-picker").length) {
    $('.date-picker').datepicker();
  }

  ////////////////////////////////////////////
  //epg-timepicker
  ////////////////////////////////////////////
  if ($("#epgTimepicker").length) {
    $('#epgTimepicker').timepicker().bind('changeTime.timepicker', changeEpgTime);
  }

  ////////////////////////////////////////////
  //epg-day tabs
  ////////////////////////////////////////////
  var $li = $('.days-tab li'); // or var $li = $('#id li');

  $li.click(function(){
      $li.removeClass('active');
      $(this).addClass('active');
  });

  ////////////////////////////////////////////
  //epg-draggable
  ////////////////////////////////////////////
  $('.draggable').pep({
    axis: 'x',
    useCSSTranslation: false,
    shouldPreventDefault: false,
    constrainTo: [0, 0, 0, ($('.draggable').width() - $(".drag-wrap").width()) * -1],
    hardwareAccelerate: false
  });

    $('.draggable').find('a').on('touchstart mousedown MSPointerDown', function(e) {
        e.stopPropagation();

        // We'll have to pass an event ID in here in the long run.
        // They'll be added to the HTML sever-side.
        $('#epgInfoPopup').show();
        loadInfoPopup(this);
    });

    $('[data-hide]').bind("click", function() {
        $("." + $(this).attr("data-hide")).hide();
    });

  /*
  					//Enable swiping...
  					$(".carousel-inner").swipe( {
  						//Generic swipe handler for all directions
  						swipeLeft:function(event, direction, distance, duration, fingerCount) {
  							$(this).parent().carousel('prev');
  						},
  						swipeRight: function() {
  							$(this).parent().carousel('next');
  						},
  						//Default is 75px, set to 0 for demo so any distance triggers swipe
  						threshold:0
  					});
  */

  ////////////////////////////////////////////
  //breakpoints
  ////////////////////////////////////////////
  $(window).setBreakpoints({
    // use only largest available vs use all available
    distinct: true,
    // array of widths in pixels where breakpoints
    // should be triggered
    breakpoints: [
      300,
      768,
      1024
    ]
  });

  $(window).bind('enterBreakpoint300',function() {
   /* $('#miniGuideCommons, #miniGuideLords, #miniGuideCommittees').slick({
          centerMode: true,
          infinite: true,
          centerPadding: '60px',
          slidesToShow: 1,
      });*/
  });

  $(window).bind('exitBreakpoint300',function() {
  	    //console.log('exit');
  	   // $('#miniGuideCommons, #miniGuideLords, #miniGuideCommittees').unslick();
  });

  $(window).bind('enterBreakpoint768',function() {


   // $(".stack").slimScroll({destroy: true});
  });

  $(window).bind('exitBreakpoint768',function() {
  });

  $(window).bind('enterBreakpoint1024',function() {
  	$('#recentlyArchivedCommons, #recentlyArchivedLords, #recentlyArchivedCommittees').slick({
          centerMode: false,
          infinite: true,
          arrows: false,
          centerPadding: '0',
          slidesToShow: 1,
          onAfterChange: updateArrowText
      });
  });

  $(window).bind('exitBreakpoint1024',function() {
     $('#recentlyArchivedCommons, #recentlyArchivedLords, #recentlyArchivedCommittees').unslick();
  });




////////////////////////////////////////////
//EPG Time Scroll
////////////////////////////////////////////
  $('#epgScrollRight, #epgScrollLeft').bind('click', scrollEpg);


  ////////////////////////////////////////////
  //EPG Time Scroll
  ////////////////////////////////////////////
  $('#epgTimeScrollRight, #epgTimeScrollLeft').bind('click', scrollEpg);
  $('#epgDateScrollRight, #epgDateScrollLeft').bind('click', changeDateTab);
});


////////////////////////////////////////////
//carousel text
////////////////////////////////////////////
function updateArrowText() {
  var next = $(this.$list.context).parents().eq(2).find('.recent-next div p'),
      prev = $(this.$list.context).parents().eq(2).find('.recent-prev div p'),
      childrenSelector = 'a div div div p',
      activeIndex = null,
      slideActive = null,
      nextIndex = null,
      prevIndex = null;

  for (var i = 0; i < this.$slides.length; i++) {
    slideActive = this.$slides[i].classList.contains("slick-active");

    if (slideActive) {
      activeIndex = i;
      break;
    }
  }

  if (activeIndex == this.$slides.length - 1)
    nextIndex = 0
  else
    nextIndex = activeIndex + 1;

  if (activeIndex == 0)
    prevIndex = this.$slides.length - 1;
  else
    prevIndex = activeIndex - 1;

  next.html($(this.$slides[nextIndex]).find(childrenSelector).text());
  prev.html($(this.$slides[prevIndex]).find(childrenSelector).text());
}


////////////////////////////////////////////
//EPG Info Popup
////////////////////////////////////////////
function loadInfoPopup(obj) {
  var offset = 2,
      top = $(obj).parents().eq(3).position().top - offset;

  $('#epgInfoPopup').css('top', top);
}


////////////////////////////////////////////
//EPG Time Scroll
////////////////////////////////////////////
function scrollEpg() {
  var container = $('.stream-container-outer'),
      containerInner = container.find('.stream-container-inner'),
      leftPosition = containerInner.position().left,
      oneHour = -300,
      clickedArrow = $(this).attr('id'),
      leftArrow = "epgTimeScrollLeft",
      rightArrow = "epgTimeScrollRight",
      disableClass = "disable",
      rightPosition = (container.find('.timeline').width() - container.width() + oneHour) * -1;

  if (clickedArrow == rightArrow) {
    if (rightPosition >= leftPosition) {
      leftPosition = rightPosition;
      $("#" + rightArrow).addClass(disableClass);
    } else {
      leftPosition += oneHour;
      $("#" + rightArrow).removeClass(disableClass);
    }
  }

  if (clickedArrow == leftArrow) {
    if (leftPosition >= oneHour) {
      leftPosition = 0;
      $("#" + leftArrow).addClass(disableClass);
    } else {
      leftPosition -= oneHour;
      $("#" + leftArrow).removeClass(disableClass);
    }
  }

  containerInner.animate({
    left: leftPosition
  }, 500);
}

function changeEpgTime(event) {
  var meridian = event.time.meridian,
      hours = event.time.hours,
      minutes = event.time.minutes,
      hourPx = 300,
      container = $('.stream-container-inner'),
      leftPosition;

  if (meridian == "PM")
    hours += 12;

  leftPosition = ((hours * hourPx) + (hourPx * (minutes / 60))) * -1;

  container.animate({
    left: leftPosition
  }, 500);

  prevEpgMin = minutes;
  prevEpgHour = hours;
}

function changeDateTab(event) {
  var days = $('.days-tab ol')[0].children,
      activeIndex,
      activeClass = 'active',
      leftArrow = 'epgDateScrollLeft',
      rightArrow = 'epgDateScrollRight';

  for (var i = 0; i < days.length; i++) {
    if ($(days[i]).hasClass(activeClass)) {
      activeIndex = i;
      break;
    }
  }

  if ($(this).attr('id') == leftArrow && activeIndex - 1 >= 0) {
      $(days[activeIndex]).removeClass(activeClass);
      $(days[activeIndex - 1]).addClass(activeClass);
  }

  if ($(this).attr('id') == rightArrow && activeIndex < days.length - 1) {
      $(days[activeIndex]).removeClass(activeClass);
      $(days[activeIndex + 1]).addClass(activeClass);
  }
}
