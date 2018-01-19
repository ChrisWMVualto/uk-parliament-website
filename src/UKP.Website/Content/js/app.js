var captchaValid = false;
var timesValid = true;
var keepError = false;

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
    fileType.on("change", checkMakeClip);
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

    startTimeBox.val(new Date(document.getElementById("StartTime").value).toTimeString().split(" ")[0]);
    endTimeBox.val(new Date(document.getElementById("EndTime").value).toTimeString().split(" ")[0]);

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

    var startTime = startDate.toISOString().split(".")[0] + "Z";
    var endTime = $("#EndTime").val();

    if (endTime === "") {
        endTime = document.getElementById("MeetingEndTime").value;
    }

    if (startTime < endTime && startTime >= meetingStartTime) {

        var endDate = new Date(endTime);

        if (endDate - startDate >= 32400000) {
            setErrorMessage("Clip cannot exceed 9 hours");
            return;
        }

        setDownloadTimeForm("StartTime", startTime);
        timesValid = true;
        checkMakeClip();
    } else {
        keepError = false;
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

    var endTime = endDate.toISOString().split(".")[0] + "Z";
    var startTime = $("#StartTime").val();

    if (endTime > startTime && endTime <= meetingEndTime) {
        setDownloadTimeForm("EndTime", endTime);
        timesValid = true;
        checkMakeClip();
    } else {
        keepError = false;
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

    urlPageNavigation("#player-tabs");
}

function enableEmail() {
    var emailMe = $(".email-me");
    var terms = $(".download-terms");

    terms.fadeOut();
    emailMe.fadeIn();

    urlPageNavigation("#player-tabs");
}

function urlPageNavigation(tag) {
    var url = window.location.href;

    if (url.indexOf("#") !== -1) {
        url = (url.substring(0, url.indexOf("#")) + tag);
    } else {
        url += tag;
    }

    window.location.href = url;
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
    document.getElementById("ClipRequested").value = false;
    document.getElementById("EmailAddress").value = "";
}

function checkMakeClip() {
    var valid = false;
    var clipRequested = (document.getElementById("ClipRequested").value === "true");

    if (isRadioChecked() && isValidEmail() && captchaValid && timesValid && !clipRequested) {
        valid = true;
    }

    if (valid) {
        $("#downloadSubmit").removeAttr("disabled");
        if (!keepError) {
            $(".error-message").prop("hidden", true);
        }
        
    } else {
        $("#downloadSubmit").prop("disabled", true);
    }

    return valid;
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

function isValidCaptcha() {
    var result = false;
    $.ajax({
        method: 'GET',
        url: "/Event/ValidateCaptcha",
        success: function (response) {
            result = response.captchaCompleted;
        },
        async: false
    });

    return result;
}

function expCallback() {
    captchaValid = false;

    $.ajax({
        url: "/Event/ResetCaptcha"
    });

    checkMakeClip();
}

function recaptchaCallback(e) {
    //API Post, pass in e & secret key

    var data = {
        response: e
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
    startDownload.addEventListener("input", mobileMask);
    endDownload.addEventListener("keydown", inputMask);
    endDownload.addEventListener("input", mobileMask);

}

function initShareInputMask() {
    var startShare = document.getElementById("shareStartTime");
    var endShare = document.getElementById("shareEndTime");

    startShare.addEventListener("keydown", inputMask);
    startShare.addEventListener("input", mobileMask);
    endShare.addEventListener("keydown", inputMask);
    endShare.addEventListener("input", mobileMask);
}

function initShareUpdateEmbed() {
    var startShare = document.getElementById("shareStartTime");
    var endShare = document.getElementById("shareEndTime");

    startShare.addEventListener("focusout", reloadEmbedData);
    endShare.addEventListener("focusout", reloadEmbedData);
}

var androidInput = false;

function mobileMask(e) {
    var mask = "00:00:00";
    var beforeInput = e.target.dataset.lastInput;
    if (androidInput) {
        var to = this.selectionStart;
        var from = this.selectionEnd;
        var input;
        if (to === from) {
            input = e.target.value.substring(to - 1, from);
            to--;
        } else input = e.target.value.substring(to, from);
        if (beforeInput !== e.target.value) {
            var key = parseInt(input);
            if (isNaN(key) || to === 8) {
                e.target.value = beforeInput;
                return;
            }
            if (beforeInput.substring(to, to + 1) === ":") {
                to++;
                from++;
            }
            if (e.target.value.substring(to + 1, to + 2) === ":") {
                to++;
                from++;
            }

            if (to === 0 && key > 2) {
                key = 2;
                if (parseInt(e.target.value.substr(to + 1, to + 2)) > 3 && key > 1) {
                    key = 1;
                }
            }
            if (to === 1 && e.target.value.substr(0, 1) === "2" && key > 3) key = 3;
            if ((to === 3 || to === 6 || to === 2 || to === 5) && key > 5) key = 5;


            e.target.value = beforeInput.substring(0, to) + key + beforeInput.substring(from, beforeInput.length);
            e.target.value += mask.substr(e.target.value.length, mask.length);

            beforeInput = e.target.value;
            e.target.dataset.lastInput = beforeInput;
            this.selectionStart = to + 1;
            this.selectionEnd = to + 1;
        }
        androidInput = false;
    }
}

function inputMask(e) {
    var mask = "00:00:00";
    var text = e.target.value;
    var to = this.selectionStart;
    var from = this.selectionEnd;
    var input = "";

    if (e.keyCode === 229) {
        androidInput = true;
        return;
    }

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

    if (to === 0 && key > 2) {
        key = 2;
    }
    if (parseInt(text.substr(to + 1, to + 2)) > 3 && key > 1) {
        key = 1;
    }
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
    element.dataset.lastInput = element.value;
}