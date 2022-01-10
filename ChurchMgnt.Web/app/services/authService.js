'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', '$window', function ($http, $q, localStorageService, ngAuthSettings, $window) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: true,
        rememberMe: true,
        role: "",
        orgId: null,      
        refreshToken : "",
        orgName: "",
        userId: "",
        currency: "",
        appVersion: "",
        lastTaxFiledFiscalEndDate: "",
        birthdayAvailable: false
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + ngAuthSettings.clientId;
        }

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
            var authData;

            if (loginData.rememberMe) {

                localStorageService.set('authorizationData',
                   {
                       token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, role: response.role,
                       orgId: response.orgId, rememberMe: loginData.rememberMe, orgName: response.orgName, userId: response.userId, currency: response.currency,
                       appVersion: response.appVersion, lastTaxFiledFiscalEndDate: response.lastTaxFiledFiscalEndDate, birthdayAvailable: response.birthdayAvailable
                   });

                authData = localStorageService.get('authorizationData');
            }
            else {
                $window.sessionStorage.setItem('authorizationData', JSON.stringify({
                    token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, role: response.role,
                    orgId: response.orgId, rememberMe: loginData.rememberMe, orgName: response.orgName, userId: response.userId, currency: response.currency, 
                    appVersion: response.appVersion, lastTaxFiledFiscalEndDate: response.lastTaxFiledFiscalEndDate, birthdayAvailable: response.birthdayAvailable
                }));

                authData = JSON.parse($window.sessionStorage.getItem('authorizationData'));
            }


            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.useRefreshTokens = authData.useRefreshTokens;
            _authentication.role = authData.role;
            _authentication.orgId = authData.orgId;
            _authentication.refreshToken = authData.refreshToken;
            _authentication.orgName = authData.orgName;
            _authentication.userId = authData.userId;
            _authentication.currency = authData.currency;
            _authentication.appVersion = authData.appVersion;
            _authentication.lastTaxFiledFiscalEndDate = authData.lastTaxFiledFiscalEndDate;
            _authentication.rememberMe = authData.rememberMe;
            _authentication.birthdayAvailable = authData.birthdayAvailable;
            

            deferred.resolve(response);

        }).catch(function (rejection, status, headers, config) {
           // _logOut();
            deferred.reject(rejection.data);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');
        $window.sessionStorage.removeItem('authorizationData');

        //if (_authentication.rememberMe == true)
        //    localStorageService.remove('authorizationData');
        //else
        //    $window.sessionStorage.removeItem('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.useRefreshTokens = true;
        _authentication.role = "";
        _authentication.orgId = null;
        _authentication.rememberMe = false;
        _authentication.refreshToken = "";
        _authentication.orgName = "";
        _authentication.userId = "";
        _authentication.currency = "";
        _authentication.appVersion = "";
        _authentication.lastTaxFiledFiscalEndDate = "";
        _authentication.birthdayAvailable = false;
    };


    var _fillAuthData = function () {

        var authData = JSON.parse($window.sessionStorage.getItem('authorizationData'));

        if (!authData) {
            authData = localStorageService.get('authorizationData');
        }

        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.useRefreshTokens = true;
            _authentication.role = authData.role;
            _authentication.orgId = authData.orgId;
            _authentication.rememberMe = authData.rememberMe;
            _authentication.refreshToken = authData.refreshToken;
            _authentication.orgName = authData.orgName;
            _authentication.userId = authData.userId;
            _authentication.currency = authData.currency;
            _authentication.appVersion = authData.appVersion;
            _authentication.lastTaxFiledFiscalEndDate = authData.lastTaxFiledFiscalEndDate;
            _authentication.birthdayAvailable = authData.birthdayAvailable;
            
        }

    };



    var _refreshToken = function () {
        var deferred = $q.defer();
        var authData = JSON.parse($window.sessionStorage.getItem('authorizationData'));

        if (!authData) {
            authData = JSON.parse(localStorageService.get('authorizationData'));
        }

        if (authData) {

            if (authData.useRefreshTokens) {

                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;

                //localStorageService.remove('authorizationData');
                $window.sessionStorage.removeItem('authorizationData');

                $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {                   

                    if (authData.rememberMe) {

                        localStorageService.set('authorizationData',
                         {
                             token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, role: response.role,
                             orgId: response.orgId, rememberMe: true, orgName: response.orgName, userId: response.userId, currency: response.currency,
                             appVersion: response.appVersion, lastTaxFiledFiscalEndDate: response.lastTaxFiledFiscalEndDate, birthdayAvailable: response.birthdayAvailable
                         });

                        authData = localStorageService.get('authorizationData');
                    }
                    else {
                        $window.sessionStorage.setItem('authorizationData', JSON.stringify({
                            token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, role: response.role,
                            orgId: response.orgId, rememberMe: false, orgName: response.orgName, userId: response.userId, currency: response.currency,
                            appVersion: response.appVersion, lastTaxFiledFiscalEndDate: response.lastTaxFiledFiscalEndDate, birthdayAvailable: response.birthdayAvailable
                        }));

                        authData = JSON.parse($window.sessionStorage.getItem('authorizationData'));
                    }                    

                    _authentication.isAuth = true;
                    _authentication.userName = authData.userName;
                    _authentication.useRefreshTokens = authData.useRefreshTokens;
                    _authentication.role = authData.role;
                    _authentication.orgId = authData.orgId;
                    _authentication.rememberMe = authData.rememberMe;
                    _authentication.refreshToken = authData.refreshToken;
                    _authentication.orgName = authData.orgName;
                    _authentication.userId = authData.userId;
                    _authentication.currency = authData.currency;
                    _authentication.appVersion = authData.appVersion;
                    _authentication.lastTaxFiledFiscalEndDate = authData.lastTaxFiledFiscalEndDate;
                    _authentication.birthdayAvailable = authData.birthdayAvailable;


                    deferred.resolve(response);

                }).error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });
            }
        }

        return deferred.promise;
    };



    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    authServiceFactory.refreshToken = _refreshToken;


    return authServiceFactory;

}]);