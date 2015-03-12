function resizePlayer() {
    var currentWidth = $(".video-container").width();
    $(".video-container").height(Math.ceil((currentWidth / 16) * 9));
}

$(function () {
    resizePlayer();
    $(window).resize(function () {
        resizePlayer();
    });
});