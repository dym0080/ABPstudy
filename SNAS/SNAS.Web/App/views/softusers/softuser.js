(function() {
    var controllerId = 'app.views.softuser';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', '$state', 'abp.services.app.softUser',
        function ($scope, $modal, $state, service) {
            
            var vm = this;

            vm.items = [];

            //查询条件
            $scope.searchItems = [{ 'field': 'loginname', 'title': '登录名称', 'type': 'text', 'operator': 'like' },
                                  { 'field': 'mobile', 'title': '手机号码', 'type': 'text', 'operator': 'like' },
                                  { 'field': 'qq', 'title': 'QQ', 'type': 'text', 'operator': 'like' },
                                  { 'field': 'source', 'title': '来源', 'type': 'list', 'operator': 'equal', 'listItems': [{ 'id': '', 'text': '所有来源' }, { 'id': '0', 'text': '用户注册' }, { 'id': '1', 'text': '后台添加' }] },
                                  { 'field': 'isActive', 'title': '状态', 'type': 'list', 'operator': 'equal', 'listItems': [{ 'id': '', 'text': '所有状态' }, { 'id': '0', 'text': '停用' }, { 'id': '1', 'text': '启用' }] },
                                  { 'field': 'creationTime', 'title': '创建时间', 'type': 'date', 'operator': 'range' }];
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

            vm.createSoftUser = function () {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/softusers/createModal.cshtml',
                    controller: 'app.views.softuser.create as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    vm.getItems();
                });
            };

            vm.edit = function (id) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/softusers/editModal.cshtml',
                    controller: 'app.views.softuser.edit as vm',
                    backdrop: 'static',
                    resolve: {
                        dataId: function () { return id; }
                    }
                });

                modalInstance.result.then(function () {
                    vm.getItems();
                });
            };

            vm.chgPassword = function (id) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/softusers/chgPassword.cshtml',
                    controller: 'app.views.softusers.chgPassword as vm',
                    backdrop: 'static',
                    resolve: {
                        dataId: function () { return id; }
                    }
                });
            };


            vm.useLicense = function (id) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/softusers/useLicenseModal.cshtml',
                    controller: 'app.views.softuser.useLicense as vm',
                    backdrop: 'static',
                    resolve: {
                        dataId: function () { return id; }
                    }
                });

                modalInstance.result.then(function () {
                    vm.getItems();
                });
            };

            vm.delete = function (id) {
                abp.message.confirm("确定删除?", function (isConfirmed) {
                    if (isConfirmed) {
                        service.delete(id).success(function (result) {
                            abp.notify.info("删除数据成功");
                            vm.getItems();
                        });
                    }
                });
            };

            vm.moreChange = function (obj, id) {

            };

            $scope.queryData = function () {
                $scope.pageInfo = { pageSize: 10, pageIndex: 1 };
                vm.getItems();
            };

            $scope.paging = function () {
                console.log("paging.");
                vm.getItems();
            };

            vm.gotoSoftLicenseOption = function (id) {
                $state.go('softlicenseoption', { softId: id });
            };

            vm.gotoSoftLicense = function (id) {
                $state.go('softlicense', { softId: id });
            };

            vm.showMoreOpenOption = function (id) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/soft/softMoreOpenOptionModal.cshtml',
                    controller: 'app.views.soft.softMoreOpenOption as vm',
                    backdrop: 'static',
                    resolve: {
                        softId: function () { return id; }
                    }
                });
            };

            vm.showRegisterOption = function (id) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/soft/softRegisterOptionModal.cshtml',
                    controller: 'app.views.soft.softRegisterOption as vm',
                    backdrop: 'static',
                    resolve: {
                        softId: function () { return id; }
                    }
                });
            };

            vm.getItems();

        }
    ]);
})();