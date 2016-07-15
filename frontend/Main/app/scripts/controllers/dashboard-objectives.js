'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardObjectivesCtrl
 * @description
 * # DashboardObjectivesCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardObjectivesCtrl', function($scope, relationatorController, dashboardService, objectiveService) {


    var relationator = new relationatorController({
        baseService: dashboardService,
        entityName: 'Dashboard',
        baseRelatedService: objectiveService,
        relatedEntityName: 'Objective',
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

    // $scope.$on('objectives-bag.drop-model', function(e, el, source) {
    //     var id = Number(el.attr('id'));
    //     var objectiveFoundInAvailable = $scope.availableObjectives.find(function(o) {
    //         return o.id == id;
    //     });

    //     var objectiveFoundInOccuppied = $scope.baseEntity.Objectives.find(function(o) {
    //         return o.id == id;
    //     });


    //     var expanded;
    //     if (objectiveFoundInAvailable) {
    //         expanded = objectiveFoundInAvailable.expanded;
    //         objectiveService.customPost('RemoveFromParent/Dashboard/' + $scope.baseEntity.id, objectiveFoundInAvailable).then(function(data) {
    //             objectiveFoundInAvailable.expanded = expanded;
    //             alertify.success('Moved successfully.');
    //         });
    //     } else if (objectiveFoundInOccuppied) {
    //         expanded = objectiveFoundInOccuppied.expanded;
    //         objectiveService.addToParent('Dashboard', $scope.baseEntity.id, objectiveFoundInOccuppied).then(function(data) {
    //             objectiveFoundInOccuppied.expanded = expanded;
    //             objectiveFoundInOccuppied.Dashboards = [];
    //             alertify.success('Moved successfully.');
    //         });
    //     } else {
    //         alertify.error('Error. Objective not found.');
    //     }
    // });


});
