/*
 * blueimp Gallery Indicator JS 1.1.0
 * https://github.com/blueimp/Gallery
 *
 * Copyright 2013, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/* global define, window, document */

(function (factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // Register as an anonymous AMD module:
        define([
            './blueimp-helper',
            './blueimp-gallery'
        ], factory);
    } else {
        // Browser globals:
        factory(
            window.blueimp.helper || window.jQuery,
            window.blueimp.Gallery
        );
    }
}(function ($, Gallery) {
    'use strict';

    $.extend(Gallery.prototype.options, {
        // The tag name, Id, element or querySelector of the indicator container:
        externalIndicatorContainer: '.externalThumbnails',
        // The class for the active indicator:
        activeExternalIndicatorClass: 'active',
    });

    var initSlides = Gallery.prototype.initSlides,
        addSlide = Gallery.prototype.addSlide,
        resetSlides = Gallery.prototype.resetSlides,
        handleClick = Gallery.prototype.handleClick,
        handleSlide = Gallery.prototype.handleSlide,
        handleClose = Gallery.prototype.handleClose;

    $.extend(Gallery.prototype, {

        setActiveExternalIndicator: function (index) {
            if (this.externalIndicators) {
                if (this.activeExternalIndicator) {
                    this.activeExternalIndicator
                        .removeClass(this.options.activeExternalIndicatorClass);
                }
                this.activeExternalIndicator = $(this.externalIndicators[index]);
                this.activeExternalIndicator
                    .addClass(this.options.activeExternalIndicatorClass);
            }
        },

        initSlides: function (reload) {
            if (!reload) {
                this.externalIndicatorContainer = $(this.options.externalIndicatorContainer);

                if (this.externalIndicatorContainer.length) {
                    this.externalIndicators = this.externalIndicatorContainer[0].children;
                    var gallery = this;
                    for (var i = 0; i < this.externalIndicators.length; i++) {
                        this.externalIndicators[i].set
                        $(this.externalIndicators[i]).on('click', function (ev) {
                            gallery.preventDefault(ev);
                            gallery.slide(i);
                        });
                    }
                }
            }
            initSlides.call(this, reload);
        },


       /* handleClick: function (event) {
            var target = event.target || event.srcElement,
                parent = target.parentNode;
            if (parent === this.indicatorContainer[0]) {
                // Click on indicator element
                this.preventDefault(event);
                this.slide(this.getNodeIndex(target));
            } else if (parent.parentNode === this.indicatorContainer[0]) {
                // Click on indicator child element
                this.preventDefault(event);
                this.slide(this.getNodeIndex(parent));
            } else {
                return handleClick.call(this, event);
            }
        },*/

        handleSlide: function (index) {
            handleSlide.call(this, index);
            this.setActiveExternalIndicator(index);
        },

        handleClose: function () {
            if (this.activeExternalIndicator) {
                this.activeExternalIndicator
                    .removeClass(this.options.activeExternalIndicatorClass);
            }
            handleClose.call(this);
        }

    });

    return Gallery;
}));
