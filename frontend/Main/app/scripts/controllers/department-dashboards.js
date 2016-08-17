'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DepartmentDashboardsCtrl
 * @description
 * # DepartmentDashboardsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DepartmentDashboardsCtrl', function($scope, relationatorController, dashboardService, departmentService) {

    var relationator = new relationatorController({
        baseService: departmentService,
        entityName: 'Department',
        baseRelatedService: dashboardService,
        relatedEntityName: 'Dashboard',
        dragulaBagName: 'entities-bag',
        scope: $scope
    }).load();

});
