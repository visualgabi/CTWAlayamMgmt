'use strict';
app.factory('smtpSettingService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var smtpSettingFactory = {};

    var _get = function (active) {

        return $http.get(serviceBase + 'api/smtpSetting/Get?active=' + active).then(function (results) {
            return results;
        });

    };

    
    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/smtpSetting/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };   

    var _enable = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/smtpSetting/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    smtpSettingFactory.get = _get;
    smtpSettingFactory.save = _save;
    smtpSettingFactory.enable = _enable;

    return smtpSettingFactory;
}]);