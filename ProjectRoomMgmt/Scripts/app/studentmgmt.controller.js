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
        
        vm.model = { searchFormValid:false };

        vm.init = function ( ) {
            
        };

        
    }
})(jQuery);
