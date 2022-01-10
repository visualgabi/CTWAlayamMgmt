'use strict';
app.factory('taxFilingService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var otaxFilingFactory = {};
    

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/TaxFiling/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/TaxFiling/save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/TaxFiling/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };


    otaxFilingFactory.save = _save;    
    otaxFilingFactory.getByOrgId = _getByOrgId;
    otaxFilingFactory.enable = _enable;

    return otaxFilingFactory;
}]);