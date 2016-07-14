(function () {
    var controllerId = 'app.views.layout';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', 'appSession', function ($scope, $modal, appSession) {
            var vm = this;
            
            Layout.init();
            Demo.init();
            vm.getShownUserName = function () {
                return appSession.user.userName;
            };
            

            vm.chgPassword = function () {

                var modalInstance = $modal.open({
                    templateUrl: '/App/views/layout/chgPassword.cshtml',
                    controller: 'app.views.chgPassword as vm',
                    backdrop: 'static'
                });
            };

        }]);
})();