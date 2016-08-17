'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardDepartmentsCtrl
 * @description
 * # DashboardDepartmentsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardDepartmentsCtrl', function($scope, relationatorController, dashboardService, departmentService) {


    var relationator = new relationatorController({
        baseService: dashboardService,
        entityName: 'Dashboard',
        baseRelatedService: departmentService,
        relatedEntityName: 'Department',
        dragulaBagName: 'entities-bag',
        scope: $scope
    }).load();




    // var theOriginalEntity;
    // var theOnScreenEntity;

    // function load() {
    //     theOriginalEntity = null;
    //     theOnScreenEntity = null;


    // };


    // $scope.afterLoadData = function() {
    //     $scope.baseEntity = theOnScreenEntity;
    // };

    // load();

    // $scope.$on('departments-bag.drop-model', function(e, el, source) {
    //     var id = Number(el.attr('id'));
    //     var departmentFoundInAvailable = $scope.availableDepartments.find(function(o) {
    //         return o.id == id;
    //     });

    //     var departmentFoundInOccuppied = $scope.baseEntity.Departments.find(function(o) {
    //         return o.id == id;
    //     });


    //     var expanded;
    //     if (departmentFoundInAvailable) {
    //         expanded = departmentFoundInAvailable.expanded;
    //         departmentService.customPost('RemoveFromParent/Dashboard/' + $scope.baseEntity.id, departmentFoundInAvailable).then(function(data) {
    //             departmentFoundInAvailable.expanded = expanded;
    //             alertify.success('Moved successfully.');
    //         });
    //     } else if (departmentFoundInOccuppied) {
    //         expanded = departmentFoundInOccuppied.expanded;
    //         departmentService.addToParent('Dashboard', $scope.baseEntity.id, departmentFoundInOccuppied).then(function(data) {
    //             departmentFoundInOccuppied.expanded = expanded;
    //             departmentFoundInOccuppied.Dashboards = [];
    //             alertify.success('Moved successfully.');
    //         });
    //     } else {
    //         alertify.error('Error. Department not found.');
    //     }
    // });


});
