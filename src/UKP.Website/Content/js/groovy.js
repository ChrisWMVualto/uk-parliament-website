$(function() {
    enableAudioOnly();
});

function enableAudioOnly() {
    var radio = $("#fileType2");
    radio.removeAttr("disabled");
}

function getTime() {
    return new Date(document.getElementById("ProgramDateTime").value);
}

function getShareTime(e) {
    var time = getTime();
    var timeString = time.toTimeString();
    var textbox = document.getElementById(e.target.dataset.textboxId);
    textbox.value = timeString.split(' ')[0];
    textbox.dataset.lastInput = textbox.value;

    var dateSelect = document.getElementById(e.target.dataset.dateSelectId);
    var partnerSelect = document.getElementById(e.target.dataset.partnerDateSelectId);

    var dateString = generateDateString(time);
    $(dateSelect).selectpicker('val', dateString);
    $(partnerSelect).selectpicker('val', dateString);

    document.getElementById(textbox.dataset.partnerId).value = textbox.value;
    compareTimes();

    reloadEmbedData();
}

function getDownloadTime(e) {
    var time = getTime();
    var timeString = time.toTimeString();
    var textbox = document.getElementById(e.target.dataset.textboxId);
    textbox.value = timeString.split(' ')[0];
    textbox.dataset.lastInput = textbox.value;
    document.getElementById(e.target.dataset.formId).value = time.toJSON();

    var dateSelect = document.getElementById(e.target.dataset.dateSelectId);
    var partnerSelect = document.getElementById(e.target.dataset.partnerDateSelectId);

    var dateString = generateDateString(time);
    $(dateSelect).selectpicker('val', dateString);
    $(partnerSelect).selectpicker('val', dateString);

    document.getElementById(textbox.dataset.partnerId).value = textbox.value;

    if (e.target.id === "downloadStartTimeSet") {
        //checkStartTime();
        compareTimes();
    }
    else if (e.target.id === "downloadEndTimeSet") {
        //checkEndTime();
        compareTimes();
    }

}

function initSetDownloadTime(e) {
    var time = getTime();
    document.getElementById(e.target.dataset.formId).value = time.toJSON();
}

function isLivePlayer() {
    return false;
}