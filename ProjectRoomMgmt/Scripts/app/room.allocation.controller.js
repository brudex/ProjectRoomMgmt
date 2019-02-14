
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

        function verifyStudentInfoByNo(studentNo) {
            var payload = {};
            payload.studentNo = studentNo;
            services.getStudentInfoByNo(payload, function(response) {
                if (response.Status === "00") {
                    vm.model.studentName = response.Message;
                }
            });
        }

        function getAvailableRooms() {
            services.getAvailableRooms(function (response) {
                console.log('the response from getavailable rooms', response);
                if (response.Status === "00") {
                    vm.rooms = response.data;
                }
            });
        }


        vm.allocateRoom = function () {
            var payload = vm.model;
            services.bookRoom(payload, function(response) {
                if (response.Status === "00") {
                    utils.alertSuccess("Room successfully allocated");
                }
            }); 
        }

         

    }
})(jQuery);
