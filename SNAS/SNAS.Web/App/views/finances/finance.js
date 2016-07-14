(function() {
    var controllerId = 'app.views.finance';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', '$state', 'abp.services.app.finance',
        function ($scope, $modal, $state, service) {
            
            var vm = this;

            vm.items = [];

            //查询条件
            $scope.searchItems = [{ 'field': 'type', 'title': '类型', 'type': 'list', 'operator': 'equal', 'listItems': [{ 'id': '', 'text': '所有类型' }, { 'id': '0', 'text': '收入' }, { 'id': '1', 'text': '支出' }] },
                                  { 'field': 'creationTime', 'title': '发生时间', 'type': 'date', 'operator': 'range' }];
            $scope.searchConds = [];

            //翻页
            $scope.pageInfo = { pageSize: 10, pageIndex: 1 };

            vm.getItems = function () {
                abp.showLoading();
                service.getPageList({ pagesize: $scope.pageInfo.pageSize, currentpage: $scope.pageInfo.pageIndex, filter: $scope.searchConds, sort: 'creationtime,1' }).success(function (result) {
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