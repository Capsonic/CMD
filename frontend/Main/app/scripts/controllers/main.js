'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('MainCtrl', function($scope, sampleService) {
	sampleService.loadAll().then(function(data){
		$scope.sampleData = sampleService.getAll();
		console.log("hola")
	});
});
