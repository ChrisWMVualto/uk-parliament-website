var playerDateTime;
var stackPos = 'top';

function scrollStack() {
    if ($(".stack").length) {
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




function updateStacks() {
    if ($('#ContainsLogMoments').val() == "False") {
        var stackUrl = $('#eventStackContainer').data("load-stack-url");
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
        var logUrl = $('#eventStackContainer').data("load-log-url");
        $.get(logUrl, function (data) {
            $('#eventStackContainer').html(data);
        });
    }

    var stackPollingInterval = parseInt($('#StackPollingInterval').val());
    setTimeout(updateLogMoments, stackPollingInterval);
}




$(function () {
    scrollStack();
    //updateStacks();
    //updateLogMoments();
});