var globHourWidth = 120,
    globOneDayWidth = 2920,
    resizing = false;

$(document).ready(function () {
    ////////////////////////////////////////////
    // Force bootstrap to release button focus state after click
    ////////////////////////////////////////////
    $('.btn').mouseup(function () {
        $(this).blur();
    });

    ////////////////////////////////////////////
    //epg-date/timepicker
    ////////////////////////////////////////////
    if ($("#epgTimepicker").length) {
        $('#epgTimepicker').timepicker({
            timeSeparator: '.'
        }).on('changeTime.timepicker', changeEpgTime);
    }
    $('.channel-day').on('click', function () {
        $('button').blur();
        $('.dropdown-menu').hide();
        $('.bootstrap-timepicker-widget.dropdown-menu').hide();
    });

    $('#epgTimepicker').timepicker().on('show.timepicker', function (e) {
        $('.bootstrap-timepicker-widget.dropdown-menu').show();
    });


    ////////////////////////////////////////////
    //epg-day tabs
    ////////////////////////////////////////////
    new changeDateTab();


    ////////////////////////////////////////////
    //epg-x-axis-movement
    ////////////////////////////////////////////
    $('.stream-container-inner').scrollLeft($('.live-now').data('start-position'));
    resizeProgrammes($('.stream-container-inner'));

    $('.stream-container-inner').on('scroll', triggerProgramResize);


    ////////////////////////////////////////////
    //EPG Time Scroll
    ////////////////////////////////////////////
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
//Change the highlighted date tab
////////////////////////////////////////////
function changeDateTab() {
    this.selectors = {
        daysContainer: '.days-tab ol',
        days: '.days-tab ol li',
        streamContainer: '.stream-container-inner',
        channelDayContainer: '.channel-day-container',
        timeline: '.timeline',
        liveline: '.live-now',
        epgInfoLink: 'a.info',
        epgPopup: '.epg-info',
        leftDayButton: '#epgDateScrollLeft',
        rightDayButton: '#epgDateScrollRight',
        channelList: '.channel-list',
        datepicker: ".date-picker"
    }

    this.settings = {
        activeClass: 'active',
        todayClass: 'today',

        baseWidth: 2880,
        limits: {
            upper: 1880,
            lower: 500,
            center: 2880
        }
    }

    this.state = {
        leftTab: true,
        rightTab: true
    }

    this.init();
}

changeDateTab.prototype = {
    init: function () {
        $(this.selectors.days).eq(1).addClass(this.settings.activeClass).addClass(this.settings.todayClass);
        this.setIndexes();

        this.selectors.clickAndDrag = $(this.selectors.streamContainer).dragscrollable({
            dragSelector: '*',
            allowY: false
        });

        window.console && console.log('Initial Event Bindings');
        $(this.selectors.streamContainer).on('scroll', $.proxy(this.scrollHandler, this));
        $(this.selectors.days).on('scrollnext', $.proxy(this.scrollnext, this));
        $(this.selectors.leftDayButton).on('click', $.proxy(this.changeTab, this));
        $(this.selectors.rightDayButton).on('click', $.proxy(this.changeTab, this));
        $(this.selectors.days).on('click', $.proxy(this.dayClicked, this));
        $(this.selectors.days).on('activate', $.proxy(this.activateTab, this));
        $(this.selectors.epgInfoLink).on('click', $.proxy(this.showEpgInfo, this));
        $(this.selectors.datepicker).on('changeDate', $.proxy(this.datepickerChange, this));
        this.pollPosition();
    },

    datepickerChange: function (event) {
        window.console && console.log('===> Date Selected');
        var date = this.stripTimezone(event.date).toISOString();
        var that = this;

        this.fetchContent({
            clear: true,
            removePast: false,
            dayUrl: $(this.selectors.channelDayContainer).data('day-base-url') + '?date=' + date,
            resetScroll: true,
            callback: function () {
                that.loadNewTabBar(date);
            }
        });
    },

    stripTimezone: function (date) {
        return new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), 0, 0, 0, 0));
    },

    pollPosition: function () {
        var that = this;

        window.setTimeout(function () {
            if (!resizing)
                that.scrollHandler();

            that.pollPosition();
        }, 500);
    },

    ///
    /// Scrolling Behavior
    ///

    scrollHandler: function (e) {

        if (typeof e === 'object')
            e.preventDefault();

        var numDaysLoaded = $(this.selectors.channelDayContainer).children().length;
        var leftPosition = $(this.selectors.streamContainer).scrollLeft();
        var upperLimit = numDaysLoaded > 1 ? this.settings.limits.upper * 2 : this.settings.limits.upper;
        var centerLimit = numDaysLoaded > 1 ? this.settings.limits.center : null;

        if (leftPosition < 0)
            return;

        if (leftPosition >= upperLimit && this.state.rightTab) {
            window.console && console.log('Trigger scrollnext');
            $(this.selectors.days).eq(this.activeTabIndex() + 1).trigger('scrollnext', [true, this]);
        }

        if ((leftPosition <= this.settings.limits.lower && this.state.leftTab) && this.activeTabIndex() > 0) {
            window.console && console.log('Trigger scrollnext');
            $(this.selectors.days).eq(this.activeTabIndex() - 1).trigger('scrollnext', [false, this]);
        }

        if (centerLimit != null) {
            if (leftPosition < centerLimit && this.state.rightTab)
                this.devanceTab();

            if (leftPosition > centerLimit && this.state.leftTab)
                this.advanceTab();
        }
    },

    disableTouch: function (e) {
        e.preventDefault();
    },

    scrollnext: function (e, append) {
        if ($(this.selectors.channelDayContainer).find('[data-day=\'' + $(e.target).data('day') + '\']').length == 0) {
            this.fetchContent({
                removePast: $(this.selectors.channelDayContainer).children().length != 1,
                dayUrl: $(e.target).data('day-view'),
                append: append
            });
        }
    },


    ///
    /// Change Date Buttons
    ///

    changeTab: function (e) {
        var tab = $(this.selectors.days),
            index = this.activeTabIndex(),
            clickedId = $(e.target.parentElement).attr('id');

        if (typeof clickedId === 'undefined')
            clickedId = $(e.target.parentElement).find('button').attr('id');

        if (clickedId == this.selectors.rightDayButton.replace('#', ''))
            index++;

        else
            index--;

        tab.eq(index).trigger('click');
    },

    dayClicked: function (e) {
        var day = $(e.target),
            that = this;
        while (typeof day.data('day') === 'undefined')
            day = day.parent();

        this.fetchContent({
            clear: true,
            removePast: false,
            dayUrl: day.data('day-view'),
            resetScroll: true,
            callback: function () {
                that.loadNewTabBar(day.data('day'));
            }
        });
    },


    ///
    /// Tab Activation
    ///

    activeTabIndex: function () {
        return $(this.selectors.days).index($(this.selectors.days + ('.' + this.settings.activeClass)));
    },

    activateTab: function (e) {
        e.preventDefault();

        var day = $(e.target);
        while (typeof day.data('day') === 'undefined')
            day = day.parent();

        $(this.selectors.days).removeClass(this.settings.activeClass);
        day.addClass(this.settings.activeClass);
    },

    advanceTab: function () {
        window.console && console.log('Trigger day tab click');
        $(this.selectors.days).eq(this.activeTabIndex() + 1).trigger('activate', this);
        this.state.rightTab = true;
        this.state.leftTab = false;

        this.loadNewTab($(this.selectors.days).last(), false);
    },

    devanceTab: function () {
        window.console && console.log('Trigger day tab click');
        $(this.selectors.days).eq(this.activeTabIndex() - 1).trigger('activate', this);
        this.state.rightTab = false;
        this.state.leftTab = true;

        this.loadNewTab($(this.selectors.days).first(), true);
    },

    loadNewTab: function (closestDate, previousDay) {
        window.console && console.log('Disable scrollnext');
        $(this.selectors.days).off('scrollnext', this.scrollnext);

        window.console && console.log('Disable tab activate');
        $(this.selectors.days).off('activate', this.activateTab);

        window.console && console.log('Disable day click');
        $(this.selectors.days).off('click', this.dayClicked);

        var url = $(this.selectors.daysContainer).data('day-tab-url');
        url += '?date=' + closestDate.data('day');
        url += '&previousDay=' + previousDay;

        var that = this;
        $.ajax(url, {
            success: function (data) {
                if (previousDay) {
                    $(that.selectors.daysContainer).prepend(data);
                    $(that.selectors.days).last().remove();
                } else {
                    $(that.selectors.daysContainer).append(data);
                    $(that.selectors.days).first().remove();
                }
            },
            complete: function () {
                window.console && console.log('Enable scrollnext');
                $(that.selectors.days).on('scrollnext', $.proxy(that.scrollnext, that));

                window.console && console.log('Enable day tab activate');
                $(that.selectors.days).on('activate', $.proxy(that.activateTab, that));

                window.console && console.log('Enable day tab click');
                $(that.selectors.days).on('click', $.proxy(that.dayClicked, that));

                that.liveline();
            }
        });
    },

    loadNewTabBar: function (date) {
        window.console && console.log('Disable scrollnext');
        $(this.selectors.days).off('scrollnext', this.scrollnext);

        window.console && console.log('Disable day tab activate');
        $(this.selectors.days).off('activate', this.activateTab);

        window.console && console.log('Disable day tab click');
        $(this.selectors.days).off('click', this.dayClicked);

        var url = $(this.selectors.daysContainer).data('date-bar-url');
        url += '?date=' + date;

        var that = this;
        $.ajax(url, {
            success: function (data) {
                $(that.selectors.days).remove();
                $(that.selectors.daysContainer).append($('li', data));
                $(that.selectors.days).eq(1).addClass(that.settings.activeClass);
                that.liveline();
            },
            error: function () {
                window.console && console.log('Enable day tab click');
                $(that.selectors.days).on('activate', $.proxy(that.activateTab, that));
                $(that.selectors.days).eq(that.activeTabIndex() + 1).trigger('activate', that);
            },
            complete: function () {
                window.console && console.log('Enable day tab activate');
                $(that.selectors.days).on('activate', $.proxy(that.activateTab, that));

                window.console && console.log('Enable scrollnext');
                $(that.selectors.days).on('scrollnext', $.proxy(that.scrollnext, that));

                window.console && console.log('Enable day tab click');
                $(that.selectors.days).on('click', $.proxy(that.dayClicked, that));
                that.selectors.clickAndDrag.start();

                that.liveline();
            }
        });
    },


    ///
    /// Fetch Content
    ///

    fetchContent: function (options) {
        var opts = $.extend({
            clear: false,
            append: true,
            removePast: true,
            dayUrl: null,
            callback: null,
            resetScroll: false
        }, options);


        window.console && console.log('Disable tab click');
        $(this.selectors.days).on('click', $.proxy(this.dayClicked, this));

        window.console && console.log('Disable tab activate');
        $(this.selectors.days).on('activate', $.proxy(this.activateTab, this));

        window.console && console.log('Disable scroll');
        $(this.selectors.streamContainer).off('scroll', this.scrollHandler);

        window.console && console.log('Disable scrollnext');
        $(this.selectors.days).off('scrollnext', this.scrollnext);

        window.console && console.log('Disable click-and-drag');
        this.selectors.clickAndDrag.stop();
        var that = this;
        $.ajax(opts.dayUrl, {
            success: function (data) {
                window.console && console.log('Load day:' + opts.dayUrl);


                if (opts.clear)
                    $(that.selectors.channelDayContainer).children().remove();


                if (opts.removePast && opts.append)
                    $(that.selectors.channelDayContainer).children().first().remove();
                else if (opts.removePast)
                    $(that.selectors.channelDayContainer).children().last().remove();


                if (opts.append)
                    $(that.selectors.channelDayContainer).append(data);
                else
                    $(that.selectors.channelDayContainer).prepend(data);


                var numDaysLoaded = $(that.selectors.channelDayContainer).children().length;
                $(that.selectors.channelDayContainer).width(that.settings.baseWidth * numDaysLoaded);
                $(that.selectors.timeline).width((that.settings.baseWidth * numDaysLoaded) + 40);

                if ((opts.append && opts.removePast) || opts.clear || (opts.append && !opts.removePast)) {
                    that.state.leftTab = true;
                    that.state.rightTab = false;
                } else if (opts.removePast || (!opts.append && !opts.removePast)) {
                    that.state.leftTab = false;
                    that.state.rightTab = true;
                }

                window.console && console.log('Enable disableTouch');
                $(document).on('touchstart', $.proxy(that.disableTouch, that));
                var scrollAmount;

                if (opts.resetScroll)
                    scrollAmount = leftPositionFromTime(8, 0);

                else if (opts.removePast && opts.append)
                    scrollAmount = $(that.selectors.streamContainer).scrollLeft() - that.settings.baseWidth;

                else if ((opts.removePast && !opts.append) || (!opts.append && !opts.removePast))
                    scrollAmount = $(that.selectors.streamContainer).scrollLeft() + that.settings.baseWidth;

                else
                    scrollAmount = $(that.selectors.streamContainer).scrollLeft();

                // More stable than jQuery method
                document.getElementsByClassName('stream-container-inner')[0].scrollLeft = scrollAmount;

                // Force iOS/Android to redraw
                $(that.selectors.streamContainer).hide().show(0);

                window.console && console.log('Disable disableTouch');
                $(document).off('touchstart', that.disableTouch);
                that.setIndexes();

                $(that.selectors.channelList).on('click', function () {
                    $('.datepicker-dropdown').remove();
                });
            },
            error: function () {
                that.state.leftTab = false;
                that.state.rightTab = true;
            },
            complete: function (response) {
                window.console && console.log('Enable scroll');
                $(that.selectors.streamContainer).on('scroll', $.proxy(that.scrollHandler, that));

                window.console && console.log('Enable click-and-drag');
                that.selectors.clickAndDrag.start();

                window.console && console.log('Enable EPG Info');
                $(that.selectors.epgInfoLink).on('click', $.proxy(that.showEpgInfo, that));

                window.console && console.log('Enable scrollnext');
                $(that.selectors.days).on('scrollnext', $.proxy(that.scrollnext, that));

                window.console && console.log('Enable tab click');
                $(that.selectors.days).on('click', $.proxy(that.dayClicked, that));

                window.console && console.log('Enable tab activate');
                $(that.selectors.days).on('activate', $.proxy(that.activateTab, that));

                $(window).trigger('forcescroll');

                if (typeof opts.callback === 'function' && response.status == 200)
                    opts.callback();
            }
        });
    },

    liveline: function () {
        if ($(this.selectors.days + '.' + this.settings.activeClass + '.' + this.settings.todayClass).length)
            $(this.selectors.liveline).show();

        else
            $(this.selectors.liveline).hide();
    },

    setIndexes: function () {
        var days = $(this.selectors.channelDayContainer).children();

        $(days[0]).css('zIndex', 20);
        $(days[1]).css('zIndex', 10);
    },

    ///
    /// EPG Info Popup
    ///

    showEpgInfo: function (e) {
        e.preventDefault();
        e.stopPropagation();
        var that = this;

        $.ajax($(e.target).parents('li').data('epg-info'), {
            success: function (model) {
                $(that.selectors.epgPopup).remove();
                $('.stream-container-outer').append(model);
                $(that.selectors.epgPopup).hide();
                $(window).trigger('forcescroll', $(that.selectors.streamContainer));
            }
        });

        $(document).one('click', '[data-hide]', function () {
            $("." + $(this).data("hide")).fadeOut(100).remove();
        });
    }
}

