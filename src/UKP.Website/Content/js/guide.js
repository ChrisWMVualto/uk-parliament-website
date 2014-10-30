﻿var globHourWidth = 120,
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

    $('.stream-container-inner').on('scroll', triggerProgramResize);

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
    var upperThesholdBase = 1910;

    var selectors = {
        daysContainer: $('.days-tab ol'),
        days: $('.days-tab ol').find('li'),
        streamContainer: $('.stream-container-inner'),
        channelDayContainer: $('.channel-day-container'),
        timeline: $('.timeline'),
        epgNextButton: $('#epgDateScrollRight'),
        epgPrevButton: $('#epgDateScrollLeft'),
        liveline: $('.live-now')
    }

    var settings = {
        activeClass: '.active',
        activeClassString: 'active',
        lowerTheshold: 150,
        upperThreshold: upperThesholdBase,
        baseWidth: 2880,
        centerThreshold: null,
        centerThresholdBase: 2880,

        singleUpperLimit: 1910,
        lowerLimit: 150,
        centerLimit: 2880
    }

    var state = {
        leftTab: true,
        rightTab: false,
        init: true
}


    ///
    /// Scrolling Behavior
    ///

    function daysLoaded() {
        return selectors.channelDayContainer.children().length;
    }

    selectors.streamContainer.on('scroll', scrollHandler);
    function scrollHandler() {
        var leftPosition = $(this).scrollLeft();
        var upperLimit = daysLoaded() > 1 ? settings.singleUpperLimit * 2 : settings.singleUpperLimit;
        var centerLimit = daysLoaded() > 1 ? settings.centerLimit : null;

        if (leftPosition >= upperLimit) {
            window.console && console.log('Currently active tab: ' + activeTabIndex());

            selectors.streamContainer.off('scroll', scrollHandler);
            selectors.days.eq(activeTabIndex() + 1).trigger('scrollnext', true);
            settings.centerThreshold = settings.centerThresholdBase;
        }


        if (leftPosition <= settings.lowerLimit) {
            window.console && console.log('Currently active tab: ' + activeTabIndex());

            if (activeTabIndex() > 0) {
                selectors.streamContainer.off('scroll', scrollHandler);
                selectors.days.eq(activeTabIndex() - 1).trigger('scrollnext', false);
            }
        }

        if (centerLimit != null) {
            if (leftPosition < centerLimit && !state.leftTab)
                devanceTab();

            if (leftPosition > centerLimit && !state.rightTab)
                advanceTab();
        }
    }

    selectors.days.on('scrollnext', function (event, append) {
        if (selectors.channelDayContainer.find('[data-day=\'' + $(this).data('day') + '\']').length == 0) {
            fetchContent({
                removePast: selectors.channelDayContainer.children().length != 1,
                day: $(this),
                append: append
            });
        }
    });


    ///
    /// Change Date Buttons
    ///

    selectors.epgNextButton.on('click', changeTab);
    selectors.epgPrevButton.on('click', changeTab);

    function changeTab() {
        if ($(this).attr('id') == selectors.epgNextButton.attr('id'))
            selectors.days.eq(activeTabIndex() + 1).trigger('click');

        if ($(this).attr('id') == selectors.epgPrevButton.attr('id'))
            selectors.days.eq(activeTabIndex() - 1).trigger('click');
    }

    selectors.days.on('click', function () {
        selectors.streamContainer.off('scroll', scrollHandler);
        $(this).trigger('activate');

        fetchContent({
            clear: true,
            removePast: false,
            day: $(this),
            resetScroll: true
        });
    });


    ///
    /// Tab Activation
    ///

    function activeTabIndex() {
        return tabIndex(selectors.daysContainer.find(settings.activeClass));
    }

    function tabIndex(tab) {
        return selectors.days.index(selectors.daysContainer.find(settings.activeClass));
    }

    selectors.days.on('activate', function () {
        window.console && console.log('Activate tab: ' + $(this).data('day'));

        selectors.days.removeClass(settings.activeClassString);
        $(this).addClass(settings.activeClassString);
    });

    function advanceTab() {
        window.console && console.log('Advance tab');

        selectors.days.eq(activeTabIndex() + 1).trigger('activate');
        state.rightTab = true;
        state.leftTab = false;
    }

    function devanceTab() {
        window.console && console.log('Devance tab');

        if ((activeTabIndex() - 1) >= 0)
        {
            selectors.days.eq(activeTabIndex() - 1).trigger('activate');
            state.rightTab = false;
            state.leftTab = true;
        }
    }


    ///
    /// Fetch Content
    ///

    function fetchContent(options) {
        var opts = $.extend({
            clear: false,
            append: true,
            removePast: true,
            day: null,
            callback: null,
            resetScroll: false
        }, options);

        $.ajax(opts.day.data('day-view'), {
            success: function (data) {
                window.console && console.log('Load day:' + opts.day.data('day-view'));


                if (opts.clear)
                    selectors.channelDayContainer.children().remove();


                if (opts.removePast && opts.append)
                    selectors.channelDayContainer.children().first().remove();
                else if (opts.removePast)
                    selectors.channelDayContainer.children().last().remove();


                if (opts.append)
                    selectors.channelDayContainer.append(data);
                else
                    selectors.channelDayContainer.prepend(data);


                window.console && console.log('Setting channel day container width ' + (settings.baseWidth * daysLoaded()));
                selectors.channelDayContainer.width(settings.baseWidth * daysLoaded());
                selectors.timeline.width((settings.baseWidth * daysLoaded()) + 40);


                if (opts.resetScroll)
                    selectors.streamContainer.scrollLeft(0);
                else if (opts.removePast && opts.append)
                    selectors.streamContainer.scrollLeft(selectors.streamContainer.scrollLeft() - settings.baseWidth);
                else if (opts.removePast)
                    selectors.streamContainer.scrollLeft(selectors.streamContainer.scrollLeft() + settings.baseWidth);
                else if (!state.init)
                    selectors.streamContainer.scrollLeft(settings.baseWidth);


                if ((opts.append && opts.removePast) || opts.clear) {
                    state.leftTab = true;
                    state.rightTab = false;
                } else if (opts.removePast || (!opts.append && !opts.removePast)) {
                    state.leftTab = false;
                    state.rightTab = true;
                }

                if (typeof opts.callback === 'function')
                    opts.callback(data, opts);

                state.init = false;

                liveline();
                selectors.streamContainer.on('scroll', scrollHandler);
            }
        });
    }

    function liveline() {
        window.console && console.log('Checking live line status');

        if (selectors.channelDayContainer.children().first().data('day') == selectors.days.first().data('day'))
            selectors.liveline.show();

        else
            selectors.liveline.hide();
    }
}

////////////////////////////////////////////
//Controls the floating EPG dates and times
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
        var leftPos = $(context).position().left + $(context).parent().parent().position().left;

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
function triggerProgramResize() {
    var that = this;
    clearInterval(resizeProgrammesTimeoutId);
    resizeProgrammesTimeoutId = setTimeout(function () {
        resizeProgrammes(that);
    }, 250);
}