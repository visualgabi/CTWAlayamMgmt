'use strict';
app.controller('fundTypeController', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService',
    function ($scope, $filter, sharedService, authService, ngTableParams, lookupService ) {
               
        $scope.fundTypes = [];
        $scope.organizations = [];

        $scope.fundType = {
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

        $scope.isUnique = function (fundTypeId, value, ctrl) {

            var results = $filter('filter')($scope.fundTypes, { name: value }, true);

            if ((fundTypeId == 0 && results.length > 0) || (fundTypeId > 0 && results.length > 1))
                ctrl.$setValidity('isunique', false);
            else
                ctrl.$setValidity('isunique', true);
        }

        $scope.formLoad = function () {            
                lookupService.getFundTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                    $scope.fundTypes = results.data;

                    $scope.tableParams = new ngTableParams({
                        page: 1,            // show first page
                        count: 10,           // count per page
                        sorting: {
                            name: 'asc'     // initial sorting
                        }
                    }, {
                        total: $scope.fundTypes.length, // length of data
                        getData: function ($defer, params) {

                            var filteredData = $filter('filter')($scope.fundTypes, $scope.filterText);

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
                var results = $filter('filter')($scope.fundTypes, { id: id });
                $scope.fundType = results[0];
            }
            
        }

        $scope.load = function () {            

            lookupService.getFundTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.fundTypes = results.data;
                $scope.tableParams.reload();
            });
        }

        $scope.loadAfterEdit = function (id) {

            lookupService.getFundTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.fundTypes = results.data;
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
            $scope.fundType = {
                id: 0,
                name: "",
                discription: "",
                organizationId: "",
                active: true,
                rowTimeStamp: ""
            };
        }     


        $scope.save = function (fundTypeForm) {

            if (fundTypeForm.$valid) {

                $scope.fundType.organizationId = $scope.authentication.orgId;

                lookupService.saveFundType($scope.fundType).then(function (results) {
                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();

                    if ($scope.fundType.id == 0) {
                        fundTypeForm.$setPristine();
                        $scope.clear();
                    }
                    else
                        $scope.loadAfterEdit($scope.fundType.id);

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                    $scope.showAlert = true;

                }, function (error) {
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;

                    if (error.data === 3) {
                        $scope.loadAfterEdit($scope.fundType.id);
                        $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                    }
                    else if (error.data === 2) {
                        $scope.loadAfterEdit($scope.fundType.id);
                        $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                    }
                    else
                        $scope.alertMsg = sharedService.getShortErrorMsg();
                });                
            }
        };



        $scope.enable = function (id, status) {

            var orgFundTypes = $filter('filter')($scope.fundTypes, { id: id });

            lookupService.enableFundType(orgFundTypes[0].id, status).then(function (results) {

                orgFundTypes[0].active = status;
                orgFundTypes[0].rowTimeStamp = results.data;

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