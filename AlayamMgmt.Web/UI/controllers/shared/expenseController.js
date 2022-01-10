'use strict';
app.controller('expenseController', ['$scope', 'lookupService', 'expenseService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'organizationService', 
    function ($scope, lookupService, expenseService, $filter, sharedService, authService, ngTableParams, organizationService) {
    
    $scope.targets = [{ id: 1, name: "Branch" }, { id: 2, name: "Organization" }];

    $scope.expenses = [];
    //$scope.expenseTypes = [];
    $scope.subExpenseTypes = [];    
    //$scope.paymentTypes = [];
    //$scope.branches = [];
    //$scope.accounts = [];
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

    $scope.action = "";
  
    $scope.expense = {
        id: 0,
        organizationId: "",
        branchId: "",
        expenseTypeId: "",
        subExpenseTypeId: "",
        paymentTypeId: "",
        amount: "",
        expenseDate: "",
        transacitonNumber: "",
        notes: "",
        active: true,
        rowTimeStamp: "",
        targetId: 1,
        branch: "",
        accountId: "",
        account:""
}

    $scope.todayDate = function () {
        return new Date();
    };

    $scope.rptMinDate = function () {
        var d = new Date();

        d.setFullYear(d.getFullYear() - 5);

        return d;
    };

    $scope.authentication = authService.authentication;
    $scope.amountTitle = 'Amount in ' + $scope.authentication.currency;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }

    $scope.clear = function () {
        $scope.expense = {
            id: 0,
            organizationId: "",
            branchId: "",
            expenseTypeId: "",
            subExpenseTypeId: "",
            paymentTypeId: "",
            amount: "",
            expenseDate: "",
            transacitonNumber: "",
            notes: "",
            active: true,
            rowTimeStamp: "",
            targetId: 1,
            branch: "",
            accountId: "",
            account: ""
        }
    }

    $scope.formLoad = function () {
        expenseService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.expenses = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.expenses.length, // length of data
                getData: function ($defer, params) {

                    //  var fitleredData = $filter('filter')(data, filterText);

                    var filteredData = $filter('filter')($scope.expenses, $scope.filterText);

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
            var results = $filter('filter')($scope.expenses, { id: id });
            $scope.expense = results[0];
            $scope.expense.expenseDate = new Date($filter('date')($scope.expense.expenseDate, 'MM/d/yyyy'));
            $scope.expense.targetId = $scope.expense.branch != null ? 1 : 2;

        }
       
        lookupService.getPaymentTypes().then(function (results) {
            $scope.paymentTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });

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

        organizationService.getBranchesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.branches = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    };

    $scope.availableExpenseTypes = function (id) {
        return function (item) {

            if (id == 0) {
                return item.active == true;
            }
            else {
                return true;
            }

        };
    };

    $scope.availableAccounts = function (id) {
        return function (item) {

            if (id == 0) {
                return item.active == true;
            }
            else {
                return true;
            }

        };
    };

    //$scope.availableSubExpenseTypes = function (id) {
    //    return function (item) {

    //        if (id == 0) {
    //            return item.active == true;
    //        }
    //        else {
    //            return true;
    //        }

    //    };
    //};

    $scope.availableSubExpenseTypes = function (id, expenseTypeId) {
        return function (item) {

            if (id == 0) {
                return item.active == true && item.expenseTypeId == expenseTypeId;
            }
            else {
                return item.expenseTypeId == expenseTypeId;
            }

        };
    };

    $scope.availableBranches = function (id) {
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

    $scope.load = function () {
        expenseService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.expenses = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {

        var orgId = $scope.authentication.orgId;

        expenseService.getByOrgId(orgId, null).then(function (results) {
            $scope.expenses = results.data;
            $scope.initialize(id);
        });
    }

    $scope.save = function (expenseForm) {

        if (expenseForm.$valid) {

            if ($scope.expense.targetId == 2) {
                $scope.expense.organizationId = $scope.authentication.orgId;
                $scope.expense.branchId = null;
            }
            else
            {
                $scope.expense.organizationId = null;
            }

            expenseService.save($scope.expense).then(function (results) {

                if ($scope.expense.id == 0) {
                    expenseForm.$setPristine();
                    $scope.clear();
                }
                else
                    $scope.loadAfterEdit($scope.expense.id);

                $scope.alertClass = "alert-success";
                $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                $scope.showAlert = true;

            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;

                if (error.data === 3) {
                    $scope.loadAfterEdit($scope.offering.id);
                    $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                }
                else if (error.data === 2) {
                    $scope.loadAfterEdit($scope.offering.id);
                    $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                }
                else
                    $scope.alertMsg = sharedService.getShortErrorMsg();
            });           
        }
    };

    $scope.enable = function (id, status) {

        var orgExpenses = $filter('filter')($scope.expenses, { id: id });

        expenseService.enable(orgExpenses[0].id, status).then(function (results) {

            orgExpenses[0].active = status;
            orgExpenses[0].rowTimeStamp = results.data;

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