(function() {
    var controllerId = 'app.views.home';
    angular.module('app').controller(controllerId, [
        '$scope', '$rootScope', 'abp.services.app.report', function ($scope, $rootScope, service) {
            var vm = this;

            $scope.legend1 = ["收入金额"];
            $scope.item1 = [];
            $scope.data1 = [[]];

            $scope.legend2 = ["授权数量"];
            $scope.item2 = [];
            $scope.data2 = [[]];

            vm.statistics = {
                soft: 0,
                softUser: 0,
                totalInCome: 0.00,
                todayInCome: 0.00
            };
            vm.getSystemStatistics = function() {
                service.getSystemStatistics().success(function (result) {
                    vm.statistics = result;
                    
                });
            };
            vm.get7DayIncome = function () {
                service.get7DayIncome().success(function (result) {
                    $scope.item1 = [];
                    $scope.data1 = [[]];

                    for (var i = 0; i < result.length; i++) {
                        $scope.item1.push(result[i].date);
                        $scope.data1[0].push(result[i].count);
                    }

                    setTimeout(function () {
                        $scope.$apply();
                    }, 100);
                    
                });
            };

            vm.get7DayLicense = function () {
                service.get7DayLicense().success(function (result) {
                    $scope.item2 = [];
                    $scope.data2 = [[]];

                    for (var i = 0; i < result.length; i++) {
                        $scope.item2.push(result[i].date);
                        $scope.data2[0].push(result[i].count);
                    }
                });
            };

            vm.getSystemStatistics();
            vm.get7DayIncome();
            vm.get7DayLicense();
        }
    ]);
})();