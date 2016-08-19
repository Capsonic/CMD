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
    'angular-gridster2',
    'smart-table',
    'ngActivityIndicator',
    'reusable',
    'ngTagsInput',
    angularDragula(angular)
], function($httpProvider) {
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
    $httpProvider.defaults.headers.put['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
}).config(function($routeProvider) {
    $routeProvider
    // .when('/', {
    //     templateUrl: 'views/main.html',
    //     controller: 'MainCtrl',
    //     controllerAs: 'main'
    // })
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
        .when('/administration', {
            templateUrl: 'views/administration.html',
            controller: 'AdministrationCtrl',
            controllerAs: 'administration'
        })
        .when('/dashboard-departments', {
            templateUrl: 'views/dashboard-departments.html',
            controller: 'DashboardDepartmentsCtrl',
            controllerAs: 'dashboardDepartments'
        })
        .when('/departments', {
            templateUrl: 'views/departments.html',
            controller: 'DepartmentsCtrl',
            controllerAs: 'departments'
        })
        .when('/department-dashboards', {
            templateUrl: 'views/department-dashboards.html',
            controller: 'DepartmentDashboardsCtrl',
            controllerAs: 'departmentDashboards'
        })
        .when('/metrics', {
            templateUrl: 'views/metrics.html',
            controller: 'MetricsCtrl',
            controllerAs: 'metrics'
        })
        .when('/department-metrics', {
            templateUrl: 'views/department-metrics.html',
            controller: 'DepartmentMetricsCtrl',
            controllerAs: 'departmentMetrics'
        })
        .when('/department-initiatives', {
            templateUrl: 'views/department-initiatives.html',
            controller: 'DepartmentInitiativesCtrl',
            controllerAs: 'departmentInitiatives'
        })
        .when('/initiatives', {
            templateUrl: 'views/initiatives.html',
            controller: 'InitiativesCtrl',
            controllerAs: 'initiatives'
        })
        .otherwise({
            redirectTo: '/dashboards'
        });
}).run(function($rootScope, $location) {
    $rootScope.$on('$routeChangeSuccess', function() {
        alertify.closeAll();
        $('.modal').modal('hide');
        $('.modal-backdrop.fade.in').remove();
    });
});
