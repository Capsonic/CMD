'use strict';

/**
 * @ngdoc service
 * @name mainApp.authService
 * @description
 * # authService
 * Factory in the mainApp.
 */
angular.module('mainApp').factory('authService', function($http, $q, localStorageService, appConfig, userService, $rootScope, $location) {
    var serviceBase = appConfig.API_URL;
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: '',
        role: null
    };

    var _saveRegistration = function(registration) {

        _logOut();

        return $http.post(serviceBase + 'account/register', '=' + JSON.stringify(registration)).then(function(response) {
            return response;
        });

    };

    var _login = function(loginData) {

        var data = 'grant_type=password&username=' + loginData.userName + '&password=' + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }).then(function(response) {

            var backendResponse = response.data;

            var token = backendResponse.access_token;
            var userName = loginData.userName;

            localStorageService.set('authorizationData', {
                token: token,
                userName: userName
            });

            userService.loadAll().then(function(data) {
                $rootScope.currentUser = userService.getByUserName(loginData.userName);

                _authentication.isAuth = true;
                _authentication.userName = $rootScope.currentUser ? $rootScope.currentUser.Value : ''
                _authentication.role = $rootScope.currentUser.Role;

                localStorageService.set('authorizationData', {
                    token: token,
                    userName: userName,
                    role: $rootScope.currentUser.Role
                });

                deferred.resolve(response.data);

            }, function(data) {
                deferred.reject(data);
                _logOut();
            });


        }, function(err, status) {
            _logOut();
            if (!err) {
                err = 'Error. Server unavailable.';
            } else {
                if (err && err.data && err.data.error) {
                    err = err.data.error;
                }
            }
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function() {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = '';
        _authentication.role = null;

        $rootScope.currentUser = null;

        $location.path('/login');

    };

    var _fillAuthData = function() {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            userService.loadAll().then(function(data) {
                $rootScope.currentUser = userService.getByUserName(authData.userName);
                _authentication.userName = $rootScope.currentUser ? $rootScope.currentUser.Value : ''
                _authentication.role = $rootScope.currentUser.Role;
            });
        }

    }


    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logout = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
});
