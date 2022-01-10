'use strict';
app.factory('familyPledgeService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var familyPledgeServiceFactory = {};


    var _getFamilyPledgeByFamilyId = function (familyId, active) {

        return $http.get(serviceBase + 'api/FamilyPledge/GetByFamilyId?familyId=' + familyId + '&active='+active).then(function (results) {
            return results;
        });

    };

    var _getFamilyPledgeByFiscalYearIdId = function (familyId, fiscalYearId) {

        return $http.get(serviceBase + 'api/FamilyPledge/GetByFamilyPledgeByFiscalYearId?familyId=' + familyId + '&fiscalYearId=' + fiscalYearId).then(function (results) {
            return results;
        });

    };

    var _getFamilyPledgeByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/FamilyPledge/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };


    var _saveFamilyPledge = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/FamilyPledge/save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enableFamilyPledge = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/FamilyPledge/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };


    familyPledgeServiceFactory.saveFamilyPledge = _saveFamilyPledge;
    familyPledgeServiceFactory.getFamilyPledgeByFamilyId = _getFamilyPledgeByFamilyId;
    familyPledgeServiceFactory.enableFamilyPledge = _enableFamilyPledge;
    familyPledgeServiceFactory.getFamilyPledgeByOrgId = _getFamilyPledgeByOrgId;
    familyPledgeServiceFactory.getFamilyPledgeByFiscalYearIdId = _getFamilyPledgeByFiscalYearIdId;
    
    

    return familyPledgeServiceFactory;

}]);