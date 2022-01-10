'use strict';
app.factory('expenseService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var expenseFactory = {};

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/Expense/GetByOrgId?orgId=' + orgId + '&active='+active).then(function (results) {
            return results;
        });

    };

    var _getById = function (id) {

        return $http.get(serviceBase + 'api/Expense/GetById?id=' + id).then(function (results) {
            return results;
        });

    };

    var _getRecentExpensesByOrgId = function (orgId) {
        return $http.get(serviceBase + 'api/Expense/getRecentExpensesByOrgId?orgId=' + orgId).then(function (results) {
            return results;
        });
    };

    var _report = function (data) {
        
        var deferred = $q.defer();

        $http.post(serviceBase + 'api/Expense/report', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;     
    };

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/Expense/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
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

        $http.post(serviceBase + 'api/Expense/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    expenseFactory.getByOrgId = _getByOrgId;
    expenseFactory.save = _save;
    expenseFactory.enable = _enable;
    expenseFactory.report = _report;
    expenseFactory.getRecentExpensesByOrgId = _getRecentExpensesByOrgId;
    expenseFactory.getById = _getById;

    return expenseFactory;
}]);