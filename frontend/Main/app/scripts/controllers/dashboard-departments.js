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
            $scope.occuppiedEntities.forEach(function(oDepartment){
                oDepartment.InfoGridster = null;
            });
        }
    }).load();

});
