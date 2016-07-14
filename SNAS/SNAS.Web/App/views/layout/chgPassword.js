(function () {
    angular.module('app').controller('app.views.chgPassword', [
        '$scope', '$modalInstance', 'abp.services.app.session',
        function ($scope, $modalInstance, service) {
            var vm = this;

            vm.data = {
                oldPassword: '',
                newPassword: '',
                newPassword2: ''
            };

            vm.save = function () {
                service.changePassword(vm.data)
                    .success(function () {
                        abp.notify.info("保存成功");
                        $modalInstance.close();
                    }).finally(function () {
                        
                        }
                    );
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };
        }
    ]);
})();