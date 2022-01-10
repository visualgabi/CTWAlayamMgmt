'use strict';
app.controller('sponsorController', ['$scope', 'lookupService', 'sponsorService', '$filter', 'sharedService', 'authService', 'ngTableParams',
    function ($scope, lookupService, sponsorService, $filter, sharedService, authService, ngTableParams) {
     

    $scope.sponsors = [];
    $scope.countries = [];
    $scope.states = [];
    
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

    $scope.action = "";

    $scope.sponsor = {
        id: 0,
        organizationId: "",
        name: "",
        address1: "",
        city: "",
        countryId: "",        
        emailId1: "",        
        phone1: "",                
        stateId: "",
        website: "",
        zipCode: "",
        active: true,
        rowTimeStamp: ""
    }

    $scope.clear = function () {
        $scope.sponsor = {
            id: 0,
            organizationId: "",
            name: "",
            address1: "",
            city: "",
            countryId: "",
            emailId1: "",
            phone1: "",
            stateId: "",
            website: "",
            zipCode: "",
            active: true,
            rowTimeStamp: ""
        }
    }

    $scope.authentication = authService.authentication;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }

    $scope.formLoad = function () {
        sponsorService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.sponsors = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.sponsors.length, // length of data
                getData: function ($defer, params) {

                    //  var fitleredData = $filter('filter')(data, filterText);

                    var filteredData = $filter('filter')($scope.sponsors, $scope.filterText);

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
            var results = $filter('filter')($scope.sponsors, { id: id });
            $scope.sponsor = results[0];            
        }

        lookupService.getCountries(true).then(function (results) {
            $scope.countries = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getStates(true).then(function (results) {
            $scope.states = results.data;

        }, function (error) {
            showErroMessage(error)
        });

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
        sponsorService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.sponsors = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {
        sponsorService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.sponsors = results.data;
            $scope.initialize(id);
        });
    }

    $scope.save = function (sponsorForm) {

        if (sponsorForm.$valid) {            
            $scope.sponsor.organizationId = $scope.authentication.orgId;

            sponsorService.save($scope.sponsor).then(function (results) {

                if($scope.sponsor.id == 0)
                {
                    sponsorForm.$setPristine();
                    $scope.clear();
                }
                else
                    $scope.loadAfterEdit($scope.sponsor.id);

                $scope.alertClass = "alert-success";
                $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                $scope.showAlert = true;           

            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;

                if (error.data === 3) {
                    $scope.loadAfterEdit($scope.sponsor.id);
                    $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                }
                else if (error.data === 2) {
                    $scope.loadAfterEdit($scope.sponsor.id);
                    $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                }
                else
                    $scope.alertMsg = sharedService.getShortErrorMsg();
            });
            
        }
    };

    $scope.enable = function (id, status) {

        var orgSponsors = $filter('filter')($scope.sponsors, { id: id });

        sponsorService.enable(orgSponsors[0].id, status).then(function (results) {

            orgSponsors[0].active = status;
            orgSponsors[0].rowTimeStamp = results.data;

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