﻿var globHourWidth = 120;

$(document).ready(function () {
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
        jQuery(this).toggleClass('active').find('i').toggleClass('fa-plus fa-minus')
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

    function dayProgressionPoint() {
        return 2920;
    }

    var resizeProgrammesTimeoutId;
    var dateLeft = true;
    $('.stream-container-inner').on('scroll', function () {
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
        resizeProgrammesTimeoutId = setTimeout(function () {
            resizeProgrammes(that);
        }, 250);
    });

    $('.days-tab li:nth-of-type(1)').on('click', function () {
        $('.stream-container-inner').scrollLeft(0);
    });

    $('.days-tab li:nth-of-type(2)').on('click', function () {
        $('.stream-container-inner').scrollLeft(dayProgressionPoint());
    });

    $('.days-tab li:nth-of-type(n+3)').on('click', function () {
        $('.stream-container-inner').scrollLeft(dayProgressionPoint() * 2);
    });

    $('#epgInfoPopup').hide();
    $('.stream-container-inner').find('a:last-of-type').on('click', function (e) {
        e.stopPropagation();

        // We'll have to pass an event ID in here in the long run.
        // They'll be added to the HTML sever-side.
        $('#epgInfoPopup').show();
    });

    $('[data-hide]').on("click", function () {
        $("." + $(this).attr("data-hide")).hide();
    });

    if ($('.epg-outer').length > 0)
        floatingNav();


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
    container = $(container);
    var that = this,
        containerPosition = container.scrollLeft(),
        edgeOffset = 20;

    $.each(container.find('ol.channel-list'), function () {
        var preSpace = 0;

        $.each($(this).children('li'), function () {
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
//Change the highlighted date tab
////////////////////////////////////////////
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