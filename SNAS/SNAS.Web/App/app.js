(function () {
    'use strict';
    
    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',

        'ui.router',
        'ui.bootstrap',
        //'ui.jq',

        'milo.search', 
        'milo.pagination',
        'milo.echart.line',
        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider',
        function($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/');

            //if (abp.auth.hasPermission('Pages.Tenants')) {
            //    $stateProvider
            //        .state('tenants', {
            //            url: '/tenants',
            //            templateUrl: '/App/Main/views/tenants/index.cshtml',
            //            menu: 'Tenants' //Matches to name of 'Tenants' menu in AlibabaPickerNavigationProvider
            //        });
            //    $urlRouterProvider.otherwise('/tenants');
            //}

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/views/home/home.cshtml',
                    menu: 'Home' 
                })
                .state('soft', {
                    url: '/soft',
                    templateUrl: '/App/views/soft/soft.cshtml',
                    menu: 'Soft'
                })
                .state('dictionary', {
                    url: '/dictionary',
                    templateUrl: '/App/views/dictionary/diclist.cshtml',
                    menu: 'Dictionary'
                })
                .state('softlicenseoption', {
                    url: '/softlicenseoption/:softId',
                    templateUrl: '/App/views/soft/softlicenseoption.cshtml',
                    menu: 'Soft'
                })
                .state('softlicense', {
                    url: '/softlicense/:softId',
                    templateUrl: '/App/views/soft/softlicense.cshtml',
                    menu: 'Soft'
                })
                .state('softuser', {
                    url: '/softuser',
                    templateUrl: '/App/views/softusers/softuser.cshtml',
                    menu: 'Softuser'
                })
                .state('finance', {
                    url: '/finance',
                    templateUrl: '/App/views/finances/finance.cshtml',
                    menu: 'Finance'
                })
                .state('softuserlicense', {
                    url: '/softuserlicense',
                    templateUrl: '/App/views/softuserlicenses/softuserlicense.cshtml',
                    menu: 'SoftuserLicense'
                })
                 .state('softuserlicensemcode', {
                     url: '/softuserlicensemcode/:softUserLicenseId',
                     templateUrl: '/App/views/softuserlicenses/softuserlicensemcode.cshtml',
                     menu: 'SoftuserLicense'
                 })
                .state('mcodechangelog', {
                    url: '/mcodechangelog/:softUserLicenseId',
                    templateUrl: '/App/views/softuserlicenses/mcodechangelog.cshtml',
                    menu: 'SoftuserLicense'
                })
                .state('softuserlogin', {
                    url: '/softuserlogin',
                    templateUrl: '/App/views/softusers/softuserlogin.cshtml',
                    menu: 'Softuserlogin'
            });
        }
    ]);
})();