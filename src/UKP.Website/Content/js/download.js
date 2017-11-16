function createDownload() {
    var form = $("#createDownloadForm");
    var data = $(form).serialize();

    $(".error-message").prop("hidden", true);
    $("#downloadSubmit").prop("disabled", true);

    $.ajax({
        method: 'POST',
        url: "/Event/CreateDownload",
        data: data,
        success: function (data) {
            var response = JSON.parse(data);

            if (response.Success) {
                $(".thankyou-header").text("Thank you");
                $(".thankyou-message").text(response.Message);
                $(".thankyou-email").text(response.Email);

                $(".download-form").prop("hidden", true);
                $(".thankyou").removeAttr("hidden");

                window.location.href += "#thankyou";
            } else {
                $(".error-message").removeAttr("hidden");
                $(".error-message").text(response.Message);
            }
        }
    });

}

window.addEventListener("message", updateCurrentTime);

function updateCurrentTime(event) {
    var messageSplit = event.data.split("_");
    document.getElementById("ProgramDateTime").value = messageSplit[1];
}