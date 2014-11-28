var playerDateTime;
var stackPos = 'top';
var playerTempTime;


function scrollStack() {
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

function scrollStackToBottom() {
    var scrollTo_val = $('.stack').prop('scrollHeight') + 'px';
    $('.stack').slimScroll({ scrollTo: scrollTo_val });

}


function updateStacks() {
    if ($('#ContainsLogMoments').val() == "False") {
        var stackUrl = $('#eventStackContainer').data("data-load-stack-log-url");
        $.get(stackUrl, function (data) {
            $('#eventStackContainer').html(data);
            scrollStack();
        });
    }
    var stackPollingInterval = parseInt($('#StackPollingInterval').val());
    // setTimeout(updateStacks, stackPollingInterval);
}


function updateLogMoments() {

    if ($('#ContainsLogMoments').val() == "True") {
        var lastLogTime = $('.stack > ol > li').last().find('.time-code').data('time');
        var logUrl = $('#eventStackContainer').data("load-new-stack-url");
        logUrl += '?startTime=' + encodeURIComponent(lastLogTime);

        $.get(logUrl, function (data) {
            $('.stack > ol ').append(data);
            if (data.length > 0 || data)
                scrollStackToBottom();
        });
    }
}

$(function () {
    scrollStack();
    //updateStacks();
    setInterval(updateLogMoments, 3000);

    $('.log-moment').click(function (e) {
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

        $('#ProgramDateTime').val(messageSplit[1]);
        var sentTime = Date.parse(messageSplit[1]);

        var logs = $('.stack > ol').children().toArray();
        for (var i = 0; i <= logs.length; i++) {
            var thisTime = Date.parse($(logs[i]).find('.time-code').data('time'));
            var nextTime = i == logs.length ? Date.now() : Date.parse($(logs[i + 1]).find('.time-code').data('time'));
            if (sentTime >= thisTime && sentTime < nextTime) {
                $(logs[i]).addClass('active');
            } else {
                $(logs[i]).removeClass('active');
            }
        }
    });
    $('.btn-moment-expand').on('click', function() {
        $($(this).children()[1]).toggleClass('fa-plus').toggleClass('fa-minus');
    });

});




