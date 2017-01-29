'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardDepartmentsCtrl
 * @description
 * # DashboardDepartmentsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardDepartmentsCtrl', function($scope, relationatorController, dashboardService, departmentService) {

    $scope.isDesignMode = false;
    var relationator = new relationatorController({
        baseService: dashboardService,
        entityName: 'Dashboard',
        baseRelatedService: departmentService,
        relatedEntityName: 'Department',
        dragulaBagName: 'entities-bag',
        scope: $scope,
        afterLoad: function() {
            $scope.occuppiedEntities.forEach(function(oDepartment) {
                oDepartment.InfoGridster = null;
            });

            var allDepartments = $scope.occuppiedEntities.concat($scope.availableEntities);

            $scope.availableYears = getAvailableYears(allDepartments);
            if (!$scope.dashboardYear) {
                $scope.dashboardYear = $scope.availableYears.slice(-1)[0];
            } else {
                if ($scope.availableYears.indexOf($scope.dashboardYear) == -1) {
                    $scope.dashboardYear = null;
                }
            }

        }
    }).load();


    var getAvailableYears = function(departments) {
        var result = [];
        if (departments) {
            departments.forEach(function(department) {
                department.Metrics.forEach(function(metric) {
                    metric.MetricYears.forEach(function(year) {
                        result.push(year.Value);
                    });
                });
            });
        }

        result = removeDuplicates(result);

        result.sort(function(a, b) {
            return a - b;
        });

        return result;
    };

    var removeDuplicates = function(arr) {
        var uniqueElements = [];
        jQuery.each(arr, function(i, el) {
            if (jQuery.inArray(jQuery.trim(el), uniqueElements) === -1) uniqueElements.push(jQuery.trim(el));
        });
        return uniqueElements;
    };

    $scope.onDepartmentBoxShown = function() {
        $scope.$broadcast('departmentBoxShown');
    };


});
