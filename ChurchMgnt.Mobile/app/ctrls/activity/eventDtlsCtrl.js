'use strict';
app.controller('eventDtlsCtrl', ['$scope', 'lookupService', 'eventService', 'organizationService', '$filter', 'sharedService', 'authService', 'ngTableParams','$stateParams',
    function ($scope, lookupService, eventService, organizationService, $filter, sharedService, authService, ngTableParams, $stateParams) {
                
        $scope.picker1 = {
            date: new Date('2015-03-01T00:00:00Z'),
            datepickerOptions: {
                showWeeks: false,
                startingDay: 1,
                dateDisabled: function (data) {
                    return (data.mode === 'day' && (new Date().toDateString() == data.date.toDateString()));
                }
            }
        };

        $scope.filterText;

        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";        

        $scope.action = "";

        $scope.event = {
            id: $stateParams.id,
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

        $scope.todayDate = function () {
            return new Date();
        };

        $scope.rptMinDate = function () {

            var d = new Date();
            d = new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy'));

            d.setDate(d.getDate() + 1);
            return d;
        };

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

            //$scope.clear();

            if ($stateParams.id !== undefined) {

                eventService.getById($stateParams.id).then(function (results) {
                    $scope.event = results.data;
                    $scope.event.eventDate = new Date($filter('date')($scope.event.eventDate, 'MM/d/yyyy'));
                })
            }

        };

       

        $scope.clearAlert = function () {
            $scope.showAlert = false;
            $scope.alertClass = "";
            $scope.alertMsg = "";          
        }

        $scope.enable = function (id, status) {            

            eventService.enable($scope.event.id, status).then(function (results) {

                $scope.event.active = status;
                $scope.event.rowTimeStamp = results.data;

                $scope.alertClass = "alert-success";
                if (status == true)
                    $scope.alertMsg = sharedService.getShortEnableSuccessMsg();
                else
                    $scope.alertMsg = sharedService.getShortDisableSuccessMsg();

                $scope.showAlert = true;

            }, function (error) {

                $scope.alertClass = "alert-danger";
                $scope.alertMsg = sharedService.getShortErrorMsg();

                $scope.showAlert = true;
                
            });

        };

    }]);