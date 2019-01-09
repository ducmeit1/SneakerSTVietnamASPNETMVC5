jQuery(function ($) {
    /*--------------------------*/
    /*Slider
    /*--------------------------*/
    jQuery('#slides').superslides({
        animation: 'slide',
        play: '5000'
    });

    /* ----------------------------------------------------------- */
    /*  Fixed Top Menubar
     /* ----------------------------------------------------------- */

    // For fixed top bar
    $(window).scroll(function () {
        if ($(window).scrollTop() > 100 /*or $(window).height()*/) {
            $(".navbar-fixed-top").addClass('past-main');
        } else {
            $(".navbar-fixed-top").removeClass('past-main');
        }
    });
    /* ----------------------------------------------------------- */
    /*  Wow smooth animation
     /* ----------------------------------------------------------- */

    wow = new WOW(
            {
                animateClass: 'animated',
                offset: 100
            }
    );
    wow.init();

    /* ----------------------------------------------------------- */
    /*  SCROLL TOP BUTTON
     /* ----------------------------------------------------------- */

    //Check to see if the window is top if not then display button

    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.scrollToTop').fadeIn();
        } else {
            $('.scrollToTop').fadeOut();
        }
    });

    //Click event to scroll to top

    $('.scrollToTop').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });

    /* ----------------------------------------------------------- */
    /*  SEARCH
     /* ----------------------------------------------------------- */
    $(function () {
        $("#searchButton").click(function () {
            $('#searchBox').addClass('popup-box-on');
        });

        $("#removeClass").click(function () {
            $('#searchBox').removeClass('popup-box-on');
        });
    });

    /* ----------------------------------------------------------- */
    /*  SWAP IMAGE OF PRODUCT 
     /* ----------------------------------------------------------- */

    $('.thumbs img').click(function () {
        var thmb = this;
        var src = this.src;
        $('.main_pic img').fadeOut(400, function () {
            $(this).fadeIn(400)[0].src = src;
        });
    });

    $(document).ready(function () {
        var tn_array = Array();
        var newsrc;
        var index;
        $('.thumbs img').each(function () {
            tn_array.push($(this).attr('src'));
        });
        var size = tn_array.length;
        var src = $('.main_pic img').attr('src');
        for (var i = 0; i < size - 1; i++) {
            if (src === tn_array[i]) {
                index = i;
            }
        }
        $('#next span.icon').click(function () {
            index += 1;
            if (index === size) {
                index = 0;
            }
            newsrc = tn_array[index];
            $('.main_pic img').fadeOut(400, function () {
                $(this).fadeIn(400)[0].src = newsrc;
            });
        });
        $('#previous span.icon').click(function () {
            index -= 1;
            if (index <= 0) {
                index = size - 1;
            }
            newsrc = tn_array[index];
            $('.main_pic img').fadeOut(400, function () {
                $(this).fadeIn(400)[0].src = newsrc;
            });
        });
    });
})