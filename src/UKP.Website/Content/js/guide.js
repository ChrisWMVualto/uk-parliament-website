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
    //epg-date/timepicker
    ////////////////////////////////////////////
    if ($("#epgTimepicker").length) {
        $('#epgTimepicker').timepicker({
            timeSeparator: '.'
        }).on('changeTime.timepicker', changeEpgTime);
    }
    $('.channel-day').on('click', function () {
        //$('.datepicker-dropdown').hide();
        $('.dropdown-menu').hide();
    });

    $('#epgTimepicker').timepicker().on('show.timepicker', function (e) {
        $('.bootstrap-timepicker-widget.dropdown-menu').show();
    });


    ////////////////////////////////////////////
    //epg-day tabs
    ////////////////////////////////////////////
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
        daysContainer: '.days-tab ol',
        days: '.days-tab ol li',
        activeDay: '.days-tab ol .active',
        streamContainer: $('.stream-container-inner'),
        channelDayContainer: $('.channel-day-container'),
        timeline: $('.timeline'),
        epgNextButton: $('#epgDateScrollRight'),
        epgPrevButton: $('#epgDateScrollLeft'),
        liveline: $('.live-now'),
        epgInfoLink: 'a.info',
        epgPopup: '.epg-info'
    };

    var settings = {
        activeClass: '.active',
        activeClassString: 'active',
        todayClass: '.today',
        todayClassString: 'today',
        lowerTheshold: 150,
        upperThreshold: upperThesholdBase,
        baseWidth: 2880,
        centerThreshold: null,
        centerThresholdBase: 2880,

        singleUpperLimit: 1910,
        lowerLimit: 150,
        centerLimit: 2880
    };

    var state = {
        leftTab: true,
        rightTab: false,
        init: true
    };


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
            $(selectors.days).eq(activeTabIndex() + 1).trigger('scrollnext', true);
            settings.centerThreshold = settings.centerThresholdBase;
        }

        if (leftPosition <= settings.lowerLimit) {
            window.console && console.log('Currently active tab: ' + activeTabIndex());

            if (activeTabIndex() > 0) {
                selectors.streamContainer.off('scroll', scrollHandler);
                var tab = $(selectors.days).eq(activeTabIndex() - 1);

                tab.trigger('scrollnext', false);
            }
        }

        if (centerLimit != null) {
            if (leftPosition < centerLimit && !state.leftTab)
                devanceTab();

            if (leftPosition > centerLimit && !state.rightTab)
                advanceTab();
        }
    }

    $(selectors.days).on('scrollnext', scrollnext);
    function scrollnext(event, append) {
        window.console && console.log('Checking for next day: ' + $(this).data('day'));

        if (selectors.channelDayContainer.find('[data-day=\'' + $(this).data('day') + '\']').length == 0) {
            fetchContent({
                removePast: selectors.channelDayContainer.children().length != 1,
                dayUrl: $(this).data('day-view'),
                append: append
            });
        }
    }


    ///
    /// Change Date Buttons
    ///

    selectors.epgNextButton.on('click', changeTab);
    selectors.epgPrevButton.on('click', changeTab);

    function changeTab() {
        if ($(this).attr('id') == selectors.epgNextButton.attr('id'))
            $(selectors.days).eq(activeTabIndex() + 1).trigger('click');

        if ($(this).attr('id') == selectors.epgPrevButton.attr('id'))
            $(selectors.days).eq(activeTabIndex() - 1).trigger('click');
    }

    $(selectors.days).on('click', dayClicked);
    function dayClicked(e) {
        e.preventDefault();

        $(this).trigger('activate');

        fetchContent({
            clear: true,
            removePast: false,
            dayUrl: $(this).data('day-view'),
            resetScroll: true
        });
        loadNewTabBar($(this).data('day'));
    };

    $(".date-picker").on('changeDate', function(event) {
        var date = event.date.toISOString();
        window.console && console.log('Change to datepicker date: ' + date);

        fetchContent({
            clear: true,
            removePast: false,
            dayUrl: selectors.channelDayContainer.data('day-base-url') + '?date=' + date,
            resetScroll: true
        });
        loadNewTabBar(date);
    });


    ///
    /// Tab Activation
    ///

    $(selectors.days).eq(1).addClass(settings.activeClassString).addClass(settings.todayClassString);

    function activeTabIndex() {
        return tabIndex($(selectors.activeDay));
    }

    function tabIndex(tab) {
        return $(selectors.days).index(tab);
    }

    $(selectors.days).on('activate', activateTab);
    function activateTab(e) {
        e.preventDefault();
        window.console && console.log('Activate tab: ' + $(this).data('day'));

        $(selectors.days).removeClass(settings.activeClassString);
        $(this).addClass(settings.activeClassString);
    };

    function advanceTab() {
        window.console && console.log('Advance tab');

        $(selectors.days).eq(activeTabIndex() + 1).trigger('activate');
        state.rightTab = true;
        state.leftTab = false;

        loadNewTab($(selectors.days).last(), false);
    }

    function devanceTab() {
        window.console && console.log('Devance tab');

        $(selectors.days).eq(activeTabIndex() - 1).trigger('activate');
        state.rightTab = false;
        state.leftTab = true;

        loadNewTab($(selectors.days).first(), true);
    }

    function loadNewTab(closestDate, previousDay) {
        $(selectors.days).off('scrollnext', scrollnext);
        $(selectors.days).off('activate', activateTab);
        $(selectors.days).off('click', dayClicked);

        var url = $(selectors.daysContainer).data('day-tab-url');
        url += '?date=' + closestDate.data('day');
        url += '&previousDay=' + previousDay;

        window.console && console.log('Loading new day tab: ' + url);

        $.ajax(url, {
            success: function (data) {
                if (previousDay) {
                    $(selectors.daysContainer).prepend(data);
                    $(selectors.days).last().remove();
                } else {
                    $(selectors.daysContainer).append(data);
                    $(selectors.days).first().remove();
                }

                $(selectors.days).on('scrollnext', scrollnext);
                $(selectors.days).on('activate', activateTab);
                $(selectors.days).on('click', dayClicked);
            }
        });
    }

    function loadNewTabBar(date) {
        $(selectors.days).off('scrollnext', scrollnext);
        $(selectors.days).off('activate', activateTab);
        $(selectors.days).off('click', dayClicked);

        var url = $(selectors.daysContainer).data('date-bar-url');
        url += '?date=' + date;

        window.console && console.log('Loading new date bar: ' + url);
        $.ajax(url, {
            success: function (data) {
                $(selectors.days).remove();
                $(selectors.daysContainer).append($('li', data));
                $(selectors.days).eq(1).addClass(settings.activeClassString);
                liveline();

                $(selectors.days).on('scrollnext', scrollnext);
                $(selectors.days).on('activate', activateTab);
                $(selectors.days).on('click', dayClicked);
            }
        });
    }


    ///
    /// Fetch Content
    ///

    function fetchContent(options) {
        var opts = $.extend({
            clear: false,
            append: true,
            removePast: true,
            dayUrl: null,
            callback: null,
            resetScroll: false
        }, options);

        selectors.streamContainer.off('scroll', scrollHandler);

        $.ajax(opts.dayUrl, {
            success: function (data) {
                window.console && console.log('Load day:' + opts.dayUrl);


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
                else if (opts.removePast || (!opts.append && !opts.removePast))
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
                $(selectors.epgInfoLink).on('click', showEpgInfo);
            }
        });
    }

    function liveline() {
        window.console && console.log('Checking live line status');

        if (selectors.channelDayContainer.children().first().data('day') == $(selectors.days).find(settings.todayClass).data('day'))
            selectors.liveline.show();

        else
            selectors.liveline.hide();
    }


    ///
    /// EPG Info Popup
    ///
    $(selectors.epgInfoLink).on('click', showEpgInfo);
    function showEpgInfo(e) {
        e.stopPropagation();
        
        $.ajax($(this).parents('li').data('epg-info'), {
            success: function (model) {
                $(selectors.epgPopup).remove();
                $('.stream-container-outer').append(model);
                $(selectors.epgPopup).hide();
                $(window).trigger('forcescroll', selectors.streamContainer);
                $(selectors.epgPopup).fadeIn(100);
            }
        });

        $(document).one('click', '[data-hide]', function () {
            $("." + $(this).data("hide")).fadeOut(100);
        });
    };
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
        freezeFrom = $('.channel-18'),
        infoPopup = '#epgInfoPopup';


    $(window).on('scroll', function () {
        if ($(window).scrollTop() >= 0) {
            epg.hide();
            timeline.hide();
            $(infoPopup).hide();
            clearInterval(triggerFloatTimeoutId);
            triggerFloatTimeoutId = setTimeout(triggerFloat, 250);
        }
    });

    $(window).on('forcescroll', function() {
        if ($(window).scrollTop() >= 0) {
            triggerFloat();
        }
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
            container.addClass(fixedClass);

            epg.css({
                'position': 'absolute',
                'top': datesOffset(freezeMenu())
            });
            timeline.css({
                'top': timesOffset(freezeMenu())
            });


            if ($('.epg-info').length) {
                $('body:not(.breakpoint-300) .epg-info').css({
                    'top': epgInfoOffset(freezeMenu()),
                    'height': null
                });

                $('body.breakpoint-300 .epg-info').css({
                    'top': null
                });
                $(infoPopup).show();
                $('body.breakpoint-300 .epg-info').css({
                    'height': window.screen.height - $('.epg-info').position().top
                });
            }
        } else {
            container.removeClass(fixedClass);

            epg.css({
                'position': 'relative',
                'top': 0
            });
            timeline.css({
                'top': 0
            });

            if ($('.epg-info').length) {
                $('.epg-info').css({
                    'top': null
                });
                $('body:not(.breakpoint-300) .epg-info').css({
                    'top': $('.timeline').height(),
                    'height': null
                });

                $(infoPopup).show();
                $('body.breakpoint-300 .epg-info').css({
                    'height': window.screen.height - $('.epg-info').position().top
                });
            }
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
    var containerPosition = container.scrollLeft(),
        offset = $('.channels').width() + 40;

    $.each(container.find('ol.channel-list'), function () {
        $.each($(this).children('li'), function () {
            calculateMove(this);
        });
    });

    function calculateMove(context) {
        var leftPos = $(context).position().left + $(context).parent().parent().position().left;

        if (leftPos <= containerPosition) {
            var margin = containerPosition - leftPos;

            if (width($(context).innerWidth(), margin) <= offset) {
                margin = $(context).innerWidth() - offset;
            }

            moveTo(margin, context);
        } else
            moveTo(0, context);
    }

    function moveTo(amount, context) {
        if ($(context).outerWidth() > offset) {
            $(context).find('.outer .inner').animate({
                'marginLeft': amount
            }, 500, 'swing', function () {
                if (width($(context).innerWidth(), amount) <= offset) {
                    $(context).addClass('event');
                }
                if (width($(context).innerWidth(), amount) > offset) {
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
