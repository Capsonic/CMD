'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardsCtrl
 * @description
 * # DashboardsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardsCtrl', function($scope, dashboardService) {
	
	dashboardService.loadAll().then(function(){
		$scope.dashboards = dashboardService.getAll();
	});

	$scope.open = function(item){
		alert('hola')
	};

	$scope.remove = function(item){
		dashboardService.remove(item);
	};

});
