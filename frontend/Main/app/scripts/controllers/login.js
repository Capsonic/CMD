'use strict';

/**
 * @ngdoc function
 * @name mainApp.controller:LoginCtrl
 * @description
 * # LoginCtrl
 * Controller of the mainApp
 */
angular.module('mainApp').controller('LoginCtrl', function($scope, $location, authService, $activityIndicator) {
    $activityIndicator.stopAnimating();
    alertify.closeAll();
    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function() {
        $activityIndicator.startAnimating();
        authService.login($scope.loginData).then(function(response) {
                $location.path('/');
            },
            function(err) {
                $scope.message = err.error_description;
            });
    };
});
