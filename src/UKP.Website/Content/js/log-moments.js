var playerDateTime;

function updateStacks() {
    if ($('#ContainsLogMoments').val() == "False") {
        var stackUrl = $('#eventStackContainer').data("load-stack-url");
        $.get(stackUrl, function (data) {
            $('#eventStackContainer').html(data);
        });
    }
    var stackPollingInterval = parseInt($('#StackPollingInterval').val());
    setTimeout(updateStacks, stackPollingInterval);
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

    updateStacks();
    //updateLogMoments();
});