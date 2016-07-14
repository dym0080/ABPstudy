(function () {
    angular.module('app').controller('app.views.softuser.useLicense', [
        '$scope', '$modalInstance', 'abp.services.app.softUser', 'dataId',
        function ($scope, $modalInstance, service, dataId) {
            var vm = this;

            vm.data = {
                softUserId: dataId,
                licenseNo: ''
            };

            vm.save = function () {
                vm.saving = !0
                service.useLicense(vm.data)
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
        }
    ]);
})();