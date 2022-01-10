'use strict';
app.factory('reportedIssueService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var eportedIssueFactory = {};

    var _save = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/ReportedIssue/save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };


    

    eportedIssueFactory.save = _save;
    

    return eportedIssueFactory;
}]);