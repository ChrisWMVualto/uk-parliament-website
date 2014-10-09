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

function reloadEmbedData() {
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
        },
        timepickerOpts: {
            defaultTime: false,
            showSeconds: true,
            showMeridian: false,
            minuteStep: 1,
            secondStep: 1,
        }
    };

    $.each(settings.options, function () {
        var that = this;

        this.checkbox.bind('click', function() {
            that.enabled = !that.enabled;
            ajaxRequest();
        });

        if (this.hasOwnProperty('input')) {
            this.input.bind('focusout', ajaxRequest);
            this.input.timepicker(settings.timepickerOpts);
            this.input.on('changeTime.timepicker', ajaxRequest);
        }
    });

    function ajaxRequest() {
        var start = "",
            end = "";

        if (settings.options.start.enabled)
            start = settings.options.start.input.val();

        if (settings.options.end.enabled)
            end = settings.options.end.input.val();

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

    $('.embed-code').hide();
    $('.embed-terms .btn-agree').bind('click', function (e) {
        e.preventDefault();

        $('.embed-terms').fadeOut();
        $('.embed-code').fadeIn();
    });
    reloadEmbedData();
});