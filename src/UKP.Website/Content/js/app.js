var captchaValid = false;
var timesValid = true;

$(function () {
    initTermsAndConditions();
    initEnableEmbed();
    initEnableEmail();
    initSetShareTime();
    initSetDownloadTime();
    initSetClipboard();
    initSetFileType();
    initCreateDownload();
    initEmailValid();
    initDownloadStartEndKeyPress();
    initMakeAnotherClip();
    initInputMask();
});

function initTermsAndConditions() {
    var checkboxes = $(".acceptance");
    checkboxes.on("click", termsAndConditionsClickHandler);

}

function initEnableEmbed() {
    var shareContinueButton = document.getElementById("shareContinue");
    shareContinueButton.addEventListener("click", enableEmbed);
}

function initEnableEmail() {
    var downloadContinueButton = document.getElementById("downloadContinue");
    downloadContinueButton.addEventListener("click", enableEmail);
}

function initSetShareTime() {
    var setTimes = $(".set-share-time");

    setTimes.on("click", getShareTime);
}

function initSetDownloadTime() {
    var setTimes = $(".set-download-time");
    setTimes.on("click", getDownloadTime);
}

function initSetClipboard() {
    var clipboard = $(".fa-clipboard");
    clipboard.on("click", copyToClipbloard);
}

function initSetFileType() {
    var fileType = $(".fileType");
    fileType.on("click", checkMakeClip);
}

function initCreateDownload() {
    var makeClip = $("#downloadSubmit");
    makeClip.on("click", createDownload);
}

function initEmailValid() {
    var email = $("#email");
    email.on("keyup", checkMakeClip);
}

function initDownloadStartEndKeyPress() {
    var startTimeBox = $("#downloadStartTime");
    var endTimeBox = $("#downloadEndTime");

    startTimeBox.on("focusout", checkStartTime);
    endTimeBox.on("focusout", checkEndTime);
}

function initMakeAnotherClip() {
    var makeAnotherClip = $("#newClip");
    makeAnotherClip.on("click", resetDownloadTab);
}

function checkStartTime() {

    var startTimeArray = $("#downloadStartTime").val().split(":");
    var startDateString = $('[data-id="startDownloadDate"]')[0];

    var startDate = new Date(Date.parse(startDateString.title));
    startDate.setHours(startDate.getHours() + startTimeArray[0]);
    startDate.setMinutes(startDate.getMinutes() + startTimeArray[1]);
    startDate.setSeconds(startDate.getSeconds() + startTimeArray[2]);

    var meetingStartTime = document.getElementById("MeetingStartTime").value;

    if (isNaN(startDate.valueOf())) {
        return;
    }

    var startTime = startDate.toISOString();
    var endTime = $("#EndTime").val();

    if (startTime < endTime && startTime >= meetingStartTime) {
        setDownloadTimeForm("StartTime", startTime);
        timesValid = true;
        $(".error-message").prop("hidden", true);
        checkMakeClip();
    } else {
        if (startTime > endTime) {
            setErrorMessage("Start Time cannot be later than the End Time");
        } else if (startTime === endTime) {
            setErrorMessage("Start Time cannot be equal to the End Time");
        }else {
            setErrorMessage("Start Time cannot be earlier than the meeting start time");
        }
    }
}

function setErrorMessage(message) {
    $(".error-message").text(message);
    $(".error-message").removeAttr("hidden");
    timesValid = false;
    checkMakeClip();
}

function setDownloadTimeForm(id, time) {
    document.getElementById(id).value = time;
}

function checkEndTime() {
    var endTimeArray = $("#downloadEndTime").val().split(":");
    var endDateString = $('[data-id="endDownloadDate"]')[0];

    var endDate = new Date(Date.parse(endDateString.title));
    endDate.setHours(endDate.getHours() + endTimeArray[0]);
    endDate.setMinutes(endDate.getMinutes() + endTimeArray[1]);
    endDate.setSeconds(endDate.getSeconds() + endTimeArray[2]);

    var meetingEndTime = document.getElementById("MeetingEndTime").value;

    if (isNaN(endDate.valueOf())) {
        return;
    }

    var endTime = endDate.toISOString();
    var startTime = $("#StartTime").val();

    if (endTime > startTime && endTime < meetingEndTime) {
        setDownloadTimeForm("EndTime", endTime);
        timesValid = true;
        $(".error-message").prop("hidden", true);
        checkMakeClip();
    } else {
        if (endTime < startTime) {
            setErrorMessage("End Time cannot be earlier than the Start Time");
        } else if (endTime === startTime) {
            setErrorMessage("End Time cannot be equal to the Start Time");
        } else {
            setErrorMessage("End Time cannot be later than the meeting end time");
        }
    }
}

function termsAndConditionsClickHandler(e) {
    var id = e.target.dataset.continueId;

    enableDisableButton(id);
}

