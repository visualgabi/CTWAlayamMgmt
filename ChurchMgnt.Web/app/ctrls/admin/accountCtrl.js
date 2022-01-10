'use strict';
app.controller('accountCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService',
    function ($scope, $filter, sharedService, authService, ngTableParams, lookupService) {
                
        $scope.accounts = [];
        $scope.expenseTypes = [];
        //$scope.banks = [];

        $scope.account = {
            id: 0,
            name: "",
            discription: "",
            bankId: "",
            accountTypeId: "",
            number:"",
            active: true,
            rowTimeStamp: ""
        }

        $scope.clear = function () {
            $scope.account = {
                id: 0,
                name: "",
                discription: "",
                bankId: "",
                accountTypeId: "",
                number: "",
                active: true,
                rowTimeStamp: ""
            }
        }

        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";

        $scope.action = "";

        $scope.authentication = authService.authentication;

        $scope.setAction = function (value) {
            $scope.action = value;

            if ($scope.action == 'add') {
                $scope.clear();
            }
        }

        $scope.availableBank = function (id) {
            return function (item) {

                if (id == 0) {
                    return item.active == true;                    
                }
                else
                {
                    return true;
                }                   

            };
        };
       
        $scope.isUnique = function (accountId, value, ctrl) {

            var results = $filter('filter')($scope.accounts, { name: value }, true);
           
            if ((accountId == 0 && results.length > 0) || (accountId > 0 && results.length > 1))
                ctrl.$setValidity('isunique', false);
            else
                ctrl.$setValidity('isunique', true);
        }


        $scope.formLoad = function () {
            lookupService.getAccountsByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.accounts = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.accounts.length, // length of data
                    getData: function ($defer, params) {

                        $scope.filteredData = $filter('filter')($scope.accounts, $scope.filterText);

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

            if (id != 0) {
                var results = $filter('filter')($scope.accounts, { id: id }, true);
                $scope.account = results[0];
            }

            lookupService.getAccountTypes(true).then(function (results) {
                $scope.accountTypes = results.data;
            })

            lookupService.getBanksByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.banks = results.data;
            })

        }

        $scope.clearAlert = function () {
            $scope.showAlert = false;
            $scope.alertClass = "";
            $scope.alertMsg = "";

            $scope.showAlertForDelete = false;
            $scope.alertClassForDelete = "";
            $scope.alertMsgForDelete = "";
        }
      

        $scope.load = function () {

            lookupService.getAccountsByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.accounts = results.data;
                $scope.tableParams.reload();
            });

        }

        $scope.loadAfterEdit = function (id) {

            var orgId = $scope.authentication.orgId;

            lookupService.getAccountsByOrgId(orgId).then(function (results) {
                $scope.accounts = results.data;
                $scope.initialize(id);
            });
        }


        $scope.save = function (accountForm) {

            if (accountForm.$valid) {                
                lookupService.saveAccount($scope.account).then(function (results) {

                    if ($scope.account.id == 0) {
                        accountForm.$setPristine();
                        $scope.clear();
                    }                    
                    else 
                        $scope.loadAfterEdit($scope.account.id);

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                    $scope.showAlert = true;

                }, function (error) {
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;

                    if (error.data === 3) {
                        $scope.loadAfterEdit($scope.account.id);
                        $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                    }
                    else if (error.data === 2) {
                        $scope.loadAfterEdit($scope.account.id);
                        $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                    }
                    else
                        $scope.alertMsg = sharedService.getShortErrorMsg();
                });

                
            }
        };



        $scope.enable = function (id, status) {

            var orgOfferingTypes = $filter('filter')($scope.accounts, { id: id }, true);

            lookupService.enableAccount(orgOfferingTypes[0].id, status).then(function (results) {

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