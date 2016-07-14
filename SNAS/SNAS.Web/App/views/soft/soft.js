(function () {
    var controllerId = 'app.views.soft';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal','$state', 'abp.services.app.soft',
        function ($scope, $modal, $state,softService) {
            var vm = this;

            vm.items = [];

            //查询条件
            $scope.searchItems = [{ 'field': 'name', 'title': '软件名称', 'type': 'text', 'operator': 'like' },
                                  { 'field': 'bindMode', 'title': '绑定模式', 'type': 'list', 'operator': 'equal', 'listItems': [{ 'id': '', 'text': '所有模式' }, { 'id': '0', 'text': '不绑定机器' }, { 'id': '1', 'text': '绑定机器' }] },
                                  { 'field': 'runMode', 'title': '运行模式', 'type': 'list', 'operator': 'equal', 'listItems': [{ 'id': '', 'text': '所有模式' }, { 'id': '0', 'text': '免费' }, { 'id': '1', 'text': '收费' }] },
                                  { 'field': 'creationTime', 'title': '创建时间', 'type': 'date', 'operator': 'range' }];
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
                    templateUrl: '/App/views/soft/createModal.cshtml',
                    controller: 'app.views.soft.create as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    vm.getItems();
                });
            };

            vm.edit = function (id) {
                
                var modalInstance = $modal.open({
                    templateUrl: '/App/views/soft/editModal.cshtml',
                    controller: 'app.views.soft.edit as vm',
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

            vm.showBindOption = function (id) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/soft/softBindOptionModal.cshtml',
                    controller: 'app.views.soft.softBindOption as vm',
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