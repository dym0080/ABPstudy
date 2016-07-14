(function () {
    angular.module('app').controller('app.views.soft.editsoftLicenseOption', [
        '$scope', '$modalInstance', 'abp.services.app.soft', 'dataId',
        function ($scope, $modalInstance, softService, dataId) {
            var vm = this;

            vm.loadData = function () {
                softService.getLicenseOption(dataId).success(function (result) {
                    vm.data = result;
                    vm.data.licenseType = vm.data.licenseType.toString();
                });
            };

            vm.data = {
                licenseType: '',
                price: ''
            };

            vm.save = function () {
                vm.saving = !0
                softService.editLicenseOption(vm.data)
                    .success(function () {
                        abp.notify.info("保存成功");
                        $modalInstance.close();
                    }).finally(function () {
                        vm.saving = !1
                    }
                    );
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            vm.loadData();

        }
    ]);
})();