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
        'function': 'getTime',
        'sender': document.location.href,
        'callback': 'setStreamUrl'
    };
    message.function = "getStreamUrl";
    message.sender = document.location.href;
    $.postMessage(JSON.stringify(message), receiver.src, receiver.contentWindow);
}

function setTime(data) {
    document.getElementById(data.elementId).value = data.time;
}

function setStreamUrl(data) {
    document.getElementById("StreamUrl").value = data.streamUrl;
}