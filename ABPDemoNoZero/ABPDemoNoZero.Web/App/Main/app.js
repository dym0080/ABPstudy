(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/');
            $stateProvider
                .state('tasklist', {
                    url: '/',
                    templateUrl: '/App/Main/views/task/list.cshtml',
                    menu: 'TaskList' //Matches to name of 'TaskList' menu in ABPDemoNoZeroNavigationProvider
                })
                .state('newtask', {
                    url: '/new',
                    templateUrl: '/App/Main/views/task/new.cshtml',
                    menu: 'NewTask' //Matches to name of 'NewTask' menu in ABPDemoNoZeroNavigationProvider
                })
                .state('Dictionary', {
                    url: '/',
                    templateUrl: '/App/Main/views/Dictionary/list.cshtml',
                    menu: 'DictionaryList' 
                });
        }
    ]);

    ////Configuration for Angular UI routing.
    //app.config([
    //    '$stateProvider', '$urlRouterProvider',
    //    function($stateProvider, $urlRouterProvider) {
    //        $urlRouterProvider.otherwise('/');
    //        $stateProvider
    //            .state('home', {
    //                url: '/',
    //                templateUrl: '/App/Main/views/home/home.cshtml',
    //                menu: 'Home' //Matches to name of 'Home' menu in ABPDemoNoZeroNavigationProvider
    //            })
    //            .state('about', {
    //                url: '/about',
    //                templateUrl: '/App/Main/views/about/about.cshtml',
    //                menu: 'About' //Matches to name of 'About' menu in ABPDemoNoZeroNavigationProvider
    //            });
    //    }
    //]);
})();