function updateTimes() {
    var titleUrl = $('#eventTitleContainer').data("load-url");
    $.get(titleUrl, function (data) {
        $('#eventTitleContainer').html(data);
    });
}

function updateClipping() {
    var clippingUrl = $('#clippingContainer').data("load-url");
    $.get(clippingUrl, function (data) {
        $('#clippingContainer').html(data);
        reloadEmbedData();
    });
}


function stateChanged(planningState, recordingState, recordedState) {
    updateTimes();
    updateClipping();
}

function reloadEmbedData() {
    var settings = {
        options: {
            start: {
                input: $('#startTime'),
            },
            end: {
                input: $('#endTime'),
            },
            audio: {
                checkbox: $('.audio-toggle'),
                enabled: false
            }
        },
        urlBase: '/Event/GetVideo/',
        eventId: $('#eventId').val(),
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

        if (this.hasOwnProperty('checkbox')) {
            this.checkbox.bind('click', function () {
                that.enabled = !that.enabled;
                generateEmbedCode();
            });
        }

        if (this.hasOwnProperty('input')) {
            this.input.bind('focusout', generateEmbedCode);
            this.input.timepicker(settings.timepickerOpts);
            this.input.on('changeTime.timepicker', generateEmbedCode);
        }
    });

    function generateEmbedCode() {

        var start = settings.options.start.input.val();
        var end = settings.options.end.input.val();

        if (settings.options.start.input.val() == "") {
            start = settings.options.start.input.val("00:00:00");
        }

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
}


function selectableEmbedCode() {
    var inputs = $('#embed, #smallUrl, #url');
    inputs.bind('click', function () {
        this.select();
    });
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

    updateTimes();
    updateClipping();
    selectableEmbedCode();
});