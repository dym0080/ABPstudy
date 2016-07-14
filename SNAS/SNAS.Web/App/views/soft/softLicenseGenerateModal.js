(function () {
    angular.module('app').controller('app.views.soft.softLicenseGenerate', [
        '$scope', '$modalInstance','abp.services.app.soft','softId',
        function ($scope, $modalInstance, softService, softId) {
            var vm = this;
            vm.licenseOptions = [];

            vm.getLicenseOptions = function () {
                
                softService.getLicenseOptionComboList(softId).success(function (result) {
                    abp.hideLoading();
                    vm.licenseOptions = result.items;
                });
            }

            vm.data = {
                softLicenseOptionId: '',
                count: '',
                softId: softId
            };

            vm.save = function () {
                vm.saving = !0;
                softService.generateLicenses(vm.data)
                    .success(function () {
                        abp.notify.info("提交成功");
                        $modalInstance.close();
                    }).finally(function () {
                            vm.saving = !1;
                        }
                    );
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            vm.getLicenseOptions();
        }
    ]);
})();