'use strict';
app.controller('expenseRptController', ['$scope', 'lookupService', 'expenseService', '$filter', 'sharedService', 'authService', '$window', 'PDFConveterService', 'ngAuthSettings', 'reportDataService',
    function ($scope, lookupService, expenseService, $filter, sharedService, authService, $window, PDFConveterService, ngAuthSettings, reportDataService) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    $scope.showPrint = false;

    $scope.showAlertForEmptyList = false;
    $scope.alertClassForEmptyList = "";
    $scope.alertMsgForEmptyList = "";

    $scope.expenses = [];
    //$scope.expenseTypes = [];
    $scope.paymentTypes = [];
    //$scope.accounts = [];
    $scope.orderBys = [{ id: 1, name: "Expense Type" }, { id: 2, name: "Sub Expense Type" }, { id: 3, name: "paymentType" }, { id: 4, name: "Expense Date" }, { id: 5, name: "Amount" }];

    $scope.totalExpense = 0;


    $scope.expenseTypesWithAmount = [];
    $scope.paymentTypesWithAmount = [];
    $scope.accountsWithAmount = [];

    //$scope.subExpenseTypes = [];
    //$scope.paymentTypes = [];

    $scope.expenseStartDate = "";
    $scope.expenseEndDate = "";

    $scope.todayDate = function () {
        return new Date();
    };
   
    $scope.rptMinDate = function () {
        var d = new Date();       

        d.setFullYear(d.getFullYear() - 5);

        return d;
    };

    $scope.maxDate = function () {
        var d = new Date($scope.expenseSearch.expenseStartDate);
        var startDate = new Date($scope.expenseSearch.expenseStartDate);

        d.setMonth(startDate.getMonth() + 6);

        return d;
    };

    $scope.isGreaterThanZero = function () {
        return function (item) {

            return item.total > 0;

        };
    };
    

    $scope.expenseSearch = {
        organizationId: "",
        expenseStartDate: "",
        expenseEndDate: "",
        expenseTypeId: "",
        accountId: "",
        orderById: ""
        
    }

    
    $scope.expense = {
        id: 0,
        organizationId: "",
        expenseTypeId: "",
        subExpenseTypeId: "",
        paymentTypeId: "",
        amount: "",
        expenseDate: "",
        transacitonNumber: "",
        notes: "",
        active: true,
        rowTimeStamp: ""
    }

    $scope.authentication = authService.authentication;

    $scope.print = function () {        

        reportDataService.expenseData = $scope.expenses;

        var orderById = "";
        if ($scope.expenseSearch.orderById == "")
            orderById = null;
        else
            orderById = $scope.expenseSearch.orderById;
       

        PDFConveterService.print(serviceBase + "#/expenseRptPrt/" + $scope.expenseSearch.organizationId + "/" + $scope.expenseSearch.expenseTypeId + "/" + $scope.expenseSearch.expenseStartDate + "/" + $scope.expenseSearch.expenseEndDate + "/" + $scope.expenseSearch.accountId + "/" + orderById).then(function (results) {
            var file = new Blob([results.data], { type: 'application/pdf' });
            var fileURL = URL.createObjectURL(file);
            $window.open(fileURL, '_blank');
        }, function (error) {
            alert(error)
        });
       
        
    };

    $scope.initialize = function () {        

        lookupService.getExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.expenseTypes = results.data;
        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getSubExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.subExpenseTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getAccountsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.accounts = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getPaymentTypes(true).then(function (results) {
            $scope.paymentTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });
        
    };   

    $scope.search = function (reportForm) {

        if (reportForm.$valid) {
            $scope.expenseSearch.organizationId = $scope.authentication.orgId;
            $scope.expenseSearch.expenseTypeId = $scope.expenseSearch.expenseTypeId == "" ? null : $scope.expenseSearch.expenseTypeId;
            $scope.expenseSearch.accountId = $scope.expenseSearch.accountId == "" ? null : $scope.expenseSearch.accountId;

            expenseService.report($scope.expenseSearch).then(function (results) {
                $scope.expenses = results.data;

                if ($scope.expenses.length > 0) {
                    $scope.showPrint = true;
                    $scope.alertClassForEmptyList = "";
                    $scope.showAlertForEmptyList = false;
                    $scope.alertMsgForEmptyList = "";

                    $scope.totalExpense = sharedService.sum($scope.expenses, 'amount');

                    $scope.paymentTypesWithAmount = [];
                    angular.forEach($scope.paymentTypes, function (item) {
                        var filteredData = $filter('filter')($scope.expenses, { paymentTypeId: item.id });
                        var total = sharedService.sum(filteredData, 'amount');
                        $scope.paymentTypesWithAmount.push({ "id": item.id, "name": item.name, "total": total });
                    });

                    $scope.expenseTypesWithAmount = [];
                    angular.forEach($scope.expenseTypes, function (item) {
                        if ($scope.expenseSearch.expenseTypeId == null || $scope.expenseSearch.expenseTypeId == item.id) {
                            var filteredData = $filter('filter')($scope.expenses, { expenseTypeId: item.id });
                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.expenseTypesWithAmount.push({ "id": item.id, "name": item.name, "total": total });
                        }
                    });

                    $scope.accountsWithAmount = [];
                    angular.forEach($scope.accounts, function (item) {
                        if ($scope.expenseSearch.accountId == null || $scope.expenseSearch.accountId == item.id) {
                            var filteredData = $filter('filter')($scope.expenses, { accountId: item.id });
                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.accountsWithAmount.push({ "id": item.id, "name": item.name, "total": total });
                        }
                    });

                }
                else {
                    $scope.showPrint = false;
                    $scope.alertMsgForEmptyList = sharedService.getShortErrorMsgForEmptyList();
                    $scope.alertClassForEmptyList = "alert-danger";
                    $scope.showAlertForEmptyList = true;
                }

            }, function (error) {
                alert(error.data.message);
            });
        }
    }

}]);