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
        'url': $(this).attr('data-url-base'),
        'queryParams': '',
    };
    init();

    function init() {
        obj.buttonCont.remove();
        fetchResults();
    }

    function buildUrl() {
        return obj.url + obj.queryParams.format(obj.keywords, obj.memberId, obj.house, obj.business, obj.eventId, obj.skipMoments);
    }

    function fetchResults() {
        $.ajax(buildUrl(), {
            success: processResults
        });
    }

    function processResults(data, textStatus, jqXHR) {
        obj.parent.append(data);
    }
}
$('.moment-more button').on('click', momentSearch);

$(function () {
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

    $('#tags')
        .autocomplete({
            serviceUrl: "http://local.ias.ukp/api/search/tags?tag=",
            delimiter: ', ',
            displayItem: "displayTag",
            objectPath: "",
            categoryItem: "category",
            minChars: 1,
        })
        .bind('input', tagInputParser);

    function tagAppender(tag, selector) {
        if (selector.val().length > 0)
            selector.val(selector.val() + ", " + tag);

        else
            selector.val(tag);
    }

    function tagDestroy() {
        var tag = $(this).text();
        tag = tag.replace(/(,)?(\s)?$/, '');

        var inputContent = $('#tags').val();
        var removeTagRegEx = new RegExp("(" + tag.replace(' ', '\\s') + "){1}(,)?(\\s)*");

        inputContent = inputContent.replace(removeTagRegEx, '');
        $('#tags').val(inputContent).trigger('input');

        $(this).remove();
    }

    function tagInputParser() {
        var inputValue = $(this).val();
        inputValue = inputValue.split(',');
        inputValue.pop();
        var validTags = [];

        $('#House').val('');
        $('#Business').val('');

        $.each(inputValue, function () {
            var tag = this.split(": ");

            if (tag[0].trim() == "House")
                tagAppender(tag[1], $('#House'));

            if (tag[0].trim() == "Business")
                tagAppender(tag[1], $('#Business'));

            if (tag[1] != null && tag[1] != "")
                validTags.push(this);
        });

        $('#selectedTags').empty();
        $.each(validTags, function () {
            $('#selectedTags').append('<button type="button" class="btn btn-default btn-tag btn-lg">' + this.replace(/^\s/, '') + ' <i class="fa fa-close fa-lg"></i></button> ');
        });
        $('#selectedTags button').bind('click', tagDestroy);
    }

    $('#searchResultsContainer').append('<span class="pagination"><a href="' + window.location + '"></a></span>');
    $('#searchResultsContainer').infinitescroll({
        debug: true,
        prefill: true,
        itemSelector: '.search-item',
        nextSelector: $('.pagination a'),
        navSelector: $('.pagination'),
        path: function (pageNum) {
            return window.location + '&page=' + pageNum;
        },
        loading: {
            finishedMsg: '',
            finished: function() {
                $('.moment-more button').on('click', momentSearch);
            }
        }
    });
});