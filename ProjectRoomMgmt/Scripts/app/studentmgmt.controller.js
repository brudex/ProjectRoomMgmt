(function ($) {
    'use strict';
    angular
        .module('kaiptcapp')
        .controller('StudentMgmtController', StudentMgmtController);
    StudentMgmtController.$inject = ['$scope','brudexservices', '$location', '$window', 'brudexutils'];

    function StudentMgmtController($scope,services, location, $window, utils) {
        var vm = this;
        vm.errorMsg = [];
        vm.successMsg = [];
        vm.searchResult = [];
        vm.model = {};

        vm.init = function () {
            vm.model.searchMode = 'byword';
            listRecentStudents();
            getAvailableRooms();
        };


        vm.openAction = function (action, studentData) {

            switch (action) {
                case "action_allocate_room":
                    vm.allocationModel = {};
                    vm.allocationModel.courseName = studentData.CourseName;
                    vm.allocationModel.studentName = studentData.FullName;
                    vm.allocationModel.studentNo = studentData.StudentNo;

                    $("#roomAllocationModal").modal("show");
                    break;
                case "action_view_admission":
                    vm.isEdit = false;
                    vm.isView = true;
                    vm.applicationModel = admissionData;
                    $("#newAdmissionModal").modal("show");
                    break;
                case "action_update_status":
                    vm.applicationModel = admissionData;
                    $("#statusUpdateModal").modal("show");
                    vm.applicationModel.CreatedAt =
                        moment(vm.applicationModel.CreatedAt).format('ddd, MMM D YYYY, h:mm:ss a')
                    break;

                default:
                    NotificationService.warning("No action related");
                    break;
            }

            $scope.$apply();
        }

        vm.findStudent = function () {

           var  table = $('#studentTable').DataTable();
            table.clear().draw();

            var payload = vm.model;
            payload.limit = 100;
            services.findStudent(payload,
                function (response) {
                    if (response.Status === "00") {
                        vm.searchResult = response.data;
                        displayStudentsTable(vm.searchResult);
                    }
                });
        }

        vm.allocateRoom = function () {
            vm.loader = true;
            var payload = vm.allocationModel;

            services.bookRoom(payload,
                function (response) {
                    vm.loader = false;
                    if (response.Status === "00") {
                        utils.alertSuccess("Room successfully allocated");
                    }
                });
        }

        vm.newAdmission = function () {
            $("#newAdmissionModal").modal("show");
        }

        vm.advancedSearch = function () {
            $("#AdvancedSearchModal").modal("show");
        }

        function getAvailableRooms() {
            services.getAvailableRooms(function (response) {

                if (response.Status === "00") {
                    var rooms = response.data;
                    vm.rooms = [];
                    angular.forEach(rooms,
                        function (room) {
                            room.blockCode = room.BlockLocation.replace(/ +/g, "");
                            room.roomTypeCode = room.RoomType.replace(/ +/g, "");
                            vm.rooms.push(room);
                        });

                }
            });
        }

        function listRecentStudents() {
            var payload = { searchMode: "list", limit: 100 };
            services.findStudent(payload, function (response) {

                if (response.Status === "00") {
                    vm.searchResult = response.data;

                    displayStudentsTable(vm.searchResult);

                }
            });

        }

        function displayStudentsTable(data) {

            var table = $('#studentTable');

            var options = {
                data: data,
                columns: [
                    {
                        title: "Actions", width: "30%", data: function (data) {
                            return '<div class="btn-group">' +
                                //'<button type= "button" class="btn btn-danger" ><i class="fa fa-trash-o"></i></button>' +
                                '<button type="button" class="btn m-r-5 btn-xs  btn-primary action_allocate_room">Allocate Room</button>' +
                                '<button type="button" class="btn m-r-5 btn-xs btn-primary action_assign_transport">Assign Transport</button>' +
                                '<button type="button" class="btn btn-xs btn-primary action_deactivate_student">Deactivate Student</button>' +
                                '</div>';
                        }
                    },
                    {
                        title: "Full Name", width: "20%", data: "FullName"
                    },
                    {
                        title: "Student No", width: "15%", data: "StudentNo"
                    },
                    {
                        title: "Country Of Origin", width: "10%", data: "CountryOfOrigin"

                    },
                    { title: "Course Name", width: "10%", data: "CourseName" },
                    {
                        title: "Occupation", width: "10%", data: "Occupation"
                    },
                    {
                        title: "PrimaryContactMobile", width: "10%", data: "PrimaryContactMobile"
                    }
                ],
                dom: "",
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

            table = $('#studentTable').DataTable();
       

            $('#studentTable tbody').on('click', 'tr .action_allocate_room', function () {

                vm.selectedStudent = table.row($($($(this).parent()).parent()).parent()).data();
                vm.openAction("action_allocate_room", vm.selectedStudent);
            });

            $('#studentTable tbody').on('click', 'tr .action_assign_transport', function () {

                vm.selectedStudent = table.row($($($(this).parent()).parent()).parent()).data();
                vm.openAction("action_assign_transport", vm.selectedStudent);
            });

            $('#studentTable tbody').on('click', 'tr .action_deactivate_student', function () {

                vm.selectedStudent = table.row($($($(this).parent()).parent()).parent()).data();
                vm.openAction("action_deactivate_student", vm.selectedStudent);
            });
        }


    }
})(jQuery);
