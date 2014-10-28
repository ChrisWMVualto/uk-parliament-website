var globHourWidth = 120,
    globOneDayWidth = 2920;

$(document).ready(function () {


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
    //epg-datepicker
    ////////////////////////////////////////////
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

    $li.click(function () {
        $li.removeClass('active');
        $(this).addClass('active');
    });

    $('.btn-search-panel').click(function () {
        $(this).toggleClass('active').find('i').toggleClass('fa-plus fa-minus')
            .removeClass('active').find('i')
            .removeClass('fa-minus').addClass('fa-plus');

    });


    ////////////////////////////////////////////
    //epg-x-axis-movement
    ////////////////////////////////////////////
    $('.stream-container-inner').scrollLeft(function () {
        var time = new Date();
        var leftBuffer = 40;
        return leftPositionFromTime(time.getHours(), time.getMinutes()) - leftBuffer;
    });
    resizeProgrammes($('.stream-container-inner'));

    $('.stream-container-inner').on('scroll', scrollDayProgression);

    $('.days-tab li:nth-of-type(1)').on('click', function () {
        $('.stream-container-inner').scrollLeft(0);
    });

    $('.days-tab li:nth-of-type(2)').on('click', function () {
        $('.stream-container-inner').scrollLeft(globOneDayWidth);
    });

    $('.days-tab li:nth-of-type(n+3)').on('click', function () {
        $('.stream-container-inner').scrollLeft(globOneDayWidth * 2);
    });

    $('#epgInfoPopup').hide();
    $('.stream-container-inner').find('a:last-of-type').on('click', function (e) {
        e.stopPropagation();

        $('#epgInfoPopup').fadeOut(100).remove();

        $.ajax($(this).parents('li').data('epg-info'), {
            success: function(model) {
                $('.stream-container-outer').append(model).hide().fadeIn(100);
            }
        });
    });

    $('[data-hide]').on("click", function() {
        $("." + $(this).attr("data-hide")).hide();
    });

    if ($('.epg-outer').length > 0)
        floatingNav();



    ////////////////////////////////////////////
    //breakpoints
    ////////////////////////////////////////////
    $(window).on('enterBreakpoint1024', function () {
        $('.stream-container-inner').dragscrollable({
            dragSelector: '*'
        });
    });


    ////////////////////////////////////////////
    //EPG Time Scroll
    ////////////////////////////////////////////
    $('#epgScrollRight, #epgScrollLeft').on('click', scrollEpg);
    $('#epgTimeScrollRight, #epgTimeScrollLeft').on('click', scrollEpg);
    $('#epgDateScrollRight, #epgDateScrollLeft').on('click', changeDateTab);


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
//EPG Time Scroll
////////////////////////////////////////////
function scrollEpg() {
    var container = $('.stream-container-inner'),
        moveDist = globHourWidth * 2,
        clickedArrow = $(this).attr('id'),
        leftArrow = "epgTimeScrollLeft",
        rightArrow = "epgTimeScrollRight";

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
function changeDateTab() {
    var days = $('.days-tab ol')[0].children,
        activeIndex,
        activeClass = 'active',
        leftArrow = 'epgDateScrollLeft',
        rightArrow = 'epgDateScrollRight',
        container = $('.stream-container-inner'),
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

    if (tabIndex == 0)
        container.scrollLeft(0);
    else if (tabIndex == 1)
        container.scrollLeft(globOneDayWidth);
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
        freezeFrom = $('.channel-18-logo'),
        infoPopup = $('#epgInfoPopup');


    $(window).on('scroll', function () {
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
    var container = $(container),
        containerPosition = container.scrollLeft(),
        edgeOffset = 20;

    $.each(container.find('ol.channel-list'), function () {
        $.each($(this).children('li'), function () {
            calculateMove(this);
        });
    });

    function calculateMove(context) {
        var leftPos = $(context).css('left').replace('px', '');

        if (leftPos <= containerPosition) {
            var margin = containerPosition - leftPos;

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
            }, 500, 'swing', function () {
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


////////////////////////////////////////////
//EPG scroll operations
////////////////////////////////////////////
var resizeProgrammesTimeoutId;
var dateLeft = true;
function scrollDayProgression() {
    // Select tomorrow
    if ($(this).scrollLeft() > globOneDayWidth && dateLeft) {
        $('#epgDateScrollRight').trigger('click');
        dateLeft = false;
    } else if ($(this).scrollLeft() < globOneDayWidth && !dateLeft) {
        $('#epgDateScrollLeft').trigger('click');
        dateLeft = true;
    }

    // Resize programmes
    var that = this;
    clearInterval(resizeProgrammesTimeoutId);
    resizeProgrammesTimeoutId = setTimeout(function () {
        resizeProgrammes(that);
    }, 250);
}