////////////////////////////////////////////
//Controls the floating EPG dates and times
////////////////////////////////////////////
window.floatingNavScrollTimeoutId = null;

function FloatingNav() {
    this.selectors = {
        body: 'body',
        mainContainer: '.main-container',
        epgOuter: '.epg-outer',
        timeline: '.timeline',
        headerMain: '.header-main div',
        titleContainer: '.title-container',
        streamContainerInner: '.stream-container-inner',
        epgInfo: '.epg-info',
        channelDay: '.channel-day'
    }

    this.breakpoints = {
        300: 'breakpoint-300',
        768: 'breakpoint-768'
    }

    this.queryClasses = {
        'fixed': 'fixed-epg'
    }

    $(window).on('scroll', $.proxy(this.scrollEvent, this));
    $(window).on('forcescroll', $.proxy(this.forcescrollEvent, this));

    var orientationEvent = window.onorientationchange !== 'undefined' ? 'resize' : 'orientationchange';
    $(window).on(orientationEvent, function () {
        $(window).trigger('forcescroll');
    });
}

FloatingNav.prototype = {
    scrollEvent: function () {
        if ($(window).scrollTop() >= 5) {
            this.hideFloatingItems();
            clearInterval(window.floatingNavScrollTimeoutId);
            window.floatingNavScrollTimeoutId = setTimeout($.proxy(this.triggerFloat, this), 500);
        } else {
            this.epgTop();
            this.showFloatingItems();
        }
    },
    forcescrollEvent: function () {
        if ($(window).scrollTop() >= 5) {
            this.triggerFloat();
        } else {
            this.epgTop();
            this.showFloatingItems();
        }
    },
    hideFloatingItems: function () {
        $(this.selectors.epgOuter).hide();
        $(this.selectors.timeline).hide();

        if ($(this.selectors.epgInfo).length)
            $(this.selectors.epgInfo).hide();
    },
    showFloatingItems: function () {
        $(this.selectors.epgOuter).show();
        $(this.selectors.timeline).show();

        if ($(this.selectors.epgInfo).length)
            $(this.selectors.epgInfo).show();
    },
    datesOffset: function () {
        return ($(window).scrollTop() - $(this.selectors.epgOuter).parent().offset().top) + $(this.selectors.headerMain).outerHeight();
    },
    timesOffset: function () {
        return this.datesOffset() + 109;
    },
    triggerFloat: function () {
        this.applyStyles($(window).scrollTop() >= $(this.selectors.titleContainer).height());
    },
    applyStyles: function (isActive) {
        $(this.selectors.streamContainerInner).css({
            'paddingTop': $(this.selectors.timeline).height()
        });

        if (isActive) {
            $(this.selectors.mainContainer).addClass(this.queryClasses.fixed);

            $(this.selectors.epgOuter).css({
                'position': 'absolute',
                'top': this.datesOffset()
            });
            $(this.selectors.channelDay).css({
                'top': 2
            });
            $(this.selectors.timeline).css({
                'top': this.timesOffset()
            });

            if ($(this.selectors.epgInfo).length) {
                var infoTop = this.timesOffset() + $(this.selectors.timeline).height(),
                    infoHeight = null,
                    position = 'absolute',
                    maxHeight = null;

                if ($(this.selectors.body).hasClass(this.breakpoints[300])) {
                    infoTop = $(this.selectors.headerMain).height() + $(this.selectors.epgOuter).height();
                    infoHeight = $(window).height() - infoTop;
                    position = '';
                    maxHeight = 350;
                }

                if ($(this.selectors.body).hasClass(this.breakpoints[768])) {
                    infoTop = (parseInt($(this.selectors.epgOuter).css('top')) + 200) - $(this.selectors.timeline).height();
                    position = '';
                    maxHeight = 350;
                }

                $(this.selectors.epgInfo).css({
                    'top': infoTop,
                    'position': position,
                    'marginLeft': '',
                    'marginRight': ''
                });

                if ($(this.selectors.body).hasClass(this.breakpoints[768])) {
                    var windowHeight;
                    if (window.screen)
                        windowHeight = window.screen.availHeight;
                    else
                        windowHeight = window.height();

                    $(this.selectors.epgInfo).show();
                    infoHeight = $(this.selectors.epgInfo)[0].getBoundingClientRect().top - $(window).height();
                    $(this.selectors.epgInfo).hide();
                }

                infoHeight = infoHeight < 0 ? infoHeight * -1 : infoHeight;
                $(this.selectors.epgInfo).css({
                    'height': infoHeight,
                    'maxHeight': maxHeight
                });
            }
        } else {
            $.proxy(this.epgTop, this);
        }

        this.showFloatingItems();
    },
    epgTop: function () {
        $(this.selectors.mainContainer).removeClass(this.queryClasses.fixed);

        $(this.selectors.epgOuter).css({
            'position': 'relative',
            'top': 0
        });
        $(this.selectors.timeline).css({
            'top': 0
        });
        $(this.selectors.channelDay).css({
            'top': 2
        });

        if ($(this.selectors.epgInfo).length) {
            var infoTop = $(this.selectors.timeline).height(),
                infoHeight = null;

            $(this.selectors.epgInfo).css({
                'top': infoTop,
                'height': infoHeight,
                'position': 'absolute'
            });

            if ($(this.selectors.body).hasClass(this.breakpoints[300])) {
                $(this.selectors.epgInfo).css({
                    'marginLeft': 0,
                    'marginRight': 0
                });
            } else {
                $(this.selectors.epgInfo).css({
                    'marginLeft': '',
                    'marginRight': ''
                });
            }

            $(this.selectors.epgInfo).show();
        }
    }
}
new FloatingNav();

