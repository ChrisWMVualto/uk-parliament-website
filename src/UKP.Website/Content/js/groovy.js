window.addEventListener("message", updateCurrentTime);

function updateCurrentTime(event) {
    var messageSplit = event.data.split("_");
    document.getElementById("ProgramDateTime").value = messageSplit[1];
}

function getTime(elementId) {
    var time = new Date(document.getElementById("ProgramDateTime").value);
    document.getElementById(elementId).value = time.toJSON();
}

function getShareTime(e) {
    var time = new Date(document.getElementById("ProgramDateTime").value).toTimeString();
    document.getElementById(e.target.dataset.textboxId).value = time.split(' ')[0];
    reloadEmbedData();
}

function getStreamUrlOnLoad() {
    //this is here to stop a console error
}