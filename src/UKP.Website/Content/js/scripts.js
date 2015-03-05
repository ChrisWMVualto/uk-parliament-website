function initCheckbox() {
    ////////////////////////////////////////////
    //bootstrap checkbox
    ////////////////////////////////////////////
    if ($(".checkbox").length) {
        $(".checkbox").checkbox({
            buttonStyle: 'btn-checkbox',
            buttonStyleChecked: 'btn-checkbox',
            checkedClass: 'fa fa-tick fa-2x',
            uncheckedClass: 'fa fa-tick fa-2x fa-dark',
            constructorCallback: null,
            defaultState: false,
            defaultEnabled: true,
            checked: false,
            enabled: true
        });
    }
}



$(document).ready(function () {

    ////////////////////////////////////////////
    //Cookies
    ////////////////////////////////////////////
    $('#cookiesAccept, #cookiesReject').bind('click', function () {

        var url = '/Cookie?accepted=';

        if ($(this).attr('id') == 'cookiesAccept')
            url += 'True';
        else
            url += 'False';

        $.ajax(url);
    });

    var cookieStateSetUrl = $('#CookieStateSetUrl').val(); 
    $.post(cookieStateSetUrl, {}, function (data) {
        $('#cookies').modal({
            show: !data.CookieSet
        });
    });



    ////////////////////////////////////////////
    //popover
    ////////////////////////////////////////////
    $('.popover-markup > .trigger').popover({
        html: true,
        title: function () {
            return $(this).parent().find('.head').html();
        },
        content: function () {
            return $(this).parent().find('.content').html();
        }
    });



    ////////////////////////////////////////////
    //home-select menu
    ////////////////////////////////////////////
    if ($(".select-home-tabs").length) {
        $('.select-home-tabs').selectpicker({
            style: 'btn-home-select',
            size: 4
        });
    }


    ////////////////////////////////////////////
    //truncate text
    ////////////////////////////////////////////
    if ($(".truncate-archive").length) {
        $('.truncate-archive').succinct({
            size: 80
        });
    }


    ////////////////////////////////////////////
    //search select param
    ////////////////////////////////////////////
    if ($(".select-search-param").length) {
        $('.select-search-param').selectpicker({
            style: 'btn-search-select form-control',
            size: 4
        });
    }


    ////////////////////////////////////////////
    //remove tag
    ////////////////////////////////////////////
    $(".btn-tag").click(function () {
        $(this).remove();
    });


    initCheckbox();

    ////////////////////////////////////////////
    //player share & info pane close btn's
    ////////////////////////////////////////////
    $(".close-info").click(function () {
        $("#info").removeClass("active");
        $("#info-tab").removeClass("active");
    });

    $(".close-share").click(function () {
        $("#share").removeClass("active");
        $("#share-tab").removeClass("active");
    });


    ////////////////////////////////////////////
    //home-tabs joined to responsive select
    ////////////////////////////////////////////
    $('select.select-home-tabs').on('change', function () {
        window.location = $(this).children(':selected').attr('data-url');
    });


    $('.multiselect-list').multiselect();


    ////////////////////////////////////////////
    //home-recently archived na btns
    ////////////////////////////////////////////
    $('#recentNextCommons').click(function () {
        $('#recentlyArchivedCommons').slickNext();
        return false;
    });

    $('#recentPrevCommons').click(function () {
        $('#recentlyArchivedCommons').slickPrev();
        return false;
    });

    $('#recentNextLords').click(function () {
        $('#recentlyArchivedLords').slickNext();
        return false;
    });

    $('#recentPrevLords').click(function () {
        $('#recentlyArchivedLords').slickPrev();
        return false;
    });

    $('#recentNextCommittees').click(function () {
        $('#recentlyArchivedCommittees').slickNext();
        return false;
    });

    $('#recentPrevCommittees').click(function () {
        $('#recentlyArchivedCommittees').slickPrev();
        return false;
    });


    ////////////////////////////////////////////
    //terms & conditions - share
    ////////////////////////////////////////////
    if ($(".tc-box ").length) {
        $('.tc-box ').slimScroll({
            railVisible: true,
            railColor: '#E6EBEE',
            railOpacity: 0.3,
            color: '#C1C7C9',
            size: '12px',
            height: '300'
        });
    }


    /////////////////////////////////////////////
    //click false
    /////////////////////////////////////////////
    $('.info, .close-pane').click(function (event) {
        event.preventDefault();
        // or use return false;
    });


    ////////////////////////////////////////////
    //datepicker
    ////////////////////////////////////////////
    if ($(".date-picker").length) {
        var startDateOb = $('.date-nav').length > 0 ? new Date($('.date-nav').data('start-date')) : null;

        if (startDateOb == null) {
            startDateOb = $('#searchPanel').length > 0 ? new Date($('#searchPanel').data('start-date')) : null;
        }

        if (startDateOb)
            startDateOb = new Date(startDateOb.setDate(startDateOb.getDate() + 1));

        $('.date-picker').datepicker({
            weekStart: 1,
            autoclose: true,
            zIndex: 2000,
            startDate: startDateOb,
            todayHighlight: true
        });

        $('.input-group-addon').on('click', function () {
            $(this).parent().find('.date-picker').datepicker('show');
        });
    }


    ////////////////////////////////////////////
    //carousels swipable
    ////////////////////////////////////////////
    if ($(".carousel").length) {
        $("#carouselCommons, #carouselLords, #carouselCommittees").swiperight(function () {
            $(this).carousel('prev');
        });
        $("#carouselCommons, #carouselLords, #carouselCommittees").swipeleft(function () {
            $(this).carousel('next');
        });
    }


    ////////////////////////////////////////////
    //breakpoints
    ////////////////////////////////////////////
    $(window).setBreakpoints({
        // use only largest available vs use all available
        distinct: true,
        // array of widths in pixels where breakpoints
        // should be triggered
        breakpoints: [
            300,
            768,
            1024
        ]
    });

    $(window).on('enterBreakpoint300', function () {

        $("#searchPanel").removeClass('in').css("height", "0");


        //terms box height
        $('.tc-box, .tc-box-container .slimScrollDiv').css({
            height: 'auto'
        });

    });

    $(window).on('exitBreakpoint300', function () {
        // $('#miniGuideCommons, #miniGuideLords, #miniGuideCommittees').unslick();
        $("#searchPanel").addClass('in').css("height", "auto");
    });

    $(window).on('enterBreakpoint768', function () {


        // $(".stack").slimScroll({destroy: true});
    });

    $(window).on('exitBreakpoint768', function () {

    });

    $(window).on('enterBreakpoint1024', function () {
        //add slick
        $('#recentlyArchivedCommons, #recentlyArchivedLords, #recentlyArchivedCommittees').slick({
            centerMode: false,
            infinite: true,
            arrows: false,
            centerPadding: '0',
            slidesToShow: 1,
            onAfterChange: updateArrowText,
            vertical: false,
            useCSS: false,
            lazyLoad: 'progressive'
        });
        

        //terms box height
        $('.tc-box, .tc-box-container .slimScrollDiv').css({
            height: '300px'
        });


    });

    $(window).on('exitBreakpoint1024', function () {
        //remove slick carousel
        $('#recentlyArchivedCommons, #recentlyArchivedLords, #recentlyArchivedCommittees').unslick();


    });

    setTimeout(function () {
        $('.archived-wrapper').removeClass('archived-hidden');
    }, 1200);

});


////////////////////////////////////////////
//carousel text
////////////////////////////////////////////
function updateArrowText() {
    var next = $(this.$list.context).parents().eq(2).find('.recent-next div p'),
        prev = $(this.$list.context).parents().eq(2).find('.recent-prev div p'),
        childrenSelector = 'a div div div p',
        activeIndex = null,
        nextIndex = null,
        prevIndex = null;


    for (var i = 0; i < this.$slides.length; i++) {
        if ($(this.$slides[i]).hasClass("slick-active")) {
            activeIndex = i;
            break;
        }
    }

    if (activeIndex == this.$slides.length - 1)
        nextIndex = 0;
    else
        nextIndex = activeIndex + 1;

    if (activeIndex == 0)
        prevIndex = this.$slides.length - 1;
    else
        prevIndex = activeIndex - 1;

    next.html($(this.$slides[nextIndex]).find(childrenSelector).text());
    prev.html($(this.$slides[prevIndex]).find(childrenSelector).text());
}


////////////////////////////////////////////
//Position scrolling based on time
////////////////////////////////////////////
function leftPositionFromTime(hour, minute) {
    return ((hour * globHourWidth) + (globHourWidth * (minute / 60)));
}