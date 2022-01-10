'use strict';
app.factory('profileService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var profileFactory = {};

    var _changePassword = function (changePasswordData) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/User/ChangePassword', JSON.stringify(changePasswordData), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _resetPassword = function (resetPasswordData) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/User/ResetPassword', JSON.stringify(resetPasswordData), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    }
    
    profileFactory.resetPassword = _resetPassword;

    profileFactory.changePassword = _changePassword;

    return profileFactory;
}]);