'use strict';
app.factory('openingBalanceService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var openingBalanceFactory = {};

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/OpeningBalance/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };

    var _report = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/OpeningBalance/report', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/OpeningBalance/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/OpeningBalance/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    openingBalanceFactory.getByOrgId = _getByOrgId;
    openingBalanceFactory.save = _save;
    openingBalanceFactory.enable = _enable;
    openingBalanceFactory.report = _report;

    return openingBalanceFactory;
}]);