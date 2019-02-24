(function () {
    angular
        .module('kaiptcapp', ['ngMessages']);

})();


(function ($) {
    'use strict';
    angular
        .module('kaiptcapp')
        .controller('AppCtrl', AppCtrl);
    AppCtrl.$inject = ['$scope', '$rootScope', 'brudexservices', '$location', '$window', 'brudexutils', 'NotificationService'];

    function AppCtrl($scope, $rootScope, services, location, $window, utils, NotificationService) {

        $rootScope.totalRoomsAvailable = 0;
        $rootScope.totalRoomsAllocated = 0;

        getAdmissionDistribution();
        getStudentsChart();
        getRoomsSummary();

        function getAdmissionDistribution() {

            services.getStudentsChartData(function(response) {
                
                if (response.Status == '00') {
                    var options = {
                        chart: {
                            width: '90%',
                            type: 'pie',
                        },
                        labels: ['Accepted', 'Pending', 'Rejected'],
                        series: [response.data.Accepted, response.data.Pending, response.data.Rejected],//data arranged in same order as labels
                        responsive: [
                            {
                                breakpoint: 480,
                                options: {
                                    chart: {
                                        width: 300
                                    },
                                    legend: {
                                        position: 'bottom'
                                    }
                                }
                            }
                        ]

                    }

                    var chart = new ApexCharts(
                        document.querySelector("#admissionsChart"),
                        options
                    );

                    chart.render();


                }
            });
        }

        function getStudentsChart() {
            services.getStudentsCount(function(response) {
                
                if (response.Status == '00') {
                    $rootScope.totalStudents = response.data;
                }
            });
        }

        function getRoomsSummary() {
            services.getAvailableRoomsCount(function (response) {
                
                if (response.Status == '00') {
                    $rootScope.totalRoomsAvailable = response.data;

                    services.getAllocatedRoomsCount(function(response) {
                        if (response.Status == '00') {
                            $rootScope.totalRoomsAllocated = response.data;


                            var options = {
                                chart: {
                                    width: '90%',
                                    type: 'pie',
                                },
                                labels: ['Rooms Available', 'Rooms Allocated'],
                                series: [$rootScope.totalRoomsAvailable, $rootScope.totalRoomsAllocated],//data arranged in same order as labels
                                responsive: [
                                    {
                                        breakpoint: 480,
                                        options: {
                                            chart: {
                                                width: 300
                                            },
                                            legend: {
                                                position: 'bottom'
                                            }
                                        }
                                    }
                                ]

                            }

                            var chart = new ApexCharts(
                                document.querySelector("#roomsChart"),
                                options
                            );

                            chart.render();

                        }
                    });
                }
            });
        }
    }
})(jQuery);;
