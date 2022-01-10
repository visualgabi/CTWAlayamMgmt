'use strict';
app.controller('orgFiscalYearController', ['$scope', 'lookupService', 'orgFiscalYearService', '$filter', 'sharedService', 'authService', 'ngTableParams',
    function ($scope, lookupService, orgFiscalYearService, $filter, sharedService, authService, ngTableParams) {
     

    $scope.orgFiscalYears = [];
   // $scope.FiscalYears = [];
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";
    
    $scope.action = "";

    $scope.orgFiscalYear = {
        id: 0,
        organizationId: "",
        fiscalYearId: "",
        fiscalYearStart: "",
        fiscalYearEnd: "",      
        active: true,
        rowTimeStamp: ""
    }

    $scope.authentication = authService.authentication;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }

    $scope.formLoad = function () {
        orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.orgFiscalYears = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.orgFiscalYears.length, // length of data
                getData: function ($defer, params) {

                    var filteredData = $filter('filter')($scope.orgFiscalYears, $scope.filterText);

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
                var results = $filter('filter')($scope.orgFiscalYears, { id: id });
                $scope.orgFiscalYear = results[0];            
                $scope.orgFiscalYear.fiscalYearStart = new Date($filter('date')($scope.orgFiscalYear.fiscalYearStart, 'MM/d/yyyy'));
                $scope.orgFiscalYear.fiscalYearEnd = new Date($filter('date')($scope.orgFiscalYear.fiscalYearEnd, 'MM/d/yyyy'));           
            }

        lookupService.getFiscalYears(true).then(function (results) {
            $scope.FiscalYears = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    };

    $scope.clear = function () {
        $scope.orgFiscalYear = {
            id: 0,
            organizationId: "",
            fiscalYearId: "",
            fiscalYearStart: "",
            fiscalYearEnd: "",
            active: true,
            rowTimeStamp: ""
        };
    }   

    $scope.loadOrgFiscalYears = function () {
        orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.orgFiscalYears = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {        

        orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.orgFiscalYears = results.data;
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


    $scope.isUnique = function (selectedObj) {
      
        var returnValue = true;

        angular.forEach($scope.orgFiscalYears, function (item) {
            if(item.id != selectedObj.id && item.fiscalYearId == selectedObj.fiscalYearId)
            {
                returnValue = false;
                return returnValue;
            }            
        })

        return returnValue;
    }

    $scope.saveOrgFiscalYears = function (OrgFiscalYearForm) {
        var unique = true;

        if (OrgFiscalYearForm.$valid) {
            $scope.orgFiscalYear.organizationId = $scope.authentication.orgId;            
            
            unique = $scope.isUnique($scope.orgFiscalYear);
            
            if (unique == true) {
                orgFiscalYearService.saveOrgFiscalYears($scope.orgFiscalYear).then(function (results) {

                    if ($scope.orgFiscalYear.id == 0) {
                        OrgFiscalYearForm.$setPristine();
                        $scope.clear();
                    }
                    else 
                        $scope.loadAfterEdit($scope.orgFiscalYear.id);

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
                    else if (error.data === 2) {
                        $scope.loadAfterEdit($scope.bank.id);
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

    $scope.enableOrgFiscalYear = function (id, status) {

        var unique = true;

        var organizationfiscalYear = $filter('filter')($scope.orgFiscalYears, { id: id });

        if (status == true)
            unique = $scope.isUnique(organizationfiscalYear[0]);

        if (unique == true) {
            orgFiscalYearService.enableOrgFiscalYear(organizationfiscalYear[0].id, status).then(function (results) {

                organizationfiscalYear[0].active = status;
                organizationfiscalYear[0].rowTimeStamp = results.data;

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