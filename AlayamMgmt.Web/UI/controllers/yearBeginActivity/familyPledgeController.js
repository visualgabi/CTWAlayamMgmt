'use strict';
app.controller('familyPledgeController', ['$scope', 'lookupService', 'familyPledgeService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'familyService', 'orgFiscalYearService',
    function ($scope, lookupService, familyPledgeService, $filter, sharedService, authService, ngTableParams, familyService, orgFiscalYearService) {

    $scope.familyPledges = [];
   // $scope.orgFiscalYears = [];
    //$scope.fundTypes = [];
   // $scope.families = [];
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

    $scope.action = "";

    $scope.familyPledge = {
        id: 0,
        familyId: "",
        orgFiscalYearId: "",
        fundTypeId: "",
        amount: "",
        active: true,
        rowTimeStamp: ""
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
        familyPledgeService.getFamilyPledgeByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.familyPledges = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.familyPledges.length, // length of data
                getData: function ($defer, params) {

                    //  var fitleredData = $filter('filter')(data, filterText);

                    var filteredData = $filter('filter')($scope.familyPledges, $scope.filterText);

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

    $scope.clear = function () {
        $scope.familyPledge = {
            id: 0,
            familyId: "",
            orgFiscalYearId: "",
            fundTypeId: "",
            amount: "",
            active: true,
            rowTimeStamp: ""
        }
    }

    $scope.initialize = function (id) {


        $scope.clear();

        if (id != 0) {
            var results = $filter('filter')($scope.familyPledges, { id: id });
            $scope.familyPledge = results[0];
        }

        orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, true).then(function (results) {
            $scope.orgFiscalYears = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        familyService.getFamiliesByOrgId($scope.authentication.orgId, true).then(function (results) {
            $scope.families = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getFundTypesByOrgId($scope.authentication.orgId, true).then(function (results) {
            $scope.fundTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    };   

    $scope.loadFamilyPledges = function () {
        familyPledgeService.getFamilyPledgeByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.familyPledges = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {
        familyPledgeService.getFamilyPledgeByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.familyPledges = results.data;
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
    

    $scope.saveFamilyPledge = function (familyPledgeForm) {

        var unique = true;
        unique = $scope.isUnique($scope.familyPledge);

        if (unique == true) {

            if (familyPledgeForm.$valid) {               

                familyPledgeService.saveFamilyPledge($scope.familyPledge).then(function (results) {

                    if ($scope.familyPledge.id == 0) {
                        familyPledgeForm.$setPristine();
                        $scope.clear();
                    }
                    else
                        $scope.loadAfterEdit($scope.familyPledge.id);

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                    $scope.showAlert = true;

                }, function (error) {
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;

                    if (error.data === 3) {
                        $scope.loadAfterEdit($scope.familyPledge.id);
                        $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                    }
                    else if (error.data === 2) {
                        $scope.loadAfterEdit($scope.familyPledge.id);
                        $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                    }
                    else
                        $scope.alertMsg = sharedService.getShortErrorMsg();
                });                
            }
        }
        else {
            $scope.alertMsg = sharedService.getShortUniqueErrorMsg();
            $scope.alertClass = "alert-danger";
            $scope.showAlert = true;
        }

    };

    $scope.isUnique = function (selectedObj) {

        var returnValue = true;

        angular.forEach($scope.familyPledges, function (item) {
            if (item.id != selectedObj.id && item.familyId == selectedObj.familyId && item.orgFiscalYearId == selectedObj.orgFiscalYearId && item.fundTypeId == selectedObj.fundTypeId) {
                returnValue = false;
                return returnValue;
            }
        })

        return returnValue;
    }

    $scope.enableFamilyPledge = function (id, status) {

        var unique = true;

        var orgFamilyPledge = $filter('filter')($scope.familyPledges, { id: id });

        if (status == true)
            unique = $scope.isUnique(orgFamilyPledge[0]);

        if (unique == true) {
            familyPledgeService.enableFamilyPledge(orgFamilyPledge[0].id, status).then(function (results) {

                orgFamilyPledge[0].active = status;
                orgFamilyPledge[0].rowTimeStamp = results.data;

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