
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

        function getRoomInfoByNo(roomNo) {
            var payload = {};
            payload.roomNo = roomNo;
            services.getRoomInfoByNo(payload, function (response) {
                if (response.Status === "00") {
                    vm.model.roomNo = response.Message;
                }
            });
        }


        vm.allocateRoom = function () {
            services.saveAdmission(vm.applicationModel,
                function (response) {
                    console.log(response);
                });
        }

         

    }
})(jQuery);
