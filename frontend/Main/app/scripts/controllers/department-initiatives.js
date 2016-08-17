'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DepartmentInitiativesCtrl
 * @description
 * # DepartmentInitiativesCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DepartmentInitiativesCtrl', function($scope, relationatorController, departmentService, initiativeService) {

    var relationator = new relationatorController({
        baseService: departmentService,
        entityName: 'Department',
        baseRelatedService: initiativeService,
        relatedEntityName: 'Initiative',
        dragulaBagName: 'entities-bag',
        scope: $scope
    }).load();

});
