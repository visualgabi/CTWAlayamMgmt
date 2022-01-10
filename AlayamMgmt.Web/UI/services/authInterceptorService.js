'use strict';
app.factory('authInterceptorService', ['$q', '$injector', '$location', 'localStorageService', 'ngAuthSettings', '$window', function ($q, $injector, $location, localStorageService,ngAuthSettings, $window) {

    var authInterceptorServiceFactory = {};   

    var inFlightAuthRequest = null;

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = JSON.parse($window.sessionStorage.getItem('authorizationData'));

        if (authData == null) {
            authData = localStorageService.get('authorizationData');
        }

        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    }

    var _responseError = function (rejection) {

        var authService = $injector.get('authService');
        var deferred = $q.defer();

        if (rejection.status === 403) {

            var authData = JSON.parse($window.sessionStorage.getItem('authorizationData'));
            if (!authData) {
                authData = JSON.parse(localStorageService.get('authorizationData'));
            }

            if (inFlightAuthRequest == null) {
                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + authData.clientId;
                inFlightAuthRequest = $injector.get("$http").post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
            }

            inFlightAuthRequest.success(function (response) {

                inFlightAuthRequest = null;

                if (authData.rememberMe) {

                    localStorageService.set('authorizationData',
                     {
                         token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, role: response.role,
                         orgId: response.orgId, clientId: response["as:client_id"], rememberMe: true, orgName: response.orgName, userId: response.userId, currency: response.currency, appVersion: response.appVersion
                     });
                    
                }
                else {
                    $window.sessionStorage.setItem('authorizationData', JSON.stringify({
                        token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, role: response.role,
                        orgId: response.orgId, clientId: response["as:client_id"], rememberMe: false, orgName: response.orgName, userId: response.userId, currency: response.currency, appVersion: response.appVersion
                    }));
                    
                }

                authService.fillAuthData();

                $injector.get("$http")(rejection.config).then(function (resp) {
                    deferred.resolve(resp);
                }, function (resp) {
                    deferred.reject();
                });

            }).error(function (err, status) {
                authService.logOut();
                $location.path('/login');

                //deferred.reject(err);
            });           
        }
        else {
            deferred.reject(rejection);
        }

        return deferred.promise;
    }

   /* var _responseError = function (rejection) {
        var deferred = $q.defer();
        if (rejection.status === 403) {            

            var authService = $injector.get('authService');            
            var authData = JSON.parse($window.sessionStorage.getItem('authorizationData'));

            if (authData == null) {
                authData = localStorageService.get('authorizationData');
            }

            if (authData) {
                if (authData.useRefreshTokens) {

                    authService.refreshToken().then(function (x) {                     
                       
                            $injector.get("$http")(rejection.config).then(function (resp) {
                                deferred.resolve(resp);
                            }, function (resp) {
                                deferred.reject();
                            });                                                    
                        }
                    )
                }
            }            
        }     
        else
        {
            deferred.reject(rejection);
        }
        return deferred.promise;
    }
    */

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;

}]);
