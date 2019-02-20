/* ============================================================
 * Form Elements
 * This file applies various jQuery plugins to form elements
 * For DEMO purposes only. Extract what you need.
 * ============================================================ */
(function($) {

    'use strict';

    var getBaseURL = function() {
        var url = document.URL;
        return url.substr(0, url.lastIndexOf('/'));
    }

    $(document).ready(function() {

        //Multiselect - Select2 plug-in
        $("#multi").val(["Jim", "Lucy"]).select2();

        //Date Pickersformat: 'MM/DD/YYYY h:mm A'
        $('#datepicker-range, #datepicker-component1, #datepicker-component2').datepicker({format: 'yyyy-mm-dd'});

        $('#datepicker-embeded').datepicker({
            daysOfWeekDisabled: "0,1"
        });


        $('#daterangepicker').daterangepicker({
            timePicker: true,
            timePickerIncrement: 30,
            format: 'MM/DD/YYYY h:mm A'
        }, function(start, end, label) {
            console.log(start.toISOString(), end.toISOString(), label);
        });

        /* Time picker
         * https://github.com/m3wolf/bootstrap3-timepicker
         */
        $('#timepicker').timepicker().on('show.timepicker', function(e) {
            var widget = $('.bootstrap-timepicker-widget');
            widget.find('.glyphicon-chevron-up').removeClass().addClass('pg-arrow_maximize');
            widget.find('.glyphicon-chevron-down').removeClass().addClass('pg-arrow_minimize');
        });



        // disabling dates
        var nowTemp = new Date();
        var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);

        //Input mask - Input helper
        $(function($) {
            $("#date").mask("99/99/9999");
            $("#phone").mask("(999) 999-9999");
            $("#tin").mask("99-9999999");
            $("#ssn").mask("999-99-9999");
        });
        //Autonumeric plug-in - automatic addition of dollar signs,etc controlled by tag attributes
        $('.autonumeric').autoNumeric('init');

        //Drag n Drop up-loader
        $("div#myId").dropzone({
            url: "/file/post"
        });
    

        
        $('#summernote').summernote({
            height: 200,
            onfocus: function(e) {
                $('body').addClass('overlay-disabled');
            },
            onblur: function(e) {
                $('body').removeClass('overlay-disabled');
            }
        });


    });

})(window.jQuery);