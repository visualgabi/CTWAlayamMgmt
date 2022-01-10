'use strict';
app.controller('balanceSheetCtrl', ['$scope', 'yearlyActivityService', '$filter', 'orgFiscalYearService',
    function ($scope, yearlyActivityService, $filter, orgFiscalYearService) {

    $scope.balanceSheet = [];
    $scope.accountBalanceSheet = [];
    $scope.balanceSheetQuarter = [];
    $scope.orgFiscalYears = [];

    $('.nav-tabs a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });


    $scope.showPrintForBalanceSheet = false;
    $scope.showPrintForBalanceSheetQuater = false;

    $scope.showAlertForBalanceSheet = false;
    $scope.alertClassForBalanceSheet = "";
    $scope.alertMsgForBalanceSheet = "";

    $scope.showAlertForBalanceSheetQuater = false;
    $scope.alertClassForBalanceSheetQuater = "";
    $scope.alertMsgForBalanceSheetQuater = "";


    $scope.balanceSheetSearch = {
        orgFiscalYearId: ""
    }

    $scope.balanceSheetSearchQuarter = {
        orgFiscalYearId: ""
    }

    $scope.initialize = function () {

        orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.orgFiscalYears = results.data;

        }, function (error) {
            showErroMessage(error)
        });
    }
       
    $scope.availableBalance = function (items) {
        
        if (items.length > 0) {
            var openingBalance = $filter('filter')(items, { orderId: 1 },true)[0].amount;
            var amountReceived = $filter('filter')(items, { orderId: 2 },true)[0].amount;
            var expenseAmount = $filter('filter')(items, { orderId: 3 },true)[0].amount;

            return amountReceived - expenseAmount + openingBalance;
        }
        else
            return 0;
    }

    $scope.availableBalanceQuarter = function (items) {
        
        if (items.length > 0) {
            var amountReceived = $filter('filter')(items, { orderId: 1 },true)[0].amount;
            var expenseAmount = $filter('filter')(items, { orderId: 2 },true)[0].amount;

            return amountReceived - expenseAmount;
        }
        else
            return 0;
    }

    $scope.searchBalanceSheet = function () {

        yearlyActivityService.getBalanceSheet($scope.balanceSheetSearch.orgFiscalYearId).then(function (results) {
            $scope.balanceSheet = results.data;

            if ($scope.balanceSheet.length > 0) {
                $scope.showPrintBalanceSheet = true;               
            }
            else {
                $scope.showPrintBalanceSheet = false;                
            }

        }, function (error) {
            alert(error.data.message);
        });

        yearlyActivityService.getAccountBalanceSheet($scope.balanceSheetSearch.orgFiscalYearId).then(function (results) {
            $scope.accountBalanceSheet = results.data;

        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.searchBalanceSheetQuarter = function () {

        yearlyActivityService.getQuarterBalanceSheet($scope.balanceSheetSearchQuarter.orgFiscalYearId).then(function (results) {
            $scope.balanceSheetQuarter = results.data;

            if ($scope.balanceSheetQuarter.length > 0) {
                $scope.showPrintForBalanceSheetQuater = true;
            }
            else {
                $scope.showPrintForBalanceSheetQuater = false;                
            }

        }, function (error) {
            alert(error.data.message);
        });
        
    }

}])

