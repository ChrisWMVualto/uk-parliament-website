var auidoOnlyButtonState = false;
var embedGenTimeoutId = null;

function loadPlayer(audioOnly) {
    var url = $('#getVideoUrl').val();
    $.getJSON(url, { audioOnly: audioOnly }, function (video) {
        $('#videoContainer').html(video.embedCode);
    });
}

function updateTitle() {
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
    updateTitle();
    updateClipping();

    if (recordingState == "RECORDING") {
        $('#StackPollingInterval').val('6000');
    } else {
        $('#StackPollingInterval').val('60000');
    }
}

function reloadEmbedData() {
    var settings = {
        options: {
            start: {
                input: $('#startTime'),
                checkbox: $('#startTimeCheck')
            },
            end: {
                input: $('#endTime'),
                checkbox: $('#endTimeCheck')
            }
        },
        urlBase: $('#share').data('url'),
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
        if (this.hasOwnProperty('input')) {
            this.input.timepicker(settings.timepickerOpts);
            this.input.on('changeTime.timepicker', generateEmbedCode);
        }
    });

    function generateEmbedCode() {

        clearTimeout(embedGenTimeoutId);

         embedGenTimeoutId = setTimeout(function() {
            var start = settings.options.start.input.val();
            var end = settings.options.end.input.val();

            if (start == "" && end != "") {
                settings.options.start.input.val("00:00:00");
                start = settings.options.start.input.val();
            }

            if (start != '') {
                settings.options.start.checkbox.prop("checked", true);
            }

            if (end != '') {
                settings.options.end.checkbox.prop("checked", true);
            }

            var url = settings.urlBase + "/" + settings.eventId + "?in=" + start + "&out=" + end;
            $.ajax(url, {
                success: updateEmbedCodes
            });
        }, 500);

    }

    function updateEmbedCodes(data) {
        settings.fields.shortUrl.val(data.shortWebPageUrl);
        settings.fields.longUrl.val(data.pageUrl);
        settings.fields.embed.text(data.embedCode);
        settings.fields.shortUrl.trigger("updated-short-url", data.shortWebPageUrl);
    }


    $(settings.options.start.checkbox).click(function () {
        settings.options.start.input.val("");
        settings.options.end.input.val("");
        settings.options.end.checkbox.prop("checked", false);
        generateEmbedCode();
    });

    settings.options.end.checkbox.click(function () {
        settings.options.end.input.val("");
        generateEmbedCode();
    });

    generateEmbedCode();
}


function selectableEmbedCode() {
    var inputs = $('#embed, #smallUrl, #url');
    inputs.bind('click', function () {
        this.select();
    });
}

function updateSocialLinks(e, url) {
    var buttons = [
            $('.btn-facebook'),
            $('.btn-twitter'),
            $('.btn-google'),
            $('.btn-linkedin')
    ];

    $.each(buttons, function () {
        $(this).attr('href', $(this).data('share-url') + url);
    });
}

function audiOnlySwitch() {
    $(document).on('click', '#audioToggle', null, function () {

        if (auidoOnlyButtonState == false) {
            auidoOnlyButtonState = true;
            loadPlayer(true);
            return;
        }

        if (auidoOnlyButtonState == true) {
            auidoOnlyButtonState = false;
            loadPlayer(false);
            return;
        }
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

    updateSocialLinks(null, $('#smallUrl').val());
    $('#smallUrl').bind("updated-short-url", updateSocialLinks);

    updateClipping();
    selectableEmbedCode();
    audiOnlySwitch();
});