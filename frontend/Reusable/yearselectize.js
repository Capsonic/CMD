'use strict';

/**
 * @ngdoc directive
 * @name appApp.directive:ngSelectize
 * @description
 * # ngSelectize
 */
angular.module('mainApp').directive('yearSelectize', function(metricService) {
    return {
        template: '<selectize config="theConfig" options="theOptions" ng-model="model"></selectize>',
        restrict: 'E',
        scope: {
            // placeholder: '@',
            // valueField: '@',
            // labelField: '@',
            // theOptions: '=',
            model: '=',
        },
        compile: function compile(tElement, tAttrs, transclude) {
            return {
                pre: function preLink(scope, iElement, iAttrs, controller) {
                    var selectize;

                    scope.theConfig = {
                        create: true,
                        sortField: 'Value', //scope.labelField,
                        placeholder: 'Year', //scope.placeholder,
                        valueField: 'id', // scope.valueField,
                        labelField: 'Value', // scope.labelField,
                        maxItems: 1,
                        persist: false,
                        onInitialize: function(sel) {
                            selectize = sel;
                        },
                        onItemAdd: function(value, $item) {
                            console.log(value, $item);
                        }

                    };

                    function addItem(value) {
                        // var newYear = {
                        //     MetricYearKey: 0,
                        //     Note: '',
                        //     Value: value,
                        //     MetricKey: scope.model.id,


                        // };
                        // metricService.save().then(function() {

                        // }.function(data) {

                        // });
                    }



                },
                post: function postLink(scope, iElement, iAttrs, controller) {

                }
            }
        }
    };
});
