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
        vm.model = { searchFormValid:false };

        vm.init = function ( ) {
            
        };

        vm.findStudent = function() {
            var payload = vm.model;
            services.findStudent(payload, function (response) {
                if (response.status === "00") {
                    vm.searchResult = response.data;
                }

            });

        }

        
    }
})(jQuery);
