$(function() {
    setTimeout(getStreamUrl, 5000);
});

var isCurrentlyLive = true;

function liveEdgeUpdate(data) {

    document.getElementById("MeetingEndTime").value = data.liveEdgeUpdateString;
    //checkStartTime();
    //checkEndTime();
    compareTimes();
}

function getShareTime(e) {
    var receiver = $('#UKPPlayer')[0];
    var message = {
        'function': 'getTime',
        'sender': document.location.href,
        'callback': 'setShareTime',
        'data': {
            'elementId': e.target.dataset.textboxId
        }
    };
    $.postMessage(JSON.stringify(message), receiver.src, receiver.contentWindow);

}

function getDownloadTime(e) {
    var receiver = $('#UKPPlayer')[0];
    var message = {
        'function': 'getTime',
        'sender': document.location.href,
        'callback': 'setDownloadTime',
        'data': {
            'elementId': e.target.dataset.textboxId
        }
    };
    $.postMessage(JSON.stringify(message), receiver.src, receiver.contentWindow);

}

function getTime(elementId) {
    var receiver = $('#UKPPlayer')[0];
    var message = {
        'function': 'getTime',
        'sender': document.location.href,
        'callback': 'setTime',
        'data' : {
            'elementId': elementId
        }
    };

    $.postMessage(JSON.stringify(message), receiver.src, receiver.contentWindow);
}

function getStreamUrl() {
    var receiver = $('#UKPPlayer')[0];
    var message = {
        'function': 'getStreamUrl',
        'sender': document.location.href,
        'callback': 'setStreamUrl'
    };
    $.postMessage(JSON.stringify(message), receiver.src, receiver.contentWindow);
}

function setTime(data) {
    document.getElementById(data.elementId).value = data.time;
}

function setShareTime(data) {
    var time = new Date(data.time).toTimeString();
    var textbox = document.getElementById(data.elementId);
    textbox.value = time.split(' ')[0];
    textbox.dataset.lastInput = textbox.value;

    document.getElementById(textbox.dataset.partnerId).value = textbox.value;
    compareTimes();

    reloadEmbedData();
}

function setDownloadTime(data) {
    var time = new Date(data.time);
    var timeString = time.toTimeString();
    var textbox = document.getElementById(data.elementId);
    textbox.value = timeString.split(' ')[0];
    textbox.dataset.lastInput = textbox.value;
    document.getElementById(textbox.dataset.formId).value = time.toJSON();

    document.getElementById(textbox.dataset.partnerId).value = textbox.value;
    reloadEmbedData();

    if (data.elementId === "downloadStartTime") {
        //checkStartTime();
        compareTimes();
    }
    else if (data.elementId === "downloadEndTime") {
        //checkEndTime();
        compareTimes();
    }
}

function setStreamUrl(data) {
    document.getElementById("StreamUrl").value = data.streamUrl;
}

function isLivePlayer() {
    return true;
}