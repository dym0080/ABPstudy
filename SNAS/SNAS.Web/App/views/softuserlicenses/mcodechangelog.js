(function () {
    var controllerId = 'app.views.mcodechangelog';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', '$state', 'abp.services.app.softUser', '$stateParams',
        function ($scope, $modal, $state, server, $stateParams) {
            var vm = this;
            vm.softUserLicenseId = $stateParams.softUserLicenseId;
            vm.softName = "";
            vm.items = [];

            //查询条件
            $scope.searchItems = [{ 'field': 'oldMCode', 'title': '旧机器码', 'type': 'text', 'operator': 'equal' },
                                  { 'field': 'newMCode', 'title': '新机器码', 'type': 'text', 'operator': 'equal' },
                                  { 'field': 'creationTime', 'title': '创建时间', 'type': 'date', 'operator': 'range' }];
            $scope.searchConds = [];

            //翻页
            $scope.pageInfo = { pageSize: 10, pageIndex: 1 };

            vm.getItems = function () {
                abp.showLoading();
                server.getMCodeChangeLogPageList(vm.softUserLicenseId, { pagesize: $scope.pageInfo.pageSize, currentpage: $scope.pageInfo.pageIndex, filter: $scope.searchConds, sort: 'creationtime,1' }).success(function (result) {
                    abp.hideLoading();
                    vm.items = result.items;
                    $scope.pageInfo = { pageSize: result.pageSize, pageIndex: result.pageIndex, totalCount: result.totalCount, pageCount: result.pageCount, itemCount: result.items.length, begin: result.begin, end: result.end };
                });
            }

        

            $scope.queryData = function () {
                $scope.pageInfo = { pageSize: 10, pageIndex: 1 };
                vm.getItems();
            };

            $scope.paging = function () {
                vm.getItems();
            };

            vm.getItems();
   

        }
    ]);
})();