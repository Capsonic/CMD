'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:ObjectivesCtrl
 * @description
 * # ObjectivesCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('ObjectivesCtrl', function($scope, objectiveService, listController) {
    var list = new listController({
        entityName: 'Objective',
        baseService: objectiveService,
        modalName: 'modal-itemToSave',
        scope: $scope
    }).load();
});
