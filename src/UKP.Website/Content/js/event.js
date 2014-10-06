function updateTimes() {
    var timesUrl = $('#eventTimesContainer').data("load-url");
    $.get(timesUrl, function (data) {
        $('#eventTimesContainer').html(data);
    });
}


updateTimes();

function stateChanged(planningState, recordingState, recordedState) {
    updateTimes();
}


$(function () {

    var eventStateHub = $.connection.eventStateHub;
    var eventId = $('#eventId').val();

    eventStateHub.client.eventStateChanged = function (changedId, planningState, recordingState, recordedState, simpleState) {

        if (eventId == changedId) {
            stateChanged(planningState, recordingState, recordedState, simpleState);
        }
    };

    $.connection.hub.start().done(function () { });
});