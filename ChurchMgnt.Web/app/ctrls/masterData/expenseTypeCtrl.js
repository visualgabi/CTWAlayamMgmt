'use strict';
app.controller('expenseTypeCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService', 
    function ($scope, $filter, sharedService, authService, ngTableParams, lookupService ) {
              

        $scope.expenseTypes = [];
        $scope.organizations = [];

        $scope.expenseType = {
            id: 0,
            name: "",
            discription: "",
            organizationId: "",
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

        $scope.isUnique = function (expenseTypeId, value, ctrl) {

            var results = $filter('filter')($scope.expenseTypes, { name: value }, true);
          
            if ((expenseTypeId == 0 && results.length > 0) || (expenseTypeId > 0 && results.length > 1))
                ctrl.$setValidity('isunique', false);
            else
                ctrl.$setValidity('isunique', true);
        }

        $scope.formLoad = function () {            
                lookupService.getExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                    $scope.expenseTypes = results.data;

                    $scope.tableParams = new ngTableParams({
                        page: 1,            // show first page
                        count: 10,           // count per page
                        sorting: {
                            name: 'asc'     // initial sorting
                        }
                    }, {
                        total: $scope.expenseTypes.length, // length of data
                        getData: function ($defer, params) {

                            $scope.filteredData = $filter('filter')($scope.expenseTypes, $scope.filterText);

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
                var results = $filter('filter')($scope.expenseTypes, { id: id }, true);
                $scope.expenseType = results[0];
            }                       
        }

        $scope.load = function () {            

            lookupService.getExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.expenseTypes = results.data;
                $scope.tableParams.reload();
            });
        }

        $scope.loadAfterEdit = function (id) {

            lookupService.getExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.expenseTypes = results.data;
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
            $scope.expenseType = {
                id: 0,
                name: "",
                discription: "",
                organizationId: "",
                active: true,
                rowTimeStamp: ""
            };
        }


        $scope.save = function (expenseTypeForm) {

            if (expenseTypeForm.$valid) {

                $scope.expenseType.organizationId = $scope.authentication.orgId;
                lookupService.saveExpenseType($scope.expenseType).then(function (results) {
                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();

                    if ($scope.expenseType.id == 0) {
                        expenseTypeForm.$setPristine();
                        $scope.clear();
                    }
                    else
                        $scope.loadAfterEdit($scope.expenseType.id);

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

            var orgOfferingTypes = $filter('filter')($scope.expenseTypes, { id: id }, true);

            lookupService.enableExpenseType(orgOfferingTypes[0].id, status).then(function (results) {

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