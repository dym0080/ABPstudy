(function () {
    angular.module('app').controller('app.views.soft.softBindOption', [
        '$scope', '$modalInstance','abp.services.app.soft','softId',
        function ($scope, $modalInstance, softService, softId) {
            var vm = this;

            vm.loadData = function () {
                softService.getBindOption(softId).success(function (result) {
                    if (result!=null) {
                        vm.data = result;
                    }
                });
            };


            vm.data = {
                allowBindCount: '',
                allowChangeBindCount: '',
                softId: softId
            };

            vm.save = function () {
                vm.saving = !0;
                softService.saveBindOption(vm.data)
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

            vm.loadData();

        }
    ]);
})();