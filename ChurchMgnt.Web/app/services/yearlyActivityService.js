'use strict';
app.factory('yearlyActivityService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var yearlyActivityFactory = {};

        //var _getTransactionDetails = function (orgFiscalYearId) {

        //    return $http.get(serviceBase + 'api/TransactionReport/GetTransactionDetailReport?orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
        //    return results;
        //    });
        //};
      
        var _getTransactionMonthly = function (orgFiscalYearId) {

            return $http.get(serviceBase + 'api/TransactionReport/GetTransactionMonthlyReport?orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
                return results;
            });
        };

        var _getTransactionDetailsByDates = function (organizationId, startDate, endDate) {

            var deferred = $q.defer();

            var orgDatesReportRequestModel = {
                organizationId: organizationId,
                startDate: startDate,
                endDate: endDate
            }

            $http.post(serviceBase + 'api/TransactionReport/GetTransactionDetailReportByDates', JSON.stringify(orgDatesReportRequestModel), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
                deferred.resolve(successResponse);
            }, function (failerResponse) {
                deferred.reject(failerResponse);
            });

            return deferred.promise;
         
        };

        var _getTransactionMonthlyByDates = function (organizationId, startDate, endDate) {

            var deferred = $q.defer();

            var orgDatesReportRequestModel = {
                organizationId: organizationId,
                startDate: startDate,
                endDate: endDate
            }

            $http.post(serviceBase + 'api/TransactionReport/GetTransactionMonthlyReportByDates', JSON.stringify(orgDatesReportRequestModel), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
                deferred.resolve(successResponse);
            }, function (failerResponse) {
                deferred.reject(failerResponse);
            });

            return deferred.promise;

        };

        var _getTransactionMonthly = function (orgFiscalYearId) {

            return $http.get(serviceBase + 'api/TransactionReport/GetTransactionMonthlyReport?orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
                return results;
            });
        };

        var _getTransactionQuarter = function (orgFiscalYearId) {

            return $http.get(serviceBase + 'api/TransactionReport/GetTransactionQuarterReport?orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
                return results;
            });
        };


        var _getBalanceSheet = function (orgFiscalYearId) {

            return $http.get(serviceBase + 'api/TransactionReport/GetBalanceSheet?orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
                return results;
            });
        };

        var _getAccountBalanceSheet = function (orgFiscalYearId) {

            return $http.get(serviceBase + 'api/TransactionReport/GetAccountBalanceSheet?orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
                return results;
            });
        };

        var _getQuarterBalanceSheet = function (orgFiscalYearId) {

            return $http.get(serviceBase + 'api/TransactionReport/GetQuarterBalanceSheet?orgFiscalYearId=' + orgFiscalYearId).then(function (results) {
                return results;
            });
        };

           // yearlyActivityFactory.getTransactionDetails = _getTransactionDetails;
            yearlyActivityFactory.getTransactionMonthly = _getTransactionMonthly;
            yearlyActivityFactory.getTransactionQuarter = _getTransactionQuarter;

            yearlyActivityFactory.getBalanceSheet = _getBalanceSheet;
            yearlyActivityFactory.getAccountBalanceSheet = _getAccountBalanceSheet;
            yearlyActivityFactory.getQuarterBalanceSheet = _getQuarterBalanceSheet;

            yearlyActivityFactory.getTransactionDetailsByDates = _getTransactionDetailsByDates;
            yearlyActivityFactory.getTransactionMonthlyByDates = _getTransactionMonthlyByDates;
            

    return yearlyActivityFactory;
}]);
