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

function reloadShareData() {
    var settings = {
        options: {
            start: {
                checkbox: $('#startTimeCheck'),
                input: $('#startTime'),
                enabled: false
            },
            end: {
                checkbox: $('#endTimeCheck'),
                input: $('#endTime'),
                enabled: false
            },
            audio: {
                checkbox: $('#audio-toggle'),
                enabled: false
            }
        },
        urlBase: '/Event/GetVideo/',
        eventId: getEventId(),
        fields: {
            shortUrl: $('#smallUrl'),
            longUrl: $('#url'),
            embed: $('#embed')
        }
    };

    $.each(settings.options, function () {
        var that = this;
        this.checkbox.bind('click', function() {
            that.enabled = !that.enabled;
        });
        if (this.hasOwnProperty('input'))
            this.input.bind('focusout', ajaxRequest);
    });

    function ajaxRequest() {
        var start = "",
            end = "";

        if (settings.options.start.enabled)
            start = timeToIso(settings.options.start.input.val());

        if (settings.options.end.enabled)
            end = timeToIso(settings.options.end.input.val());

        var url = settings.urlBase + settings.eventId + "?in=" + start + "&out=" + end + "&audioOnly=" + settings.options.audio.enabled;

        $.ajax(url, {
            success: handleSuccessResponse
        });
    }

    function handleSuccessResponse(data) {
        settings.fields.shortUrl.val(data.shortWebPageUrl);
        settings.fields.longUrl.val(data.pageUrl);
        settings.fields.embed.text(data.embedCode);
    }

    function getEventId() {
        return window.location.toString().match(/(\d|[a-z]){8}-(\d|[a-z]){4}-(\d|[a-z]){4}-(\d|[a-z]){4}-(\d|[a-z]){12}/i)[0];
    }

    function timeToIso(time) {
        var splitTime = time.split(/(\.|,|:){1}/);

        var seconds = "00";
        var minutes = "00";
        var hours = "00";

        if (splitTime.length >= 1)
            hours = twoDigitString(splitTime[0]);

        if (splitTime.length >= 3)
            minutes = twoDigitString(splitTime[2]);

        if (splitTime.length >= 5)
            seconds = twoDigitString(splitTime[4]);

        var date = new Date(Date.now());

        return date.toISOString().substr(0, 11) + hours + ":" + minutes + ":" + seconds + "+00:00";
    }

    function twoDigitString(digit) {
        while(digit.length < 2)
            digit = "0" + digit;

        return digit.substr(0, 2);
    }
}

reloadShareData();