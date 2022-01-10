'use strict';
app.factory('offeringService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var offeringFactory = {};

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/offering/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };

    var _getByFamilyId = function (familyId, startDate, endDate) {

        return $http.get(serviceBase + 'api/offering/GetByFamilyId?familyId=' + familyId + '&startDate=' + startDate + '&endDate=' + endDate).then(function (results) {
            return results;
        });

    };

    var _getFiscalOfferingByFamilyId = function (familyId, orgFiscalYearId) {

        return $http.get(serviceBase + 'api/offering/FiscalOfferingByFamilyId?familyId=' + familyId + '&orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
            return results;
        });

    };

    var _getFiscalOfferingByFamilyMemberId = function (familyMemberId, orgFiscalYearId) {

        return $http.get(serviceBase + 'api/offering/FiscalOfferingByfamilyMemberId?familyMemberId=' + familyMemberId + '&orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
            return results;
        });

    };

    var _getFiscalOfferingBySponsorId = function (sponsorId, orgFiscalYearId) {

        return $http.get(serviceBase + 'api/offering/FiscalOfferingByfamilyMemberId?sponsorId=' + sponsorId + '&orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
            return results;
        });

    };

    var _getFiscalOfferingByFamilyId = function (familyId, orgFiscalYearId) {

        return $http.get(serviceBase + 'api/offering/FiscalOfferingByFamilyId?familyId=' + familyId + '&orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
            return results;
        });

    };


    var _getRecentOfferingsByOrgId = function (orgId) {
        return $http.get(serviceBase + 'api/Offering/getRecentOfferingsByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/offering/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _report = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/offering/report', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _familyOfferingReport = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/offering/FamilyOfferingReport', JSON.stringify(data),  { ignoreLoadingBar: true },{ headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _familyMemberOfferingReport = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/offering/FamilyMemberOfferingReport', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _sponsorOfferingReport = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/offering/SponsorOfferingReport', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/offering/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    offeringFactory.getByOrgId = _getByOrgId;
    offeringFactory.save = _save;
    offeringFactory.enable = _enable;
    offeringFactory.report = _report;
    offeringFactory.getByFamilyId = _getByFamilyId;
    offeringFactory.familyOfferingReport = _familyOfferingReport;
    offeringFactory.familyMemberOfferingReport = _familyMemberOfferingReport;
    offeringFactory.sponsorOfferingReport = _sponsorOfferingReport;
    offeringFactory.getRecentOfferingsByOrgId = _getRecentOfferingsByOrgId;
    offeringFactory.getFiscalOfferingByFamilyId = _getFiscalOfferingByFamilyId;
    offeringFactory.getFiscalOfferingByFamilyMemberId = _getFiscalOfferingByFamilyMemberId;
    offeringFactory.getFiscalOfferingBySponsorId = _getFiscalOfferingBySponsorId;
    
    return offeringFactory;
}]);