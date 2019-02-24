
(function ($) {
    'use strict';
    angular
        .module('kaiptcapp')
        .controller('RoomAllocationController', RoomAllocationController);
    RoomAllocationController.$inject = ['brudexservices', '$location', '$window', 'brudexutils', 'NotificationService'];
    function RoomAllocationController(services, location, $window, utils, NotificationService) {
        var vm = this;
        vm.errorMsg = [];
        vm.successMsg = [];
        vm.rooms = [];
        vm.model = {};


        vm.init = function () {
            vm.model.studentNo = $("#__studentNo__").val();
            vm.model.roomNo = $("#__roomNo__").val();
            if (vm.model.studentNo) {
                verifyStudentInfoByNo(vm.model.studentNo);
            }
            getAvailableRooms();
        };

        vm.verifyStudentId =function() {
            verifyStudentInfoByNo(vm.model.studentNo);
        }

        vm.newRoom=function() {
            $("#newRoomModal").modal('show');
        }

        function verifyStudentInfoByNo(studentNo) {
            var payload = {};
            payload.studentNo = studentNo;
            services.getStudentInfoByNo(payload, function (response) {
                if (response.Status === "00") {
                    vm.model.studentName = response.Message;
                }
            });
        }

        function getAvailableRooms() {
            services.getAllRooms(function (response) {

                if (response.Status === "00") {
                    var rooms = response.data;
                    vm.rooms = [];

                    angular.forEach(rooms,
                        function (room) {
                            room.blockCode = room.BlockLocation.replace(/ +/g, "");
                            room.roomTypeCode = room.RoomType.replace(/ +/g, "");
                            vm.rooms.push(room);
                        });

                    var $container = $('#gridcontainer'),
                        $checkboxes = $('#filters input');

                    $container.isotope({
                        itemSelector: '.gallery-item'
                    });
                    // get Isotope instance
                    var isotope = $container.data('isotope');

                    $checkboxes.change(function () {
                        var filters = [];
                        // get checked checkboxes values
                        $checkboxes.filter(':checked').each(function () {
                            filters.push(this.value);
                        });
                        // ['.red', '.blue'] -> '.red, .blue'
                        filters = filters.join(', ');
                        $container.isotope({ filter: filters });

                    });

                    console.log("data", $checkboxes);
                }
            });
        }

        vm.openAllocationModal = function (room) {
            vm.selectedRoom = room;
            $("#roomAllocationModal").modal("show");
        }

        vm.allocateRoom = function () {
            vm.loader = true;
            var payload = vm.model;
            //payload.roomNo = vm.selectedRoom.roomNo;

            services.bookRoom(payload, function (response) {
                vm.loader = false;
                if (response.Status === "00") {
                    utils.alertSuccess("Room successfully allocated");
                }
            });
        }



    }
})(jQuery);