function enableDisableButton(id) {
    var button = document.getElementById(id);

    if (button.hasAttribute("disabled")) {
        $(button).removeAttr("disabled");
    } else {
        $(button).prop("disabled", true);
    }
}

function enableEmbed() {
    var embed = $(".embed-code");
    var terms = $(".share-terms");

    terms.fadeOut();
    embed.fadeIn();

    window.location.href += "#embed";
}

function enableEmail() {
    var emailMe = $(".email-me");
    var terms = $(".download-terms");

    terms.fadeOut();
    emailMe.fadeIn();

    window.location.href += "#email";
}

function copyToClipbloard() {
    var copyTextarea = document.querySelector('#embed');
    copyTextarea.select();

    var log = $(".response-message");

    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        log.text("Copied to Clipboard");
        log.addClass("show");
    } catch (err) {
        log.text('unable to copy');
    }
}

function resetDownloadTab() {
    $(".thankyou").prop("hidden", true);
    $(".download-form").removeAttr("hidden");
    $("#email").val("");
}

function checkMakeClip() {
    var valid = false;

    if (isRadioChecked() && isValidEmail() && captchaValid && timesValid) {
        valid = true;
    }

    if (valid) {
        $("#downloadSubmit").removeAttr("disabled");
    } else {
        $("#downloadSubmit").prop("disabled", true);
    }
}

function isRadioChecked() {
    var checkedRadio = $("input:radio.radioFileType:checked");

    if (checkedRadio.length > 0) {
        $("#AudioOnly").val(checkedRadio[0].id === "fileType2");
        return true;
    }

    return false;

}

function isValidEmail() {
    var email = $("#email").val();
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var result = re.test(email);

    if (result) {
        $("#EmailAddress").val(email);
    }

    return result;
}

function expCallback() {
    captchaValid = false;
    checkMakeClip();
}

function recaptchaCallback(e) {
    //API Post, pass in e & secret key

    var data = {
        response: e,
        secret: "6LflGjcUAAAAAPuiIW9PDdfEPZvGhoCMUQx4izQ9"
    };

    $.ajax({
        method: 'POST',
        url: "/Event/ValidateCaptchaToken",
        data: data,
        success: function (response) {
            captchaValid = response;
            checkMakeClip();
        }
    });
}

function initInputMask() {
    var startDownload = document.getElementById("downloadStartTime");
    var endDownload = document.getElementById("downloadEndTime");

    startDownload.addEventListener("keydown", inputMask);
    endDownload.addEventListener("keydown", inputMask);

}

function initShareInputMask() {
    var startShare = document.getElementById("shareStartTime");
    var endShare = document.getElementById("shareEndTime");

    startShare.addEventListener("keydown", inputMask);
    endShare.addEventListener("keydown", inputMask);
}

function initShareUpdateEmbed() {
    var startShare = document.getElementById("shareStartTime");
    var endShare = document.getElementById("shareEndTime");

    startShare.addEventListener("focusout", reloadEmbedData);
    endShare.addEventListener("focusout", reloadEmbedData);
}

function inputMask(e) {
    var mask = "00:00:00";
    var text = e.target.value;
    var to = this.selectionStart;
    var from = this.selectionEnd;
    var input = "";

    //Arrow Keys
    if (e.keyCode < 41 && e.keyCode > 36) return;
    e.preventDefault();

    //Backspace
    if (e.keyCode === 8) {
        if (to === from) to--;
        input = text.substring(0, to) + mask.substring(to, from) + text.substring(from, text.length);
        ApplyMask(e.target, input, mask, to);
    }

    //Delete
    if (e.keyCode === 46) {
        if (to === from && e.keyCode === 46) from++;
        input = text.substring(0, to) + mask.substring(to, from) + text.substring(from, text.length);
        ApplyMask(e.target, input, mask, from);
    }

    //Numbers
    var key = parseInt(e.key);
    if (isNaN(key)) return;
    if (to === 8) return;

    if (to === 0 && key > 2) key = 2;
    if (to === 1 && text.substr(0, 1) === "2" && key > 3) key = 3;
    if ((to === 3 || to === 6 || to === 2 || to === 5) && key > 5) key = 5;

    if (text.substring(to, to + 1) === ":") {
        to++;
        from++;
    }
    if (to === from) {
        input = text.substr(0, to) + key + text.substr(from + 1, text.length);
    } else {
        var replace = key + mask.substr(to + 1, from - 1);
        input = text.substring(0, to) + replace;
        var length = input.length;
        input += text.substr(length, 8);
    }

    ApplyMask(e.target, input, mask, to + 1);
}

function ApplyMask(element, value, mask, cursor) {
    value += mask.substr(value.length, mask.length);
    element.value = value;
    element.selectionStart = cursor;
    element.selectionEnd = element.selectionStart;
}