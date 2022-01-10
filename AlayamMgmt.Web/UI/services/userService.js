'use strict';
app.factory('userService', ['$http', '$q', 'ngAuthSettings', 'authService', function ($http, $q, ngAuthSettings, authService) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    

    var userFactory = {};

    var _getById = function (data) {

        return $http.get(serviceBase + 'api/user/GetById?Id=' + data).then(function (results) {
            return results;
        });

    };

    var _get = function (active) {

        if (authService.authentication.role == 1) {
            return $http.get(serviceBase + 'api/user/Get?active='+active).then(function (results) {
                return results;
            });
        }
        else
        {
            return $http.get(serviceBase + 'api/user/GetByOrgId?orgId=' + authService.authentication.orgId + '&active=' + active).then(function (results) {
                return results;
            });
        }

    };

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/user/GetByOrgId?orgId=' + orgId +'&active='+active).then(function (results) {
            return results;
        });

    };


    var _isUnique = function (data) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/user/IsUnique?id=' + data.id + '&name=' + data.name, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/user/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _edit = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/user/edit', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _forgotPwd = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/user/forgotPassword', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/user/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    userFactory.get = _get;
    userFactory.getByOrgId = _getByOrgId;    
    userFactory.getById = _getById;
    userFactory.forgotPwd = _forgotPwd;
    userFactory.save = _save;
    userFactory.enable = _enable;
    userFactory.edit = _edit;
    userFactory.isUnique = _isUnique;
    

    return userFactory;
}]);