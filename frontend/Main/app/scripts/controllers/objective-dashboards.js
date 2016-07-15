'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:ObjectiveDashboardsCtrl
 * @description
 * # ObjectiveDashboardsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('ObjectiveDashboardsCtrl', function($scope, relationatorController, dashboardService, objectiveService) {

    var relationator = new relationatorController({
        baseService: objectiveService,
        entityName: 'Objective',
        baseRelatedService: dashboardService,
        relatedEntityName: 'Dashboard',
        dragulaBagName: 'entities-bag',
        scope: $scope
    }).load();

});
