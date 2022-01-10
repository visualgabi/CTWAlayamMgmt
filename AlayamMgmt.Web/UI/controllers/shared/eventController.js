'use strict';
app.controller('eventController', ['$scope', 'lookupService', 'eventService', 'organizationService', '$filter', 'sharedService', 'authService', 'ngTableParams', 
    function ($scope, lookupService, eventService, organizationService, $filter, sharedService, authService, ngTableParams) {
      

    $scope.events = [];
    //$scope.eventTypes = [];    
    //$scope.branches = [];        
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

    $scope.action = "";    

    $scope.event = {
        id: 0,
        branchId: "",        
        eventTypeId: "",
        name : "",
        description: "",
        isSpecialEvent: false,
        noOfAdultAttended: "",
        noOfMenAttended: "",
        noOfWomenAttended: "",
        noOfKidsParticipated : "",
        offering: "",
        expense: "",
        eventDate: "",        
        notes: "",
        active: true,
        rowTimeStamp: ""
    }

    $scope.todayDate = function () {
        return new Date();
    };

    $scope.rptMinDate = function () {
        var d = new Date();

        d.setFullYear(d.getFullYear() - 5);

        return d;
    };

    $scope.clear = function () {
        $scope.event = {
            id: 0,
            branchId: "",
            eventTypeId: "",
            name: "",
            description: "",
            isSpecialEvent: false,
            noOfAdultAttended: "",
            noOfMenAttended: "",
            noOfWomenAttended: "",
            noOfKidsParticipated: "",
            offering: "",
            expense: "",
            eventDate: "",
            notes: "",
            active: true,
            rowTimeStamp: ""
        }
    }

  
    $scope.expenseTitle = 'Expense in ' + $scope.authentication.currency;
    $scope.offeringTitle = 'Offering in ' + $scope.authentication.currency;

    $scope.authentication = authService.authentication;
    $scope.expenseTitle = 'Expense in ' + $scope.authentication.currency;
    $scope.offeringTitle = 'Offering in ' + $scope.authentication.currency;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }

    $scope.formLoad = function () {
        eventService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.events = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.events.length, // length of data
                getData: function ($defer, params) {

                    var filteredData = $filter('filter')($scope.events, $scope.filterText);

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
            var results = $filter('filter')($scope.events, { id: id });
            $scope.event = results[0];
            $scope.event.eventDate = new Date($filter('date')($scope.event.eventDate, 'MM/d/yyyy'));
        }            

        lookupService.getEventTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.eventTypes = results.data;
        }, function (error) {
            showErroMessage(error)
        });

        organizationService.getBranchesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.branches = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    };

    $scope.availableEventTypes = function (id) {
        return function (item) {

            if (id == 0) {
                return item.active == true;
            }
            else {
                return true;
            }

        };
    };

    $scope.availableBranches = function (id) {
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
        eventService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.events = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {

        eventService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.events = results.data;
            $scope.initialize(id);
        });
    }

    $scope.save = function (eventForm) {

        if (eventForm.$valid) {                     

            eventService.save($scope.event).then(function (results) {

                if ($scope.event.id == 0) {
                    eventForm.$setPristine();
                    $scope.clear();
                }
                else
                    $scope.loadAfterEdit($scope.event.id);

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

        var orgEvents = $filter('filter')($scope.events, { id: id });

        eventService.enable(orgEvents[0].id, status).then(function (results) {

            orgEvents[0].active = status;
            orgEvents[0].rowTimeStamp = results.data;

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