(function () {
    var controllerId = 'app.views.softuserlicensemcode';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', '$state', 'abp.services.app.softUser', '$stateParams',
        function ($scope, $modal, $state, server, $stateParams) {
            var vm = this;
            vm.softUserLicenseId = $stateParams.softUserLicenseId;
            vm.softName = "";
            vm.items = [];

            //查询条件
            $scope.searchItems = [{ 'field': 'mcode', 'title': '机器码', 'type': 'text', 'operator': 'equal' }];
            $scope.searchConds = [];

            //翻页
            $scope.pageInfo = { pageSize: 10, pageIndex: 1 };

            vm.getItems = function () {
                abp.showLoading();
                server.getLicenseMcodePageList(vm.softUserLicenseId, { pagesize: $scope.pageInfo.pageSize, currentpage: $scope.pageInfo.pageIndex, filter: $scope.searchConds, sort: 'creationtime,1' }).success(function (result) {
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
            

            vm.enableLicense = function (id) {
                abp.message.confirm("确定启用该机器码?", function (isConfirmed) {
                    if (isConfirmed) {
                        server.enableLicenseMcode(id).success(function (result) {
                            abp.notify.info("操作成功");
                            vm.getItems();
                        });
                    }
                });
            };

            vm.disableLicense = function (id) {
                abp.message.confirm("确定停用该机器码?", function (isConfirmed) {
                    if (isConfirmed) {
                        server.disableLicenseMcode(id).success(function (result) {
                            abp.notify.info("操作成功");
                            vm.getItems();
                        });
                    }
                });
            };


            vm.delete = function (id) {
                abp.message.confirm("确定删除?", function (isConfirmed) {
                    if (isConfirmed) {
                        server.deleteLicenseMcode(id).success(function (result) {
                            abp.notify.info("删除数据成功");
                            vm.getItems();
                        });
                    }
                });
            };

            vm.changeMcode = function (id) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/softuserlicenses/changeMcodeModal.cshtml',
                    controller: 'app.views.soft.changeMcode as vm',
                    backdrop: 'static',
                    resolve: {
                        softUserLicenseMcodeId: function () { return id; }
                    }
                });

                modalInstance.result.then(function () {
                    vm.getItems();
                });
            };

            vm.getItems();
   

        }
    ]);
})();