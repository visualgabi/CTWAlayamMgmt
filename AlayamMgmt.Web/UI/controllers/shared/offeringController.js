'use strict';
app.controller('offeringController', ['$scope', 'lookupService', 'offeringService', 'familyMemberService', '$filter', 'sharedService', 'authService','ngTableParams', 'sponsorService',
    function ($scope, lookupService, offeringService, familyMemberService, $filter, sharedService, authService, ngTableParams, sponsorService) {
       
    $scope.sources = [{ id: 1, name: "Family" }, { id: 2, name: "Sponsor" }];

    $scope.offerings = [];
    //$scope.familyMembers = [];    
    //$scope.offeringTYpe = [];
   // $scope.fundTypes = [];
    //$scope.paymentTypes = [];
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

    $scope.action = "";

    $scope.offering = {
        id: 0,
        organizationId: "",
        familyMemberId: "",
        offeringTypeId: "",
        fundTypeId: "",
        amount: "",
        offeringDate: "",
        transacitonNumber: "",
        notes: "",
        active: true,
        rowTimeStamp: "",
        sponsorId: "",
        sourceId:1
    }

    $scope.directiveOptions = {
        no_results_text: "SO SORRY"
    };

    $scope.todayDate = function () {
        return new Date();
    };

    $scope.rptMinDate = function () {
        var d = new Date();
        d.setFullYear(d.getFullYear() - 5);
        return d;
    };


    $scope.clear = function () {
        $scope.offering = {
            id: 0,
            organizationId: "",
            familyMemberId: "",
            offeringTypeId: "",
            fundTypeId: "",
            amount: "",
            offeringDate: "",
            transacitonNumber: "",
            notes: "",
            active: true,
            rowTimeStamp: "",
            sponsorId: "",
            sourceId: 1
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
        offeringService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.offerings = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.offerings.length, // length of data
                getData: function ($defer, params) {                 

                    var filteredData = $filter('filter')($scope.offerings, $scope.filterText);

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
            var results = $filter('filter')($scope.offerings, { id: id });
            $scope.offering = results[0];            
            $scope.offering.offeringDate = new Date($filter('date')($scope.offering.offeringDate, 'MM/d/yyyy'));            
            $scope.offering.sourceId = $scope.offering.familyMemberId != null ? 1 : 2;
        }        

        familyMemberService.getTaxPayerByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.familyMembers = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        sponsorService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.sponsors = results.data;

        }, function (error) {
            showErroMessage(error)
        });
        

        lookupService.getPaymentTypes(true).then(function (results) {
            $scope.paymentTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getOfferingTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.offeringTypes = results.data;
        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getFundTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.fundTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    };

    $scope.availableFundTypes = function (id) {
        return function (item) {

            if (id == 0) {
                return item.active == true;
            }
            else {
                return true;
            }

        };
    };

    $scope.availableOfferingTypes = function (id) {
        return function (item) {

            if (id == 0) {
                return item.active == true;
            }
            else {
                return true;
            }

        };
    };

    $scope.availableFamilyMembers = function (id) {
        return function (item) {

            if (id == 0) {
                return item.active == true;
            }
            else {
                return true;
            }

        };
    };

    $scope.availableSponsors = function (id) {
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
        offeringService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.offerings = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {

        var orgId = $scope.authentication.orgId;

        offeringService.getByOrgId(orgId, null).then(function (results) {
            $scope.offerings = results.data;
            $scope.initialize(id);
        });
    }

    $scope.save = function (offeringForm) {        

            if (offeringForm.$valid) {
                $scope.offering.organizationId = $scope.authentication.orgId;

                offeringService.save($scope.offering).then(function (results) {

                    if ($scope.offering.id == 0) {
                        offeringForm.$setPristine();
                        $scope.clear();
                    }                   
                    else 
                        $scope.loadAfterEdit($scope.offering.id);

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

                $scope.showAlert = true;
            }
    };
  
    $scope.enable = function (id, status) {        

        var orgOfferings = $filter('filter')($scope.offerings, { id: id });        
        
        offeringService.enable(orgOfferings[0].id, status).then(function (results) {

            orgOfferings[0].active = status;
            orgOfferings[0].rowTimeStamp = results.data;

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