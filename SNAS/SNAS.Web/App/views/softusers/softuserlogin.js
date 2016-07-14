(function () {
    var controllerId = 'app.views.softuserlogin';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', '$state', 'abp.services.app.softUser',
        function ($scope, $modal, $state, service) {

            var vm = this;

            vm.items = [];

            //查询条件
            $scope.searchItems = [{ 'field': 'loginname', 'title': '登录名称', 'type': 'text', 'operator': 'like', 'ignore': true },
                                  { 'field': 'softName', 'title': '软件名称', 'type': 'text', 'operator': 'like', 'ignore': true },
                                  { 'field': 'mcode', 'title': '机器码', 'type': 'text', 'operator': 'equal' },
                                  { 'field': 'ip', 'title': 'IP', 'type': 'text', 'operator': 'equal' },
                                  { 'field': 'creationTime', 'title': '登录时间', 'type': 'date', 'operator': 'range' }];
            $scope.searchConds = [];

            //翻页
            $scope.pageInfo = { pageSize: 10, pageIndex: 1 };

            vm.getItems = function () {
                abp.showLoading();
                service.getLoginPageList({ pagesize: $scope.pageInfo.pageSize, currentpage: $scope.pageInfo.pageIndex, filter: $scope.searchConds, sort: 'creationtime,1' }).success(function (result) {
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
                console.log("paging.");
                vm.getItems();
            };


            vm.getItems();

        }
    ]);
})();