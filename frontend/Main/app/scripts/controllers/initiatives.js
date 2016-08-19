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
        scope: $scope,
        afterLoad: function() {
            $scope.theDashboards = initiativeService.catalogs.Dashboards.getAll();
        }
    }).load();
    
    $scope.loadDashboardsTags = function($query, currentList) {
        return $scope.theDashboards.filter(function(item) {
            return item.Value.toLowerCase().indexOf($query.toLowerCase()) != -1;
        });
    };
    $scope.on_dashboardTag_Added = function(tagAdded, metric) {
        // metric.HiddenForDashboardsTags = [tagAdded]
    };
});
