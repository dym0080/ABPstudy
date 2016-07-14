(function() {
    var controllerId = 'app.views.softuserlicense';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', '$state', 'abp.services.app.softUser',
        function ($scope, $modal, $state, service) {
            
            var vm = this;

            vm.items = [];

            //查询条件
            $scope.searchItems = [{ 'field': 'loginname', 'title': '登录名称', 'type': 'text', 'operator': 'like', 'ignore': true },
                                  { 'field': 'softName', 'title': '软件名称', 'type': 'text', 'operator': 'like', 'ignore': true },
                                  { 'field': 'type', 'title': '类型', 'type': 'list', 'operator': 'equal', 'listItems': [{ 'id': '', 'text': '所有类型' }, { 'id': '0', 'text': '试用' }, { 'id': '1', 'text': '免费' }, { 'id': '2', 'text': '收费' }] },
                                  { 'field': 'isActive', 'title': '状态', 'type': 'list', 'operator': 'equal', 'listItems': [{ 'id': '', 'text': '所有状态' }, { 'id': '0', 'text': '停用' }, { 'id': '1', 'text': '启用' }] },
                                  { 'field': 'authorizeTime', 'title': '授权时间', 'type': 'date', 'operator': 'range' },
                                  { 'field': 'expireTime', 'title': '到期时间', 'type': 'date', 'operator': 'range' }];
            $scope.searchConds = [];

            //翻页
            $scope.pageInfo = { pageSize: 10, pageIndex: 1 };

            vm.getItems = function () {
                abp.showLoading();
                service.getLicensePageList({ pagesize: $scope.pageInfo.pageSize, currentpage: $scope.pageInfo.pageIndex, filter: $scope.searchConds, sort: 'creationtime,1' }).success(function (result) {
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

            vm.enableLicense = function (id) {
                abp.message.confirm("确定启用该授权?", function (isConfirmed) {
                    if (isConfirmed) {
                        service.enableLicense(id).success(function (result) {
                            abp.notify.info("操作成功");
                            vm.getItems();
                        });
                    }
                });
            };
        
            vm.disableLicense = function (id) {
                abp.message.confirm("确定停用该授权?", function (isConfirmed) {
                    if (isConfirmed) {
                        service.disableLicense(id).success(function (result) {
                            abp.notify.info("操作成功");
                            vm.getItems();
                        });
                    }
                });
            };

       

            vm.gotoLicenseMcode = function (id) {
                $state.go('softuserlicensemcode', { softUserLicenseId: id });
            };

            vm.gotoMcodeChangeLog = function (id) {
                $state.go('mcodechangelog', { softUserLicenseId: id });
            };

            vm.getItems();

        }
    ]);
})();