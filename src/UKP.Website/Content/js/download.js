function createDownload() {

    var clipRequested = document.getElementById("ClipRequested");

    if (clipRequested.value === true) {
        return;
    }

    if (!isValidCaptcha()) {
        $(".error-message").removeAttr("hidden");
        $(".error-message").text("Please complete the captcha before continuing");
        return;
    }

    //todo check form, return if incomplete
    checkMakeClip();

    var endTime = document.getElementById("EndTime");
    if (endTime.value === "") {
        endTime.value = document.getElementById("MeetingEndTime").value;
    }

    var form = $("#createDownloadForm");
    var data = $(form).serialize();

    $(".error-message").prop("hidden", true);
    $("#downloadSubmit").prop("disabled", true);

    clipRequested.value = true;

    $.ajax({
        method: 'POST',
        url: "/Event/CreateDownload",
        data: data,
        success: function (data) {
            var response = JSON.parse(data);

            if (response.Success) {
                $(".thankyou-header").text("Thank you");
                $(".thankyou-message").text(response.Message);
                $(".thankyou-email").text(response.Email);

                $(".download-form").prop("hidden", true);
                $(".thankyou").removeAttr("hidden");

                urlPageNavigation("#player-tabs");

            } else {
                $(".error-message").removeAttr("hidden");
                $(".error-message").text(response.Message);
                clipRequested.value = false;
            }
        }
    });

}

window.addEventListener("message", receiveMessage, false);

function receiveMessage(event) {
    var message = JSON.parse(event.data);
    var sender = $('#UKPPlayer')[0].src;
    if (message.sender === sender) window[message.function](message.data);
}

function updateCurrentTime(data) {
    var messageSplit = data.currentTime.split("_");
    document.getElementById("ProgramDateTime").value = messageSplit[1];
}