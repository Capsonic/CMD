'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:DashboardsCtrl
 * @description
 * # DashboardsCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('DashboardsCtrl', function($scope, dashboardService, listController) {

    var list = new listController({
        entityName: 'Dashboard',
        baseService: dashboardService,
        modalName: 'modal-itemToSave',
        scope: $scope,
        afterLoad: function() {
            $scope.theUsers = dashboardService.catalogs.Users.getAll();
        }
    }).load();


    $scope.loadUsersTags = function($query, currentList) {
        return $scope.theUsers.filter(function(item) {
            return item.Value.toLowerCase().indexOf($query.toLowerCase()) != -1;
        });
    };
    $scope.on_userTag_Added = function(tagAdded, metric) {
        // metric.HiddenForUsersTags = [tagAdded]
    };


});
