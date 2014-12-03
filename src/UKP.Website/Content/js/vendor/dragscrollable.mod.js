/*
 * jQuery dragscrollable Plugin
 * version: 1.0 (25-Jun-2009)
 * Copyright (c) 2009 Miquel Herrera
 *
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 *
 */
; (function ($) { // secure $ jQuery alias

    /**
     * Adds the ability to manage elements scroll by dragging
     * one or more of its descendant elements. Options parameter
     * allow to specifically select which inner elements will
     * respond to the drag events.
     *
     * options properties:
     * ------------------------------------------------------------------------
     *  dragSelector         | jquery selector to apply to each wrapped element
     *                       | to find which will be the dragging elements.
     *                       | Defaults to '>:first' which is the first child of
     *                       | scrollable element
     * ------------------------------------------------------------------------
     *  acceptPropagatedEvent| Will the dragging element accept propagated
     *	                     | events? default is yes, a propagated mouse event
     *	                     | on a inner element will be accepted and processed.
     *	                     | If set to false, only events originated on the
     *	                     | draggable elements will be processed.
     * ------------------------------------------------------------------------
     *  preventDefault       | Prevents the event to propagate further effectivey
     *                       | dissabling other default actions. Defaults to true
     * ------------------------------------------------------------------------
     *
     *  usage examples:
     *
     *  To add the scroll by drag to the element id=viewport when dragging its
     *  first child accepting any propagated events
     *	$('#viewport').dragscrollable();
     *
     *  To add the scroll by drag ability to any element div of class viewport
     *  when dragging its first descendant of class dragMe responding only to
     *  evcents originated on the '.dragMe' elements.
     *	$('div.viewport').dragscrollable({dragSelector:'.dragMe:first',
     *									  acceptPropagatedEvent: false});
     *
     *  Notice that some 'viewports' could be nested within others but events
     *  would not interfere as acceptPropagatedEvent is set to false.
     *
     */
    $.fn.dragscrollable = function (options) {
        return new DragScroll(options, this);
    }; //end plugin dragscrollable

    function DragScroll(options, elem) {
        var defaults = {
            dragSelector: '>:first',
            acceptPropagatedEvent: true,
            preventDefault: true,
            // Hovav:
            allowY: true
        }

        this.settings = $.extend({ target: elem }, defaults, options);
        this.start();
    }

    DragScroll.prototype = {
        start: function () {
            var _this = this;

            // set up the initial events
            $(this.settings.target).each(function () {
                // closure object data for each scrollable element
                var data = {
                    scrollable: $(this),
                    acceptPropagatedEvent: _this.settings.acceptPropagatedEvent,
                    preventDefault: _this.settings.preventDefault,
                    context: _this
                }
                // Set mouse initiating event on the desired descendant
                $(this).find(_this.settings.dragSelector).
                                bind('mousedown', data, $.proxy(_this.mouseDownHandler, _this));
            });
        },
        stop: function () {
            var _this = this;

            $(this.settings.target).each(function () {
                $(this).find(_this.settings.dragSelector).
                                off('mousedown', $.proxy(_this.mouseDownHandler, _this));
            });
        },
        mouseDownHandler: function (event) {
            // mousedown, left click, check propagation
            if (event.which != 1 ||
                (!event.data.acceptPropagatedEvent && event.target != this)) {
                return false;
            }

            // Initial coordinates will be the last when dragging
            event.data.lastCoord = { left: event.clientX, top: event.clientY };

            $.event.add(document, "mouseup",
                         this.mouseUpHandler, event.data);
            $.event.add(document, "mousemove",
                         this.mouseMoveHandler, event.data);
            if (event.data.preventDefault) {
                event.preventDefault();
                return false;
            }
        },
        mouseMoveHandler: function (event) { // User is dragging
            // How much did the mouse move?
            var delta = {
                left: (event.clientX - event.data.lastCoord.left),
                top: ((event.data.context.settings.allowY) ? event.clientY - event.data.lastCoord.top : 0)
            };

            // Set the scroll position relative to what ever the scroll is now
            event.data.scrollable.scrollLeft(
                            event.data.scrollable.scrollLeft() - delta.left);
            event.data.scrollable.scrollTop(
                            event.data.scrollable.scrollTop() - delta.top);

            // Save where the cursor is
            event.data.lastCoord = { left: event.clientX, top: event.clientY }
            if (event.data.preventDefault) {
                event.preventDefault();
                return false;
            }

        },
        mouseUpHandler: function (event) { // Stop scrolling
            $.event.remove(document, "mousemove", this.mouseMoveHandler);
            $.event.remove(document, "mouseup", this.mouseUpHandler);
            if (event.data.preventDefault) {
                event.preventDefault();
                return false;
            }
        }
    }

})(jQuery); // confine scope
