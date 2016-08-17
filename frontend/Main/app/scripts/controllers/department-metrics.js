'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DepartmentMetricsCtrl
 * @description
 * # DepartmentMetricsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DepartmentMetricsCtrl', function($scope, relationatorController, departmentService, metricService) {

    var relationator = new relationatorController({
        baseService: departmentService,
        entityName: 'Department',
        baseRelatedService: metricService,
        relatedEntityName: 'Metric',
        dragulaBagName: 'entities-bag',
        scope: $scope
    }).load();

});
