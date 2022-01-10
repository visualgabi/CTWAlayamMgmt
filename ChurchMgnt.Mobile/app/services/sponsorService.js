'use strict';
app.factory('sponsorService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var sponsorFactory = {};

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/Sponsor/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };

    var _getBasicDataById = function (id) {

        return $http.get(serviceBase + 'api/Sponsor/GetBasicDataById?id=' + id ,{ ignoreLoadingBar: true }).then(function (results) {
            return results;
        });

    };

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/Sponsor/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/Sponsor/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _isUnique = function (data) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/Sponsor/IsUnique?id=' + data.id + '&name=' + data.name, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    sponsorFactory.getByOrgId = _getByOrgId;
    sponsorFactory.save = _save;
    sponsorFactory.enable = _enable;
    sponsorFactory.isUnique = _isUnique;
    sponsorFactory.getBasicDataById = _getBasicDataById;
    

    return sponsorFactory;
}]);