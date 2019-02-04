
(function ($) {
    'use strict';
    angular
        .module('kaiptcapp')
        .controller('AdmissionController', AdmissionController);
    AdmissionController.$inject = ['brudexservices', '$location', '$window', 'brudexutils', 'NotificationService'];
    function AdmissionController(services, location, $window, utils, NotificationService) {
        var vm = this;
        vm.errorMsg = [];
        vm.successMsg = [];
        vm.searchResult = [];
        vm.model = { searchFormValid: false, searchMode: 'list' };

        vm.init = function () {
            getAdmissions();

        };

        function getAdmissions() {
            var payload = vm.model;
            services.searchAdmissions(payload, function (response) {
                if (response.status === "00") {
                    vm.searchResult = response.data;
                }
            });
        }


        vm.findStudent = function () {
            var payload = vm.model;
            services.findStudent(payload, function (response) {
                if (response.status === "00") {
                    vm.searchResult = response.data;
                    
                }
            });
        }

        vm.newAdmission = function () {
            $("#newAdmissionModal").modal("show");
        }

        vm.saveAdmission = function () {
            services.saveAdmission(vm.applicationModel,
                function (response) {
                    console.log(response);
                });
        }

        vm.advancedSearch = function () {
            $("#AdvancedSearchModal").modal("show");
        }

        var initApplicationsTable = function () {

            var table = $('#applicationsTable');

            var settings = {
                "sDom": "t",
                "destroy": true,
                "paging": false,
                "scrollCollapse": true,
                "aoColumnDefs": [
                    {
                        'bSortable': false,
                        'aTargets': [0]
                    }
                ],
                "order": [
                    [1, "desc"]
                ]

            };

            table.dataTable(settings);

            $('#applicationsTable input[type=checkbox]').click(function () {
                if ($(this).is(':checked')) {
                    $(this).closest('tr').addClass('selected');
                } else {
                    $(this).closest('tr').removeClass('selected');
                }

            });
        }

    }
})(jQuery);
