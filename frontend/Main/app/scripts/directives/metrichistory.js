'use strict';

/**
 * @ngdoc directive
 * @name mainApp.directive:metricHistory
 * @description
 * # metricHistory
 */
angular.module('mainApp').directive('metricHistory', function($timeout) {
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
        controller: function($scope, listController, metricHistoryService, $q, $activityIndicator, metricYearService) {

            var table;

            var list = new listController({
                entityName: 'MetricHistory',
                baseService: metricHistoryService,
                scope: $scope,
                afterLoad: function(data) {
                    theData = $scope.baseList;
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
                    insertRow(rows);

                    rows.sort(function(a, b) {
                        return b.ConvertedMetricDate - a.ConvertedMetricDate;
                    });
                }

                return rows;
            };

            var insertRow = function(rows, change) {
                if (!rows.length || (rows[0] && rows[0].id > -1)) {
                    rows.unshift({});
                    if (change) {
                        table.selectCellByProp(1, change[1]);
                    }
                }
            }

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
                        className: 'htCenter'
                    }, {
                        data: 'ConvertedMetricDay',
                        type: 'dropdown',
                        source: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31],
                        className: 'htCenter'
                    }, {
                        data: 'ConvertedMetricTime',
                        type: 'time',
                        timeFormat: 'h:mm:ss a',
                        correctFormat: true,
                        className: 'htCenter'
                    }, { data: 'Note' }, {
                        data: 'CurrentValue',
                        className: 'htRight'
                    }, ],
                    // {
                    //     data: 'GoalValue',
                    //     readOnly: true,
                    //     className: 'htRight'
                    // }, 

                    // data: $scope.baseList,
                    // afterCreateRow: function(index, amount, source) {
                    //     if ($scope.metricYear && $scope.metricYear.id) {
                    //         var theDate = new Date();
                    //         theDate.setFullYear($scope.metricYear.Value);

                    //         //This newMetricHistory should be made as if it comes from the server:
                    //         var newMetricHistory = {
                    //             id: 0,
                    //             MetricDate: theDate.toJSON()
                    //         };

                    //         metricHistoryService.adapt(newMetricHistory);
                    //         $scope.baseList[index] = newMetricHistory;
                    //     }
                    // },
                    // afterCreateRow: function(index, amount, source) {
                    //     // setSize();

                    // },
                    // afterRender: function() {
                    //     setSize();
                    // },
                    afterChange: function(changes, source) {
                        if (source == 'loadData') {
                            return;
                        }
                        changes.forEach(function(change) {

                            var current = $scope.baseList[change[0]];

                            var allFieldsAreEmtpy = true;
                            for (var prop in current) {
                                if (['ConvertedMetricMonth', 'ConvertedMetricDay', 'ConvertedMetricTime', 'Note', 'CurrentValue'].indexOf(prop) > -1) {
                                    if (current[prop] != '' && current[prop] != null && current[prop] != undefined) {
                                        allFieldsAreEmtpy = false;
                                        break;
                                    }
                                }
                            }

                            if (allFieldsAreEmtpy) {
                                if (current.hasOwnProperty('id')) {
                                    current.removed = true;
                                    current.edited = false;
                                }
                            } else {
                                current.edited = true;
                                current.removed = false;
                                if (!current.hasOwnProperty('id') || current.id == null || current.id == undefined) {
                                    current.id = 0;
                                    current.MetricHistoryKey = 0;
                                    current.MetricYearKey = $scope.metricYear.id;
                                    delete current.EF_State;
                                }
                                if (source != 'timeValidator') {
                                    $timeout(function() {
                                        insertRow($scope.baseList, change);
                                        table.render();
                                    });
                                }
                            }
                        });
                    }
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

            function getMonthNumber(monthName) {
                return metricHistoryService.months.indexOf(monthName);
            }

            function HTDate_To_JSDate(row) {

                if (!row.ConvertedMetricDay) {
                    return null;
                }

                if (getMonthNumber(row.ConvertedMetricMonth) == -1) {
                    return null;
                }

                var momentTime = moment(row.ConvertedMetricTime, 'h:mm:ss a');
                if (!momentTime.isValid()) {
                    return null;
                }

                var theDate = moment({
                    year: $scope.metricYear.Value,
                    month: getMonthNumber(row.ConvertedMetricMonth),
                    day: Number(row.ConvertedMetricDay),
                    hour: momentTime.hour(),
                    minute: momentTime.minute(),
                    second: momentTime.second()
                });

                if (theDate.isValid()) {
                    return theDate;
                }

                return null;
            }

            var savePending = function() {

                $activityIndicator.startAnimating();
                var arrPromiseConstructors = [];


                //Items to be updated or inserted:
                var arrItemsToBeSaved = $scope.baseList.filter(function(item) {
                    if (item.edited) {
                        item.ConvertedMetricDate = HTDate_To_JSDate(item);
                    }
                    return item.edited;
                });
                arrItemsToBeSaved.forEach(function(item) {
                    item.FormatKey = $scope.metricYear.FormatKey;
                    var promiseConstructor = function() {
                        return metricHistoryService.save(item);
                    }
                    arrPromiseConstructors.push(promiseConstructor);
                });

                //Items to be deleted:
                var arrItemsToBeDeleted = $scope.baseList.filter(function(item) {
                    return item.removed;
                });
                arrItemsToBeDeleted.forEach(function(item) {
                    var promiseConstructor = function() {
                        return metricHistoryService.remove(item);
                    }
                    arrPromiseConstructors.push(promiseConstructor);
                });

                //Metric Year update
                if ($scope.metricYear && $scope.metricYear.editMode) {
                    var promiseConstructor = function() {
                        return metricYearService.save($scope.metricYear, $scope.metric.MetricYears);
                    }
                    arrPromiseConstructors.push(promiseConstructor);
                }


                //Reloading all
                var promiseConstructor = function() {
                    return list.loadByParentKey('MetricYear', $scope.metricYear.id);
                }

                arrPromiseConstructors.push(promiseConstructor);

                //Sending transactions in serial way:
                $q.serial(arrPromiseConstructors).finally(function(data) {
                    $scope.$emit('RefreshMetric', $scope.metric);
                    $timeout(function() {
                        alertify.message('Process completed.');
                    }, 100);
                });

            };


            $scope.$on('ShowMetricHistory', function() {
                reset();
            });

            $scope.$on('SaveMetricHistory', function() {
                var MetricYearKey = $scope.metricYear.id;
                if (!MetricYearKey) {
                    alertify.message('Nothing to save.');
                } else {
                    savePending();
                }
            });
        }
    };
});


var theData;
