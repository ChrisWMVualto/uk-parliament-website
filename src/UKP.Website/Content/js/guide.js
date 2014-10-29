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
    $('.btn-search-panel').click(function () {
        $(this).toggleClass('active').find('i').toggleClass('fa-plus fa-minus')
            .removeClass('active').find('i')
            .removeClass('fa-minus').addClass('fa-plus');

    });
    changeDateTab();


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

    $('#epgInfoPopup').hide();
    $('.stream-container-inner').find('a:last-of-type').on('click', function (e) {
        e.stopPropagation();

        $.ajax($(this).parents('li').data('epg-info'), {
            success: function(model) {
                $('.stream-container-outer').append(model).hide().fadeIn(100);
            }
        });

        $(document).one('click', '[data-hide]', function () {
            $("." + $(this).data("hide")).fadeOut(100).remove();
        });
    });

    if ($('.epg-outer').length > 0)
        floatingNav();



    ////////////////////////////////////////////
    //breakpoints
    ////////////////////////////////////////////
    enableClickDrag();


    ////////////////////////////////////////////
    //EPG Time Scroll
    ////////////////////////////////////////////
    $('#epgScrollRight, #epgScrollLeft').on('click', scrollEpg);
    $('#epgTimeScrollRight, #epgTimeScrollLeft').on('click', scrollEpg);


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
//Enables EPG click + drag
////////////////////////////////////////////
function enableClickDrag() {
    $('.stream-container-inner').dragscrollable({
        dragSelector: '*'
    });
}

////////////////////////////////////////////
//Change the highlighted date tab
////////////////////////////////////////////
function changeDateTab() {
    var daysContainer = $('.days-tab ol'),
        days = daysContainer.find('li'),
        activeClass = 'active',
        streamContainer = $('.stream-container-inner'),
        channelDayContainer = $('.channel-day-container'),
        timeline = $('.timeline'),
        liveline = $('.live-now');
    

    var epgNextButton = $('#epgDateScrollRight'),
        epgPrevButton = $('#epgDateScrollLeft');


    var lowerThreshold = 150,
        upperThreshold = 1910,
        removePast = false;


    $(days).on('scrollnext', function () {
        days.removeClass(activeClass);
        $(this).addClass(activeClass);

        if (channelDayContainer.find('[data-day=\'' + $(this).data('day') + '\']').length == 0) {
            $.ajax($(this).data('day-view'), {
                success: function (data) {
                    if (removePast) {
                        channelDayContainer.children().first().remove();
                        streamContainer.scrollLeft(streamContainer.scrollLeft() - 2880);
                    } else {
                        channelDayContainer.width(2880 * 2);
                        timeline.width(2880 * 2);
                        removePast = true;
                    }

                    channelDayContainer.append(data);
                    streamContainer.on('scroll', scrollHandler);
                }
            });
        }
    });

    streamContainer.on('scroll', scrollHandler);
    function scrollHandler() {
        var leftPosition = $(this).scrollLeft();

        if (leftPosition >= upperThreshold) {
            streamContainer.off('scroll', scrollHandler);
            var index = days.index(daysContainer.find('.active'));

            days.eq(index + 1).trigger('scrollnext');
            upperThreshold = 1910 * 2;
        }
    }

    epgNextButton.on('click', changeTab);
    epgPrevButton.on('click', changeTab);

    function changeTab() {
        var active = daysContainer.find('.active');
        var index = days.index(active);

        if ($(this).attr('id') == epgNextButton.attr('id'))
            days.eq(index + 1).trigger('click');

        if ($(this).attr('id') == epgPrevButton.attr('id'))
            days.eq(index - 1).trigger('click');
    }

    $(days).on('click', function () {
        days.removeClass(activeClass);
        $(this).addClass(activeClass);

        $.ajax($(this).data('day-view'), {
            success: function (data) {
                channelDayContainer.children().remove();
                streamContainer.scrollLeft(0);
                removePast = false;
                upperThreshold = 1910;

                channelDayContainer.append(data);
            }
        });
    });
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
    container = $(container);
    var containerPosition = container.scrollLeft();

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
    /*if ($(this).scrollLeft() > globOneDayWidth && dateLeft) {
        $('#epgDateScrollRight').trigger('click');
        dateLeft = false;
    } else if ($(this).scrollLeft() < globOneDayWidth && !dateLeft) {
        $('#epgDateScrollLeft').trigger('click');
        dateLeft = true;
    }*/

    // Resize programmes
    var that = this;
    clearInterval(resizeProgrammesTimeoutId);
    resizeProgrammesTimeoutId = setTimeout(function () {
        resizeProgrammes(that);
    }, 250);
}
