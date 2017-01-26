'use strict';

/**
 * @ngdoc directive
 * @name mainApp.directive:metricHistoryReadOnly
 * @description
 * # metricHistoryReadOnly
 */
angular.module('mainApp').directive('metricHistoryReadOnly', function($timeout) {
    var elem;
    return {
        //templateUrl: 'views/metricHistory.html',
        template: '<div></div>',
        restrict: 'E',
        scope: {
            metricYear: '=',
            metric: '='
        },
        replace: true,
        link: function(scope, iElement, iAttrs) {
            elem = iElement.get(0);
        },
        // compile: function() {
        //     return {
        //         pre: function preLink(scope, iElement, iAttrs) {

        //         }
        //     }
        // },
        controller: function($scope, listController, metricHistoryService, $q, $activityIndicator) {

            var table;

            var list = new listController({
                entityName: 'MetricHistory',
                baseService: metricHistoryService,
                scope: $scope,
                afterLoad: function(data) {
                    if (table) {
                        adaptToHandsontable($scope.baseList);
                        table.loadData($scope.baseList);
                    }
                }
            });

            var adaptToHandsontable = function(rows) {
                if (!rows) {
                    rows = [];
                }

                if ($scope.metricYear && $scope.metricYear.id > -1) {

                    rows.sort(function(a, b) {
                        return b.ConvertedMetricDate - a.ConvertedMetricDate;
                    });
                }

                return rows;
            };

            var getHandsontableHeight = function() {
                return 300;
            };

            var getHandsontableWidth = function() {
                if (table) {
                    // console.log(table)
                }
                return 610;
            };

            function setSize() {
                var el = jQuery(elem);
                var newHeight = el.find('table').eq(0).outerHeight();
                el.children().eq(0).children().css('height', newHeight + 10);
                el.children().eq(0).children().children().css('height', newHeight + 2);
            }

            var reset = function() {
                table = new Handsontable(elem, {
                    allowInsertRow: true,
                    height: getHandsontableHeight,
                    width: getHandsontableWidth,
                    startRows: 1,
                    colHeaders: true,
                    minSpareRows: 0,
                    copyable: true,
                    colWidths: [100, 70, 90, 200, 120, 120],
                    colHeaders: ['Month', 'Day', 'Time', 'Note', 'Current Value'],
                    columns: [{
                        data: 'ConvertedMetricMonth',
                        type: 'dropdown',
                        source: metricHistoryService.months,
                        className: 'htCenter',
                        readOnly: true
                    }, {
                        data: 'ConvertedMetricDay',
                        type: 'dropdown',
                        source: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31],
                        className: 'htCenter',
                        readOnly: true
                    }, {
                        data: 'ConvertedMetricTime',
                        type: 'time',
                        timeFormat: 'h:mm:ss a',
                        correctFormat: true,
                        className: 'htCenter',
                        readOnly: true
                    }, {
                        data: 'Note',
                        readOnly: true
                    }, {
                        data: 'CurrentValue',
                        className: 'htRight',
                        readOnly: true
                    }, ],
                });

                adaptToHandsontable($scope.baseList);
                table.loadData($scope.baseList);
            };

            $scope.$watch(function() {
                return $scope.metricYear;
            }, function() {
                if ($scope.metricYear) {
                    // list.loadByParentKey($scope.metricYear.id);
                    list.loadByParentKey('MetricYear', $scope.metricYear.id).then(function() {

                    });
                } else {
                    list.loadByParentKey('MetricYear', 0); //Clear list
                }
            });

            $scope.$on('ShowMetricHistoryReadOnly', function() {
                reset();
            });
        }
    };
});
