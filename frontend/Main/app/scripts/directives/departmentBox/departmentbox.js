'use strict';

/**
 * @ngdoc directive
 * @name mainApp.directive:departmentBox
 * @description
 * # departmentBox
 */
angular.module('mainApp').directive('departmentBox', function($timeout) {
    return {
        templateUrl: 'scripts/directives/departmentBox/departmentbox.html',
        restrict: 'E',
        scope: {
            department: '='
        },
        link: function postLink(scope, element, attrs) {
            element.hide();
            $timeout(function() {
                scope.$watch(function() {
                    return element.parent().width() + element.parent().height() + angular.element(window).width() + angular.element(window).height();
                }, function() {
                    element.hide();
                    resetSizes();
                    element.show();
                });
            }, 200);

            function resetSizes() {
                $timeout(function() {
                    switch (true) {
                        case element.parent().width() > 1800:
                            element.css('font-size', 42);
                            element.find('h2').css('font-size', 56);
                            break;
                        case element.parent().width() > 1500:
                            element.css('font-size', 36);
                            element.find('h2').css('font-size', 40);
                            break;
                        case element.parent().width() > 900:
                            element.css('font-size', 18);
                            element.find('h2').css('font-size', 21);
                            break;
                        case element.parent().width() > 350:
                            element.css('font-size', 12);
                            element.find('h2').css('font-size', 16);
                            break;
                        default:
                            element.css('font-size', 10);
                            element.find('h2').css('font-size', 12);
                    };
                }, 200);
            };

            scope.theWidth = function() {
                return element.parent().width();
            };

            scope.editMetric = function(metric) {
                scope.$parent.$parent.selectedMetric = metric;
                angular.element('#modal-metricToSave').modal('show');
                angular.copy(metric, scope.$parent.metricToSave);
            };
            scope.editInitiative = function(initiative) {
                scope.$parent.$parent.selectedInitiative = initiative;
                angular.element('#modal-initiativeToSave').modal('show');
                angular.copy(initiative, scope.$parent.initiativeToSave);
            };

        }
    };
});
