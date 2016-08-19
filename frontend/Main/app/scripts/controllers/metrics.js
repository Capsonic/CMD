'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:MetricsCtrl
 * @description
 * # MetricsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('MetricsCtrl', function($scope, listController, metricService) {
    var list = new listController({
        entityName: 'Metric',
        baseService: metricService,
        modalName: 'modal-itemToSave',
        scope: $scope,
        afterLoad: function() {
            $scope.theDashboards = metricService.catalogs.Dashboards.getAll();
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
