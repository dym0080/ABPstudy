(function () {
    angular.module('app').controller('app.views.soft.edit', [
        '$scope', '$modalInstance', 'abp.services.app.soft', 'dataId',
        function ($scope, $modalInstance, softService, dataId) {
            var vm = this;

            vm.loadData = function () {
                softService.get(dataId).success(function (result) {
                    vm.data = result;
                    vm.data.bindMode = vm.data.bindMode.toString();
                    vm.data.runMode = vm.data.runMode.toString();
                });
            };

            vm.data = {
                name: '',
                bindMode: '',
                runMode: '',
                remark: ''
            };

            vm.save = function () {
                vm.saving = !0
                softService.edit(vm.data)
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