'use strict';
app.controller('fiscalYearBudgetCtrl', ['$scope', 'lookupService', 'fiscalYearBudgetService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'orgFiscalYearService',
    function ($scope, lookupService, fiscalYearBudgetService, $filter, sharedService, authService, ngTableParams, orgFiscalYearService) {
        
    $scope.fiscalYearBudgets = [];
   // $scope.orgFiscalYears = [];
   // $scope.fundTypes = [];
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";
    
    $scope.action = "";

    $scope.fiscalYearBudget = {
        id: 0,
        //organizationId: "",
        orgFiscalYearId: "",
        fundTypeId: "",
        amount: "",
        active: true,
        rowTimeStamp: ""
    }

    $scope.authentication = authService.authentication;
    $scope.amountTitle = 'Amount in ' + $scope.authentication.currency;

    $scope.authentication = authService.authentication;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }

    $scope.formLoad = function () {
        fiscalYearBudgetService.getFiscalYearBudgetByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.fiscalYearBudgets = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.fiscalYearBudgets.length, // length of data
                getData: function ($defer, params) {

                    //  var fitleredData = $filter('filter')(data, filterText);

                    $scope.filteredData = $filter('filter')($scope.fiscalYearBudgets, $scope.filterText);

                    var orderedData = params.sorting() ?
                   $filter('orderBy')($scope.filteredData, params.orderBy()) :
                   $scope.filteredData;

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
            var results = $filter('filter')($scope.fiscalYearBudgets, { id: id },true);
            $scope.fiscalYearBudget = results[0];
        }

        orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, true).then(function (results) {
            $scope.orgFiscalYears = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getFundTypesByOrgId($scope.authentication.orgId, true).then(function (results) {
            $scope.fundTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    };

    $scope.filterYears = function (year) {
        if ($scope.fiscalYearBudget.id == 0) {
            for (var i = 0; i < $scope.orgFiscalYears.length; i++) {
                if ($scope.orgFiscalYears[i].fiscalYear == year.name && $scope.orgFiscalYears[i].active == true)
                    return false;
            }
        }
        return true;
    };

    $scope.loadFiscalYearBudgets = function () {
        fiscalYearBudgetService.getFiscalYearBudgetByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.fiscalYearBudgets = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {

        fiscalYearBudgetService.getFiscalYearBudgetByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.fiscalYearBudgets = results.data;
            $scope.initialize(id);
        });
    }


    $scope.clearAlert = function () {
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";
    }

    $scope.clear = function () {
        $scope.fiscalYearBudget = {
            id: 0,
            //organizationId: "",
            orgFiscalYearId: "",
            fundTypeId: "",
            amount: "",
            active: true,
            rowTimeStamp: ""
        }
    }

    $scope.saveFiscalYearBudget = function (fiscalYearBudgetForm) {

        var unique = true;
        unique = $scope.isUnique($scope.fiscalYearBudget);

        if (unique == true) {

            if (fiscalYearBudgetForm.$valid) {
                //$scope.orgFiscalYear.organizationId = $scope.authentication.orgId;

                fiscalYearBudgetService.saveFiscalYearBudget($scope.fiscalYearBudget).then(function (results) {

                    if ($scope.fiscalYearBudget.id == 0)
                    {
                        fiscalYearBudgetForm.$setPristine();
                        $scope.clear();
                    }
                    else
                        $scope.loadAfterEdit($scope.fiscalYearBudget.id);

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                    $scope.showAlert = true;

                }, function (error) {
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;

                    if (error.data === 3) {
                        $scope.loadAfterEdit($scope.fiscalYearBudget.id);
                        $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                    }
                    else if (error.data === 2) {
                        $scope.loadAfterEdit($scope.fiscalYearBudget.id);
                        $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                    }
                    else
                        $scope.alertMsg = sharedService.getShortErrorMsg();
                });

             
            }
        }
        else {
            $scope.alertMsg = sharedService.getShortUniqueErrorMsg();
            $scope.alertClass = "alert-danger";
            $scope.showAlert = true;
        }

    };

    $scope.isUnique = function (selectedObj) {

        var returnValue = true;

        angular.forEach($scope.fiscalYearBudgets, function (item) {
            if (item.id != selectedObj.id && item.orgFiscalYearId == selectedObj.orgFiscalYearId && item.fundTypeId == selectedObj.fundTypeId) {
                returnValue = false;
                return returnValue;
            }
        })

        return returnValue;
    }

    $scope.enableFiscalYearBudget = function (id, status) {

        var unique = true;

        var orgFiscalYearBudget = $filter('filter')($scope.fiscalYearBudgets, { id: id },true);

        if (status == true)
            unique = $scope.isUnique(orgFiscalYearBudget[0]);

        if (unique == true) {
            fiscalYearBudgetService.enableFiscalYearBudget(orgFiscalYearBudget[0].id, status).then(function (results) {

                orgFiscalYearBudget[0].active = status;
                orgFiscalYearBudget[0].rowTimeStamp = results.data;

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
        }
        else
        {
            $scope.alertMsgForDelete = sharedService.getShortUniqueErrorMsg();
            $scope.alertClassForDelete = "alert-danger";
            $scope.showAlertForDelete = true;           
        }

    };

}]);