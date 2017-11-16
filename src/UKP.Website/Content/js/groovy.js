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
    document.getElementById(e.target.dataset.textboxId).value = timeString.split(' ')[0];

    reloadEmbedData();
}

function getDownloadTime(e) {
    var time = getTime();
    var timeString = time.toTimeString();
    document.getElementById(e.target.dataset.textboxId).value = timeString.split(' ')[0];
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
    debugger;
    document.getElementById(e.target.dataset.formId).value = time.toJSON();
}

function getStreamUrlOnLoad() {
    //this is here to stop a console error
}