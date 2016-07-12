'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardObjectivesCtrl
 * @description
 * # DashboardObjectivesCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardObjectivesCtrl', function($scope, $routeParams, dashboardService, objectiveService) {
    var theOriginalEntity;
    var theOnScreenEntity;

    function load() {
        theOriginalEntity = null;
        theOnScreenEntity = null;

        switch (true) {
            case $routeParams.id !== true && $routeParams.id > 0: //Get By id
                dashboardService.loadEntity($routeParams.id).then(function(data) {
                    // $activityIndicator.stopAnimating();


                    //Loading Dashboards with Objectives
                    theOriginalEntity = dashboardService.getById($routeParams.id);
                    if (!theOriginalEntity) {
                        alertify.alert('Nonexistent record.').set('modal', true).set('closable', false);
                        $scope.openingMode = 'error';
                        return;
                    }
                    theOnScreenEntity = angular.copy(theOriginalEntity);


                    //Loading Available Objectives
                    objectiveService.customGet('GetAvailableForEntity/Dashboard/' + $routeParams.id).then(function(data) {
                        $scope.availableObjectives = data;
                        $scope.afterLoadData();
                    });


                });
                break;

            default:
                $scope.openingMode = 'error';
                // $activityIndicator.stopAnimating();
                alertify.alert('Verify URL parameters.').set('modal', true).set('closable', false);
                return;
        };
    };


    $scope.afterLoadData = function() {
        $scope.baseEntity = theOnScreenEntity;
    };

    load();



    $scope.$on('objectives-bag.drop-model', function(e, el, source) {
        var id = Number(el.attr('id'));
        var objectiveFoundInAvailable = $scope.availableObjectives.find(function(o) {
            return o.id == id;
        });

        var objectiveFoundInOccuppied = $scope.baseEntity.Objectives.find(function(o) {
            return o.id == id;
        });



        if (objectiveFoundInAvailable) {
            objectiveService.customPost('RemoveFromParent/Dashboard/' + $scope.baseEntity.id, objectiveFoundInAvailable).then(function(data) {
                alertify.success('Moved successfully.');
            });
        } else if (objectiveFoundInOccuppied) {
            var expanded = objectiveFoundInOccuppied.expanded;
            objectiveService.addToParent('Dashboard', $scope.baseEntity.id, objectiveFoundInOccuppied).then(function(data) {
                data.expanded = expanded;
                alertify.success('Moved successfully.');
            });
        } else {
            alertify.error('Error. Objective not found.');
        }
    });


});
