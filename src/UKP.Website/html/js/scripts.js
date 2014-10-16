$(document).ready(function() {

////////////////////////////////////////////
//Cookie & language modal
////////////////////////////////////////////
 $('#cookies').modal({
      show: true
    })


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
  //home-select menu
  ////////////////////////////////////////////
  if ($(".select-home-tabs").length) {
    $('.select-home-tabs').selectpicker({
        style: 'btn-home-select',
        size: 4
    });
  }

  ////////////////////////////////////////////
  //truncate text
  ////////////////////////////////////////////
  if ($(".truncate-archive").length) {
          $('.truncate-archive').succinct({
            size: 80
        });
  }

  ////////////////////////////////////////////
  //search select param
  ////////////////////////////////////////////
  if ($(".select-filter").length) {
    $('.select-filter').selectpicker({
        style: 'btn-filter form-control',
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
  $('.select-home-tabs').on('change', function (e) {
      $('.home-nav-tabs li a').eq($(this).val()).tab('show');
  });

  ////////////////////////////////////////////
  //audio active toggle
  ////////////////////////////////////////////
 // $( ".audio-toggle" ).click(function() {
  //  $( this ).toggleClass( "active" );
 // });

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



////////////////////////////////////////////
//terms & conditions - share
////////////////////////////////////////////
  if ($(".tc-box ").length) {
    $('.tc-box ').slimScroll({
        railVisible: true,
        railColor: '#E6EBEE',
        railOpacity: 0.3,
        color: '#C1C7C9',
        size: '12px',
        height: '300'
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




$('.end-date').datepicker({
    autoclose: true
});


$('.start-date').datepicker({
    autoclose: true
});




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



jQuery('.btn-search-panel').click(function() {
    jQuery(this).toggleClass('active').find('i').toggleClass('fa-plus fa-minus')
           .removeClass('active').find('i')
           .removeClass('fa-minus').addClass('fa-plus');

});








  ////////////////////////////////////////////
  //epg-draggable
  ////////////////////////////////////////////
  $('.draggable').pep({
    axis: 'x',
    useCSSTranslation: false,
    shouldPreventDefault: false,
    constrainTo: [0, 0, 0, ($('.draggable ol:first-of-type').width() - $(".drag-wrap").width()) * -1],
    hardwareAccelerate: false
  });

  $('.draggable').find('a').on('touchstart mousedown MSPointerDown', function(e){
    e.stopPropagation();

    // We'll have to pass an event ID in here in the long run.
    // They'll be added to the HTML sever-side.
    $('#epgInfoPopup').show();
    loadInfoPopup(this);
  })

  $('.draggable').css({
    left: function() {
      var time = new Date();
      return leftPositionFromTime(time.getHours(), time.getMinutes());
    }
  });

  $('.live-now').css({
    left: function() {
      var time = new Date();
      return leftPositionFromTime(time.getHours(), time.getMinutes()) * -1 + 175;
    }
  })

  $('[data-hide]').bind("click", function() {
    $("." + $(this).attr("data-hide")).hide();
  })

  floatingNav();


  ////////////////////////////////////////////
  //carousels swipable
  ////////////////////////////////////////////

  if ($(".carousel").length) {
       $("#carouselCommons, #carouselLords, #carouselCommittees").swiperight(function() {
          $(this).carousel('prev');
          });
       $("#carouselCommons, #carouselLords, #carouselCommittees").swipeleft(function() {
          $(this).carousel('next');
     });
     }



  //////////////////////////////////////////////////////////////////////////////////////////////////////////////
  //breakpoints
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

   $("#searchPanel").removeClass('in').css( "height", "0" );


   //terms box height
$('.tc-box, .tc-box-container .slimScrollDiv').css({ height: 'auto'});

  });

  $(window).bind('exitBreakpoint300',function() {
  	    //console.log('exit');
  	   // $('#miniGuideCommons, #miniGuideLords, #miniGuideCommittees').unslick();
   $("#searchPanel").addClass('in').css( "height", "auto" );
  });

  $(window).bind('enterBreakpoint768',function() {


   // $(".stack").slimScroll({destroy: true});
  });

  $(window).bind('exitBreakpoint768',function() {

  });

  $(window).bind('enterBreakpoint1024',function() {
    //add slick
  	$('#recentlyArchivedCommons, #recentlyArchivedLords, #recentlyArchivedCommittees').slick({
          centerMode: false,
          infinite: true,
          arrows: false,
          centerPadding: '0',
          slidesToShow: 1,
          onAfterChange: updateArrowText
      });


//terms box height
$('.tc-box, .tc-box-container .slimScrollDiv').css({ height: '300px'});


  });

  $(window).bind('exitBreakpoint1024',function() {
    //remove slick carousel
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

  ////////////////////////////////////////////
  //Embed terms
  ////////////////////////////////////////////
    $('.embed-code').hide();
    $('.embed-terms .btn-agree').bind('click', function (e) {
        e.preventDefault();

        $('.embed-terms').fadeOut();
        $('.embed-code').fadeIn();
    });

    ////////////////////////////////////////////
    //Embed timepickers
    ////////////////////////////////////////////
    var opts = {
        defaultTime: false,
        showSeconds: true,
        showMeridian: false,
        minuteStep: 1,
        secondStep: 1
    };
    $('#startTime').timepicker(opts);
    $('#endTime').timepicker(opts);


    ////////////////////////////////////////////
    //EPG Dates window resize handler
    ////////////////////////////////////////////
    resize();
    $(window).bind("resize", resize);
    function resize() {
        $('.days-tab-inner').css('width', $('.stream-container-outer').width());
    }
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
      oneHour = -150,
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
      hourPx = 150,
      container = $('.stream-container-inner');

  if (meridian == "PM")
    hours += 12;

  container.animate({
    left: leftPositionFromTime(hours, minutes)
  }, 500);
}

function leftPositionFromTime(hour, minute) {
  var hourSize = 150;
  return ((hour * hourSize) + (hourSize * (minute / 60))) * -1;
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

function floatingNav() {
    $(window).bind('scroll', function() {
       triggerFloat();
    });

    function datesOffset() {
        return ($(window).scrollTop() - $('.epg-outer').parent().offset().top) + $('.header-main div').outerHeight();
    }

    function timesOffset() {
        return datesOffset() + $('.epg-outer').outerHeight() - $('.timeline').height();
    }

    function containerOffset() {
        return $('.timeline').height();
    }

    function triggerFloat() {
        var triggerPoint = $('.title-container').height(),
            windowPoint  = $(window).scrollTop();

        if (windowPoint >= triggerPoint)
            applyStyles(true);
        else
            applyStyles(false);
    }

    function applyStyles(isActive) {
        var fixedClass = "fixed-epg";

        if (isActive) {
            $('.epg-outer').css({
                'position': 'absolute',
                'top': datesOffset()
            });
            $('.timeline').css({
                'position': 'absolute',
                'top': timesOffset()
            });
            $('.stream-container-inner').css({
                'marginTop': containerOffset()
            });
            $('.main-container').addClass(fixedClass);
        } else {
            $('.epg-outer').css({
                'position': 'relative',
                'top': 0
            });
            $('.timeline').css({
                'position': 'relative',
                'top': 0
            });
            $('.stream-container-inner').css({
                'marginTop': 0
            });
            $('.main-container').removeClass(fixedClass);
        }
    }
}
