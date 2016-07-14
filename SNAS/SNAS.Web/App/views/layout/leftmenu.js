(function () {
    var controllerId = 'app.views.layout.leftmenu';
    angular.module('app').controller(controllerId, [
        '$rootScope','$scope','$state', 'appSession',
        function ($rootScope,$scope, $state, appSession) {
            var vm = this;

            vm.languages = abp.localization.languages;
            vm.currentLanguage = abp.localization.currentLanguage;
            vm.menu = abp.nav.menus.MainMenu; 
            $rootScope.currentMenuStack = new Array($state.current.menu); //菜单栈

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                
                var name = toState.menu;                
                $rootScope.currentMenuStack = new Array(name.toUpperCase());

                //如果该菜单有上级，将上级菜单名称写入栈(只考虑2级菜单的情况)
                for (var i = 0; i < abp.nav.menus.MainMenu.items.length; i++) {
                    var lev1Menu = abp.nav.menus.MainMenu.items[i];
                    for (var j = 0; j < lev1Menu.items.length; j++) {
                        var lev2Menu = lev1Menu.items[j];
                        if (lev2Menu.name.toUpperCase() == name.toUpperCase()) {
                            $rootScope.currentMenuStack.push(lev1Menu.name.toUpperCase());
                        }
                    }
                }
                setTimeout(function () {
                    $scope.$apply();
                }, 200);
                
                console.log("$rootScope.currentMenuStack:" + $rootScope.currentMenuStack);
            });

            Layout.init();            
            Demo.init();

            vm.checkIsSelect = function (name) {

                //console.log("checkIsSelect name:"+name);

                //判断当前菜单是否需要被选中(下级选中自动选择上级)
                for (var i = 0; i < $rootScope.currentMenuStack.length; i++) {
                    if ($rootScope.currentMenuStack[i] == name.toUpperCase()) {
                        //setTimeout(function () {
                        //    $scope.$apply();
                        //}, 100);
                        return true;
                    }
                }                
                
                return false;
            };

         
        }
    ]);
})();