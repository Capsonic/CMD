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
            $scope.theDashboards = metricService.catalogs.Dashboards.getAll();
        },
        openModal: function(entity) {
            // console.log(entity);
        }
    }).loadAll();


    $scope.loadDashboardsTags = function($query, currentList) {
        if ($scope.theDashboards) {
            return $scope.theDashboards.filter(function(item) {
                return item.Value.toLowerCase().indexOf($query.toLowerCase()) != -1;
            });
        }
    };
    $scope.on_dashboardTag_Added = function(tagAdded, metric) {
        // metric.HiddenForDashboardsTags = [tagAdded]
    };

    $scope.loadMetricYearsTags = function($query, currentList) {
        if ($scope.theMetricYears) {
            return $scope.theMetricYears.filter(function(item) {
                return item.Value.toLowerCase().indexOf($query.toLowerCase()) != -1;
            });
        }
    };
    $scope.on_dashboardTag_Added = function(tagAdded, metric) {
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


    /*//Metric History
    $scope.addMetricHistory = function(historyToAdd, metricSource) {
        var newMetricHistory = {
            FormattedCurrentValue: metricService.getFormattedValue(historyToAdd.CurrentValue, metricSource.FormatKey),
            FormattedGoalValue: metricService.getFormattedValue(historyToAdd.GoalValue, metricSource.FormatKey),
            EqualityValue: metricService.getFormattedEquality(metricSource.ComparatorMethodKey),
            CurrentValue: historyToAdd.CurrentValue,
            GoalValue: historyToAdd.GoalValue,
            ConvertedMetricDate: historyToAdd.ConvertedMetricDate,
            MetricDate: null, // historyToAdd.ConvertedMetricDate ? historyToAdd.ConvertedMetricDate.toJSON() : null,
            Note: historyToAdd.Note,
            MetricKey: metricSource.id
        };
        metricSource.MetricHistorys.push(newMetricHistory);

        $scope.resetMetricHistoryToAdd();

        $('[data-toggle="tooltip"]').tooltip();

    };

    $scope.resetMetricHistoryToAdd = function() {

        $scope.metricToSave.metricHistoryToSave = {
            CurrentValue: null,
            GoalValue: null,
            Note: null,
            ConvertedMetricDate: new Date(),
            mode: 'Add'
        };
        $scope.metricToSave.metricHistoryToSave.ConvertedMetricDate.setSeconds(0);
        $scope.metricToSave.metricHistoryToSave.ConvertedMetricDate.setMilliseconds(0);

    };

    $scope.setMetricHistoryToUpdate = function(oMetricHistory) {
        oMetricHistory.mode = 'Update';
        $scope.metricToSave.metricHistoryToSave = angular.copy(oMetricHistory);
    };

    $scope.updateMetricHistory = function(oMetricHistoryUpdated) {

        $scope.metricToSave.MetricHistorys.forEach(function(currentMetricHistory) {
            if (currentMetricHistory.id == oMetricHistoryUpdated.id) {
                angular.copy(metricService.adaptMetricHistory(oMetricHistoryUpdated, $scope.metricToSave), currentMetricHistory);
                currentMetricHistory.mode = null;
                currentMetricHistory.EF_State = 2;
            }
        });
        $scope.resetMetricHistoryToAdd();


        // metricService.customPost('updateMetricHistory', oMetricHistory).then(function(response) {
        //     $scope.metricToSave.MetricHistorys.forEach(function(metricHistory) {
        //         if (metricHistory.id == response.id) {
        //             angular.copy(metricService.adaptMetricHistory(response, $scope.metricToSave), metricHistory);
        //         }
        //     });
        //     $scope.resetMetricHistoryToAdd();
        // });
    };

    $scope.setDeleteMetricHistory = function(oMetricHistory) {
        oMetricHistory.EF_State = oMetricHistory.EF_State == 3 ? 0 : 3;
    };

    $scope.cancelUpdateMetricHistory = function(oMetric) {
        oMetric.MetricHistorys.forEach(function(oMetricHistory) {
            oMetricHistory.mode = null;
        });
        $scope.resetMetricHistoryToAdd();
    };*/

    $scope.saveAllMetricHistorys = function() {
        $scope.$broadcast('SaveMetricHistory');
    };

    $scope.RemoveMetricYear = function(metricYear) {
        if (metricYear && metricYear.id) {
            metricYearService.remove(metricYear, $scope.itemToSave.MetricYears).then(function(data) {
                $scope.$broadcast('DeleteMetricYear');
            });
        } else {
            alertify.message('Nothing selected');
        }
    }

    $scope.onCloseMetricHistory = function() {
        $scope.itemToSave = null;
    }
    

});
