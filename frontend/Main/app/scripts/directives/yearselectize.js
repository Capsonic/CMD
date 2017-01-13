'use strict';

/**
 * @ngdoc directive
 * @name mainApp.directive:yearSelectize
 * @description
 * # yearSelectize
 */
angular.module('mainApp').directive('yearSelectize', function($timeout) {
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
                            scope.metric.SelectedMetricYear = {};
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
                // onOptionAdd: function(value, data) {
                //     var newYear = {
                //         Value: value,
                //         id: 0,
                //         MetricKey: scope.metric.id
                //     };
                //     // scope.metric.MetricYears.push(newYear);
                //     // refreshOptions(scope.metric.MetricYears);

                // },
                onItemAdd: function(value, $item) {
                    // console.log(value, $item);
                }
            })[0].selectize;

            scope.$watch('metric', function(newValue) {
                refreshOptions(newValue && newValue.MetricYears ? newValue.MetricYears : []);
                loadSelected(newValue && newValue.SelectedMetricYear ? newValue.SelectedMetricYear.id : undefined);
            });

            function loadSelected(selected) {
                selectize.clear();
                if (selected != undefined) {
                    selectize.addItem(selected);
                }
                selectize.refreshItems();
            }

            function refreshOptions(newOptions) {
                options = newOptions;
                selectize.clearOptions();
                if (newOptions) {
                    selectize.addOption(newOptions);
                }
                selectize.refreshOptions();
            }

        }
    };
});
