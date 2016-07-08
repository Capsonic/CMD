'use strict';

/**
 * @ngdoc overview
 * @name mainApp
 * @description
 * # mainApp
 *
 * Main module of the application.
 */
angular.module('mainApp', [
    'ngAnimate',
    'ngRoute',
    'ngSanitize',
    'ngTouch',
    'inspiracode.crudFactory',
    'CMD.CRUDServices',
    'angular-gridster2'
]).config(function($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'views/main.html',
            controller: 'MainCtrl',
            controllerAs: 'main'
        })
        .when('/about', {
            templateUrl: 'views/about.html',
            controller: 'AboutCtrl',
            controllerAs: 'about'
        })
        .when('/dashboards', {
          templateUrl: 'views/dashboards.html',
          controller: 'DashboardsCtrl',
          controllerAs: 'dashboards'
        })
        .when('/dashboard', {
          templateUrl: 'views/dashboard.html',
          controller: 'DashboardCtrl',
          controllerAs: 'dashboard'
        })
        .otherwise({
            redirectTo: '/'
        });
});
