(function () {
    angular.module('app').controller('app.views.softuser.create', [
        '$scope', '$modalInstance','abp.services.app.softUser',
        function ($scope, $modalInstance, service) {
            var vm = this;

            vm.data = {
                loginName: '',
                password: '123456',
                mobile: '',
                qq: '',
                remark: ''
            };

            vm.save = function () {
                vm.saving = !0
                service.create(vm.data)
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