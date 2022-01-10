'use strict';
app.controller('bankCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService', 
    function ($scope, $filter, sharedService, authService, ngTableParams, lookupService) {
                
        $scope.banks = [];
        $scope.organizations = [];       

        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";

        $scope.action = "";

        $scope.bank = {
            id: 0,
            name: "",
            discription: "",
            organizationId: "",
            active: true,
            rowTimeStamp: ""
        };

        $scope.authentication = authService.authentication;

        $scope.setAction = function (value) {
            $scope.action = value;

            if ($scope.action == 'add') {              
                $scope.clear();
            }
        }     

        $scope.isUnique = function (bankId, value, ctrl) {
           
            var results = $filter('filter')($scope.banks, { name: value }, true);

            if ( (bankId == 0 && results.length > 0) || (bankId > 0 && results.length > 1) )
                ctrl.$setValidity('isunique', false);
            else
                ctrl.$setValidity('isunique', true);
            
        }

        $scope.formLoad = function () {            
                lookupService.getBanksByOrgId($scope.authentication.orgId, null).then(function (results) {
                    $scope.banks = results.data;

                    $scope.tableParams = new ngTableParams({
                        page: 1,            // show first page
                        count: 10,           // count per page
                        sorting: {
                            name: 'asc'     // initial sorting
                        }
                    }, {
                        total: $scope.banks.length, // length of data
                        getData: function ($defer, params) {

                            $scope.filteredData = $filter('filter')($scope.banks, $scope.filterText);

                            var orderedData = params.sorting() ?
                           $filter('orderBy')($scope.filteredData, params.orderBy()) :
                           $scope.filteredData;

                            params.total(orderedData.length);
                            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                        }
                    })

                }, function (error) {                    
                    toastr.error('Something went wrong!')
                });                      
        }

        $scope.$watch("filterText", function () {
            $scope.tableParams.reload();
            $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
        });

        $scope.initialize = function (id) {                  

            if (id != 0) {
                var results = $filter('filter')($scope.banks, { id: id }, true);
                $scope.bank = results[0];
            }           
        }

        $scope.load = function () {
            var orgId = $scope.authentication.orgId;

            lookupService.getBanksByOrgId(orgId, null).then(function (results) {
                $scope.banks = results.data;
                $scope.tableParams.reload();               
            });
        }

        $scope.loadAfterEdit = function (id) {                     

            var orgId = $scope.authentication.orgId;

            lookupService.getBanksByOrgId(orgId, null).then(function (results) {
                $scope.banks = results.data;
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
            $scope.bank = {
                id: 0,
                name: "",
                discription: "",
                organizationId: "",
                active: true,
                rowTimeStamp: ""
            };
        }


        $scope.save = function (bankForm) {

            if (bankForm.$valid) {

                $scope.bank.organizationId = $scope.authentication.orgId;
                lookupService.saveBank($scope.bank).then(function (results) {                    

                    if ($scope.bank.id == 0) {
                        bankForm.$setPristine();
                        $scope.clear();
                    }
                    else {                     
                        $scope.loadAfterEdit($scope.bank.id);
                    }

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                    $scope.showAlert = true;

                }, function (error) {
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;

                    if (error.data === 3) {
                        $scope.loadAfterEdit($scope.bank.id);
                        $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                    }
                    else if (error.data === 2)
                        {
                        $scope.loadAfterEdit($scope.bank.id);
                        $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                    }
                    else
                        $scope.alertMsg = sharedService.getShortErrorMsg();
                });

                
            }
        };



        $scope.enable = function (id, status) {

            var orgOfferingTypes = $filter('filter')($scope.banks, { id: id }, true);

            lookupService.enableBank(orgOfferingTypes[0].id, status).then(function (results) {

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