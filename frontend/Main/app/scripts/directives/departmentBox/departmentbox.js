'use strict';

/**
 * @ngdoc directive
 * @name mainApp.directive:departmentBox
 * @description
 * # departmentBox
 */
angular.module('mainApp').directive('departmentBox', function($timeout, metricService, initiativeService) {
    return {
        templateUrl: 'views/departmentbox.html',
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

            var currentDashboard = function() {
                return scope.$parent.$parent.baseEntity;
            };

            scope.getMetrics = function() {
                return scope.department.Metrics.filter(function(metric) {
                    return scope.isHiddenForCurrentDashboard(metric) == false;
                });
            };
            scope.getInitiatives = function() {
                return scope.department.Initiatives.filter(function(initiative) {
                    return scope.isHiddenForCurrentDashboard(initiative) == false;
                });
            };

            scope.isHiddenForCurrentDashboard = function(metricOrInitiative) {
                if (metricOrInitiative.HiddenForDashboards) {
                    var hiddenDashboards = metricOrInitiative.HiddenForDashboards.split(',');
                    var oFound = hiddenDashboards.find(function(id) {
                        return id == currentDashboard().id;
                    });
                    return oFound != undefined;
                } else {
                    return false;
                }
            };

            scope.hideMetric = function(metric) {
                if (!metric.HiddenForDashboardsTags) {
                    metric.HiddenForDashboardsTags = [];
                }
                metric.HiddenForDashboardsTags.push(currentDashboard());
                metricService.save(metric);
            };
            scope.hideInitiative = function(initiative) {
                if (!initiative.HiddenForDashboardsTags) {
                    initiative.HiddenForDashboardsTags = [];
                }
                initiative.HiddenForDashboardsTags.push(currentDashboard());
                initiativeService.save(initiative);
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

            scope.departmentToUpdate = function(dep) {
                for (var i = 0; i < scope.$parent.$parent.baseEntity.Departments.length; i++) {
                    var current = scope.$parent.$parent.baseEntity.Departments[i];
                    if (current.id == dep.id) {
                        current.editMode = true;
                        scope.$parent.$parent.pendingToSave = scope.$parent.$parent.getPendingToSaveCount();
                        break;
                    }
                }
            }

        }
    };
});
