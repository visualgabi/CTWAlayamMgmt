'use strict';
app.factory('depositService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var depositFactory = {};

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/deposit/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };

    var _getRecentDepositsByOrgId = function (orgId) {
        return $http.get(serviceBase + 'api/deposit/getRecentDepositsByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/deposit/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _report = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/deposit/report', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/deposit/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    depositFactory.getByOrgId = _getByOrgId;
    depositFactory.save = _save;
    depositFactory.enable = _enable;
    depositFactory.report = _report;
    depositFactory.getRecentDepositsByOrgId = _getRecentDepositsByOrgId;
    

    return depositFactory;
}]);