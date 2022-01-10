'use strict';
app.factory('organizationService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var ordersServiceFactory = {};

    var _getOrganizations = function (active) {
        return $http.get(serviceBase + 'api/organization/get?active=' + active).then(function (results) {
            return results;
        });       
    };

    var _getOrganization = function () {
        return $http.get(serviceBase + 'api/organization/GetOrganization').then(function (results) {
            return results;
        });
    };

    var _getOrganizationById = function (data) {
        return $http.get(serviceBase + 'api/organization/getbyid?id='+data).then(function (results) {
            return results;
        });
    };

    var _getOrganizationBasicDataById = function (data) {
        return $http.get(serviceBase + 'api/organization/GetBasicDataById?id=' + data, { ignoreLoadingBar: true }).then(function (results) {
            return results;
        });
    };

    var _getBranch = function (data) {
        
        return $http.get(serviceBase + 'api/organization/GetBranch?id=' + data).then(function (results) {
            return results;
        });
    };

    var _getBranchesByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/organization/GetBranchesByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };

    var _saveOrganization = function (data) {       

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/organization/save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enableOrganization = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

       // var data = "id=" + id + "&active=" + active;
        $http.post(serviceBase + 'api/organization/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _isUnique = function (data) {    

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/organization/IsUnique?id=' + data.id + '&name=' + data.name, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    ordersServiceFactory.getOrganizations = _getOrganizations;
    ordersServiceFactory.getOrganization = _getOrganization;    
    ordersServiceFactory.getOrganizationById = _getOrganizationById;
    ordersServiceFactory.saveOrganization = _saveOrganization;
    ordersServiceFactory.isUnique = _isUnique;
    ordersServiceFactory.enableOrganization = _enableOrganization;
    ordersServiceFactory.getBranchesByOrgId = _getBranchesByOrgId;
    ordersServiceFactory.getBranch = _getBranch;
    ordersServiceFactory.getOrganizationBasicDataById = _getOrganizationBasicDataById;

    return ordersServiceFactory;

}]);