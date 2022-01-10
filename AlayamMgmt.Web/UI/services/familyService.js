'use strict';
app.factory('familyService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var familyServiceFactory = {};

    var _getFamilies = function (active) {
        return $http.get(serviceBase + 'api/family/get/?active='+ active).then(function (results) {
            return results;
        });
    }; 

    var _getRecentMembersByOrgId = function (orgId) {
        return $http.get(serviceBase + 'api/family/getRecentMembersByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };

    var _getRecentVisitorsByOrgId = function (orgId) {
        return $http.get(serviceBase + 'api/family/getRecentVisitorsByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };
   

    var _getFamilyById = function (data) {
        return $http.get(serviceBase + 'api/family/getbyid?id=' + data).then(function (results) {
            return results;
        });
    };   
   
    var _getFamilyBasicDataById = function (data) {
        return $http.get(serviceBase + 'api/family/GetBasicDataById?id=' + data, { ignoreLoadingBar: true }).then(function (results) {
            return results;
        });
    };

    var _getFamiliesByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/family/GetFamiliesByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };

    var _saveFamily = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/family/save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _changeStatus = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        // var data = "id=" + id + "&active=" + active;
        $http.post(serviceBase + 'api/family/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };
  

    familyServiceFactory.getFamilies = _getFamilies;
    familyServiceFactory.getFamilyById = _getFamilyById;
    familyServiceFactory.saveFamily = _saveFamily;
    familyServiceFactory.changeStatus = _changeStatus;
    familyServiceFactory.getRecentMembersByOrgId = _getRecentMembersByOrgId;
    familyServiceFactory.getRecentVisitorsByOrgId = _getRecentVisitorsByOrgId;
    familyServiceFactory.getFamiliesByOrgId = _getFamiliesByOrgId;
    familyServiceFactory.getFamilyBasicDataById = _getFamilyBasicDataById;    
    
    return familyServiceFactory;
}]);