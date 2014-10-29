// Adds the ability to use .net stylr string formatting
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined' ? args[number] : match;
        });
    };
}

function momentSearch() {
    var obj = {
        'parent': $(this).parent().parent(),
        'buttonCont': $(this).parent(),
        'url': $(this).attr('data-url-base')
    };


    function init() {
        obj.buttonCont.remove();
        fetchResults();
    }

    function fetchResults() {
        $.ajax(obj.url, {
            success: processResults
        });
    }

    function processResults(data, textStatus, jqXHR) {
        obj.parent.append(data);
    }

    init();
}



$(function () {

    $('.moment-more button').on('click', momentSearch);

    $('#members').autocomplete({
        serviceUrl: $('#members').attr('data-ajax-url'),
        displayItem: "DisplayAs",
        objectPath: "Members.Member",
        minChars: 3,
        // callback function:
        onSelect: function (member) {
            $('#MemberId').val(member['@Member_Id']);
        },
        noCache: true
    });


    $('#searchResultsContainer').append('<span class="pagination"><a href="' + window.location + '"></a></span>');
    $('#searchResultsContainer').infinitescroll({
        debug: true,
        prefill: true,
        itemSelector: '.search-item',
        nextSelector: $('.pagination a'),
        navSelector: $('.pagination'),
        path: function (pageNum) {
            return window.location.href + '&page=' + pageNum;
        },
        loading: {
            finishedMsg: '',
            finished: function() {
                $('.moment-more button').on('click', momentSearch);
            }
        }
    });
});