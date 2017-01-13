'use strict';

/**
 * @ngdoc directive
 * @name appApp.directive:ngSelectize
 * @description
 * # ngSelectize
 */
angular.module('mainApp').directive('yearSelectize', function(metricService) {
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


            var selectize = element.selectize({
                create: true,
                sortField: 'Value',// scope.labelField,
                placeholder: 'Year', // scope.placeholder,
                valueField: 'id', // scope.valueField,
                labelField: 'Value', // scope.labelField,
                // options: scope.options,
                maxItems: 1,
                persist: false,
                //Callbacks
                onChange: function(value) {
                    if (scope.options) {
                        scope.entity = scope.options.find(function(option) {
                            return option[scope.valueField] == value;
                        });
                        if (!scope.entity) {
                            scope.entity = {};
                            scope.entity[scope.valueField] = null;
                            scope.entity[scope.labelField] = value;
                        }
                        ngModel.$setViewValue(scope.entity);

                    }
                },
                onOptionAdd: function(value, data) {
                    // console.log(value, data);
                },
                onItemAdd: function(value, $item) {
                    // console.log(value, $item);
                }
            })[0].selectize;


            scope.$watch('metric', function(newValue) {
                selectize.clearOptions();
                if (newValue && newValue.MetricYears) {
                    selectize.addOption(newValue.MetricYears);
                }
                // selectize.refreshOptions();
            });

            // ngModel.$render = function() {
            //     selectize.addItem(ngModel.$modelValue);
            //     selectize.refreshItems();
            // };
            


        }
    };
});
