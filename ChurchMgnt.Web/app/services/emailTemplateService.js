'use strict';
app.factory('emailTemplateService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var emailServiceFactory = {};


    var _getById = function (id) {
        return $http.get(serviceBase + 'api/EMailTemplate/GetById?id=' + id).then(function (results) {
            return results;
        });
    };

    var _getByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/EMailTemplate/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/EMailTemplate/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/EMailTemplate/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    emailServiceFactory.getById = _getById;
    emailServiceFactory.getByOrgId = _getByOrgId;
    emailServiceFactory.save = _save;
    emailServiceFactory.enable = _enable;


    return emailServiceFactory;
}]);