(function () {
    angular.module('app').controller('app.views.soft.createsoftLicenseOption', [
        '$scope', '$modalInstance','abp.services.app.soft','softId',
        function ($scope, $modalInstance, softService, softId) {
            var vm = this;

            vm.data = {
                licenseType: '',
                price: '',
                softId: softId
            };

            vm.save = function () {
                vm.saving = !0;
                softService.createLicenseOption(vm.data)
                    .success(function () {
                        abp.notify.info("保存成功");
                        $modalInstance.close();
                    }).finally(function () {
                            vm.saving = !1;
                        }
                    );
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };
        }
    ]);
})();