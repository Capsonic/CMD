'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DepartmentsCtrl
 * @description
 * # DepartmentsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DepartmentsCtrl', function($scope, departmentService, listController) {
    var list = new listController({
        entityName: 'Department',
        baseService: departmentService,
        modalName: 'modal-itemToSave',
        scope: $scope
    }).load();
});
