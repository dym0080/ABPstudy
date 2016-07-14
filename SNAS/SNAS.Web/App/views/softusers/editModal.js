(function () {
    angular.module('app').controller('app.views.softuser.edit', [
        '$scope', '$modalInstance','abp.services.app.softUser', 'dataId',
        function ($scope, $modalInstance, service, dataId) {
            var vm = this;

            vm.loadData = function () {
                service.get(dataId).success(function (result) {
                    vm.data = result;
                });
            };

            vm.data = {
                loginName: '',
                mobile: '',
                qq: '',
                remark: ''
            };

            vm.save = function () {
                vm.saving = !0
                service.edit(vm.data)
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