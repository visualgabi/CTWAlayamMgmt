'use strict';
app.controller('offeringTypeCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService',
    function ($scope, $filter, sharedService, authService, ngTableParams, lookupService) {
              

        $scope.offeringTypes = [];
        $scope.organizations = [];      
        

        $scope.offeringType = {
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

        $scope.isUnique = function (offeringTypeId, value, ctrl) {

            var results = $filter('filter')($scope.offeringTypes, { id: '!' + offeringTypeId, name: value }, true);
            if (results.length > 0)
                ctrl.$setValidity('isunique', false);
            else
                ctrl.$setValidity('isunique', true);
        }

        $scope.formLoad = function () {                        
                lookupService.getOfferingTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                    $scope.offeringTypes = results.data;

                    $scope.tableParams = new ngTableParams({
                        page: 1,            // show first page
                        count: 10,           // count per page
                        sorting: {
                            name: 'asc'     // initial sorting
                        }
                    }, {
                        total: $scope.offeringTypes.length, // length of data
                        getData: function ($defer, params) {

                            $scope.filteredData = $filter('filter')($scope.offeringTypes, $scope.filterText);

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
                var results = $filter('filter')($scope.offeringTypes, { id: id },true);
                $scope.offeringType = results[0];
            }                       
        }

        $scope.load = function () {            

            lookupService.getOfferingTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.offeringTypes = results.data;
                $scope.tableParams.reload();
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
            $scope.offeringType = {
                id: 0,
                name: "",
                discription: "",
                organizationId: "",
                active: true,
                rowTimeStamp: ""
            };
        }

        $scope.loadAfterEdit = function (id) {

            lookupService.getOfferingTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.offeringTypes = results.data;
                $scope.initialize(id);
            });
        }

        $scope.save = function (offeringTypeForm) {

            if (offeringTypeForm.$valid) {

                $scope.offeringType.organizationId = $scope.authentication.orgId;

                lookupService.saveOfferingType($scope.offeringType).then(function (results) {
                        $scope.alertClass = "alert-success";
                        $scope.alertMsg = sharedService.getShortSaveSuccessMsg();                        

                        if ($scope.offeringType.id == 0) {
                            offeringTypeForm.$setPristine();
                            $scope.clear();
                        }
                        else
                            $scope.loadAfterEdit($scope.offeringType.id);

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                    $scope.showAlert = true;

                    }, function (error) {
                        $scope.alertClass = "alert-danger";
                        $scope.showAlert = true;

                        if (error.data === 3) {
                            $scope.loadAfterEdit($scope.offeringType.id);
                            $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                        }
                        else if (error.data === 2) {
                            $scope.loadAfterEdit($scope.offeringType.id);
                            $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                        }
                        else
                            $scope.alertMsg = sharedService.getShortErrorMsg();
                    });                    
                }            
        };

       

        $scope.enable = function (id, status) {

            var orgOfferingTypes = $filter('filter')($scope.offeringTypes, { id: id },true);

            lookupService.enableOfferingType(orgOfferingTypes[0].id, status).then(function (results) {

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