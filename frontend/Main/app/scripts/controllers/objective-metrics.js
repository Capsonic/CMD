'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:ObjectiveMetricsCtrl
 * @description
 * # ObjectiveMetricsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('ObjectiveMetricsCtrl', function($scope, relationatorController, objectiveService, metricService) {

    var relationator = new relationatorController({
        baseService: objectiveService,
        entityName: 'Objective',
        baseRelatedService: metricService,
        relatedEntityName: 'Metric',
        dragulaBagName: 'entities-bag',
        scope: $scope
    }).load();

});
