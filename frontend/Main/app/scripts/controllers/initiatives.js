'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:InitiativesCtrl
 * @description
 * # InitiativesCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('InitiativesCtrl', function($scope, listController, initiativeService) {
    
    var list = new listController({
        entityName: 'Initiative',
        baseService: initiativeService,
        modalName: 'modal-itemToSave',
        scope: $scope
    }).load();

});
