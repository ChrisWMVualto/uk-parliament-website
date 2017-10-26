function createDownload() {
    var form = $("#createDownloadForm");
    var data = $(form).serialize();

    $.ajax({
        method: 'POST',
        url: "/Event/CreateDownload",
        data: data,
        success: function (data) {
            var response = JSON.parse(data);
            alert(response.Message);
        }
    });

}

window.addEventListener("message", updateCurrentTime);

function updateCurrentTime(event) {
    var messageSplit = event.data.split("_");
    document.getElementById("ProgramDateTime").value = messageSplit[1];
}