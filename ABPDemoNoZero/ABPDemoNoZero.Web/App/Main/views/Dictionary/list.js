(function() {
    var app = angular.module('app');

    var controllerId = 'sts.views.dictionary.list';
    app.controller(controllerId, [
        '$scope', 'abp.services.tasksystem.dictionary',
        function ($scope, dictionaryService) {
            var vm = this;

            vm.localize = abp.localization.getSource('ABPDemoNoZero');

            vm.dictionarys = [];

            $scope.selectedTaskState = 0;

            $scope.$watch('selectedTaskState', function(value) {
                vm.refreshTasks();
            });

            vm.refreshTasks = function() {
                abp.ui.setBusy( //Set whole page busy until getTasks complete
                    null,
                    dictionaryService.getDictionarys({ //Call application service method directly from javascript
                        state: $scope.selectedTaskState > 0 ? $scope.selectedTaskState : null
                    }).success(function(data) {
                        vm.tasks = data.tasks;
                    })
                );
            };

            vm.changeDictionaryState = function (dictionary) {
                //var newState;
                //if (task.state == 1) {
                //    newState = 2; //Completed
                //} else {
                //    newState = 1; //Active
                //}

                dictionaryService.updateDictionary({
                    dictionaryId: dictionary.id,
                    state: newState
                }).success(function() {
                    dictionary.state = newState;
                    abp.notify.info(vm.localize('TaskUpdatedMessage'));
                });
            };

            vm.getDictionaryCountText = function () {
                return abp.utils.formatString(vm.localize('Xtasks'), vm.dictionarys.length);
            };
        }
    ]);
})();