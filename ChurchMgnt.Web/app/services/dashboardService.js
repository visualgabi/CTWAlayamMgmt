'use strict';
app.factory('dashboardService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var dashboardFactory = {};

    var _getMemberVisitorCountGraph = function (orgId, active) {

        return $http.get(serviceBase + 'api/Dashboard/GetMemberVisitorCountGraph?orgId=' + orgId).then(function (results) {
            return results;
        });

    };

    var _getBudgetGraph = function (orgId, active) {

        return $http.get(serviceBase + 'api/Dashboard/getBudgetGraph?orgId=' + orgId).then(function (results) {
            return results;
        });

    };

    var _getExpenseGraph = function (orgId, active) {

        return $http.get(serviceBase + 'api/Dashboard/getExpenseGraph?orgId=' + orgId).then(function (results) {
            return results;
        });

    };

    var _getFundTypeOfferingGraph = function (orgId, active) {

        return $http.get(serviceBase + 'api/Dashboard/getFundTypeOfferingGraph?orgId=' + orgId).then(function (results) {
            return results;
        });

    };  

    dashboardFactory.getMemberVisitorCountGraph = _getMemberVisitorCountGraph;
    dashboardFactory.getBudgetGraph = _getBudgetGraph;
    dashboardFactory.getExpenseGraph = _getExpenseGraph;
    dashboardFactory.getFundTypeOfferingGraph = _getFundTypeOfferingGraph;
    

    return dashboardFactory;
}]);