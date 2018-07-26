$(document).ready(function() {
    var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;

    if (iOS) {
        $("#deviceMessage")
            .text(
                "Unfortunately, it is not currently possible to save parliamentlive.tv downloads on an iOS device (e.g. Apple iPhones, iPads). Please return to this page from an alternative device to save your download.");
    }
});