'use strict';
app.factory('reportDataService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', '$window', function ($http, $q, localStorageService, ngAuthSettings, $window) {
    
    var reportDataFactory = {};
    var _expenseData = [];


    reportDataFactory.expenseData = _expenseData;
    return reportDataFactory;
}]);