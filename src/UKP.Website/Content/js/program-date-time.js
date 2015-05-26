var stackPos = 'top';
var logPos = 'top';
var lastClickedTimecode = null;
var highlightItem = false;

function scrollStackAndLogs() {

    if ($('.log-list').length) {
        $('.log-list').slimScroll({
            railVisible: true,
            railColor: '#E6EBEE',
            railOpacity: 1,
            color: '#C1C7C9',
            size: '12px',
            height: '525px',
            alwaysVisible: false,
            start: logPos
        });
        $('.log-list').slimScroll().bind('slimscroll', function (e, pos) {
            logPos = pos;
        });
    }


    // Do not scroll stacks on mobile / small width devices
    var mql = window.matchMedia("(max-width: 480px)");
    if (mql.matches) {
        return;
    }

    if ($('.stack-list').length) {
        $('.stack-list').slimScroll({
            railVisible: true,
            railColor: '#E6EBEE',
            railOpacity: 1,
            color: '#C1C7C9',
            size: '12px',
            height: '525px',
            alwaysVisible: false,
            start: logPos
        });
        $('.stack-list').slimScroll().bind('slimscroll', function (e, pos) {
            stackPos = pos;
        });
    }
}

function refreshStackItems() {
    var stackUrl = $('#stackTab').data("refresh-stack-url");
    $.get(stackUrl, {}, function (data) {
        $('.stack-list').html(data);
        scrollStackAndLogs();
    });
}

function refreshStackInterval() {
    setInterval(refreshStackItems, 240000);
}

function appendArchiveLog() {
    $($('.log-list > li').get().reverse()).each(function (index, item) {

        var lastLogTime = $(item).find('.time-code').data('time');
        if (lastLogTime == '') {
            return true;
        }

        var logUrl = $('#logTab').data("load-new-log-url");
        $.get(logUrl, { startTime: lastLogTime }, function (data) {
            $(data).insertAfter($(item));
        });

        return false;
    });
}


function appendLiveLog() {
    var lastLogTime = $('.log-list > li').first().find('.time-code').data('time');
    var logUrl = $('#logTab').data("load-new-log-url");
    $.get(logUrl, { startTime: lastLogTime }, function (data) {
        $('.log-list').prepend(data);
    });
}

function appendLogMoments() {

    if ($('#LiveLogging').val() == "True") {
        appendLiveLog();
    } else {
        appendArchiveLog();
    }
}

function refreshLogMoments() {
    var logUrl = $('#logTab').data("refresh-log-url");
    $.get(logUrl, {}, function (data) {
        $('.log-list').html(data);
        scrollStackAndLogs();
    });
}

function highlightLiveLog(sentTime) {
    var logItems = $('.log-list > li.logouter');
    logItems.removeClass('active');
    logItems = $('.log-list > li.logouter').get().reverse();

    $(logItems).each(function (index, item) {

        var logItem = $(item);
        var thisTime = new Date(logItem.find('.time-code').data('time'));

        if (sentTime >= thisTime) {
            $(logItems[index - 1]).removeClass('active');
            logItem.addClass('active');
            return;
        }
    });
}

function highlightArchiveLog(sentTime) {

    var logItems = $('.log-list > li.logouter');
    logItems.removeClass('active');

    logItems.each(function (index, item) {

        var logItem = $(item);
        var thisTime = new Date(logItem.find('.time-code').data('time'));

        if (sentTime >= thisTime) {
            $(logItems[index - 1]).removeClass('active');
            logItem.addClass('active');
            return;
        }
    });
}

function highlightLogItems(sentTime) {

    if ($('#LiveLogging').val() == "True") {
        highlightLiveLog(sentTime);
    } else {
        highlightArchiveLog(sentTime);
    }
}




$(function () {

    var eventStateHub = $.connection.eventStateHub;
    var eventId = $('#eventId').val();

    if ($('#ContainsLogMoments').val() == 'True') {
        if ($('#DefaultToStackTab').val() == 'False') {
            $('#stacks').removeClass('active').removeClass('in');
            $('#logs').addClass('active').addClass('in');
        }
    }

    eventStateHub.client.logUpdate = function (logUpdateType, changedId, logMomentId) {

        if (eventId == changedId) {

            // first log item goes in!
            if ($('#ContainsLogMoments').val() == 'False') {
                $('#LiveLogging').val('True');
                $('#ContainsLogMoments').val('True');
                $('#logTab').removeClass('invisible');
                $('#logTab a').tab('show');
                
                refreshLogMoments();
                return;
            }


            if (logUpdateType == 'Add') {
                appendLogMoments();
            } else {
                refreshLogMoments();
            }
        }
    };

    $.connection.hub.start().done(function () { });

    scrollStackAndLogs();
    refreshStackInterval();

    $(document).on("click", ".logouter", function (e) {
        var self = $(this);
        var time = self.find('.time-code').data('time');
        lastClickedTimecode = new Date(time);
        highlightItem = false;
        var receiver = $('#UKPPlayer')[0];
        $.postMessage("seek-program-date-time_" + time, receiver.src, receiver.contentWindow);
        
        setTimeout(function() {
            var logItems = $('.log-list > li.logouter');
            logItems.removeClass('active');
            self.addClass('active');
        }, 10);

        e.preventDefault();
        return false;
    });

    //This is the receive message event for the highlighting of current log items
    $.receiveMessage(function (event) {
        var messageSplit = event.data.split('_');
        if (messageSplit.length < 2) return;
        if ((messageSplit[0].indexOf("program-date-time") == -1)) return;

        var sentTime = new Date(messageSplit[1]);
        $('#ProgramDateTime').val(sentTime.toISOString());

       
        if (lastClickedTimecode == null || sentTime > lastClickedTimecode) {
            highlightItem = true;
            highlightLogItems(sentTime);
        }
        
    });
});



