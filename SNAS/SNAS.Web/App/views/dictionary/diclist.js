(function () {
    var controllerId = 'app.views.dictionary';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal','$state', 'abp.services.app.dictionary',
        function ($scope, $modal, $state,softService) {
            var vm = this;

            vm.items = [];

            //查询条件
            $scope.searchItems = [{ 'field': 'name', 'title': '字典名称', 'type': 'text', 'operator': 'like' },
                                  { 'field': 'Description', 'title': '字典描述', 'type': 'text', 'operator': 'like' }];
            $scope.searchConds = [];

            //翻页
            $scope.pageInfo = { pageSize: 10, pageIndex: 1 };

            vm.getItems = function () {
                abp.showLoading();
                softService.getPageList({ pagesize: $scope.pageInfo.pageSize, currentpage: $scope.pageInfo.pageIndex, filter: $scope.searchConds, sort: 'creationtime,1' }).success(function (result) {
                    abp.hideLoading();
                    vm.items = result.items;
                    $scope.pageInfo = { pageSize: result.pageSize, pageIndex: result.pageIndex, totalCount: result.totalCount, pageCount: result.pageCount, itemCount: result.items.length, begin: result.begin, end: result.end };
                    
                });
            }

            vm.createSoft = function () {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/dictionary/createModal.cshtml',
                    controller: 'app.views.dictionary.create as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    vm.getItems();
                });
            };

            vm.edit = function (id) {
                
                var modalInstance = $modal.open({
                    templateUrl: '/App/views/dictionary/editModal.cshtml',
                    controller: 'app.views.dictionary.edit as vm',
                    backdrop: 'static',
                    resolve: {
                        dataId: function () { return id; }
                    }
                });

                modalInstance.result.then(function () {
                    vm.getItems();
                });
            };

            vm.delete = function(id) {
                abp.message.confirm("确定删除?", function (isConfirmed) {
                    if (isConfirmed) {
                        softService.delete(id).success(function (result) {
                            abp.notify.info("删除数据成功");
                            vm.getItems();
                        });
                    }
                });
            };

            vm.moreChange = function(obj,id) {

            };

            $scope.queryData = function () {
                $scope.pageInfo = { pageSize: 10, pageIndex: 1 };
                vm.getItems();
            };

            $scope.paging=function()
            {
                console.log("paging.");
                vm.getItems();
            };


            vm.getItems();
            
        }
    ]);
})();