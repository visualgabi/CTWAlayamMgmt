'use strict';
app.factory('lookupService', ['$http', 'ngAuthSettings', '$q', function ($http, ngAuthSettings, $q) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var lookupServiceFactory = {};

    var _getCountries = function (active) {
        return $http.get(serviceBase + 'api/Country/get?active='+active).then(function (results) {
            return results;
        });
    };

    var _getStates = function (active) {
        return $http.get(serviceBase + 'api/State/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getLanguages = function (active) {
        return $http.get(serviceBase + 'api/Language/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getDenominations = function (active) {
        return $http.get(serviceBase + 'api/Denomination/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getEthnicOrigins = function (active) {
        return $http.get(serviceBase + 'api/EthnicOrigin/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getAccountTypes = function (active) {
        return $http.get(serviceBase + 'api/AccountType/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getMembershipStatuses = function (active) {
        return $http.get(serviceBase + 'api/MembershipStatus/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getCurrencies = function (active) {
        return $http.get(serviceBase + 'api/Currency/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getRelationships = function (active) {
        return $http.get(serviceBase + 'api/Relationship/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getFiscalYearStartAndEndMonths = function (active) {
        return $http.get(serviceBase + 'api/FiscalYearStartAndEndMonth/get?active=' + active).then(function (results) {
            return results;
        });
    };
        
    var _getFiscalYears = function (active) {
        return $http.get(serviceBase + 'api/FiscalYear/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getFundTypes = function (active) {
        return $http.get(serviceBase + 'api/FundType/Get/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getFundTypesByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/FundType/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };

    var _getOfferingTypes = function () {
        return $http.get(serviceBase + 'api/OfferingType/Get').then(function (results) {
            return results;
        });
    };

    var _getOfferingTypesByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/OfferingType/GetByOrgId?orgId=' + orgId + '&active='+active).then(function (results) {
            return results;
        });
    };

    var _getEventTypes = function (active) {
        return $http.get(serviceBase + 'api/EventType/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getEventTypesByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/EventType/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };    

    var _saveOfferingType = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/OfferingType/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enableOfferingType = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/OfferingType/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _isUniqueOfferingType = function (data) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/OfferingType/IsUnique?id=' + data.id + '&orgId=' + data.orgId + '&name=' + data.name, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };


    var _getPaymentTypes = function (active) {
        return $http.get(serviceBase + 'api/PaymentType/get?active='+active).then(function (results) {
            return results;
        });
    };

    var _getExpenseTypes = function (active) {
        return $http.get(serviceBase + 'api/ExpenseType/get?active=' + active).then(function (results) {
            return results;
        });
    };

    var _getExpenseTypesByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/ExpenseType/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };

    var _getSubExpenseTypes = function (active) {
        return $http.get(serviceBase + 'api/SubExpenseType/get?active='+active).then(function (results) {
            return results;
        });
    };

    var _getSubExpenseTypesByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/SubExpenseType/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };


    var _saveExpenseType = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/ExpenseType/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _saveSubExpenseType = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/SubExpenseType/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enableExpenseType = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/ExpenseType/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enableSubExpenseType = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/SubExpenseType/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _isUniqueExpenseType = function (data) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/ExpenseType/IsUnique?id=' + data.id + '&orgId=' + data.orgId + '&name=' + data.name, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };


    var _saveEventType = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/EventType/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enableEventType = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/EventType/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _isUniqueEventType = function (data) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/EventType/IsUnique?id=' + data.id + '&orgId=' + data.orgId + '&name=' + data.name, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _saveFundType = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/FundType/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enableFundType = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/FundType/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _isUniqueFundType = function (data) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/FundType/IsUnique?id=' + data.id + '&orgId=' + data.orgId + '&name=' + data.name, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _getBanksByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/Bank/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };

    var _getBankById = function (id) {
        return $http.get(serviceBase + 'api/Bank/GetById?id=' + id ).then(function (results) {
            return results;
        });
    };

    var _saveBank = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/Bank/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;   
    };

    var _enableBank = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/Bank/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _isUniqueBank = function (data) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/Bank/IsUnique?id=' + data.id + '&orgId=' + data.orgId + '&name=' + data.name, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _getAccountsByOrgId = function (orgId, active) {
        return $http.get(serviceBase + 'api/BankAccount/GetByOrgId?orgId=' + orgId + '&active=' + active).then(function (results) {
            return results;
        });
    };

    var _saveAccount = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/BankAccount/Save', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _enableAccount = function (id, active) {

        var deferred = $q.defer();

        var data = {
            id: id,
            active: active
        };

        $http.post(serviceBase + 'api/BankAccount/enable', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    var _isUniqueAccount = function (data) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/BankAccount/IsUnique?id=' + data.id + '&orgId=' + data.orgId + '&name=' + data.name, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    lookupServiceFactory.getCountries = _getCountries;
    lookupServiceFactory.getStates = _getStates;
    lookupServiceFactory.getLanguages = _getLanguages;
    lookupServiceFactory.getDenominations = _getDenominations;
    lookupServiceFactory.getEthnicOrigins = _getEthnicOrigins;
    lookupServiceFactory.getMembershipStatuses = _getMembershipStatuses;
    lookupServiceFactory.getCurrencies = _getCurrencies;
    lookupServiceFactory.getFiscalYears = _getFiscalYears;
    lookupServiceFactory.getFundTypes = _getFundTypes;
    lookupServiceFactory.getFundTypesByOrgId = _getFundTypesByOrgId;
    lookupServiceFactory.getEventTypes = _getEventTypes;
    lookupServiceFactory.getEventTypesByOrgId = _getEventTypesByOrgId;
    lookupServiceFactory.getPaymentTypes = _getPaymentTypes;
    lookupServiceFactory.getOfferingTypes = _getOfferingTypes;
    lookupServiceFactory.getOfferingTypesByOrgId = _getOfferingTypesByOrgId;
    lookupServiceFactory.saveOfferingType = _saveOfferingType;
    lookupServiceFactory.saveExpenseType = _saveExpenseType;
    lookupServiceFactory.saveSubExpenseType = _saveSubExpenseType;
    lookupServiceFactory.saveEventType = _saveEventType;
    lookupServiceFactory.saveFundType = _saveFundType;
    lookupServiceFactory.enableOfferingType = _enableOfferingType;
    lookupServiceFactory.enableExpenseType = _enableExpenseType;
    lookupServiceFactory.enableSubExpenseType = _enableSubExpenseType;
    lookupServiceFactory.enableEventType = _enableEventType;
    lookupServiceFactory.enableFundType = _enableFundType;
    lookupServiceFactory.getExpenseTypes = _getExpenseTypes;
    lookupServiceFactory.getExpenseTypesByOrgId = _getExpenseTypesByOrgId;
    lookupServiceFactory.getSubExpenseTypes = _getSubExpenseTypes;
    lookupServiceFactory.getSubExpenseTypesByOrgId = _getSubExpenseTypesByOrgId;
    lookupServiceFactory.isUniqueOfferingType = _isUniqueOfferingType;
    lookupServiceFactory.isUniqueExpenseType = _isUniqueExpenseType;
    lookupServiceFactory.isUniqueEventType = _isUniqueEventType;
    lookupServiceFactory.isUniqueFundType = _isUniqueFundType;
    
    
    lookupServiceFactory.getAccountTypes = _getAccountTypes;

    lookupServiceFactory.getBankById = _getBankById;
    lookupServiceFactory.saveBank = _saveBank;
    lookupServiceFactory.enableBank = _enableBank;
    lookupServiceFactory.isUniqueBank = _isUniqueBank;
    lookupServiceFactory.getBanksByOrgId = _getBanksByOrgId;

    lookupServiceFactory.saveAccount = _saveAccount;
    lookupServiceFactory.enableAccount = _enableAccount;
    lookupServiceFactory.isUniqueAccount = _isUniqueAccount;
    lookupServiceFactory.getAccountsByOrgId = _getAccountsByOrgId;

    lookupServiceFactory.getRelationships = _getRelationships;
    lookupServiceFactory.getFiscalYearStartAndEndMonths = _getFiscalYearStartAndEndMonths;

    return lookupServiceFactory;
}]);
