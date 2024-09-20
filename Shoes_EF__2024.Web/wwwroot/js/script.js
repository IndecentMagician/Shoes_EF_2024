/**
 * WEBSITE: https://furioustheme.com
 * GITHUB: https://github.com/furioustheme/
 */

(function ($) {
    'use strict';



    /*---------------------------------
        ---> Menu
        ---------------------------------*/
    var toolbarToggle = $('.toolbar-toggle'),
        toolbarDropdown = $('.toolbar-dropdown'),
        toolbarSection = $('.toolbar-section');

    function closeToolBox() {
        toolbarToggle.removeClass('active');
        toolbarSection.removeClass('current');
    }

    toolbarToggle.on('click', function (e) {
        var currentValue = $(this).attr('href');
        if ($(e.target).is('.active')) {
            closeToolBox();
            toolbarDropdown.removeClass('open');
        } else {
            toolbarDropdown.addClass('open');

            closeToolBox();
            $(this).addClass('active');
            $(currentValue).addClass('current');
        }
        e.preventDefault();
    });


    // hero slider
    $('.hero-slider').slick({
        arrows: false,
        dots: true
    });


    // mixitup filter
    var containerEl = document.querySelector('[data-ref~="mixitup-container"]');
    var mixer;
    if (containerEl) {
        mixer = mixitup(containerEl, {
            selectors: {
                target: '[data-ref~="mixitup-target"]'
            }
        });
    }

    //Active changer
    $('.control').on('click', function () {
        $('.control').removeClass("active");
        $(this).addClass("active");
    });


    //   magnific popup
    $('.image-zoom-gallary').magnificPopup({
        type: 'image',
        // other options
        gallery: {
            enabled: true
        }
    })
    $('.image-zoom').magnificPopup({
        type: 'image'
    })

    // testimonial slider
    $('.testimonial-slider').slick({
        dots: true,
        infinite: false,
        speed: 300,
        slidesToShow: 3,
        slidesToScroll: 1,
        arrows: false,
        responsive: [{
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    infinite: true,
                    dots: true
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]
    });

})(jQuery);