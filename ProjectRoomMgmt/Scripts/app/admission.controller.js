(function ($) {
    'use strict';
    angular
        .module('kaiptcapp')
        .controller('AdmissionController', AdmissionController);
    AdmissionController.$inject = ['$scope', '$rootScope', 'brudexservices', '$location', '$window', 'brudexutils', 'NotificationService'];
    function AdmissionController($scope, $rootScope, services, location, $window, utils, NotificationService) {
        var vm = this;
        vm.errorMsg = [];
        vm.successMsg = [];
        vm.searchResult = [];
        vm.model = { searchFormValid: false, searchMode: 'list' };

        vm.init = function () {
            getAdmissions();
        };

        vm.openAction = function (action, admissionData) {

            switch (action) {
                case "action_edit_admission":
                    vm.isEdit = true;
                    vm.applicationModel = admissionData;
                    vm.viewTitle = "Edit Admission";
                    vm.subTitle = vm.applicationModel.FullName + " [" + vm.applicationModel.AdmissionId + "]";
                    $("#newAdmissionModal").modal("show");
                    break;
                case "action_view_admission":
                    vm.isEdit = false;
                    vm.applicationModel = admissionData;
                    $("#newAdmissionModal").modal("show");
                    break;
                case "action_update_status":
                    vm.applicationModel = admissionData;
                    $("#statusUpdateModal").modal("show");
                    vm.applicationModel.CreatedAt = moment(vm.applicationModel.CreatedAt).format('ddd, MMM D YYYY, h:mm:ss a')
                    break;

                default:
                    NotificationService.warning("No action related");
                    break;
            }

            $scope.$apply();

        }

        function getAdmissions() {
            vm.loader = true;
            var payload = vm.model;
            services.searchAdmissions(payload, function (response) {
                vm.loader = false;
                if (response.Status === "00") {
                    vm.searchResult = response.data;


                    var table = $('#applicationsTable');

                    var options = {
                        data: vm.searchResult,
                        columns: [
                            {
                                title: "Actions", width: "25%", data: function (data) {
                                    return '<div class="btn- group ">' +
                                        //'<button type= "button" class="btn btn-danger" ><i class="fa fa-trash-o"></i></button>' +
                                        '<button type="button" class="btn  btn-warning action_edit_admission"><i class="fa fa-pencil"></i></button>' +
                                        '<button type="button" class="btn btn-default action_view_admission"><i class="fa fa-eye"></i></button>' +
                                        '<button type="button" class="btn btn-success action_update_status"><i class="fa fa-certificate"></i> Update Status</button>' +
                                        '</div>';
                                }
                            },
                            {
                                title: "Applicant Name", width: "20%", data: "FullName"
                            },
                            {
                                title: "Country Of Origin", width: "15%", data: "CountryOfOrigin"
                            },
                            {
                                title: "Application Date", width: "10%", data: function (data) {
                                    return moment(data.CreatedAt).format('ddd, MMM D YYYY, h:mm:ss a');
                                }

                            },
                            { title: "Course Name", width: "10%", data: "CourseName" },
                            {
                                title: "Admission Status", width: "10%", data: "AdmissionStatus"
                            }
                        ],
                        dom: "<'exportOptions'T>t<'row'<p i>>",
                        order: [[5, "desc"]],
                        destroy: true,
                        "bScrollInfinite": true,
                        "bScrollCollapse": true,
                        oLanguage: {
                            "sLengthMenu": "_MENU_ ",
                            "sInfo": "Showing <b>_START_ to _END_</b> of _TOTAL_ entries"
                        },
                        iDisplayLength: 50
                    };

                    table.DataTable(options);

                    table = $('#applicationsTable').DataTable();

                    $('#applicationsTable tbody').on('click', 'tr .action_edit_admission', function () {

                        vm.selectedAdmission = table.row($($($(this).parent()).parent()).parent()).data();
                        vm.openAction("action_edit_admission", vm.selectedAdmission);
                    });

                    $('#applicationsTable tbody').on('click', 'tr .action_view_admission', function () {

                        vm.selectedAdmission = table.row($($($(this).parent()).parent()).parent()).data();
                        vm.openAction("action_view_admission", vm.selectedAdmission);
                    });

                    $('#applicationsTable tbody').on('click', 'tr .action_update_status', function () {

                        vm.selectedAdmission = table.row($($($(this).parent()).parent()).parent()).data();
                        vm.openAction("action_update_status", vm.selectedAdmission);
                    });


                }
            });
        }


        vm.findStudent = function () {
            var payload = vm.model;
            services.findStudent(payload, function (response) {

                if (response.Status === "00") {
                    vm.searchResult = response.data;
                    initApplicationsTable();
                }
            });
        }

        vm.newAdmission = function () {
            vm.isEdit = true;
            vm.viewTitle = "New Admission";
            vm.applicationModel = {};
            $("#newAdmissionModal").modal("show");
        }

        vm.saveAdmission = function () {
            vm.loader = true;
            services.saveAdmission(vm.applicationModel,
                function (response) {

                    vm.loader = false;

                    if (response.Status == "00") {
                        NotificationService.success("New application added successfully");
                        $("#newAdmissionModal").modal("hide");
                        getAdmissions();
                    }
                    else {
                        NotificationService.alert("An error occured.Please try again.");
                    }

                });
        }

        vm.setAdmissionStatus = function (admission, status) {
            vm.loader = true;
            console.log('The admission >>', admission);
            var payload = {};
            payload.Status = status;
            payload.Id = admission.AdmissionId;
            services.setAdmissionStatus(payload, function (response) {
                vm.loader = false;
                if (response.Status === "00") {
                    $("#statusUpdateModal").modal("hide");
                    utils.alertSuccess(response.Message);
                    getAdmissions();
                }
            });
        }

        vm.advancedSearch = function () {
            $("#AdvancedSearchModal").modal("show");
        }

       
    }
})(jQuery);
