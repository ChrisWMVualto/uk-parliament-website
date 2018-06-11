function createDownload() {

    var clipRequested = document.getElementById("ClipRequested");

    if (clipRequested.value === true) {
        return;
    }

    if (!compareTimes()) {
        return;
    }

    var endTime = document.getElementById("EndTime");
    if (endTime.value === "") {
        endTime.value = document.getElementById("MeetingEndTime").value;
    }
    var captchaToken = captchaResponse();
    $("#CaptchaToken").val(captchaToken);
    var form = document.getElementById("createDownloadForm");
    var data = $(form).serialize();
    var url = form.dataset.postUrl;

    $(".error-message").prop("hidden", true);
    $("#downloadSubmit").prop("disabled", true);

    clipRequested.value = true;

    $.ajax({
        method: 'POST',
        url: url,
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
                keepError = true;
                clipRequested.value = false;
                grecaptcha.reset();
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

function generateDateString(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();

    var dateString = year + "-";

    if (month < 10) dateString += "0";
    dateString += month + "-";

    if (day < 10) dateString += "0";
    dateString += day;

    return dateString;
}

function updateDatePickers(time, textbox) {
    var dateSelect = document.getElementById(textbox.dataset.dateSelectId);
    var partnerSelect = document.getElementById(textbox.dataset.partnerDateSelectId);

    var dateString = generateDateString(time);
    $(dateSelect).selectpicker('val', dateString);
    $(partnerSelect).selectpicker('val', dateString);
}

$(function () {
    initInputMask();
    initEnableEmail();
    initCreateDownload();
    initDownloadStartEndKeyPress();
    initMakeAnotherClip();
});



function initEnableEmail() {
    var downloadContinueButton = document.getElementById("downloadContinue");
    downloadContinueButton.addEventListener("click", enableEmail);
}

function initCreateDownload() {
    var makeClip = $("#downloadSubmit");
    makeClip.on("click", createDownload);
}

function initDownloadStartEndKeyPress() {
    var startTimeBox = $("#downloadStartTime");
    var endTimeBox = $("#downloadEndTime");

    startTimeBox.val(new Date(document.getElementById("StartTime").value).toTimeString().split(" ")[0]);
    endTimeBox.val(new Date(document.getElementById("EndTime").value).toTimeString().split(" ")[0]);

    startTimeBox.on("focusout", compareTimes);
    endTimeBox.on("focusout", compareTimes);

    startTimeBox.on("focusout", updatePartnerElement);
    endTimeBox.on("focusout", updatePartnerElement);
}

function initMakeAnotherClip() {
    document.getElementById("newClip").addEventListener("click", resetDownloadTab);
}
