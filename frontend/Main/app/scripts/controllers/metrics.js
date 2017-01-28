'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:MetricsCtrl
 * @description
 * # MetricsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('MetricsCtrl', function($scope, listController, metricService, metricYearService) {
    var list = new listController({
        entityName: 'Metric',
        baseService: metricService,
        modalName: 'modal-itemToSave',
        scope: $scope,
        afterLoad: function() {
            $scope.theDashboards = metricYearService.catalogs.Dashboards.getAll();
            $scope.catMetricFormat = metricYearService.catalogs.MetricFormat.getAll();
            $scope.catMetricBasis = metricYearService.catalogs.MetricBasis.getAll();
            $scope.catComparatorMethod = metricYearService.catalogs.ComparatorMethod.getAll();
        },
        openModal: function(entity) {
            // console.log(entity);
        }
    });

    list.loadAll();


    $scope.loadDashboardsTags = function($query, currentList) {
        if ($scope.theDashboards) {
            return $scope.theDashboards.filter(function(item) {
                return item.Value.toLowerCase().indexOf($query.toLowerCase()) != -1;
            });
        }
    };
    $scope.on_dashboardTag_Added = function(tagAdded, metric) {
        metric.editoMode = true;
        // metric.HiddenForDashboardsTags = [tagAdded]
    };

    $scope.openMetricHistory = function(oMetric) {
        angular.element('#modal-MetricHistory').modal('show');
        angular.element('#modal-MetricHistory').off('shown.bs.modal').on('shown.bs.modal', function(e) {
            $scope.$apply(function() {
                $scope.$broadcast('ShowMetricHistory');
            });
        });
    };

    $scope.saveAllMetricHistorys = function() {
        $scope.$broadcast('SaveMetricHistory');
    };

    $scope.RemoveMetricYear = function(metricYear) {
        $scope.$broadcast('DeleteMetricYear', metricYear);
    };

    $scope.$on('RefreshMetric', function(scope, oMetric) {
        // list.loadAll();
        if ($scope.baseList) {
            $scope.baseList.forEach(function(oEntity) {
                if (oEntity.id == oMetric.id) {
                    angular.copy(oMetric, oEntity);
                }
            });
        }
    });

});
