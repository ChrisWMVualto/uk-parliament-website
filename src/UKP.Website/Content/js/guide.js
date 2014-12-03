var globHourWidth = 120,
    globOneDayWidth = 2920,
    resizing = false;

$(document).ready(function () {
    $('.btn').mouseup(function() {
        $(this).blur();
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

    $('.channel-day-container').on('click', function () {
        $('.datepicker').datepicker('hide');
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

    if ($('.epg-outer').length > 0)
        floatingNav();


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
        epgNextButton: '#epgDateScrollRight',
        epgPrevButton: '#epgDateScrollLeft',
        liveline: '.live-now',
        epgInfoLink: 'a.info',
        epgPopup: '.epg-info',
        leftDayButton: '#epgDateScrollLeft',
        rightDayButton: '#epgDateScrollRight'
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
            dragSelector: '*'
        });

        window.console && console.log('Initial Event Bindings');
        $(this.selectors.streamContainer).on('scroll', $.proxy(this.scrollHandler, this));
        $(this.selectors.days).on('scrollnext', $.proxy(this.scrollnext, this));
        $(this.selectors.epgNextButton).on('click', $.proxy(this.changeTab, this));
        $(this.selectors.epgPrevButton).on('click', $.proxy(this.changeTab, this));
        $(this.selectors.days).on('click', $.proxy(this.dayClicked, this));
        $(this.selectors.days).on('activate', $.proxy(this.activateTab, this));
        $(this.selectors.epgInfoLink).on('click', $.proxy(this.showEpgInfo, this));
        $(".date-picker").on('changeDate', $.proxy(this.datepickerChange, this));
        $(this.selectors.leftDayButton).on('click', $.proxy(this.devanceTab, this));
        $(this.selectors.rightDayButton).on('click', $.proxy(this.advanceTab, this));
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
            callback: function() {
                that.loadNewTabBar(date);
            }
        });
    },

    stripTimezone: function(date) {
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


        //window.console && console.log('Left position of ' + leftPosition + ' is being compared to a upper limit of ' + upperLimit + ' and a lower limit of ' + this.settings.limits.lower);

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

    disableTouch: function(e) {
        e.preventDefault();
    },

    scrollnext: function(e, append) {
        //window.console && console.log('Checking for next day: ' + $(e.target).data('day'));

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

    changeTab: function(e) {
        if ($(e.target.parentElement).attr('id') == $(this.selectors.epgNextButton).attr('id')) {
            window.console && console.log('Trigger day tab click');
            $(this.selectors.days).eq(this.activeTabIndex() + 1).trigger('click', this);
        }

        if ($(e.target.parentElement).attr('id') == $(this.selectors.epgPrevButton).attr('id')) {
            window.console && console.log('Trigger day tab click');
            $(this.selectors.days).eq(this.activeTabIndex() - 1).trigger('click', this);
        }
    },

    dayClicked: function(e) {
        e.preventDefault();

        var day = $(e.target),
            that = this;
        while (typeof day.data('day') === 'undefined')
            day = day.parent();

        this.fetchContent({
            clear: true,
            removePast: false,
            dayUrl: day.data('day-view'),
            resetScroll: true,
            callback: function() {
                that.loadNewTabBar(day.data('day'));
            }
        });
    },


    ///
    /// Tab Activation
    ///

    activeTabIndex: function() {
        return $(this.selectors.days).index($(this.selectors.days + ('.' + this.settings.activeClass)));
    },
    
    activateTab: function(e) {
        e.preventDefault();

        var day = $(e.target);
        while (typeof day.data('day') === 'undefined')
            day = day.parent();

        //window.console && console.log('Activate tab: ' + day.data('day'));

        $(this.selectors.days).removeClass(this.settings.activeClass);
        day.addClass(this.settings.activeClass);
    },

    advanceTab: function() {
        //window.console && console.log('Advance tab');

        window.console && console.log('Trigger day tab click');
        $(this.selectors.days).eq(this.activeTabIndex() + 1).trigger('activate', this);
        this.state.rightTab = true;
        this.state.leftTab = false;

        this.loadNewTab($(this.selectors.days).last(), false);
    },

    devanceTab: function() {
        //window.console && console.log('Devance tab');

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

        //window.console && console.log('Loading new day tab: ' + url);

        var that = this;
        $.ajax(url, {
            success: function(data) {
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

        //window.console && console.log('Loading new date bar: ' + url);
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
            }
        });
    },


    ///
    /// Fetch Content
    ///

    fetchContent: function(options) {
        var opts = $.extend({
            clear: false,
            append: true,
            removePast: true,
            dayUrl: null,
            callback: null,
            resetScroll: false
        }, options);

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
                //window.console && console.log('Set channel day container width ' + (that.settings.baseWidth * numDaysLoaded));

                if ((opts.append && opts.removePast) || opts.clear || (opts.append && !opts.removePast)) {
                    that.state.leftTab = true;
                    that.state.rightTab = false;
                } else if (opts.removePast || (!opts.append && !opts.removePast)) {
                    that.state.leftTab = false;
                    that.state.rightTab = true;
                }

                that.liveline();

                window.console && console.log('Enable disableTouch');
                $(document).on('touchstart', $.proxy(that.disableTouch, that));
                var scrollAmount;

                if (opts.resetScroll)
                    scrollAmount = 0;

                else if (opts.removePast && opts.append)
                    scrollAmount = $(that.selectors.streamContainer).scrollLeft() - that.settings.baseWidth;

                else if ((opts.removePast && !opts.append) || (!opts.append && !opts.removePast))
                    scrollAmount = $(that.selectors.streamContainer).scrollLeft() + that.settings.baseWidth;

                else
                    scrollAmount = $(that.selectors.streamContainer).scrollLeft();

                //window.console && console.log('Setting scrollLeft() value to ' + scrollAmount);

                // More stable than jQuery method
                document.getElementsByClassName('stream-container-inner')[0].scrollLeft = scrollAmount;

                // Force iOS/Android to redraw
                $(that.selectors.streamContainer).hide().show(0);

                window.console && console.log('Disable disableTouch');
                $(document).off('touchstart', that.disableTouch);
                that.setIndexes();
            },
            complete: function () {
                //window.console && console.log('Rebinding event handlers');
                window.console && console.log('Enable scroll');
                $(that.selectors.streamContainer).on('scroll', $.proxy(that.scrollHandler, that));

                window.console && console.log('Enable click-and-drag');
                that.selectors.clickAndDrag.start();

                window.console && console.log('Enable EPG Info');
                $(that.selectors.epgInfoLink).on('click', $.proxy(that.showEpgInfo, that));

                window.console && console.log('Enable scrollnext');
                $(that.selectors.days).on('scrollnext', $.proxy(that.scrollnext, that));

                if (typeof opts.callback === 'function')
                    opts.callback();
            }
        });
    },

    liveline: function() {
        //window.console && console.log('Checking live line status');

        if ($(this.selectors.days + '.' + this.settings.activeClass + '.' + this.settings.todayClass).length)
            $(this.selectors.liveline).show();

        else
            $(this.selectors.liveline).hide();
    },

    setIndexes: function() {
        var days = $(this.selectors.channelDayContainer).children();

        $(days[0]).css('zIndex', 20);
        $(days[1]).css('zIndex', 10);
    },

    ///
    /// EPG Info Popup
    ///
    
    showEpgInfo: function(e) {
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
        if ($(window).scrollTop() >= 5) {
            epg.hide();
            timeline.hide();
            $(infoPopup).hide();
            clearInterval(triggerFloatTimeoutId);
            triggerFloatTimeoutId = setTimeout(triggerFloat, 500);
        } else {
            epgTop();
            epg.show();
            timeline.show();
            $(infoPopup).show();
        }
    });

    $(window).on('forcescroll', function() {
        if ($(window).scrollTop() >= 5) {
            triggerFloat();
        } else {
            epgTop();
            epg.show();
            timeline.show();
            $(infoPopup).show();
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
            epgTop();
        }
        epg.show();
        timeline.show();
        
    }

    function epgTop()
    {
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
        resizing = true;
        resizeProgrammes(that);
        resizing = false;
    }, 50);
}
