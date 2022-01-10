'use strict';
app.controller('transactionRptController', ['$scope', 'lookupService', 'yearlyActivityService', 'sharedService', 'authService', 'orgFiscalYearService',
    function ($scope, lookupService, yearlyActivityService, sharedService, authService, orgFiscalYearService) {

    $scope.showPrintDaily = false;
    $scope.showPrintMonthly = false;
    $scope.showPrintQuarter = false;

    $scope.showAlertForEmptyListDaily = false;
    $scope.alertClassForEmptyListDaily = "";
    $scope.alertMsgForEmptyListDaily = "";

    $scope.showAlertForEmptyListMonthly = false;
    $scope.alertClassForEmptyListMonthly = "";
    $scope.alertMsgForEmptyListMonthly = "";

    $scope.showAlertForEmptyListQuarter = false;
    $scope.alertClassForEmptyListQuarter = "";
    $scope.alertMsgForEmptyListQuarter = "";

    $scope.authentication = authService.authentication;

    $scope.transactionDetailsDaily = [];
    $scope.transactionDetailsMonthly = [];
    $scope.transactionDetailsQuarter = [];
    $scope.orgFiscalYears = [];    

    $scope.transactionSearchDaily = {        
        organizationId: "",
        startDate: "",
        endDate: ""
    }

    $scope.transactionSearchMonthly = {        
        organizationId: "",
        startDate: "",
        endDate: ""
    }

    $scope.transactionSearchQuarter = {
        orgFiscalYearId: ""
    }
    
    $('.nav-tabs a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });

    $scope.todayDate = function () {
        return new Date();
    };

    $scope.rptMinDate = function () {
        var d = new Date();

        d.setFullYear(d.getFullYear() - 5);

        return d;
    };

    $scope.maxDate = function () {
        var d = new Date($scope.transactionSearchDaily.startDate);
        var startDate = new Date($scope.transactionSearchDaily.startDate);

        d.setMonth(startDate.getMonth() + 6);

        return d;
    };

    $scope.maxDateMonthly = function () {
        var d = new Date($scope.transactionSearchMonthly.startDate);
        var startDate = new Date($scope.transactionSearchMonthly.startDate);

        d.setMonth(startDate.getMonth() + 12);

        return d;
    };

    $scope.initialize = function () {       

        orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.orgFiscalYears = results.data;

        }, function (error) {
            showErroMessage(error)
        });
    }

    $scope.getClass = function (type) {        
        if (type == 'Deposit')
            return "white-deposit";
        else if (type == 'Offering')
            return "white-offering";
        else if (type == 'Expense')
            return "white-expense";

    }

    $scope.searchDaily = function (reportFormDaily) {
        if (reportFormDaily.$valid) {
            $scope.transactionSearchDaily.organizationId = $scope.authentication.orgId;

            //yearlyActivityService.getTransactionDetails($scope.transactionSearchDaily.orgFiscalYearId).then(function (results) {            
            yearlyActivityService.getTransactionDetailsByDates($scope.transactionSearchDaily.organizationId, $scope.transactionSearchDaily.startDate, $scope.transactionSearchDaily.endDate).then(function (results) {
                $scope.transactionDetailsDaily = results.data;

                if ($scope.transactionDetailsDaily.length > 0) {
                    $scope.showPrintDaily = true;
                    $scope.alertClassForEmptyListDaily = "";
                    $scope.showAlertForEmptyListDaily = false;
                    $scope.alertMsgForEmptyListDaily = "";
                }
                else {
                    $scope.showPrintDaily = false;
                    $scope.alertMsgForEmptyListDaily = sharedService.getShortErrorMsgForEmptyList();
                    $scope.alertClassForEmptyListDaily = "alert-danger";
                    $scope.showAlertForEmptyListDaily = true;
                }

            }, function (error) {
                alert(error.data.message);
            });
        }
    }

    $scope.searchMonthly = function (reportFormMonthly) {

        if (reportFormMonthly.$valid) {
            $scope.transactionSearchMonthly.organizationId = $scope.authentication.orgId;

            yearlyActivityService.getTransactionMonthlyByDates($scope.transactionSearchMonthly.organizationId, $scope.transactionSearchMonthly.startDate, $scope.transactionSearchMonthly.endDate).then(function (results) {
                $scope.transactionDetailsMonthly = results.data;

                if ($scope.transactionDetailsMonthly.length > 0) {
                    $scope.showPrintMonthly = true;
                    $scope.alertClassForEmptyListMonthly = "";
                    $scope.showAlertForEmptyListMonthly = false;
                    $scope.alertMsgForEmptyListMonthly = "";
                }
                else {
                    $scope.showPrintMonthly = false;
                    $scope.alertMsgForEmptyListMonthly = sharedService.getShortErrorMsgForEmptyList();
                    $scope.alertClassForEmptyListMonthly = "alert-danger";
                    $scope.showAlertForEmptyListMonthly = true;
                }

            }, function (error) {
                alert(error.data.message);
            });
        }
    }

    $scope.searchQuarter = function () {

        yearlyActivityService.getTransactionQuarter($scope.transactionSearchQuarter.orgFiscalYearId).then(function (results) {
            $scope.transactionDetailsQuarter = results.data;

            if ($scope.transactionDetailsQuarter.length > 0) {
                $scope.showPrintQuarter = true;
                $scope.alertClassForEmptyListQuarter = "";
                $scope.showAlertForEmptyListQuarter = false;
                $scope.alertMsgForEmptyListQuarter = "";
            }
            else {
                $scope.showPrintQuarter = false;
                $scope.alertMsgForEmptyListQuarter = sharedService.getShortErrorMsgForEmptyList();
                $scope.alertClassForEmptyListQuarter = "alert-danger";
                $scope.showAlertForEmptyListQuarter = true;
            }

        }, function (error) {
            alert(error.data.message);
        });
    }

}]);