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
            metricYear: '='
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
                    theData = $scope.baseList;
                    if (table) {
                        table.loadData($scope.baseList);
                    }
                    // $scope.theDashboards = metricService.catalogs.Dashboards.getAll();
                }
            });

            var getHandsontableHeight = function() {
                return 300;
            };

            var getHandsontableWidth = function() {
                if (table) {
                    // console.log(table)
                }
                return 750;
            };

            function setSize() {
                var el = jQuery(elem);
                var newHeight = el.find('table').eq(0).outerHeight();
                console.log(newHeight);
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
                    colWidths: [80, 100, 50, 120, 250, 130],
                    colHeaders: ['Year', 'Month', 'Day', 'Time', 'Note', 'Current Value'],
                    columns: [{
                            data: 'ConvertedMetricYear',
                            type: 'numeric',
                            readOnly: true
                        }, {
                            data: 'ConvertedMetricMonth',
                            type: 'dropdown',
                            source: ['January',
                                'February',
                                'March',
                                'April',
                                'May',
                                'June',
                                'July',
                                'August',
                                'September',
                                'October',
                                'November',
                                'December'
                            ]
                        }, {
                            data: 'ConvertedMetricDay',
                            type: 'dropdown',
                            source: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31]
                        }, {
                            data: 'ConvertedMetricTime',
                            type: 'time',
                            timeFormat: 'h:mm a',
                            correctFormat: true
                        },
                        // 'text' is default, you don't actually need to declare it
                        { data: 'Note' },
                        { data: 'CurrentValue' },
                    ],
                    data: $scope.baseList,
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
                                if (['ConvertedMetricDate', 'Note', 'CurrentValue'].indexOf(prop) > -1) {
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
                            }

                        });
                    }
                });

                table.alter('insert_row', 0);

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

            var savePending = function() {

                $activityIndicator.startAnimating();
                var arrPromiseConstructors = [];


                //Items to be update or inserted:
                var arrItemsToBeSaved = $scope.baseList.filter(function(item) {
                    return item.edited;
                });



                arrItemsToBeSaved.forEach(function(item) {
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


                //Reloading all
                var promiseConstructor = function() {
                    return list.loadByParentKey('MetricYear', 1);
                }

                arrPromiseConstructors.push(promiseConstructor);

                //Sending transactions in serial way:
                $q.serial(arrPromiseConstructors).finally(function(data) {
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

                }

                savePending();
            });



        }

    };
});


var theData;
