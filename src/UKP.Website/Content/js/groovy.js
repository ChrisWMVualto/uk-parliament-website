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

    var timeString = getTime().toTimeString();
    var textbox = document.getElementById(e.target.dataset.textboxId);
    textbox.value = timeString.split(' ')[0];
    textbox.dataset.lastInput = textbox.value;

    reloadEmbedData();
}

function getDownloadTime(e) {
    var time = getTime();
    var timeString = time.toTimeString();
    var textbox = document.getElementById(e.target.dataset.textboxId);
    textbox.value = timeString.split(' ')[0];
    textbox.dataset.lastInput = textbox.value;
    document.getElementById(e.target.dataset.formId).value = time.toJSON();

    if (e.target.id === "downloadStartTimeSet") {
        checkStartTime();
    }
    else if (e.target.id === "downloadEndTimeSet") {
        checkEndTime();
    }

}

function initSetDownloadTime(e) {
    var time = getTime();
    document.getElementById(e.target.dataset.formId).value = time.toJSON();
}