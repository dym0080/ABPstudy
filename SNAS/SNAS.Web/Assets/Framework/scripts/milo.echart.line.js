angular.module('milo.echart.line', []).directive('miloEchartLine', [function () {
    return {
        scope: {
            id: "@",
            legend: "=",
            item: "=",
            data: "="
        },
        restrict: 'E',
        template: '<div style="height:350px;"></div>',
        replace: true,
        link: function ($scope, element, attrs, controller) {


            $scope.initChat = function () {
                var option = {
                    // 提示框，鼠标悬浮交互时的信息提示
                    tooltip: {
                        show: true,
                        trigger: 'item'
                    },
                    grid: {
                        show: false,
                        left: '0',
                        right: '5',
                        bottom: '0',
                        containLabel: true,
                        borderColor: '#9eacb4',
                        borderWidth:0.5
                    },
                    // 图例
                    legend: {
                        data: $scope.legend,
                        textStyle: {
                            color: '#9eacb4'
                        }
                    },
                    // 横轴坐标轴
                    xAxis: [{
                        type: 'category',
                        data: $scope.item,
                        nameTextStyle: {
                            color: '#9eacb4'
                        }
                    }],
                    // 纵轴坐标轴
                    yAxis: [{
                        type: 'value'
                    }],
                    // 数据内容数组
                    series: function () {
                        var serie = [];
                        for (var i = 0; i < $scope.legend.length; i++) {
                            var item = {
                                name: $scope.legend[i],
                                type: 'line',
                                data: $scope.data[i]
                            };
                            serie.push(item);
                        }
                        return serie;
                    }()
                };
                var myChart = echarts.init(document.getElementById($scope.id), 'macarons');
                myChart.setOption(option);
            }
            

            $scope.$watch('data', function (value) {
                /*Checking if the given value is not undefined*/
                if (value) {
                    $scope.initChat();
                }
            });

            $scope.initChat();
        }
    };
}]);