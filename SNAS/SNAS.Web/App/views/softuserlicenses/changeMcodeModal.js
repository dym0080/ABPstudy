(function () {
    angular.module('app').controller('app.views.soft.changeMcode', [
        '$scope', '$modalInstance', 'abp.services.app.softUser', 'softUserLicenseMcodeId',
        function ($scope, $modalInstance, softService, softUserLicenseMcodeId) {
            var vm = this;
            vm.licenseOptions = [];

            vm.getLicenseOptions = function () {
                
                softService.getLicenseOptionComboList(softId).success(function (result) {
                    abp.hideLoading();
                    vm.licenseOptions = result.items;
                });
            }

            vm.data = {
                softUserLicenseMcodeId: softUserLicenseMcodeId,
                newMCode: ''
            };

        


            vm.save = function () {
                vm.saving = !0;
                softService.changeMcode(vm.data)
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

            
        }
    ]);
})();