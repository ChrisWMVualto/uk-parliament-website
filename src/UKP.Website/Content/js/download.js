function createDownload() {
    var url = $("#createDownloadUrl");
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