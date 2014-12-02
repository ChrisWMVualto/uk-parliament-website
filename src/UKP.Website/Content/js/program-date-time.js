var playerDateTime;
var stackPos = 'top';
var playerTempTime;


function scrollStack() {
    if ($(".stack").length) {

        // TODO
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
    var scrollToVal = $('.stack').prop('scrollHeight') + 'px';
    $('.stack').slimScroll({ scrollTo: scrollToVal });
}


function appendLogMoments() {

    var lastLogTime = $('.stack > ol > li').last().find('.time-code').data('time');
    var logUrl = $('#eventStackContainer').data("load-new-stack-url");

    $.get(logUrl, { startTime: lastLogTime }, function (data) {
        $('.stack > ol ').append(data);

        if (data.length > 0 || data) {
            scrollStackToBottom();
        }     
    });
}

function refreshLogMoments() {

    var logUrl = $('#eventStackContainer').data("refresh-stack-url");
    $.get(logUrl, null, function (data) {

        $('#eventStackContainer ol').html(data);
        scrollStackToBottom();
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
                console.log('firstime');
                return;
            }
            console.log(logUpdateType);
            
            if (logUpdateType == 'Add') {
                appendLogMoments();
            } else {
                refreshLogMoments();
            }
        }
    };

    $.connection.hub.start().done(function () { });

    scrollStack();


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

    $('.btn-moment-expand').on('click', function () {
        $($(this).children()[1]).toggleClass('fa-plus').toggleClass('fa-minus');
    });

});



