'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardCtrl
 * @description
 * # DashboardCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardCtrl', function($scope, dashboardService, $routeParams, objectiveService, filterFilter, $q) {

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

    function itemChange(gridsterItem, scope) {
        scope.item.InfoGridster.cols = scope.item.cols;
        scope.item.InfoGridster.rows = scope.item.rows;
        scope.item.InfoGridster.x = scope.item.x;
        scope.item.InfoGridster.y = scope.item.y;
        if (scope.item.EF_State != 1) {
            scope.item.EF_State = 2;
        }
    };

    function eventStop() {};


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

        if ($scope.baseEntity && $scope.baseEntity.Objectives) {
            $scope.baseEntity.Objectives.forEach(function(objective) {
                objective.cols = objective.InfoGridster.cols;
                objective.rows = objective.InfoGridster.rows;
                objective.x = objective.InfoGridster.x;
                objective.y = objective.InfoGridster.y;
                objective.initCallback = onItemInit;
            });
        }
    };

    $scope.addItem = function() {
        objectiveService.createEntity().then(function(data) {
            var item = data;
            item.areGridsterPropertiesMissing = true;
            item.initCallback = onItemInit;
            $scope.baseEntity.Objectives.push(item);
            console.log($scope.baseEntity)
        });
    };

    function onItemInit(a, b, c) {
        for (var i = 0; i < $scope.baseEntity.Objectives.length; i++) {
            var current = $scope.baseEntity.Objectives[i];
            if (current.areGridsterPropertiesMissing) {
                current.InfoGridster.cols = a.cols;
                current.InfoGridster.rows = a.rows;
                current.InfoGridster.x = a.x;
                current.InfoGridster.y = a.y;

                current.cols = current.InfoGridster.cols;
                current.rows = current.InfoGridster.rows;
                current.x = current.InfoGridster.x;
                current.y = current.InfoGridster.y;

                current.areGridsterPropertiesMissing = false;
                current.initCallback = null;
                a.item = current;
            }
        }
    };

    $scope.modePresentation = function() {
        alert("Full scren")
    };

    function itemsToSave() {
        if ($scope.baseEntity && $scope.baseEntity.Objectives) {
            return filterFilter($scope.baseEntity.Objectives, genericItemsToBeSaved);
        }
    };

    function genericItemsToBeSaved(item) {
        return item.EF_State > 0;
    };

    $scope.saveAll = function() {
        var arrItems = itemsToSave();

        var arrPromiseConstructors = [];

        arrItems.forEach(function(item) {
            var promiseConstructor = function() {
                return $scope.saveItem(item);
            }
            arrPromiseConstructors.push(promiseConstructor);
        });

        $q.serial(arrPromiseConstructors).then(function() {
            alertify.success('Dashboard saved successfully.');
        });
    };

    $scope.saveBaseEntity = function() {
        if (!$scope.baseEntity) {
            return false;
        }
        return dashboardService.save($scope.baseEntity);
    };

    $scope.saveItem = function(item) {
        return objectiveService.addToParent('Dashboard', $scope.baseEntity.id, item);
    };
});
