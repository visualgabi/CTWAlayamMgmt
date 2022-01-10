'use strict';
app.factory('downloadService', ['$http', '$q', 'ngAuthSettings', '$window', function ($http, $q, ngAuthSettings, $window) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var downloadFactory = {};

    var _printContributionLeter = function (data) {      

        var deferred = $q.defer();        
        $http.post(serviceBase + 'api/Download/ContributionLetter', JSON.stringify(data), { 'responseType': 'arraybuffer' }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _printExpenseReport = function (data) {

        var deferred = $q.defer();
        $http.post(serviceBase + 'api/Download/ExpenseReport', JSON.stringify(data), { 'responseType': 'arraybuffer' }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _printOfferingReport = function (data) {

        var deferred = $q.defer();
        $http.post(serviceBase + 'api/Download/OfferingReport', JSON.stringify(data), { 'responseType': 'arraybuffer' }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _printFamilyPledgeReport = function (data) {

        var deferred = $q.defer();
        $http.post(serviceBase + 'api/Download/FamilyPledgeReport', JSON.stringify(data), { 'responseType': 'arraybuffer' }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };
    
    downloadFactory.printFamilyPledgeReport = _printFamilyPledgeReport;
    downloadFactory.printOfferingReport = _printOfferingReport;
    downloadFactory.printContributionLeter = _printContributionLeter;
    downloadFactory.printExpenseReport = _printExpenseReport;

    return downloadFactory;

}]);