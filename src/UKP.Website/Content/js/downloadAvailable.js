$(document).ready(function() {
    var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;

    if (iOS) {
        $("#deviceMessage")
            .text(
                "It is not possible to download to iOS devices (e.g. Apple iPhones, iPads), we recommend saving to an alternative device.");
    }
});