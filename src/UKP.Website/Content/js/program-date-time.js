var stackPos = 'top';
var logPos = 'top';
var lastClickedTimecode = null;
var highlightItem = true;

function randomIntFromInterval(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

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
    var playerStateLiveReloadTime = randomIntFromInterval(1, 15) * 1000;
    // Why do we do this? This stops many concurrent users all hitting the api at once.
    setTimeout(function () {
        var lastLogTime = $('.log-list > li').first().find('.time-code').data('time');
        var logUrl = $('#logTab').data("load-new-log-url");

        $.get(logUrl, { startTime: lastLogTime }, function (data) {
            $('.log-list').prepend(data);
        });
    }, playerStateLiveReloadTime);
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

    var playerStateLiveReloadTime = randomIntFromInterval(1, 45) * 1000;
    // Why do we do this? This stops many concurrent users all hitting the api at once.
    setTimeout(function () {
        $.get(logUrl, {}, function (data) {
            $('.log-list').html(data);
            scrollStackAndLogs();
        });
    }, playerStateLiveReloadTime);
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

function logItemClicked(e) {
    var self = $(this);
    highlightItem = false;
    var time = self.find('.time-code').data('time');
    lastClickedTimecode = new Date(time);

    
    var logItems = $('.log-list > li.logouter');
    logItems.removeClass('active');
    self.addClass('active');

    $('#seekToLiveButton').removeClass('btn-seek-to-live-grey');
    
    var receiver = $('#UKPPlayer')[0];
    $.postMessage("seek-program-date-time_" + time, receiver.src, receiver.contentWindow);

    setTimeout(function () {
        highlightItem = true;
    }, 6000);

    e.preventDefault();
    return false;
}

function seekToLive() {

    $('#seekToLiveButton').addClass('btn-seek-to-live-grey');

    var receiver = $('#UKPPlayer')[0];
    $.postMessage("seek-to-live_true", receiver.src, receiver.contentWindow);

    setTimeout(function() {
        var logItems = $('.log-list > li.logouter');
        logItems.removeClass('active');
    }, 2000);
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
                $('#index-message').removeClass('hidden');
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

    

    $(document).on("tap", ".logouter", logItemClicked);
    $(document).on("click", ".logouter", logItemClicked);
    $(document).on("click", "#seekToLiveButton", seekToLive);

    //This is the receive message event for the highlighting of current log items
    $.receiveMessage(function (event) {
        var messageSplit = event.data.split('_');
        if (messageSplit.length < 2) return;
        if ((messageSplit[0].indexOf("program-date-time") == -1)) return;

        var sentTime = new Date(messageSplit[1]);
        $('#ProgramDateTime').val(sentTime.toISOString());

       
        if (lastClickedTimecode == null || sentTime > lastClickedTimecode) {
            if (highlightItem) {
                highlightLogItems(sentTime);
            }
            
        }
        
    });
});



