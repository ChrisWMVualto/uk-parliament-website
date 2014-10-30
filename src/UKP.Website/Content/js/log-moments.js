var playerDateTime;
var stackPos = 'top';
var playerTempTime;


function scrollStack() {
    if ($(".stack").length) {

        $('.stack-and-logs').css('display','block');
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
    
});


