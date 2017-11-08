window.addEventListener("message", updateCurrentTime);

$(function() {
    enableAudioOnly();
});

function enableAudioOnly() {
    var radio = $("#fileType2");
    radio.removeAttr("disabled");
}

function updateCurrentTime(event) {
    var messageSplit = event.data.split("_");
    document.getElementById("ProgramDateTime").value = messageSplit[1];
}

function getTime() {
    return new Date(document.getElementById("ProgramDateTime").value);
}

function getShareTime(e) {

    var timeString = getTime().toTimeString();
    document.getElementById(e.target.dataset.textboxId).value = timeString.split(' ')[0];

    reloadEmbedData();
}

function getDownloadTime(e) {
    var time = getTime();
    var timeString = time.toTimeString();
    document.getElementById(e.target.dataset.textboxId).value = timeString.split(' ')[0];

    document.getElementById(e.target.dataset.formId).value = time.toJSON();
}

function initSetDownloadTime(e) {
    var time = getTime();

    document.getElementById(e.target.dataset.formId).value = time.toJSON();
}

function getStreamUrlOnLoad() {
    //this is here to stop a console error
}