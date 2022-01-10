'use strict';
app.factory('orgFiscalYearService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var orgFiscalYearFactory = {};

    var _getOrgFiscalYears = function (active) {
        return $http.get(serviceBase + 'api/OrgFiscalYear/get?active' + active).then(function (results) {
            return results;
        });
    };

    var _getOrgFiscalYearsByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/OrgFiscalYear/GetOrgFiscalYearByOrgId?orgId=' + orgId + '&active='+ active).then(function (results) {
            return results;
        });
      
    };

    var _saveOrgFiscalYears = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/OrgFiscalYear/save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };


    var _enableOrgFiscalYear = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/OrgFiscalYear/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

       
    orgFiscalYearFactory.saveOrgFiscalYears = _saveOrgFiscalYears;
    orgFiscalYearFactory.getOrgFiscalYears = _getOrgFiscalYears;
    orgFiscalYearFactory.getOrgFiscalYearsByOrgId = _getOrgFiscalYearsByOrgId;
    orgFiscalYearFactory.enableOrgFiscalYear = _enableOrgFiscalYear;

    return orgFiscalYearFactory;
}]);