function resizePlayer() {
    var currentWidth = $(".video-container").width();
    $(".video-container").height(Math.ceil((currentWidth / 16) * 9));
}

$(function () {
    setTimeout(function() {
        resizePlayer();
        $(window).resize(function() {
            resizePlayer();
        });
    }, 300);
});