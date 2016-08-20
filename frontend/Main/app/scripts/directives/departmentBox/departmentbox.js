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

            var sizer = element.find('.Sizer');
            var input = element.find('.Sizer.form-control');
            sizer.on('mousedown', function(e) {
                // e.preventDefault();
                e.stopPropagation();
                $timeout(function() {
                    input[0].focus();
                });
            });

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
                    element.find('.columnMaxWidth').css('max-width', element.parent().width() * .6);
                }, 200);
            };

            scope.theWidth = function() {
                return element.parent().width();
            };

            var currentDashboardId = function() {
                return scope.$parent.$parent.baseEntity.id;
            };

            scope.getMetrics = function() {
                return scope.department.Metrics.filter(function(metric) {
                    return scope.isHiddenForCurrentDashboard(metric) == false;
                });
            };

            scope.isHiddenForCurrentDashboard = function(metricOrInitiative) {
                if (metricOrInitiative.HiddenForDashboards) {
                    var hiddenDashboards = metricOrInitiative.HiddenForDashboards.split(',');
                    var oFound = hiddenDashboards.find(function(id) {
                        return id == currentDashboardId();
                    });
                    return oFound != undefined;
                } else {
                    return false;
                }
            };

            scope.hideRow = function(metricOrInitiative) {
                if (!metricOrInitiative.HiddenForDashboards) {
                    metricOrInitiative.HiddenForDashboards = '';
                }
                metricOrInitiative.HiddenForDashboards += ',' + currentDashboardId();
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

            scope.showHidden = function(department) {
                scope.$parent.$parent.selectedDepartment = department;
                angular.element('#modal-showHidden').modal('show');
                angular.copy(department, scope.$parent.departmentToSave);
            };

        }
    };
});