////////////////////////////////////////////
//Resizes the programmes to keep them on screen
////////////////////////////////////////////
function resizeProgrammes(container) {
    container = $(container);
    var containerPosition = container.scrollLeft(),
        minWidth = 160;

    $.each(container.find('ol.channel-list'), function () {
        $.each($(this).children('li'), function () {
            calculateMove(this);
        });
    });

    function calculateMove(context) {
        var cnxtLeftPos = $(context).position().left + $(context).parent(1).position().left;

        if (containerPosition >= cnxtLeftPos) {
            var targetMargin = containerPosition - cnxtLeftPos,
                widthResult = width($(context).innerWidth(), targetMargin);

            if (widthResult <= minWidth)
                targetMargin = $(context).innerWidth() - minWidth;

            moveTo(targetMargin, context);
            return;
        }

        moveTo(0, context);
    }

    function moveTo(amount, context) {
        if ($(context).outerWidth() > minWidth) {
            $(context).find('.outer .inner').animate({
                'marginLeft': amount
            },
                500,
                'swing',
                function () {
                    if (width($(context).innerWidth(), amount) <= minWidth)
                        $(context).addClass('event');

                    if (width($(context).innerWidth(), amount) > minWidth)
                        $(context).removeClass('event');
                }
            );
        }
    }

    function width(inWidth, inMargin) {
        return inWidth - inMargin;
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
        resizing = true;
        resizeProgrammes(that);
        resizing = false;
    }, 50);
}