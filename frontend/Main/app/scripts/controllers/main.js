'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('MainCtrl', function($scope, sampleService) {


    $scope.vm = {
        options: {
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
            swap:true
        },
        dashboard: [
            { cols: 2, rows: 1, y: 0, x: 0 },
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
        ]
    };



    sampleService.loadAll().then(function(data) {
        $scope.sampleData = sampleService.getAll();
        console.log("hola")
    });

    function itemChange() {
    }

    function eventStop() {
    }
});
