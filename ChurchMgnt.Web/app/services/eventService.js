'use strict';
app.factory('eventService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var eventFactory = {};

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/event/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };

    var _getRecentEventsByOrgId = function (orgId) {
        return $http.get(serviceBase + 'api/Event/getRecentEventsByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/event/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _report = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/event/report', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/event/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    eventFactory.getByOrgId = _getByOrgId;
    eventFactory.save = _save;
    eventFactory.enable = _enable;
    eventFactory.report = _report;
    eventFactory.getRecentEventsByOrgId = _getRecentEventsByOrgId;
    

    return eventFactory;
}]);