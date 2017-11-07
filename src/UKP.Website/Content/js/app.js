var captchaValid = false;

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
}

function enableEmail() {
    var emailMe = $(".email-me");
    var terms = $(".download-terms");

    terms.fadeOut();
    emailMe.fadeIn();
}

function copyToClipbloard() {
    var copyTextarea = document.querySelector('#embed');
    copyTextarea.select();

    var log = $(".response-message");

    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        log.text("Copied to Clip Board");
        log.addClass("show");
    } catch (err) {
        log.text('unable to copy');
    }
}

function checkMakeClip() {
    var valid = false;
    var radioChecked = $(".fileType").prop("checked", true);
    if (radioChecked.length > 0 && isValidEmail() && captchaValid) {
        valid = true;
    }

    if (valid) {
        $("#downloadSubmit").removeAttr("disabled");
    } else {
        $("#downloadSubmit").prop("disabled", true);
    }
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