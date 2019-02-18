(function ($) {
    angular
        .module('kaiptcapp')
        .factory('brudexservices', DataService);
    DataService.$inject = ['$http', '$location', '$window'];
    function DataService($http, $location, $window) {
        var baseUrl = "";
        return {
            searchAdmissions: postData('/api/AdmissionApi/Search'),
            setAdmissionStatus: postData("/api/AdmissionApi/SetAdmissionStatus"),
            saveAdmission: postData("/api/AdmissionApi/Capture"),
            findStudent: postData("/api/StudentApi/SearchStudents"),
            getStudentInfoByNo: postData("/api/StudentApi/StudentInfo"),
            bookRoom: postData("/api/RoomApi/RoomBooking"),
            getAvailableRooms: getData("/api/RoomApi/GetAvailableRooms"),

        };

        function postData(endpoint) {
            return function (data, callback) {
                if (!callback) {
                    callback = data;
                    data = {};
                }
                var url = baseUrl + endpoint;
                doPost(url, data, function (err, response) {
                    if (err) {
                        console.error(err);
                        return;
                    }
                    callback(response.data);
                });
            };
        }

        function getData(url) {

            return function () {
                var callback = arguments[0];
                var finalUrl = url;
                if (arguments.length > 1) {
                    if (typeof arguments[0] === 'object') {
                        console.log('the first arguments >>', arguments[0]);
                        finalUrl = url + "?" + $.param(arguments[0]);
                    } else {
                        finalUrl = url + "/" + arguments[0];
                    }
                    callback = arguments[1];
                }
                console.log(finalUrl);
                doGet(url, function (err, response) {
                    if (err) {
                        console.error(err);
                        return;
                    }
                    callback(response.data);
                });
            }
        }

        function doPost(url, data, callback) {
            return $http.post(url, data)
                .then(function (response) {
                    if (response === null) {
                        return callback(null, { status: "07", message: "Error in response" });
                    }
                    return callback(null, response);
                })
                .catch(function (error) {
                    console.log(error);
                    callback(error);
                });
        }

        function doGet(endpoint, callback) {
            var url = baseUrl + endpoint;
            return $http.get(url)
                .then(function (response) {
                    if (response === null) {
                        return callback(null, { status: "07", message: "Error in response" });
                    }
                    return callback(null, response);
                })
                .catch(function (error) {
                    console.log(error);
                    return callback(error);
                });
        }


    }
})(jQuery);