(function () {
    angular.module('app').controller('app.views.softusers.chgPassword', [
        '$scope', '$modalInstance', 'abp.services.app.softUser', 'dataId',
        function ($scope, $modalInstance, service, dataId) {
            var vm = this;

            vm.data = {
                softUserId: dataId,
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