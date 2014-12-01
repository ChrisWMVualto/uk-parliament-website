var globHourWidth = 120;

$(document).ready(function() {
    ////////////////////////////////////////////
    //Cookies
    ////////////////////////////////////////////
    $('#cookies').modal({
        show: true
    });

    $('#cookiesAccept, #cookiesReject').bind('click', function () {
        var url = '/Cookie?accepted=';

        if ($(this).attr('id') == 'cookiesAccept')
            url += 'True';
        else
            url += 'False';

        $.ajax(url);
    });


    ////////////////////////////////////////////
    //popover
    ////////////////////////////////////////////
    $('.popover-markup>.trigger').popover({
        html: true,
        title: function() {
            return $(this).parent().find('.head').html();
        },
        content: function() {
            return $(this).parent().find('.content').html();
        }
    });


    ////////////////////////////////////////////
    //clip date
    ////////////////////////////////////////////
    if ($(".clip-date").length) {
        $('.clip-date').selectpicker({
            style: 'btn-clip-date form-control input-lg',
            size: 4
        });
    }


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
    $(".btn-tag").click(function() {
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
    $(".close-info").click(function() {
        $("#info").removeClass("active");
        $("#info-tab").removeClass("active");
    });

    $(".close-share").click(function() {
        $("#share").removeClass("active");
        $("#share-tab").removeClass("active");
    });


    ////////////////////////////////////////////
    //home-tabs joined to responsive select
    ////////////////////////////////////////////
    $('.select-home-tabs').on('change', function(e) {
        $('.home-nav-tabs li a').eq($(this).val()).tab('show');
    });


    ////////////////////////////////////////////
    //audio active toggle
    ////////////////////////////////////////////
    $(".audio-toggle").click(function() {
        $(this).toggleClass("active");
    });


    ////////////////////////////////////////////
    //home-recently archived na btns
    ////////////////////////////////////////////
    $('#recentNextCommons').click(function() {
        $('#recentlyArchivedCommons').slickNext();
        return false;
    });

    $('#recentPrevCommons').click(function() {
        $('#recentlyArchivedCommons').slickPrev();
        return false;
    });

    $('#recentNextLords').click(function() {
        $('#recentlyArchivedLords').slickNext();
        return false;
    });

    $('#recentPrevLords').click(function() {
        $('#recentlyArchivedLords').slickPrev();
        return false;
    });

    $('#recentNextCommittees').click(function() {
        $('#recentlyArchivedCommittees').slickNext();
        return false;
    });

    $('#recentPrevCommittees').click(function() {
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
    $('.info, .audio-toggle, .close-pane').click(function(event) {
        event.preventDefault();
        // or use return false;
    });


    ////////////////////////////////////////////
    //epg-datepicker
    ////////////////////////////////////////////
    if ($(".date-picker").length) {
        $('.date-picker').datepicker({
            weekStart: 1
        });
    }

    $('.end-date').datepicker({
        autoclose: true,
        weekStart: 1
    });


    $('.start-date').datepicker({
        autoclose: true,
        weekStart: 1
    });


    ////////////////////////////////////////////
    //epg-timepicker
    ////////////////////////////////////////////
    if ($("#epgTimepicker").length) {
        $('#epgTimepicker').timepicker({
            timeSeparator: '.'
        }).on('changeTime.timepicker', changeEpgTime);
    }


    ////////////////////////////////////////////
    //epg-day tabs
    ////////////////////////////////////////////
    var $li = $('.days-tab li'); // or var $li = $('#id li');

    $li.click(function() {
        $li.removeClass('active');
        $(this).addClass('active');
    });

    $('.btn-search-panel').click(function() {
        jQuery(this).toggleClass('active').find('i').toggleClass('fa-plus fa-minus')
            .removeClass('active').find('i')
            .removeClass('fa-minus').addClass('fa-plus');

    });


    ////////////////////////////////////////////
    //epg-x-axis-movement
    ////////////////////////////////////////////
    $('.stream-container-inner').scrollLeft(function() {
        var time = new Date();
        var leftBuffer = 40;
        return leftPositionFromTime(time.getHours(), time.getMinutes()) - leftBuffer;
    });
    resizeProgrammes($('.stream-container-inner'));

    $('.stream-container-inner').on('scroll', scrollDayProgression)

    $('.days-tab li:nth-of-type(1)').on('click', function() {
        $('.stream-container-inner').scrollLeft(0);
    });

    $('.days-tab li:nth-of-type(2)').on('click', function() {
        $('.stream-container-inner').scrollLeft(dayProgressionPoint());
    });

    $('.days-tab li:nth-of-type(n+3)').on('click', function() {
        $('.stream-container-inner').scrollLeft(dayProgressionPoint() * 2);
    });

    $('#epgInfoPopup').hide();
    $('.stream-container-inner').find('a:last-of-type').on('click', function(e) {
        e.stopPropagation();

        // We'll have to pass an event ID in here in the long run.
        // They'll be added to the HTML sever-side.
        $('#epgInfoPopup').show();
    })

    $('[data-hide]').on("click", function() {
        $("." + $(this).attr("data-hide")).hide();
    })

    if ($('.epg-outer').length > 0)
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

    $(window).on('enterBreakpoint300', function() {

        $("#searchPanel").removeClass('in').css("height", "0");


        //terms box height
        $('.tc-box, .tc-box-container .slimScrollDiv').css({
            height: 'auto'
        });

    });

    $(window).on('exitBreakpoint300', function() {
        //console.log('exit');
        // $('#miniGuideCommons, #miniGuideLords, #miniGuideCommittees').unslick();
        $("#searchPanel").addClass('in').css("height", "auto");
    });

    $(window).on('enterBreakpoint768', function() {


        // $(".stack").slimScroll({destroy: true});
    });

    $(window).on('exitBreakpoint768', function() {

    });

    $(window).on('enterBreakpoint1024', function() {
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
        $('.tc-box, .tc-box-container .slimScrollDiv').css({
            height: '300px'
        });

        if (typeof $().dragscrollable === 'function') {
            $('.stream-container-inner').dragscrollable({
                dragSelector: '*'
            });
        }
    });

    $(window).on('exitBreakpoint1024', function() {
        //remove slick carousel
        $('#recentlyArchivedCommons, #recentlyArchivedLords, #recentlyArchivedCommittees').unslick();


    });


    ////////////////////////////////////////////
    //EPG Time Scroll
    ////////////////////////////////////////////
    $('#epgScrollRight, #epgScrollLeft').on('click', scrollEpg);
    $('#epgTimeScrollRight, #epgTimeScrollLeft').on('click', scrollEpg);
    $('#epgDateScrollRight, #epgDateScrollLeft').on('click', changeDateTab);


    ////////////////////////////////////////////
    //Embed terms
    ////////////////////////////////////////////
    $('.embed-code').hide();
    $('.embed-terms .btn-agree').on('click', function(e) {
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
    $(window).on("resize", resize);

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
//EPG Time Scroll
////////////////////////////////////////////
function scrollEpg() {
    var container = $('.stream-container-inner'),
        moveDist = globHourWidth * 2,
        clickedArrow = $(this).attr('id'),
        leftArrow = "epgTimeScrollLeft",
        rightArrow = "epgTimeScrollRight",
        disableClass = "disable";

    if (clickedArrow == rightArrow) {
        container.scrollLeft(container.scrollLeft() + moveDist);
    }

    if (clickedArrow == leftArrow) {
        container.scrollLeft(container.scrollLeft() - moveDist);
    }
}

////////////////////////////////////////////
//EPG Timepicker scrolling
////////////////////////////////////////////
function changeEpgTime(event) {
    var meridian = event.time.meridian,
        hours = event.time.hours,
        minutes = event.time.minutes,
        hourPx = globHourWidth,
        container = $('.stream-container-inner');

    if (meridian == "PM")
        hours += 12;

    container.scrollLeft(leftPositionFromTime(hours, minutes));
}

////////////////////////////////////////////
//Position scrolling based on time
////////////////////////////////////////////
function leftPositionFromTime(hour, minute) {
    return ((hour * globHourWidth) + (globHourWidth * (minute / 60)));
}

////////////////////////////////////////////
//Change the highlighted date tab
////////////////////////////////////////////
function changeDateTab(event) {
    var days = $('.days-tab ol')[0].children,
        activeIndex,
        activeClass = 'active',
        leftArrow = 'epgDateScrollLeft',
        rightArrow = 'epgDateScrollRight',
        container =  $('.stream-container-inner'),
        tabIndex = 0;

    $('.stream-container-inner').off('scroll');

    for (var i = 0; i < days.length; i++) {
        if ($(days[i]).hasClass(activeClass)) {
            activeIndex = i;
            break;
        }
    }

    if ($(this).attr('id') == leftArrow && activeIndex - 1 >= 0) {
        $(days[activeIndex]).removeClass(activeClass);
        tabIndex = activeIndex - 1;
        $(days[tabIndex]).addClass(activeClass);
    }

    if ($(this).attr('id') == rightArrow && activeIndex < days.length - 1) {
        $(days[activeIndex]).removeClass(activeClass);
        tabIndex = activeIndex + 1;
        $(days[tabIndex]).addClass(activeClass);
    }

    console.log(tabIndex);

    if (tabIndex == 0)
        container.scrollLeft(0);
    else if (tabIndex == 1)
        container.scrollLeft(2920);
    else
        container.scrollLeft(10000);

    $('.stream-container-inner').on('scroll', scrollDayProgression);
}

////////////////////////////////////////////
//Controls the flaoting EPG dates and times
////////////////////////////////////////////
var triggerFloatTimeoutId;
function floatingNav() {
    var fixedClass = 'fixed-epg',
        container = $('.main-container'),
        epg = $('.epg-outer'),
        timeline = $('.timeline'),
        header = $('.header-main div'),
        title = $('.title-container'),
        streamContainer = $('.stream-container-inner'),
        freezeFrom = $('.channel-eightteen'),
        infoPopup = $('#epgInfoPopup');


    $(window).on('scroll', function() {
        epg.hide();
        timeline.hide();
        clearInterval(triggerFloatTimeoutId);
        triggerFloatTimeoutId = setTimeout(triggerFloat, 10);
    });

    function datesOffset(freeze) {
        if (freeze)
            return freezeFrom.offset().top - freezeFrom.parent().parent().offset().top + header.outerHeight();
        else
            return ($(window).scrollTop() - epg.parent().offset().top) + header.outerHeight();
    }

    function timesOffset(freeze) {
        return datesOffset(freeze) + 109;
    }

    function epgInfoOffset(freeze) {
        return timesOffset(freeze) + (timeline.height() * 1);
    }

    function triggerFloat() {
        applyStyles($(window).scrollTop() >= title.height());
    }

    function freezeMenu() {
        return $(window).scrollTop() >= freezeFrom.offset().top;
    }

    function applyStyles(isActive) {
        streamContainer.css({
            'paddingTop': timeline.height()
        });

        if (isActive) {
            epg.css({
                'position': 'absolute',
                'top': datesOffset(freezeMenu())
            });
            timeline.css({
                'top': timesOffset(freezeMenu())
            });
            infoPopup.css({
                'top': epgInfoOffset(freezeMenu())
            });
            container.addClass(fixedClass);
        } else {
            epg.css({
                'position': 'relative',
                'top': 0
            });
            timeline.css({
                'top': 0
            });
            infoPopup.css({
                'top': timeline.height()
            });
            container.removeClass(fixedClass);
        }
        epg.show();
        timeline.show();
    }
}

////////////////////////////////////////////
//Resizes the programmes to keep them on screen
////////////////////////////////////////////
function resizeProgrammes(container) {
    var that = this,
        container = $(container),
        containerPosition = container.scrollLeft(),
        edgeOffset = 20;

    $.each(container.find('ol.channel-list'), function() {
        var preSpace = 0;

        $.each($(this).children('li'), function() {
            if ($(this).attr('data-width') == null)
                $(this).attr('data-width', $(this).width())

            if ($(this).has('.blank').length == 0)
                calculateMove(preSpace, this);

            preSpace += $(this).outerWidth();
        });
    });

    function calculateMove(preSpace, context) {
        preSpace += edgeOffset;

        if (preSpace <= containerPosition) {
            var margin = containerPosition - preSpace;

            if (width($(context).innerWidth(), margin) <= 180) {
                margin = $(context).innerWidth() - 180;
            }

            moveTo(margin, context);
        } else
            moveTo(0, context);
    }

    function moveTo(amount, context) {
        if ($(context).outerWidth() > 180) {
            $(context).find('.outer .inner').animate({
                'marginLeft': amount
            }, 500, 'swing', function() {
                if (width($(context).innerWidth(), amount) <= 180) {
                    $(context).addClass('event');
                }
                if (width($(context).innerWidth(), amount) > 180) {
                    $(context).removeClass('event');
                }
            });
        }
    }

    function width(margin, original) {
        return (original - margin) * -1;
    }
}

function dayProgressionPoint() {
    oneDay = 2920;
    return oneDay;
}
var resizeProgrammesTimeoutId;
var dateLeft = true;
function scrollDayProgression() {
    // Select tomorrow
    if ($(this).scrollLeft() > dayProgressionPoint() && dateLeft) {
        $('#epgDateScrollRight').trigger('click');
        dateLeft = false;
    } else if ($(this).scrollLeft() < dayProgressionPoint() && !dateLeft) {
        $('#epgDateScrollLeft').trigger('click');
        dateLeft = true;
    }

    // Resize programmes
    var that = this;
    clearInterval(resizeProgrammesTimeoutId);
    resizeProgrammesTimeoutId = setTimeout(function() {
        resizeProgrammes(that);
    }, 250);
}
