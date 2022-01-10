'use strict';
app.controller('subExpenseTypeController', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService', 
    function ($scope, $filter, sharedService, authService, ngTableParams, lookupService) {
              

        $scope.subExpenseTypes = [];
        //$scope.expenseTypes = [];        

        $scope.subExpenseType = {
            id: 0,
            name: "",
            discription: "",
            expenseTypeId: "",            
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

        $scope.setAction = function (value) {
            $scope.action = value;

            if ($scope.action == 'add') {
                $scope.clear();
            }
        }

        $scope.isUnique = function (subExpenseTypeId, expenseTypeId, value, ctrl) {

            var results = $filter('filter')($scope.subExpenseTypes, { expenseTypeId: expenseTypeId, name: value }, true);

            if ((subExpenseTypeId == 0 && results.length > 0) || (subExpenseTypeId > 0 && results.length > 1))
                ctrl.$setValidity('isunique', false);
            else
                ctrl.$setValidity('isunique', true);
        }

        $scope.formLoad = function () {            
                lookupService.getSubExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                    $scope.subExpenseTypes = results.data;

                    $scope.tableParams = new ngTableParams({
                        page: 1,            // show first page
                        count: 10,           // count per page
                        sorting: {
                            name: 'asc'     // initial sorting
                        }
                    }, {
                        total: $scope.subExpenseTypes.length, // length of data
                        getData: function ($defer, params) {

                            var filteredData = $filter('filter')($scope.subExpenseTypes, $scope.filterText);

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
                var results = $filter('filter')($scope.subExpenseTypes, { id: id });
                $scope.subExpenseType = results[0];
            }            

            lookupService.getExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.expenseTypes = results.data;                
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

        $scope.clear = function () {
            $scope.subExpenseType = {
                id: 0,
                name: "",
                discription: "",
                expenseTypeId: "",                
                active: true,
                rowTimeStamp: ""
            };
        }

        $scope.load = function () {           

            lookupService.getSubExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.subExpenseTypes = results.data;
                $scope.tableParams.reload();
            });

        }

        $scope.loadAfterEdit = function (id) {

            lookupService.getSubExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.subExpenseTypes = results.data;
                $scope.initialize(id);
            });
        }

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


        $scope.save = function (subExpenseTypeForm) {

            if (subExpenseTypeForm.$valid) {

                lookupService.saveSubExpenseType($scope.subExpenseType).then(function (results) {
                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();

                    if ($scope.subExpenseType.id == 0) {
                        subExpenseTypeForm.$setPristine();
                        $scope.clear();
                    }
                    else
                        $scope.loadAfterEdit($scope.subExpenseType.id);

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                    $scope.showAlert = true;

                }, function (error) {
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;

                    if (error.data === 3) {
                        $scope.loadAfterEdit($scope.subExpenseType.id);
                        $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                    }
                    else if (error.data === 2) {
                        $scope.loadAfterEdit($scope.subExpenseType.id);
                        $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                    }
                    else
                        $scope.alertMsg = sharedService.getShortErrorMsg();
                });

                $scope.showAlert = true;
            }
        };



        $scope.enable = function (id, status) {

            var orgOfferingTypes = $filter('filter')($scope.subExpenseTypes, { id: id });

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