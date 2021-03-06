﻿function momentSearch() {
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

    $('#Member').keypress(function () {
        $('#MemberId').val('');
    });

    $('#Member').autocomplete({
        serviceUrl: $('#Member').attr('data-ajax-url') + '?name=',
        displayItem: "Member",
        minChars: 3,
        delimiter: '',
        onSelect: function (member) {
            $('#MemberId').val(member['MembersNameId']);
        },
        noCache: false
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
            finished: function () {
                $('.moment-more button').on('click', momentSearch);
            },
            msgText: ''
        }
    });


    $('.start-date').datepicker().on('changeDate', function (e) {

        $('.end-date').datepicker('update', e.date);
    });


    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        $('#Start, #End').attr('readonly', true);
    }
});