(function () {
    var controllerId = 'app.views.softlicense';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', '$state', 'abp.services.app.soft', '$stateParams',
        function ($scope, $modal, $state, softService, $stateParams) {
            var vm = this;
            vm.softId = $stateParams.softId;
            vm.softName = "";
            vm.items = [];

            //查询条件
            $scope.searchItems = [{ 'field': 'licenseNo', 'title': '卡密', 'type': 'text', 'operator': 'equal' },
                                  { 'field': 'loginName', 'title': '授权用户', 'type': 'text', 'operator': 'like','ignore':true },
                                  { 'field': 'status', 'title': '状态', 'type': 'list', 'operator': 'equal', 'listItems': [{ 'id': '', 'text': '所有状态' }, { 'id': '0', 'text': '正常' }, { 'id': '1', 'text': '已售出' }, { 'id': '2', 'text': '已使用' }, { 'id': '3', 'text': '退货' }] },
                                  { 'field': 'licenseType', 'title': '类型', 'type': 'list', 'operator': 'equal', 'listItems': [{ 'id': '', 'text': '所有模式' }, { 'id': '0', 'text': '月' }, { 'id': '1', 'text': '周' }, { 'id': '2', 'text': '天' }, { 'id': '3', 'text': '年' }, { 'id': '4', 'text': '小时' }, { 'id': '5', 'text': '永久' }] },
                                  { 'field': 'creationTime', 'title': '创建时间', 'type': 'date', 'operator': 'range' }];
            $scope.searchConds = [];

            //翻页
            $scope.pageInfo = { pageSize: 10, pageIndex: 1 };

            vm.getItems = function () {
                abp.showLoading();
                softService.getLicensePageList(vm.softId, { pagesize: $scope.pageInfo.pageSize, currentpage: $scope.pageInfo.pageIndex, filter: $scope.searchConds, sort: 'creationtime,1' }).success(function (result) {
                    abp.hideLoading();
                    vm.items = result.items;
                    $scope.pageInfo = { pageSize: result.pageSize, pageIndex: result.pageIndex, totalCount: result.totalCount, pageCount: result.pageCount, itemCount: result.items.length, begin: result.begin, end: result.end };
                });
            }

            vm.getSoftInfo = function() {
                softService.get(vm.softId).success(function (result) {
                    vm.softName = result.name;
                });
            };

            $scope.queryData = function () {
                $scope.pageInfo = { pageSize: 10, pageIndex: 1 };
                vm.getItems();
            };

            $scope.paging = function () {
                vm.getItems();
            };


            vm.generateSoftLicense = function () {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/soft/softLicenseGenerateModal.cshtml',
                    controller: 'app.views.soft.softLicenseGenerate as vm',
                    backdrop: 'static',
                    resolve: {
                        softId: function () { return vm.softId; }
                    }
                });

                modalInstance.result.then(function () {
                    vm.getItems();
                });
            };

            vm.delete = function (id) {
                abp.message.confirm("确定删除?", function (isConfirmed) {
                    if (isConfirmed) {
                        softService.deleteLicense(id).success(function (result) {
                            abp.notify.info("删除数据成功");
                            vm.getItems();
                        });
                    }
                });
            };

            vm.sellLicense = function (id) {
                abp.message.confirm("确定出售该卡密?", function (isConfirmed) {
                    if (isConfirmed) {
                        softService.sellLicense(id).success(function (result) {
                            abp.notify.info("操作成功");
                            vm.getItems();
                        });
                    }
                });
            };

            vm.returnLicense = function (id) {
                abp.message.confirm("确定退货该卡密?", function (isConfirmed) {
                    if (isConfirmed) {
                        softService.returnLicense(id).success(function (result) {
                            abp.notify.info("操作成功");
                            vm.getItems();
                        });
                    }
                });
            };

            vm.getItems();
            vm.getSoftInfo();

        }
    ]);
})();