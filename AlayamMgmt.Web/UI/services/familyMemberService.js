'use strict';
app.factory('familyMemberService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var familyMemberServiceFactory = {};

    var _getFamilies = function (active) {
        return $http.get(serviceBase + 'api/familyMember/get?active='+ active).then(function (results) {
            return results;
        });
    };    

    var _getfamilyMemberById = function (data) {
        return $http.get(serviceBase + 'api/familyMember/getbyid?id=' + data).then(function (results) {
            return results;
        });
    };

    var _getfamilyMembersByFamilyId = function (familyId, active) {
        return $http.get(serviceBase + 'api/familyMember/GetByFamilyId?id=' + familyId + '&active='+active).then(function (results) {
            return results;
        });
    };

    var _getByGreaterThanAge = function (data) {
        return $http.get(serviceBase + 'api/familyMember/GetByGreaterThanAge?age=' + data).then(function (results) {
            return results;
        });
    };

    var _getTaxPayerByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/familyMember/GetTaxPayerByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };


    var _savefamilyMember = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/familyMember/save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enablefamilyMember = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        // var data = "id=" + id + "&active=" + active;
        $http.post(serviceBase + 'api/familyMember/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    familyMemberServiceFactory.getFamilies = _getFamilies;
    familyMemberServiceFactory.getfamilyMemberById = _getfamilyMemberById;
    familyMemberServiceFactory.savefamilyMember = _savefamilyMember;
    familyMemberServiceFactory.enablefamilyMember = _enablefamilyMember;
    familyMemberServiceFactory.enablefamilyMember = _enablefamilyMember;
    familyMemberServiceFactory.getfamilyMembersByFamilyId = _getfamilyMembersByFamilyId;
    familyMemberServiceFactory.getByGreaterThanAge = _getByGreaterThanAge;
    familyMemberServiceFactory.getTaxPayerByOrgId = _getTaxPayerByOrgId;
    
    
    return familyMemberServiceFactory;
}]);