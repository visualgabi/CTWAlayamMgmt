'use strict';
app.controller('depositRptCtrl', ['$scope', 'lookupService', 'depositService', '$filter', 'sharedService', 'authService', '$window', 'ngAuthSettings', 'userService',
    function ($scope, lookupService, depositService, $filter, sharedService, authService, $window, ngAuthSettings, userService) {
        
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    $scope.showPrint = false;

    $scope.showAlertForEmptyList = false;
    $scope.alertClassForEmptyList = "";
    $scope.alertMsgForEmptyList = "";

    $scope.deposits = [];
    $scope.users = [];
    //$scope.accounts = [];
    $scope.orderBys = [{ id: 1, name: "Account" }, { id: 2, name: "User" }, { id: 3, name: "Amount" }];

    $scope.totalDeposit = 0;

    $scope.depositStartDate = "";
    $scope.depositEndDate = "";

    $scope.depositSearch = {
        organizationId: "",
        depositStartDate: "",
        depositEndDate: "",
        userId: "",
        accountId: "",
        orderById:""
    }

    //$scope.initializeDatePicker = function () {

    //    var d = new Date();

    //    d.setFullYear(d.getFullYear() - 5);      

    //    $('#dtpDepositStartDate').datetimepicker({            
    //        maxDate: new Date(),
    //        minDate: d,
    //        format: 'MM/DD/YYYY'
    //    });

    //    $('#dtpDepositEndDate').datetimepicker({            
    //        format: 'MM/DD/YYYY'
    //    });

    //}

    $scope.todayDate = function () {
        return new Date();
    };

    $scope.rptMinDate = function () {
        var d = new Date();

        d.setFullYear(d.getFullYear() - 5);

        return d;
    };

    $scope.isGreaterThanZero = function () {
        return function (item) {

            return item.total > 0;

        };
    };

    $scope.maxDate = function () {
        var d = new Date($scope.depositSearch.depositStartDate);
        var startDate = new Date($scope.depositSearch.depositStartDate);

        d.setMonth(startDate.getMonth() + 12);

        return d;
    };


    $scope.deposit = {
        id: 0,
        accountId: "",
        userId: "",
        description: "",
        amount: "",
        depositDate: "",
        location: "",
        active: true,
        rowTimeStamp: ""
    }

    $scope.authentication = authService.authentication;


    $scope.print = function () {
        //var orderById = "";
        //if ($scope.depositSearch.orderById == "")
        //    orderById = null;
        //else
        //    orderById = $scope.depositSearch.orderById;

        //var accountName = "All";

        //if ($scope.depositSearch.accountId != null )
        //    accountName = $filter('filter')($scope.accounts, { id: $scope.depositSearch.accountId }, true)[0].name;

        //PDFConveterService.print(serviceBase + "#/depositRptPrt/" + $scope.depositSearch.organizationId + "/" + $scope.depositSearch.userId + "/" + $scope.depositSearch.accountId + "/" + $scope.depositSearch.depositStartDate + "/" + $scope.depositSearch.depositEndDate + "/" + orderById + "/" + accountName).then(function (results) {
        //    var file = new Blob([results.data], { type: 'application/pdf' });
        //    var fileURL = URL.createObjectURL(file);
        //    $window.open(fileURL, '_blank');
        //}, function (error) {
        //    alert(error)
        //});

        //$window.open("#/expenseRptPrt/" + $scope.expenseSearch.organizationId + "/" + $scope.expenseSearch.expenseTypeId + "/" + $scope.expenseSearch.expenseStartDate + "/" + $scope.expenseSearch.expenseEndDate);
        //$location.path("/expenseRptPrt/" + $scope.expenseSearch.organizationId + "/" + $scope.expenseSearch.expenseTypeId+ "/" + $scope.expenseSearch.expenseStartDate + "/" + $scope.expenseSearch.expenseEndDate);
        // $location.path("/expenseRptPrt/" + $scope.expenseSearch.organizationId); //+ "/" + $filter('date')($scope.expenseSearch.expenseStartDate, 'MM/dd/yyyy') + "/" + $filter('date')($scope.expenseSearch.expenseEndDate, 'MM/d/yyyy'));

    };

    $scope.initialize = function () {

       // $scope.initializeDatePicker();

        userService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.users = results.data;
        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getAccountsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.accounts = results.data;
        }, function (error) {
            showErroMessage(error)
        });

    };    

    $scope.search = function (reportForm) {

        if (reportForm.$valid) {
            $scope.depositSearch.organizationId = $scope.authentication.orgId;
            $scope.depositSearch.userId = $scope.depositSearch.userId == "" ? null : $scope.depositSearch.userId;
            $scope.depositSearch.accountId = $scope.depositSearch.accountId == "" ? null : $scope.depositSearch.accountId;

            depositService.report($scope.depositSearch).then(function (results) {
                $scope.deposits = results.data;               

                if ($scope.deposits.length > 0) {
                    $scope.showPrint = true;
                    $scope.alertClassForEmptyList = "";
                    $scope.showAlertForEmptyList = false;
                    $scope.alertMsgForEmptyList = "";

                    $scope.totalDeposit = sharedService.sum($scope.deposits, 'amount');

                    $scope.accountSummary = [];
                    angular.forEach($scope.accounts, function (item) {
                        if ($scope.depositSearch.accountId == null || $scope.depositSearch.accountId == item.id) {
                            var filteredData = $filter('filter')($scope.deposits, { accountId: item.id }, true);
                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.accountSummary.push({ "id": item.id, "name": item.name, "total": total });
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