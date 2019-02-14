(function ($) {
    'use strict';
    angular
        .module('kaiptcapp')
        .controller('StudentMgmtController', StudentMgmtController);
    StudentMgmtController.$inject = ['brudexservices', '$location', '$window', 'brudexutils'];
    function StudentMgmtController(services, location, $window, utils) {
        var vm = this;
        vm.errorMsg = [];
        vm.successMsg = [];
        vm.searchResult = [];
        vm.model = { };

        vm.init = function () {
            listRecentStudents();
        };



        function listRecentStudents() {
            var payload = { searchMode: "list" ,limit:100};
            services.findStudent(payload, function (response) {
                 
                    if (response.Status === "00") {
                        vm.searchResult = response.data;
                    }
                }); 
        }

        vm.findStudent = function () {
            var payload = vm.model;
            services.findStudent(payload, function (response) {
                if (response.Status === "00") {
                    vm.searchResult = response.data;
                }
            });
        }

        vm.newAdmission = function () {
            $("#newAdmissionModal").modal("show");
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
