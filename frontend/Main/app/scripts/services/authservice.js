'use strict';

/**
 * @ngdoc service
 * @name mainApp.authService
 * @description
 * # authService
 * Factory in the mainApp.
 */
angular.module('mainApp').factory('authService', function($http, $q, localStorageService, appConfig, userService, $rootScope, $location) {

    var settings = {
        authority: 'http://localhost:61521',
        client_id: 'cmd',
        popup_redirect_uri: 'http://localhost:9000/auth_redirect.html',
        silent_redirect_uri: 'http://localhost:9000/silent-renew.html',
        post_logout_redirect_uri: "http://localhost:9000",

        response_type: 'id_token token',
        scope: 'openid profile email api',

        accessTokenExpiringNotificationTime: 4,
        automaticSilentRenew: true,

        filterProtocolClaims: true
    };

    var _authentication = {
        isAuth: false,
        userName: ""
    };

    var manager = new Oidc.UserManager(settings);

    manager.events.addUserLoaded(function(authResponse) {

        localStorageService.set('authorizationData', {
            token: authResponse.access_token,
            userName: authResponse.profile.given_name
        });

        _authentication.isAuth = true;
        _authentication.userName = authResponse.profile.given_name;

        userService.loadAll().then(function(users) {
            $rootScope.currentUser = userService.getById(authResponse.profile.sub);
        });

    });

    manager.events.addSilentRenewError(function(error) {
        console.error('error while renewing the access token', error);
    });

    manager.events.addUserSignedOut(function() {
        console.log("User signed out.");

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";

        $location.path('/login');

    });

    var _login = function() {
        return manager
            .signinPopup()
            .catch(function(error) {
                console.error('error while logging in through the popup', error);
                _logout();
            });
    };

    var _logout = function() {
        return manager
            .signoutRedirect()
            .catch(function(error) {
                console.error('error while signing out user', error);
            });
    };

    var _fillAuthData = function() {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            userService.loadAll().then(function(data) {
                $rootScope.currentUser = userService.getByUserName(authData.userName);
            });
        }

    }

    var authServiceAPI = {};

    authServiceAPI.login = _login;
    authServiceAPI.logout = _logout;
    authServiceAPI.fillAuthData = _fillAuthData;
    authServiceAPI.authentication = _authentication;

    return authServiceAPI;

});
