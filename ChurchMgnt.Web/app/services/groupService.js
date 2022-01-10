'use strict';
app.factory('groupService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var groupFactory = {};

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/group/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };

    var _getCustomAndBuildInGroupByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/group/GetCustomAndBuildInGroupByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };


    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/group/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/group/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    groupFactory.getByOrgId = _getByOrgId;
    groupFactory.getCustomAndBuildInGroupByOrgId = _getCustomAndBuildInGroupByOrgId;    
    groupFactory.save = _save;
    groupFactory.enable = _enable;

    return groupFactory;
}]);