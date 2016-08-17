'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:AdministrationCtrl
 * @description
 * # AdministrationCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('AdministrationCtrl', function($scope, dashboardService, departmentService) {
    dashboardService.loadAll().then(function() {
        $scope.dashboards = dashboardService.getAll();
    });


    $scope.selectDashboard = function(dashboard) {
    	$scope.selectedDashboard = dashbaord;
    };
});
