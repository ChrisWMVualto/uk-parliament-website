var captchaValid = false;
var timesValid = true;
var keepError = false;

$(function () {
    initTermsAndConditions();
    initEnableEmbed();
    initSetShareTime();
    initSetDownloadTime();
    initSetClipboard();
    initSetFileType();
    initEmailValid();
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

function initEmailValid() {
    var email = $("#email");
    email.on("change", checkMakeClip);
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
        if (!keepError) {
            $(".error-message").prop("hidden", true);
        }
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

        if (isLivePlayer()) {
            var liveSeconds = new Date(meetingEndTime).getTime() / 1000;
            var endSeconds = endDate.getTime() / 1000;
            if (liveSeconds - endSeconds <= 11) {
                setErrorMessage("End Time cannot be within 10 seconds of the live edge");
                timesValid = false;
                return;
            }
        }

        setDownloadTimeForm("EndTime", endTime);
        timesValid = true;
        if (!keepError) {
            $(".error-message").prop("hidden", true);
        }
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

function compareTimes() {
    var startTimeArray = document.getElementById("downloadStartTime").value.split(":");
    var startDateString = document.querySelectorAll('[data-id="startDownloadDate"]')[0];

    var endTimeArray = document.getElementById("downloadEndTime").value.split(":");
    var endDateString = document.querySelectorAll('[data-id="endDownloadDate"]')[0];

    var startDate = getDate(startTimeArray, startDateString);
    var endDate = getDate(endTimeArray, endDateString);

    var meetingStartTime = document.getElementById("MeetingStartTime").value;
    var meetingEndTime = document.getElementById("MeetingEndTime").value;

    if (isNaN(endDate.valueOf()) || isNaN(startDate.valueOf())) {
        return false;
    }

    var startTime = startDate.toISOString().split(".")[0] + "Z";
    var endTime = endDate.toISOString().split(".")[0] + "Z";

    if ((endDate - startDate >= 10000) && endTime <= meetingEndTime && startTime >= meetingStartTime) {

        if (endDate - startDate >= 32400000) {
            setErrorMessage("Clip cannot exceed 9 hours");
            return false;
        }

        if (!checkLiveEdge(meetingEndTime, endDate)) {
            return false;
        }

        setDownloadTimeForm("StartTime", startTime);
        setDownloadTimeForm("EndTime", endTime);
        timesValid = true;
        if (!keepError) {
            document.getElementsByClassName("error-message")[0]["hidden"] = true;
        }
        return checkMakeClip();
        
    } else {
        keepError = false;
        setTimeError(startTime, endTime, meetingStartTime);

        var start = new Date(meetingStartTime);
        var meetingStartDate = new Date(start.getFullYear(), start.getMonth(), start.getDate());

        var end = new Date(meetingEndTime);
        var meetingEndDate = new Date(end.getFullYear(), end.getMonth(), end.getDate());

        if (Math.floor((meetingEndDate.getTime() - meetingStartDate.getTime()) / (1000 * 3600 * 24)) > 0) {
            $(".error-message").append(" Please ensure you have selected the correct date");
        }

        return false;
    }

}

function getDate(timeArray, dateString) {
    var date = new Date(Date.parse(dateString.title));
    date.setHours(date.getHours() + timeArray[0]);
    date.setMinutes(date.getMinutes() + timeArray[1]);
    date.setSeconds(date.getSeconds() + timeArray[2]);
    return date;
}

function checkLiveEdge(meetingEndTime, endDate) {

    if (isLivePlayer()) {
        var liveSeconds = new Date(meetingEndTime).getTime() / 1000;
        var endSeconds = endDate.getTime() / 1000;
        if (liveSeconds - endSeconds <= 11) {
            setErrorMessage("End Time cannot be within 10 seconds of the live edge");
            timesValid = false;
            return false;
        }
    }

    return true;
}

function setTimeError(startTime, endTime, meetingStartTime) {

    startDate = new Date(startTime);
    endDate = new Date(endTime);

    if (startTime > endTime) {
        setErrorMessage("Start Time cannot be later than the End Time.");
    } else if (endDate - startDate < 10000) {
        setErrorMessage("Start Time and End Time must be at least 10 seconds apart.");
    } else if (startTime < meetingStartTime) {
        setErrorMessage("Start Time cannot be earlier than the meeting start time.");
    } else if (endTime < startTime) {
        setErrorMessage("End Time cannot be earlier than the Start Time.");
    } else {
        setErrorMessage("End Time cannot be later than the meeting end time.");
    }
}

function termsAndConditionsClickHandler(e) {
    var id = e.target.dataset.continueId;

    enableDisableButton(id, e.target.checked);
}

function enableDisableButton(id, checked) {
    var button = document.getElementById(id);

    if (checked) {
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
    grecaptcha.reset();
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

function captchaResponse() {
    var response = grecaptcha.getResponse();

    return response;
}

function expCallback() {
    captchaValid = false;
    checkMakeClip();
}

function recaptchaCallback(e) {
    captchaValid = true;
    checkMakeClip();
}

function initShareUpdateEmbed() {
    var startShare = document.getElementById("shareStartTime");
    var endShare = document.getElementById("shareEndTime");

    if (startShare != null && endShare != null) {
        startShare.addEventListener("focusout", reloadEmbedData);
        endShare.addEventListener("focusout", reloadEmbedData);

        startShare.addEventListener("focusout", updatePartnerElement);
        endShare.addEventListener("focusout", updatePartnerElement);

        startShare.addEventListener("focusout", compareTimes);
        endShare.addEventListener("focusout", compareTimes);
    }
}

function updatePartnerElement(e) {
    document.getElementById(e.target.dataset.partnerId).value = e.target.value;
    reloadEmbedData();
}
