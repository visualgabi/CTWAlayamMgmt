'use strict';
app.factory('fiscalYearBudgetService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var fiscalYearBudgetFactory = {};


    var _getFiscalYearBudgetByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/OrgFiscalYearBudget/GetOrgFiscalYearBudgetsByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };

    var _saveFiscalYearBudget = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/OrgFiscalYearBudget/save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enableFiscalYearBudget = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/OrgFiscalYearBudget/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };


    fiscalYearBudgetFactory.saveFiscalYearBudget = _saveFiscalYearBudget;
    fiscalYearBudgetFactory.getFiscalYearBudgetByOrgId = _getFiscalYearBudgetByOrgId;
    fiscalYearBudgetFactory.enableFiscalYearBudget = _enableFiscalYearBudget;

    return fiscalYearBudgetFactory;
}]);