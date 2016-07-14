/**
 * 搜索面板
 */
angular.module('milo.search', []).directive('miloSearch', [function () {
    return {
        restrict: 'EA',
        templateUrl: '/Assets/Framework/scripts/milo.search.tlp.html',
        replace: true,
        //scope: {
        //    searchItems: "=",
        //    moreSearchCount: "=",
        //    searchChoose: "=",
        //    isExpand:"="
        //},
        link: function (scope, element, attrs) {

            scope.moreSearchCount = 2;
            scope.isExpand = false;
            scope.searchConds = [];
            //初始化
            scope.onInit = function () {
                scope.searchChoose = new Array();
                for (var i = 0; i < scope.moreSearchCount + 1; i++) {
                    scope.searchChoose.push({
                        index: i,
                        logic: 'AND',
                        field: '',
                        type:'',
                        operator: '',
                        value: '',
                        ignore:false,
                        hasvalue: false,
                        listItems: [],
                        maxValue: '',
                        minValue:'',
                        currentValueCtrl: '',
                        textHint: ''//值输入框提示信息
                    });
                }
                setTimeout(function () {
                    for (var i = 0; i < scope.searchChoose.length; i++) {
                        scope.searchChoose[i].field = scope.searchItems[0].field;
                        scope.onChangeField(i, scope.searchItems[0].field);
                    }

                }, 100);
            };
            scope.onInit();

            setTimeout(function () {
                //延迟初始化(等ui生成之前后触发)
                //页面上切换筛选字段
                $(element.find(".search-field select")).bind('change', function (obj) {
                    var index = parseInt(obj.target.id.replace('search-field-', ''));
                    setTimeout(function() {
                        scope.onChangeField(index, scope.searchChoose[index].field);
                    }, 100);
                });

                $(element.find("#search-button-query")).bind("click", function(event) {
                    //整理已经选中条件数据
                    scope.searchConds = new Array();
                    for (var i = 0; i < scope.searchChoose.length; i++) {
                        var chooseItem = scope.searchChoose[i];
                        if (chooseItem.field === '') continue;
                        if (chooseItem.value === '' && chooseItem.minValue === '' && chooseItem.maxValue === '') continue;
                        scope.searchConds.push({
                            field: chooseItem.field,
                            logic: chooseItem.logic,
                            operator: chooseItem.operator,
                            type: chooseItem.type,
                            value: chooseItem.value,
                            minValue: chooseItem.minValue,
                            maxValue: chooseItem.maxValue,
                            ignore: chooseItem.ignore
                            });
                    }
                    scope.$apply(attrs.searchhandle);
                });

            }, 1000);
            
            scope.changeMoreStatus = function() {
                scope.isExpand = !scope.isExpand;
            };
            
            scope.onChangeField = function (index, field) {
                var chooseItem = scope.searchChoose[index];
                chooseItem.value = '';
                chooseItem.maxValue = '';
                chooseItem.minValue = '';

                var searchItem = scope.getSearchItem(chooseItem.field);
                if (searchItem == null) return;
                chooseItem.type = searchItem.type;
                chooseItem.operator = searchItem.operator;
                chooseItem.ignore = searchItem.ignore;
                switch (searchItem.type) {
                    case "text":
                        chooseItem.currentValueCtrl = 'text';
                        if (searchItem.operator === "like") {
                            chooseItem.textHint = "请输入"+searchItem.title+"进行模糊查询";
                        }
                        if (searchItem.operator === "equal") {
                            chooseItem.textHint = "请输入" + searchItem.title + "进行精确查询";
                        }
                        break;
                    case "list":
                        chooseItem.currentValueCtrl = 'list';
                        chooseItem.listItems = searchItem.listItems;
                        break;
                    case "date":
                        chooseItem.currentValueCtrl = 'date';
                        //只需要支持范围查询
                        setTimeout(function () {
                            $(".date-picker").datepicker({
                                rtl: false,
                                format: 'yyyy-mm-dd',
                                orientation: "left",
                                autoclose: true
                            });
                        }, 100);

                        if (searchItem.operator === "range") {
                            
                        }
                        
                        break;
                    default:
                }
                scope.$apply();
            };

            scope.getSearchItem = function(field) {
                for (var i = 0; i < scope.searchItems.length; i++) {
                    if (scope.searchItems[i].field == field)
                        return scope.searchItems[i];
                }
                return null;
            };
        }
    };
}]);
