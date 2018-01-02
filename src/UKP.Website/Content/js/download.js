function createDownload() {

    if (document.getElementById("ClipRequested") === true) {
        return;
    }

    if (!isValidCaptcha()) {
        $(".error-message").removeAttr("hidden");
        $(".error-message").text("Please complete the captcha before continuing");
        return;
    }

    var endTime = document.getElementById("EndTime");
    if (endTime.value === "") {
        endTime.value = document.getElementById("MeetingEndTime").value;
    }

    var form = $("#createDownloadForm");
    var data = $(form).serialize();

    $(".error-message").prop("hidden", true);
    $("#downloadSubmit").prop("disabled", true);

    document.getElementById("ClipRequested").value = true;

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