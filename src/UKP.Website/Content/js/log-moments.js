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


        $('.stack').slimScroll().bind('slimscroll', function(e, pos){
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

        $.get(logUrl, function(data) {
            $('.stack > ol ').append(data);
            if(data.length > 0 || data)
                scrollStackToBottom();
        });


    }


}









$(function () {



    scrollStack();
    //updateStacks();
    setInterval(updateLogMoments, 3000);

    $('.log-moment').click(function() {
        var time = $(this).parent().find('.time-code').data('time');
        var receiver = document.getElementById("UKPPlayer");
        $.postMessage("GoToLogItem_" + time, src, receiver.contentWindow);
    });

    $.receiveMessage(function (event) {
        var messageSplit = event.data.split('_');
        if (messageSplit.length < 2)
            return;
        var sentTime = Date.parse(messageSplit[1]);
        $('.stack > ol > li').each(function() {
            var thisTime = $(this).find('.time-code').data('time');
            
            console.log(thisTime);
            console.log(Date.parse(thisTime));
            //if (thisTime >= sentTime) {
            //    $(this).addClass('active');
            //}

        });
    });

});


