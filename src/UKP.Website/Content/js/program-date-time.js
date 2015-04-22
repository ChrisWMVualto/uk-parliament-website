var stackPos = 'top';
var logPos = 'top';
var scrollTimeoutId = null;

function scrollStackAndLogs() {

    if ($('.log-list').length)
    {
        $('.log-list').slimScroll({
            railVisible: true,
            railColor: '#E6EBEE',
            railOpacity: 1,
            color: '#C1C7C9',
            size: '12px',
            height: '625px',
            alwaysVisible: false,
            start: logPos
        });
        $('.log-list').slimScroll().bind('slimscroll', function (e, pos) {
            stackPos = pos;
        });
    }
}


function scrollStacksAndLogsToActiveItem() {

    if (!$(".log-list > ul > li.active").length) return;

    var relativeY = $(".log-list > ul > li.active").offset().top - $(".log-list > ul").offset().top;
    $('.log-list').slimScroll({ scrollTo: relativeY + 'px' });
}


function scrollTimer() {
    clearInterval(scrollTimeoutId);
    scrollTimeoutId = setInterval(scrollStacksAndLogsToActiveItem, 10000);
}

function autoScrollStackAndLogs() {
    scrollTimer();

    $('#logs').on('mousemove', function () {
        scrollTimer();
    });
}

function appendLogMoments() {

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

    // TODO: old way before reversal
    /*
    var lastLogTime = $('.stack > ol > li').last().find('.time-code').data('time');
    var logUrl = $('#eventStackContainer').data("load-new-stack-url");
    $.get(logUrl, { startTime: lastLogTime }, function (data) {
        $('.stack > ol').append(data);
    });*/
}

function refreshLogMoments() {
    $('#logTab').removeClass('invisable');
    var logUrl = $('#logTab').data("refresh-log-url");
    $.get(logUrl, {}, function (data) {
        $('#logs').html(data);
    });
}

function highlightLogItems(sentTime) {

    var logItems = $('.log-list > li.stackouter');
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


$(function () {

    var eventStateHub = $.connection.eventStateHub;
    var eventId = $('#eventId').val();

    eventStateHub.client.logUpdate = function (logUpdateType, changedId, logMomentId) {

        if (eventId == changedId) {

            if ($('#ContainsLogMoments').val() == 'False') {
                $('#ContainsLogMoments').val('True');
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
    autoScrollStackAndLogs();

    $(document).on("click", ".logouter", function (e) {
        var time = $(this).find('.time-code').data('time');
        var receiver = $('#UKPPlayer')[0];
        $.postMessage("seek-program-date-time_" + time, receiver.src, receiver.contentWindow);
        var logItems = $('.log-list > li.stackouter');
        logItems.removeClass('active');
        $(this).addClass('active');
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
        highlightLogItems(sentTime);   
    });

    $('.btn-moment-expand').on('click', function () {
        $($(this).children()[1]).toggleClass('fa-plus').toggleClass('fa-minus');
    });

    //$('#logTab').on('click', function() {
    //    setTimeout(scrollStackAndLogs(),2000);
    //});

});



