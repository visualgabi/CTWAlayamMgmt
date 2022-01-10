'use strict';
app.controller('eventCtrl', ['$scope', 'lookupService', 'eventService', 'organizationService', '$filter', 'sharedService', 'authService', 'ngTableParams','$stateParams',
    function ($scope, lookupService, eventService, organizationService, $filter, sharedService, authService, ngTableParams, $stateParams) {
      

    $scope.events = [];      
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.action = "";

    $scope.eventMinDate;
    $scope.eventMaxDate;

    $scope.event = {
        id: $stateParams.id,
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
        d = new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy'));

        $scope.eventMinDate = d.setDate(d.getDate() + 1);        
    };

    $scope.rptMinDate();

    $scope.rptMaxDate = function () {
        $scope.eventMaxDate = new Date(new Date().setFullYear(new Date().getFullYear() + 1))
    };

    $scope.rptMaxDate();

    $scope.disableEditAction = function (recordDate) {
        return new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy')) > new Date($filter('date')(recordDate, 'MM/d/yyyy'))
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

    $scope.initialize = function () {

        $scope.clear();

        if ($stateParams.id !== undefined) {

            eventService.getById($stateParams.id).then(function (results) {
                $scope.event = results.data;
                $scope.event.eventDate = new Date($filter('date')($scope.event.eventDate, 'MM/d/yyyy'));
            })
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



    $scope.today = function () {
        $scope.event.eventDate = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.event.eventDate = null;
    };

    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: $scope.eventMinDate,
        showWeeks: true
    };

    $scope.dateOptions = {
        dateDisabled: disabled,
        formatYear: 'yy',        
        maxDate: $scope.eventMaxDate,
        minDate: $scope.eventMinDate,
        startingDay: 1
    };

        // Disable weekend selection
    function disabled(data) {
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }

    $scope.toggleMin = function () {
        $scope.inlineOptions.minDate = $scope.eventMinDate;
        $scope.dateOptions.minDate = $scope.eventMinDate;
    };

    $scope.toggleMin();

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };   

    $scope.setDate = function (year, month, day) {
        $scope.dt = new Date(year, month, day);
    };

    $scope.format = 'MM/dd/yyyy';
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };

    

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 1);
    $scope.events = [
      {
          date: tomorrow,
          status: 'full'
      },
      {
          date: afterTomorrow,
          status: 'partially'
      }
    ];

    function getDayClass(data) {
        var date = data.date,
          mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    }

}]);