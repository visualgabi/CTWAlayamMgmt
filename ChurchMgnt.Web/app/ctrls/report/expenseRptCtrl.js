'use strict';
app.controller('expenseRptCtrl', ['$scope', 'lookupService', 'expenseService', '$filter', 'sharedService', 'authService', '$window', 'downloadService', 'ngAuthSettings', 'reportDataService', '$location',
function ($scope, lookupService, expenseService, $filter, sharedService, authService, $window, downloadService, ngAuthSettings, reportDataService, location) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    $scope.showPrint = false;
    $scope.showExcel = false;

    $scope.showAlertForEmptyList = false;
    $scope.alertClassForEmptyList = "";
    $scope.alertMsgForEmptyList = "";

    $scope.expenses = [];    
    $scope.paymentTypes = [];    
    $scope.orderBys = [{ id: 1, name: "Expense Type" }, { id: 2, name: "Sub Expense Type" }, { id: 3, name: "paymentType" }, { id: 4, name: "Expense Date" }, { id: 5, name: "Amount" }];
    $scope.totalExpense = 0;

    $scope.expenseTypesWithAmount = [];
    $scope.paymentTypesWithAmount = [];
    $scope.accountsWithAmount = [];   

    $scope.expenseStartDate = "";
    $scope.expenseEndDate = "";

    $scope.expenseSummary = { summary: "", amount: 0 }

    $scope.downloadSummaryByPayment = [];
    $scope.expenseSummaryByPayment = { summary: "", amount: 0 };

    $scope.downloadSummaryByAccounts = [];
    $scope.expenseSummaryByAccount = { summary: "", amount: 0 };

    $scope.downloadSummaryByExpenseTypes = [];
    $scope.expenseSummaryByExpenseType = { summary: "", amount: 0 };
    $scope.DownloadExpenses = [];    

    $scope.downloadPdf = function () {        

        $scope.filter = { StartDate: $scope.expenseSearch.expenseStartDate, EndDate: $scope.expenseSearch.expenseEndDate, expenseType: "", account: "", orderBy: "" };

        if ($scope.expenseSearch.expenseTypeId == null || $scope.expenseSearch.expenseTypeId == "")
            $scope.filter.expenseType = "All";
        else
            $scope.filter.expenseType = $filter('filter')($scope.expenseTypes, { id: $scope.expenseSearch.expenseTypeId }, true)[0].name;
        
        if ($scope.expenseSearch.accountId == "" || $scope.expenseSearch.accountId == null)
            $scope.filter.account = "All";
        else
            $scope.filter.account = $filter('filter')($scope.accounts, { id: $scope.expenseSearch.accountId }, true)[0].name;

        if ($scope.expenseSearch.paymentTypeId == "" || $scope.expenseSearch.paymentTypeId == null)
            $scope.filter.payment = "All";
        else
            $scope.filter.payment = $filter('filter')($scope.payments, { id: $scope.expenseSearch.paymentId }, true)[0].name;

        if ($scope.expenseSearch.orderById == "" || $scope.expenseSearch.orderById === null)
            $scope.filter.orderBy = "Created date";
        else
            $scope.filter.orderBy = $filter('filter')($scope.orderBys, { id: $scope.expenseSearch.orderById }, true)[0].name;


        $scope.summaryByPayment = $filter('filter')($scope.paymentTypesWithAmount, function (obj) {
            return obj.total > 0;
        });

        $scope.summaryByAccount = $filter('filter')($scope.accountsWithAmount, function (obj) {
            return obj.total > 0;
        });

        $scope.summaryByExpenseType = $filter('filter')($scope.expenseTypesWithAmount, function (obj) {
            return obj.total > 0;
        });

        $scope.printObj = {
            currency: $scope.authentication.currency,
            totalExpese: $scope.totalExpense,
            SummaryByPaymentTypes: $scope.summaryByPayment,
            SummaryByAccounts: $scope.summaryByAccount,
            summaryByExpenseTypes: $scope.summaryByExpenseType,
            expenses: $scope.expenses,
            churchMgntURL: location.absUrl().split('#')[0],
            filter: $scope.filter,
            orgName: $scope.authentication.orgName
        }

        downloadService.printExpenseReport($scope.printObj).then(function (results) {
            var file = new Blob([results.data], { type: 'application/pdf' });
            var fileURL = URL.createObjectURL(file);
            $window.open(fileURL, '_blank');
        }, function (error) {
            alert(error)
        });
        

    };


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

        d.setMonth(startDate.getMonth() + 12);

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
                    $scope.showExcel = true;
                    $scope.alertClassForEmptyList = "";
                    $scope.showAlertForEmptyList = false;
                    $scope.alertMsgForEmptyList = "";

                    $scope.totalExpense = sharedService.sum($scope.expenses, 'amount');

                    $scope.paymentTypesWithAmount = [];
                    angular.forEach($scope.paymentTypes, function (item) {
                        var filteredData = $filter('filter')($scope.expenses, { paymentTypeId: item.id }, true);
                        var total = sharedService.sum(filteredData, 'amount');
                        $scope.paymentTypesWithAmount.push({ "id": item.id, "name": item.name, "total": total });
                    });

                    $scope.expenseTypesWithAmount = [];
                    angular.forEach($scope.expenseTypes, function (item) {
                        if ($scope.expenseSearch.expenseTypeId == null || $scope.expenseSearch.expenseTypeId == item.id) {
                            var filteredData = $filter('filter')($scope.expenses, { expenseTypeId: item.id }, true);
                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.expenseTypesWithAmount.push({ "id": item.id, "name": item.name, "total": total });
                        }
                    });

                    $scope.accountsWithAmount = [];
                    angular.forEach($scope.accounts, function (item) {
                        if ($scope.expenseSearch.accountId == null || $scope.expenseSearch.accountId == item.id) {
                            var filteredData = $filter('filter')($scope.expenses, { accountId: item.id },true);
                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.accountsWithAmount.push({ "id": item.id, "name": item.name, "total": total });
                        }
                    });

                }
                else {
                    $scope.showPrint = false;
                    $scope.showExcel = false;
                    $scope.alertMsgForEmptyList = sharedService.getShortErrorMsgForEmptyList();
                    $scope.alertClassForEmptyList = "alert-danger";
                    $scope.showAlertForEmptyList = true;
                }

            }, function (error) {
                alert(error.data.message);
            });
        }
    }

    $scope.downloadExcel = function () {
        $scope.expenseExcel = [];

        angular.forEach($scope.expenses, function (item) {
            $scope.expenseExcel.push({ "ExpenseType": item.expenseType, "SubExpenseType": item.subExpenseType, "PaymentType": item.paymentType, "Amount": item.amount, "ExpenseDate": item.expenseDate, "Account": item.account });
        });

        sharedService.jsonToCSVConvertor($scope.expenseExcel, "Expense Report", true);
    }

}]);