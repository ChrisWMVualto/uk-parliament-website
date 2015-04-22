function resizePlayer() {
    var currentWidth = $(".video-container").width();
    $(".video-container").height(Math.ceil((currentWidth / 16) * 9));
    $(".video-container iframe").css("height", "100%");
}

$(function () {
    resizePlayer();

    setTimeout(function() {
        resizePlayer();
    }, 1000);

    
    $(window).resize(function () {
        resizePlayer();
    });
});