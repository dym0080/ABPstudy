(function () {
    angular.module('app').controller('app.views.soft.create', [
        '$scope', '$modalInstance','abp.services.app.soft',
        function ($scope, $modalInstance, softService) {
            var vm = this;

            vm.data = {
                name: '',
                bindmode: '',
                runmode: '',
                remark: ''
            };

            vm.save = function () {
                vm.saving = !0
                softService.create(vm.data)
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