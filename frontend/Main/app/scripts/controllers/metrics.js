'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:MetricsCtrl
 * @description
 * # MetricsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('MetricsCtrl', function($scope, listController, metricService) {
    var list = new listController({
        entityName: 'Metric',
        baseService: metricService,
        modalName: 'modal-itemToSave',
        scope: $scope
    }).load();
});
