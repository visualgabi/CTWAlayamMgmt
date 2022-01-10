'use strict';
app.factory('dashboardService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var dashboardFactory = {};

    var _getByOrgId = function (orgId, active) {

        return $http.get(serviceBase + 'api/Dashboard/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });

    };    

    dashboardFactory.getByOrgId = _getByOrgId;
    

    return dashboardFactory;
}]);