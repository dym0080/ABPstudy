(function () {
    var controllerId = 'app.views.softlicenseoption';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', '$state', 'abp.services.app.soft', '$stateParams',
        function ($scope, $modal, $state, softService, $stateParams) {
            var vm = this;
            vm.softId = $stateParams.softId;
            console.log("softId:" + vm.softId);

            vm.items = [];
            
            vm.getItems = function () {
                abp.showLoading();
                softService.getLicenseOptionPageList(vm.softId).success(function (result) {
                    abp.hideLoading();
                    vm.items = result.items;
                });
            }

            vm.createSoftLicenseOption = function () {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/soft/softLicenseOptionCreateModal.cshtml',
                    controller: 'app.views.soft.createsoftLicenseOption as vm',
                    backdrop: 'static',
                    resolve: {
                        softId: function () { return vm.softId; }
                    }
                });

                modalInstance.result.then(function () {
                    vm.getItems();
                });
            };

            vm.edit = function (id) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/soft/softLicenseOptionEditModal.cshtml',
                    controller: 'app.views.soft.editsoftLicenseOption as vm',
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
                        softService.deleteLicenseOption(id).success(function (result) {
                            abp.notify.info("删除数据成功");
                            vm.getItems();
                        });
                    }
                });
            };

            vm.getItems();

        }
    ]);
})();