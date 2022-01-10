'use strict';
app.factory('roleService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var roleFactory = {};

    var _get = function (active) {

        return $http.get(serviceBase + 'api/role/Get?active=' + active).then(function (results) {
            return results;
        });

    };

    roleFactory.get = _get;

    return roleFactory;
}]);