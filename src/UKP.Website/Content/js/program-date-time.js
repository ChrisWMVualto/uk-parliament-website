var stackPos = 'top';
var scrollTimeoutId = null;

function scrollStackAndLogs() {
    if ($(".stack").length) {

        $('.stack-and-logs').removeClass('stack-and-logs');

        $('.stack').slimScroll({
            railVisible: true,
            railColor: '#ffffff',
            railOpacity: 0.3,
            color: '#ffffff',
            size: '12px',
            height: 'auto',
            alwaysVisible: false,
            start: stackPos
        });


        $('.stack').slimScroll().bind('slimscroll', function (e, pos) {
            stackPos = pos;
        });
    }
}


function scrollStacksAndLogsToActiveItem() {

    if (!$(".stack > ol > li.active").length) return;

    var relativeY = $(".stack > ol > li.active").offset().top - $(".stack > ol").offset().top;
    $('.stack').slimScroll({ scrollTo: relativeY + 'px' });
}


function scrollTimer() {
    clearInterval(scrollTimeoutId);
    scrollTimeoutId = setInterval(scrollStacksAndLogsToActiveItem, 10000);
}

function autoScrollStackAndLogs() {
    scrollTimer();

    $('#eventStackContainer').on('mousemove', function () {
        scrollTimer();
    });
}

function appendLogMoments() {

    var lastLogTime = $('.stack > ol > li').last().find('.time-code').data('time');
    var logUrl = $('#eventStackContainer').data("load-new-stack-url");
    $.get(logUrl, { startTime: lastLogTime }, function (data) {
        $('.stack > ol').append(data);
    });
}

function refreshLogMoments() {

    var logUrl = $('#eventStackContainer').data("refresh-stack-url");
    $.get(logUrl, {}, function (data) {
        $('#eventStackContainer ol').html(data);
    });
}

function highlightLogItems(sentTime) {

    var logItems = $('.stack > ol li.logMoment');
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

    $(document).on("click", ".log-moment", function (e) {
        var time = $(this).parent().find('.time-code').data('time');
        var receiver = $('#UKPPlayer')[0];
        $.postMessage("seek-program-date-time_" + time, receiver.src, receiver.contentWindow);
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

});



