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
    'selectize',
    'ngTagsInput',
    'LocalStorageModule',
    angularDragula(angular)
], function($httpProvider) {
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
    $httpProvider.defaults.headers.put['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
}).config(function($routeProvider, $activityIndicatorProvider, $httpProvider, localStorageServiceProvider) {

    localStorageServiceProvider.setPrefix('CMD');

    $routeProvider
    // .when('/', {
    //     templateUrl: 'views/main.html',
    //     controller: 'MainCtrl',
    //     controllerAs: 'main'
    // })
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
        .when('/login', {
            templateUrl: 'views/login.html',
            controller: 'LoginCtrl',
            controllerAs: 'loginn' //cannot have same name as view
        })
        .when('/signup', {
            templateUrl: 'views/signup.html',
            controller: 'SignupCtrl',
            controllerAs: 'signup'
        })
        .otherwise({
            redirectTo: '/dashboards'
        });

    $activityIndicatorProvider.setActivityIndicatorStyle('CircledWhite');
    alertify.set('notifier', 'position', 'top-left');
    alertify.set('notifier', 'delay', 2);

    $httpProvider.interceptors.push('authInterceptorService');

}).run(function(authService, $rootScope, $location) {
    authService.fillAuthData();

    // register listener to watch route changes
    $rootScope.$on('$routeChangeSuccess', function(event, next, current) {
        alertify.closeAll();
        $('.modal').modal('hide');
        $('.modal-backdrop.fade.in').remove();

        var authentication = authService.authentication;
        if (!authentication || !authentication.isAuth) {
            // no logged user, we should be going to #login
            if (next.templateUrl == "views/login.html") {
                // already going to #login, no redirect needed
            } else {
                $location.path('/login');
            }
        } else {
            // var tokenPayload = jwtHelper.decodeToken(jwt);
            // LoginService.update(tokenPayload.data.userId, tokenPayload.data.userName);
        }
    });

    $rootScope.$on('$routeChangeSuccess', function() {
        $rootScope.activePath = $location.path();
        if ($rootScope.activePath == '/dashboard') {
            $('body').css('overflow', 'hidden');
        } else {
            $('body').css('overflow', '');
        }
    });


    $rootScope.logout = function() {
        authService.logout();
    };

});
