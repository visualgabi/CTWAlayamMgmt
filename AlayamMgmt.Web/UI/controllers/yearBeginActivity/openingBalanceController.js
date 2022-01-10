'use strict';
app.controller('openingBalanceController', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService','openingBalanceService','orgFiscalYearService',
    function ($scope, $filter, sharedService, authService, ngTableParams, lookupService, openingBalanceService, orgFiscalYearService) {


        $scope.openingBalances = [];
       // $scope.orgFiscalYears = [];
       // $scope.accounts = [];

        $scope.openingBalance = {
            id: 0,            
            orgFiscalYearId: "",
            accountId: "",
            amount:"",
            active: true,
            rowTimeStamp: ""
        }


        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";

        $scope.action = "";

        $scope.authentication = authService.authentication;        
        $scope.amountTitle = 'Amount in ' + $scope.authentication.currency;

        $scope.setAction = function (value) {
            $scope.action = value;

            if ($scope.action == 'add') {
                $scope.clear();
            }
        }

        $scope.isUnique = function (submitedData) {

            var results = $filter('filter')($scope.openingBalances, { accountId: submitedData.accountId, orgFiscalYearId: submitedData.orgFiscalYearId }, true);

            if ((submitedData.id == 0 && results.length > 0) || (submitedData.id > 0 && results.length > 1))
                return false;
            else
                return true;
        }

        $scope.formLoad = function () {
            openingBalanceService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.openingBalances = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.openingBalances.length, // length of data
                    getData: function ($defer, params) {

                        var filteredData = $filter('filter')($scope.openingBalances, $scope.filterText);

                        var orderedData = params.sorting() ?
                       $filter('orderBy')(filteredData, params.orderBy()) :
                       filteredData;

                        params.total(orderedData.length);
                        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                    }
                })

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.$watch("filterText", function () {
            $scope.tableParams.reload();
            $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
        });

        $scope.initialize = function (id) {

            $scope.clear();

            if (id != 0) {
                var results = $filter('filter')($scope.openingBalances, { id: id });
                $scope.openingBalance = results[0];
            }

            orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.orgFiscalYears = results.data;

            }, function (error) {
                showErroMessage(error)
            });

            lookupService.getAccountsByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.accounts = results.data;

            }, function (error) {
                showErroMessage(error)
            });

        }

        $scope.availableList = function (id) {
            return function (item) {

                if (id == 0) {
                    return item.active == true;
                }
                else {
                    return true;
                }

            };
        };

        $scope.clearAlert = function () {
            $scope.showAlert = false;
            $scope.alertClass = "";
            $scope.alertMsg = "";

            $scope.showAlertForDelete = false;
            $scope.alertClassForDelete = "";
            $scope.alertMsgForDelete = "";
        }

        $scope.clear = function () {
            $scope.openingBalance = {
                id: 0,
                orgFiscalYearId: "",
                accountId: "",
                amount: "",
                active: true,
                rowTimeStamp: ""
            }
        }

        $scope.load = function () {

            openingBalanceService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.openingBalances = results.data;
                $scope.tableParams.reload();
            });

        }

        $scope.loadAfterEdit = function (id) {

            openingBalanceService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.openingBalances = results.data;
                $scope.initialize(id);
            });
        }


        $scope.save = function (openingBalanceForm) {

            if (openingBalanceForm.$valid) {

                var unique = $scope.isUnique($scope.openingBalance);

               if (unique == true) {
                   openingBalanceService.save($scope.openingBalance).then(function (results) {
                       $scope.alertClass = "alert-success";
                       $scope.alertMsg = sharedService.getShortSaveSuccessMsg();

                       if ($scope.openingBalance.id == 0) {
                           openingBalanceForm.$setPristine();
                           $scope.clear();
                       }
                       else
                           $scope.loadAfterEdit($scope.openingBalance.id);

                       $scope.alertClass = "alert-success";
                       $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                       $scope.showAlert = true;

                   }, function (error) {
                       $scope.alertClass = "alert-danger";
                       $scope.showAlert = true;

                       if (error.data === 3) {
                           $scope.loadAfterEdit($scope.openingBalance.id);
                           $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                       }
                       else if (error.data === 2) {
                           $scope.loadAfterEdit($scope.openingBalance.id);
                           $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                       }
                       else
                           $scope.alertMsg = sharedService.getShortErrorMsg();
                   });
               }
               else
               {
                   $scope.alertMsg = sharedService.getShortUniqueErrorMsg();
                   $scope.alertClass = "alert-danger";
                   $scope.showAlert = true;
               }
                
            }
        };



        $scope.enable = function (id, status) {

            var orgOfferingTypes = $filter('filter')($scope.openingBalances, { id: id });

            lookupService.enableSubExpenseType(orgOfferingTypes[0].id, status).then(function (results) {

                orgOfferingTypes[0].active = status;
                orgOfferingTypes[0].rowTimeStamp = results.data;

                $scope.alertClassForDelete = "alert-success";
                if (status == true)
                    $scope.alertMsgForDelete = sharedService.getShortEnableSuccessMsg();
                else
                    $scope.alertMsgForDelete = sharedService.getShortDisableSuccessMsg();

                $scope.showAlertForDelete = true;

                $scope.tableParams.reload();

            }, function (error) {

                $scope.alertClassForDelete = "alert-danger";
                $scope.alertMsgForDelete = sharedService.getShortErrorMsg();

                $scope.showAlertForDelete = true;

                $scope.tableParams.reload();
            });


        };

    }]);