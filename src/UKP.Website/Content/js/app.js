$(function () {
    initTermsAndConditions();
    initEnableEmbed();
    initSetShareTime();
    initSetDownloadTime();
    initSetClipboard();
});

function initTermsAndConditions() {
    var checkboxes = $(".acceptance");
    checkboxes.on("click", termsAndConditionsClickHandler);

}

function initEnableEmbed() {
    var continueButton = document.getElementById("shareContinue");
    continueButton.addEventListener("click", enableEmbed);
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
    var terms = $(".terms");

    terms.fadeOut();
    embed.fadeIn();
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