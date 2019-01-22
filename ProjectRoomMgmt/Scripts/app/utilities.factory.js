(function () {
    angular
        .module('kaiptcapp')
        .factory('brudexutils', UtilityFunctions);
    UtilityFunctions.$inject = ['$http', '$location', '$window'];
    function UtilityFunctions($http, $location,$window) {
         return {
            alertSuccess: createAlert('success'),
            alertError: createAlert('error'),
             alertConfirm: createAlertCustomized('info'),
            toastSuccess:createToast('success'),
            toastInfo:createToast('info'),
            toastError:createToast('error'),
            toastWarning:createToast('warning'),
            storeLocally :storeLocally,
            getSavedData: getSavedData,
            getIsoDateString: getIsoDateString,
            getStore:getStore
        }; 

        function createAlert(alertType) {
            return function showAlert(title, message) {
                var args = [].slice.call(arguments);
                if (args.length === 1) {
                    message = args[0];
                    title = alertType;
                }
                $window.swal({
                    title: title,
                    text: message,
                    type: alertType,
                    showCancelButton: false,
                    confirmButtonClass: "bttn-material-flat bttn-md bttn-lg bttn-sm bttn-default bttn-no-outline",
                });
            };
        } 

        function createToast(alertType) {
            return function(title, message) {
                iziToast[alertType]({
                    title: title,
                    message: message
                });
            };
        }

        function createAlertCustomized(alertType) {
            return function showAlert(title, message, callback) {
                var args = [].slice.call(arguments);
                if (args.length === 1) {
                    message = args[0];
                    title = alertType;
                } 
                $window.swal({
                        title: title,
                        text: message,
                        type: alertType,
                        showIcon :false,
                        showCancelButton: true,
                        confirmButtonClass: "bttn-material-flat bttn-md bttn-lg bttn-sm bttn-primary bttn-no-outline",
                        cancelButtonClass: "bttn-material-flat bttn-md bttn-lg bttn-sm bttn-default bttn-no-outline",
                        confirmButtonText: "Yes",
                        cancelButtonText: "No",
                        closeOnConfirm: true
                    },
                    function() {
                        if (callback) {
                            callback();
                        }
                    }); 
            };
        } 

        function storeLocally(key,value) {
            $window.store.set(key, value); 
        }

        function getSavedData(key) {
            return $window.store.get(key);
        }
        function getStore() {
            return $window.store;
        }
 

        function getIsoDateString() {
            function pad(number) {
                if (number < 10) {
                    return '0' + number;
                }
                return number;
            }
            var date = new Date();
            return date.getUTCFullYear() +
                '' +
                pad(date.getUTCMonth() + 1) +
                '' +
                pad(date.getUTCDate());
        }
    }
})();