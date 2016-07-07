(function() {
    var app = angular.module('app');

    var controllerId = 'sts.views.dictionary.list';
    app.controller(controllerId, [
        '$scope', 'abp.services.tasksystem.dictionary',
        function ($scope, dictionaryService) {
            var vm = this;

            vm.localize = abp.localization.getSource('ABPDemoNoZero');

            vm.dictionarys = [];

            

            //vm.people = []; //TODO: Move Person combo to a directive?

            //personService.getAllPeople().success(function (data) {
            //    vm.people = data.people;
            //});


            dictionaryService.getDictionarys().success(function (data) {
                console.log("dictionarys data");
                vm.dictionarys = data.dictionarys;
            });
            
        }
    ]);
})();