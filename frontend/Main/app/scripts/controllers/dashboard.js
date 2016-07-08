'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardCtrl
 * @description
 * # DashboardCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardCtrl', function($scope, dashboardService, $routeParams) {

    $scope.options = {
        gridType: 'fit',
        itemChangeCallback: itemChange,
        margin: 10,
        outerMargin: true,
        draggable: {
            enabled: true,
            stop: eventStop
        },
        resizable: {
            enabled: true,
            stop: eventStop
        },
        swap: true
    };

    $scope.dashboard = [
        { cols: 2, rows: 1, y: 0, x: 0, data: 'hola' },
        { cols: 2, rows: 2, y: 0, x: 2 },
        { cols: 1, rows: 1, y: 0, x: 4 },
        { cols: 1, rows: 1, y: 0, x: 5 },
        { cols: 2, rows: 1, y: 1, x: 0 },
        { cols: 1, rows: 1, y: 1, x: 4 },
        { cols: 1, rows: 2, y: 1, x: 5 },
        { cols: 1, rows: 3, y: 2, x: 0 },
        { cols: 2, rows: 1, y: 2, x: 1 },
        { cols: 1, rows: 1, y: 2, x: 3 },
        { cols: 1, rows: 1, y: 3, x: 4, initCallback: function(item) {} }
    ];

    function itemChange() {}

    function eventStop() {}


    var theOriginalEntity;
    switch (true) {
        case $routeParams.id !== true && $routeParams.id > 0: //Get By id
            dashboardService.loadEntity($routeParams.id).then(function(data) {
                // $activityIndicator.stopAnimating();

                theOriginalEntity = dashboardService.getById($routeParams.id);
                if (!theOriginalEntity) {
                    alertify.alert('Nonexistent record.').set('modal', true).set('closable', false);
                    $scope.openingMode = 'error';
                    return;
                }
                $scope.baseEntity = angular.copy(theOriginalEntity);
                $scope.afterLoadData();
            });
            break;

        default:
            $scope.openingMode = 'error';
            // $activityIndicator.stopAnimating();
            alertify.alert('Verify URL parameters.').set('modal', true).set('closable', false);
            return;
    };

    $scope.afterLoadData = function() {
        console.log($scope.baseEntity)

        if ($scope.baseEntity && $scope.baseEntity.Objectives) {
            $scope.baseEntity.Objectives.forEach(function(objective) {
                objective.cols = objective.InfoGridster.cols;
                objective.rows = objective.InfoGridster.rows;
                objective.x = objective.InfoGridster.x;
                objective.y = objective.InfoGridster.y;
            });
        }

    };
});
