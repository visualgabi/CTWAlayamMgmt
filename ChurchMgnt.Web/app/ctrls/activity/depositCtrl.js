'use strict';
app.controller('depositCtrl', ['$scope', 'lookupService', 'depositService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'userService',
    function ($scope, lookupService, depositService, $filter, sharedService, authService, ngTableParams, userService) {

    $scope.deposits = [];
    //$scope.users = [];
    //$scope.accounts = [];    
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

    $scope.action = "";

    $scope.deposit = {
        id: 0,
        accountId: "",
        userId: "",        
        description: "",        
        amount: "",        
        depositDate: "",
        offeringDate: "",
        active: true,
        rowTimeStamp: ""
    }

    $scope.todayDate = function () {
        return new Date();
    };

    $scope.rptMinDate = function () {        
        
        var d = new Date();
        d = new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy'));

        d.setDate(d.getDate() + 1);
        return d;
    };

    $scope.disableEditAction = function (recordDate) {
        return new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy'))  > new Date($filter('date')(recordDate, 'MM/d/yyyy'))
    };

    $scope.clear = function () {
        $scope.deposit = {
            id: 0,
            accountId: "",
            userId: "",
            description: "",
            amount: "",            
            depositDate: "",
            offeringDate: "",
            active: true,
            rowTimeStamp: ""
        }
    }

    $scope.authentication = authService.authentication;
    $scope.amountTitle = 'Amount in ' + $scope.authentication.currency;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }

    $scope.formLoad = function () {
        depositService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.deposits = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.deposits.length, // length of data
                getData: function ($defer, params) {

                    $scope.filteredData = $filter('filter')($scope.deposits, $scope.filterText);

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
            var results = $filter('filter')($scope.deposits, { id: id }, true);
            $scope.deposit = results[0];
            $scope.deposit.depositDate = new Date($filter('date')($scope.deposit.depositDate, 'MM/d/yyyy'));
            $scope.deposit.offeringDate = new Date($filter('date')($scope.deposit.offeringDate, 'MM/d/yyyy'));
        }

        lookupService.getAccountsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.accounts = results.data;
        }, function (error) {
            showErroMessage(error)
        });

        userService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.users = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    };

    $scope.availableUsers = function (id) {
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

    $scope.clearAlert = function () {
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";
    }

    $scope.load = function () {
        depositService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.deposits = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {        

        depositService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.deposits = results.data;
            $scope.initialize(id);
        });
    }

    $scope.save = function (depositForm) {

        if (depositForm.$valid) {

            depositService.save($scope.deposit).then(function (results) {

                if ($scope.deposit.id == 0) {
                    depositForm.$setPristine();
                    $scope.clear();
                }
                else
                    $scope.loadAfterEdit($scope.deposit.id);

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

        var orgDeposits = $filter('filter')($scope.deposits, { id: id }, true);

        depositService.enable(orgDeposits[0].id, status).then(function (results) {

            orgDeposits[0].active = status;
            orgDeposits[0].rowTimeStamp = results.data;

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