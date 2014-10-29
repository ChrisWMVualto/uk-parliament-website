var auidoOnlyButtonState = false;
var embedGenTimeoutId = null;
var eventTimePollingsInterval = 20000;
var stackPollingIntervalLive = 10000;
var stackPollingIntervalNotLive = 60000;


function initSelectDates() {
    if ($(".clip-date").length) {
        $('.clip-date').selectpicker({
            style: 'btn-clip-date form-control input-lg',
            size: 4
        });
    }
}

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

        initSelectDates();
        initCheckbox();
        reloadEmbedData();
    });
}


function pollEventTimes() {
    setTimeout(function () {
        
        updateTitle();

        if ($('#AllowClippingRefresh').val() == "True") {
            updateClipping();
        }

        pollEventTimes();

    }, eventTimePollingsInterval);
}

function stateChanged(planningState, recordingState, recordedState) {
    updateTitle();
    updateClipping();

    if (recordingState == "RECORDING") {
        $('#StackPollingInterval').val(stackPollingIntervalLive);
    } else {
        $('#StackPollingInterval').val(stackPollingIntervalNotLive);
    }

    if (recordedState == "REVOKE") {
        window.location.reload();
    }
}

function reloadEmbedData() {
    var settings = {
        options: {
            start: {
                input: $('#startTime'),
                date: $('#startClipDate'),
                hiddenStart: $('#hiddenStart'),
                checkbox: $('#startTimeCheck')
            },
            end: {
                input: $('#endTime'),
                date: $('#endClipDate'),
                hiddenEnd: $('#hiddenEnd'),
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
            secondStep: 1
        }
    };

    $.each(settings.options, function () {
        if (this.hasOwnProperty('input')) {
            this.input.timepicker(settings.timepickerOpts);
            this.input.on('changeTime.timepicker', generateEmbedCode);
        }
        if (this.hasOwnProperty('date')) {
            this.date.on('change', generateEmbedCode);
        }
    });

    function generateEmbedCode() {

        clearTimeout(embedGenTimeoutId);

        embedGenTimeoutId = setTimeout(function () {
            var start = settings.options.start.input.val();
            var end = settings.options.end.input.val();

            if (start == "" && end != "") {
                settings.options.start.input.val(settings.options.start.hiddenStart.val());
                start = settings.options.start.input.val();
            }

            if (start != '') {
                settings.options.start.checkbox.prop("checked", true);
                start = settings.options.start.date.val() + 'T' + start;
            }

            if (end != '') {
                settings.options.end.checkbox.prop("checked", true);
                end = settings.options.end.date.val() + 'T' + end;
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

        if (settings.options.start.checkbox.prop("checked") == false) {
            settings.options.start.input.val("");
            settings.options.end.input.val("");
            settings.options.end.checkbox.prop("checked", false);
            initCheckbox();

        } else {
            settings.options.start.input.val(settings.options.start.hiddenStart.val());
        }

        generateEmbedCode();
    });

    settings.options.end.checkbox.click(function () {
        if (settings.options.end.checkbox.prop("checked") == false) {
            settings.options.end.input.val("");
        } else {
            settings.options.end.input.val(settings.options.end.hiddenEnd.val());
        }

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
            $(this).addClass("active");
            return;
        }

        if (auidoOnlyButtonState == true) {
            auidoOnlyButtonState = false;
            loadPlayer(false);
            $(this).removeClass("active");
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
    pollEventTimes();
});