(function () {
    angular.module('app').controller('app.views.soft.softMoreOpenOption', [
        '$scope', '$modalInstance','abp.services.app.soft','softId',
        function ($scope, $modalInstance, softService, softId) {
            var vm = this;

            vm.loadData = function () {
                softService.getMoreOpenOption(softId).success(function (result) {
                    if (result!=null) {
                        vm.data = result;
                        vm.data.moreOpenRange = vm.data.moreOpenRange.toString();
                    }
                   
                });
            };


            vm.data = {
                moreOpenRange: '',
                verifyCycle: '',
                limitCount: '',
                softId: softId
            };

            vm.save = function () {
                vm.saving = !0;
                softService.saveMoreOpenOption(vm.data)
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