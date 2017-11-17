$(function () {
    messageListener();
});

function messageListener() {
    window.addEventListener("message", receiveMessage, false);
}

function receiveMessage(event) {
    var message = JSON.parse(event.data);
    var sender = $('#UKPPlayer')[0].src;
    if(message.sender === sender) window[message.function](message.data);
}

function liveEdgeUpdate(data) {

    document.getElementById("MeetingEndTime").value = data.liveEdgeUpdateString;
    checkStartTime();
    checkEndTime();
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

function getStreamUrlOnLoad() {
    getStreamUrl();
    setTimeout(getStreamUrl, 5000);
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
    document.getElementById(data.elementId).value = time.split(' ')[0];
    reloadEmbedData();
}

function setDownloadTime(data) {
    var time = new Date(data.time);
    var timeString = time.toTimeString();
    var textbox = document.getElementById(data.elementId);
    textbox.value = timeString.split(' ')[0];
    document.getElementById(textbox.dataset.formId).value = time.toJSON();

    if (data.elementId === "downloadStartTime") {
        checkStartTime();
    }
    else if (data.elementId === "downloadEndTime") {
        checkEndTime();
    }
}

function setStreamUrl(data) {
    document.getElementById("StreamUrl").value = data.streamUrl;
}