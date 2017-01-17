'use strict';

/**
 * @ngdoc directive
 * @name mainApp.directive:yearSelectize
 * @description
 * # yearSelectize
 */
angular.module('mainApp').directive('yearSelectize', function($timeout, metricYearService) {
    return {
        template: '<select></select>',
        restrict: 'E',
        // require: 'ngModel',
        scope: {
            metric: '='
                // placeholder: '@',
                // valueField: '@',
                // labelField: '@',
                // options: '='
        },
        link: function postLink(scope, element, attrs, ngModel) {

            var options;
            var valueField = 'id';
            var labelField = 'Value';
            var loading = true;

            var selectize = element.selectize({
                create: true,
                sortField: labelField, // scope.labelField,
                placeholder: 'Year', // scope.placeholder,
                valueField: valueField, // scope.valueField,
                labelField: labelField, // scope.labelField,
                // options: scope.options,
                maxItems: 1,
                persist: true,
                //Callbacks
                onChange: function(value) {
                    $timeout(function() {
                        if (value == '') {
                            if (scope.metric) {
                                scope.metric.SelectedMetricYear = {};
                            }
                            return;
                        }

                        if (options) {
                            scope.metric.SelectedMetricYear = options.find(function(option) {
                                return option[valueField] == value;
                            });
                            if (!scope.metric.SelectedMetricYear) {
                                scope.metric.SelectedMetricYear = {};
                                scope.metric.SelectedMetricYear[valueField] = 0;
                                scope.metric.SelectedMetricYear[labelField] = value;
                            }
                        }
                    });
                },
                onOptionAdd: function(value, data) {
                    if (!loading) {
                        metricYearService.save({
                            id: 0,
                            Value: value,
                            MetricKey: scope.metric.id
                        }, scope.metric.MetricYears).then(function(data) {
                            // console.log(data, scope.metric);
                            scope.metric.SelectedMetricYear = data.Result;
                            // loadSelected(scope.metric.SelectedMetricYear.id);
                            load(scope.metric);
                        });
                    }
                },
                onItemAdd: function(value, $item) {
                    // if (!loading) {
                    //     metricYearService.save({
                    //         id: 0,
                    //         Value: value,
                    //         MetricKey: scope.metric.id
                    //     }, scope.metric.MetricYears).then(function(data) {
                    //         console.log(data, scope.metric);
                    //         scope.metric.SelectedMetricYear = data.Result;
                    //     });
                    // }
                }
            })[0].selectize;

            scope.$watch('metric', function(newValue) {
                load(newValue);
            });

            function load(metric) {
                loading = true;
                refreshOptions(metric && metric.MetricYears ? metric.MetricYears : []);
                loadSelected(metric && metric.SelectedMetricYear ? metric.SelectedMetricYear.id : undefined);
                loading = false;
            }

            function loadSelected(selected) {
                selectize.clear();
                if (selected != undefined) {
                    selectize.addItem(selected, true);
                }
                selectize.refreshItems();
                $timeout(function() {
                    selectize.close();
                }, 100);
            }

            function refreshOptions(newOptions) {
                options = newOptions;
                selectize.clearOptions();
                if (newOptions) {
                    selectize.addOption(newOptions);
                }
                selectize.refreshOptions();
            }

            scope.$on('DeleteMetricYear', function() {
                load(scope.metric);
            });

        }
    };
});
