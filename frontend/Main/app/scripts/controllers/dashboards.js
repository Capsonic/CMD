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
            $scope.baseList = dashboardService.getAll()
        },
    }).load();

});
