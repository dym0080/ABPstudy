/**
 * 分页工具栏
 */
angular.module('milo.pagination', []).directive('miloPagination', [function () {
    return {
        restrict: 'EA',
        templateUrl: '/Assets/Framework/scripts/milo.pagination.tlp.html',
        replace: true,
        link: function (scope, element, attrs) {

            scope.pageToInject = {};

            scope.pageInfo = {
                pageSize: 10,
                pageIndex: 1,
                totalCount: 0,
                pageCount: 1,
                itemCount: 0,
                begin: 0,
                end: 0
            };

            scope.status = {
                firstEnable: false,
                prevEnable: false,
                nextEnable: false,
                lastEnable: false
            };

            scope.gotoFirst = function () {
                if (!scope.status.firstEnable) return;
                scope.pageInfo.pageIndex = 1;
                scope.doPaging();
            };

            scope.prevPage = function () {
                if (!scope.status.prevEnable) return;
                scope.pageInfo.pageIndex--;
                scope.doPaging();
            };

            scope.nextPage = function () {
                if (!scope.status.nextEnable)return;
                scope.pageInfo.pageIndex++;
                scope.doPaging();
            };

            scope.gotoLast = function () {
                if (!scope.status.lastEnable) return;
                scope.pageInfo.pageIndex = scope.pageInfo.pageCount;
                scope.doPaging();
            };

            scope.doPaging=function() {
                //console.log("click paging.");
                setTimeout(function () {
                    scope.$apply(attrs.paginghandle);
                }, 100);
            };

            scope.$watch('pageInfo', function (value) {
                /*Checking if the given value is not undefined*/
                if (value) {
                    //console.log("pageInfo change.");
                    scope.status.firstEnable = scope.pageInfo.pageIndex !== 1;
                    scope.status.prevEnable = (scope.pageInfo.pageIndex) > 1;
                    scope.status.nextEnable = (scope.pageInfo.pageIndex + 1) <= scope.pageInfo.pageCount;
                    scope.status.lastEnable = scope.pageInfo.pageIndex  < scope.pageInfo.pageCount;
                }
            });



        }
    };
}]);
