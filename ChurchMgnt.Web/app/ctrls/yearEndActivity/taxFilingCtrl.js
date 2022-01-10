'use strict';
app.controller('taxFilingCtrl', ['$scope', 'taxFilingService', '$filter', 'orgFiscalYearService','authService','ngTableParams','sharedService',
    function ($scope, taxFilingService, $filter, orgFiscalYearService, authService, ngTableParams, sharedService) {

        $scope.taxFilings = [];
        $scope.orgFiscalYears = [];


        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";

        $scope.taxFiling = {
            id: 0,            
            orgFiscalYearId: "",            
            notes: "",
            filedBy: "",
            filedDate : "",
            totalAsset: "",
            totalExpense: "",
            totalLiability: "",
            totalRevenue: "",
            active: true,
            rowTimeStamp: ""          
        }

        $scope.authentication = authService.authentication;
        $scope.assetTitle = 'Asset in ' + $scope.authentication.currency;
        $scope.expenseTitle = 'Expense in ' + $scope.authentication.currency;
        $scope.liabilityTitle = 'Liability in ' + $scope.authentication.currency;
        $scope.revenueTitle = 'Revenue in ' + $scope.authentication.currency;


        $scope.clearAlert = function () {
            $scope.showAlert = false;
            $scope.alertClass = "";
            $scope.alertMsg = "";

            $scope.showAlertForDelete = false;
            $scope.alertClassForDelete = "";
            $scope.alertMsgForDelete = "";
        }

        $scope.clear = function () {
            $scope.taxFiling = {
                id: 0,
                orgFiscalYearId: "",                
                notes: "",
                filedBy: "",
                filedDate: "",
                totalAsset: "",
                totalExpense: "",
                totalLiability: "",
                totalRevenue: "",
                active: true,
                rowTimeStamp: ""
            }
        }

        $scope.minDate = function () {            

            if ($scope.taxFiling.orgFiscalYearId > 0) {
                var orgFiscalYear = $filter('filter')($scope.orgFiscalYears, { id: $scope.taxFiling.orgFiscalYearId },true);

                return new Date($filter('date')(orgFiscalYear[0].fiscalYearEnd, 'MM/d/yyyy'));
            }
            else
                return new Date();
           
        };

        $scope.maxDate = function () {
             return new Date();            
        };

        $scope.setAction = function (value) {
            $scope.action = value;

            if ($scope.action == 'add') {
                $scope.clear();
            }
        }

        $scope.isUnique = function (selectedObj) {

            var returnValue = true;

            angular.forEach($scope.taxFilings, function (item) {
                if (item.id != selectedObj.id) {
                    returnValue = false;
                    return returnValue;
                }
            })

            return returnValue;
        }       

        $scope.formLoad = function () {
            taxFilingService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.taxFilings = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.taxFilings.length, // length of data
                    getData: function ($defer, params) {

                        $scope.filteredData = $filter('filter')($scope.taxFilings, $scope.filterText);

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

            orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, true).then(function (results) {
                $scope.orgFiscalYears = results.data;

            }, function (error) {
                showErroMessage(error)
            });
        }
       
        $scope.$watch("filterText", function () {
            $scope.tableParams.reload();
            $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
        });

        $scope.initialize = function (id) {

            $scope.clear();

            if (id != 0) {
                var results = $filter('filter')($scope.taxFilings, { id: id },true);
                $scope.taxFiling = results[0];
                $scope.taxFiling.filedDate = new Date($filter('date')($scope.taxFiling.filedDate, 'MM/d/yyyy'));
            }           
        }

        $scope.load = function () {

            taxFilingService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.taxFilings = results.data;
                $scope.tableParams.reload();
            });
        }

        $scope.loadAfterEdit = function (id) {

            taxFilingService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.taxFilings = results.data;
                $scope.initialize(id);
            });
        }

        $scope.save = function (taxFilingForm) {

            var unique = true;

            if (taxFilingForm.$valid) {

                unique = $scope.isUnique($scope.taxFiling);
                
                if (unique == true) {
                    taxFilingService.save($scope.taxFiling).then(function (results) {
                        $scope.alertClass = "alert-success";
                        $scope.alertMsg = sharedService.getShortSaveSuccessMsg();

                        if ($scope.taxFiling.id == 0) {
                            taxFilingForm.$setPristine();
                            $scope.clear();
                        }
                        else
                            $scope.loadAfterEdit($scope.taxFiling.id);

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
                else {
                    $scope.alertMsg = sharedService.getShortUniqueErrorMsg();
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;
                }

            }
        };

        $scope.enable = function (id, status) {

            var unique = true;
            var orgTaxFilings = $filter('filter')($scope.taxFilings, { id: id },true);

            if (status == true)
                unique = $scope.isUnique(orgTaxFilings[0]);

            if (unique == true) {
               

                taxFilingService.enable(orgTaxFilings[0].id, status).then(function (results) {

                    orgTaxFilings[0].active = status;
                    orgTaxFilings[0].rowTimeStamp = results.data;

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
            else {
                $scope.alertMsgForDelete = sharedService.getShortUniqueErrorMsg();
                $scope.alertClassForDelete = "alert-danger";
                $scope.showAlertForDelete = true;
            }


        };


    }]);