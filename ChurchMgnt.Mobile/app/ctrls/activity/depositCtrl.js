'use strict';
app.controller('depositCtrl', ['$scope', 'lookupService', 'depositService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'userService','$stateParams',
    function ($scope, lookupService, depositService, $filter, sharedService, authService, ngTableParams, userService, $stateParams) {

    $scope.deposits = [];        

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";    

    $scope.action = "";

    $scope.deposit = {
        id: $stateParams.id,
        accountId: "",
        userId: "",        
        description: "",        
        amount: "",        
        depositDate: "",
        offeringDate: "",
        active: true,
        rowTimeStamp: ""
    }

    $scope.todayDate = function () {
        return new Date();
    };

    //$scope.rptMinDate = function () {        
        
    //    var d = new Date();
    //    d = new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy'));

    //    d.setDate(d.getDate() + 1);
    //    return d;
        //};


    $scope.rptMinDate = function () {

        var d = new Date();
        d = new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy'));

        $scope.depositMinDate = d.setDate(d.getDate() + 1);
    };

    $scope.rptMinDate();

    $scope.rptMaxDate = function () {
        $scope.depositMaxDate = new Date();
    };

    $scope.rptMaxDate();

    $scope.disableEditAction = function (recordDate) {
        return new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy'))  > new Date($filter('date')(recordDate, 'MM/d/yyyy'))
    };

    $scope.clear = function () {
        $scope.deposit = {
            id: 0,
            accountId: "",
            userId: "",
            description: "",
            amount: "",            
            depositDate: "",
            offeringDate: "",
            active: true,
            rowTimeStamp: ""
        }
    }

    $scope.authentication = authService.authentication;
    $scope.amountTitle = $scope.authentication.currency;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }

    $scope.initialize = function () {

        $scope.clear();

        if ($stateParams.id !== undefined) {

            depositService.getById($stateParams.id).then(function (results) {
                $scope.deposit = results.data;
                $scope.deposit.depositDate = new Date($filter('date')($scope.deposit.depositDate, 'MM/d/yyyy'));
                $scope.deposit.offeringDate = new Date($filter('date')($scope.deposit.offeringDate, 'MM/d/yyyy'));
            })            
        }

        lookupService.getAccountsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.accounts = results.data;
        }, function (error) {
            showErroMessage(error)
        });

        userService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.users = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    };

    $scope.availableUsers = function (id) {
        return function (item) {

            if (id == 0) {
                return item.active == true;
            }
            else {
                return true;
            }

        };
    };

    $scope.availableAccounts = function (id) {
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
        depositService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.deposits = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {        

        depositService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.deposits = results.data;
            $scope.initialize(id);
        });
    }

    $scope.save = function (depositForm) {

        if (depositForm.$valid) {

            depositService.save($scope.deposit).then(function (results) {

                if ($scope.deposit.id == 0) {
                    depositForm.$setPristine();
                    $scope.clear();
                }
                else
                    $scope.loadAfterEdit($scope.deposit.id);

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
        if ($scope.popup1 !== undefined && $scope.popup1.opened !== undefined && $scope.popup1.opened == true)
            $scope.deposit.depositDate = new Date();
       
        if ($scope.popup2 !== undefined && $scope.popup2.opened !== undefined && $scope.popup2.opened == true)
            $scope.deposit.offeringDate = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        
        if ($scope.popup1.opened !== undefined && $scope.popup1.opened == true)
            $scope.deposit.depositDate = null;

        if ($scope.popup2.opened !== undefined && $scope.popup2.opened == true)
            $scope.deposit.offeringDate = null;

    };

    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: $scope.depositMinDate,
        showWeeks: true
    };

    $scope.dateOptions = {
        dateDisabled: disabled,
        formatYear: 'yy',
        maxDate: $scope.depositMaxDate,
        minDate: $scope.depositMinDate,
        startingDay: 1
    };

        // Disable weekend selection
    function disabled(data) {
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }

    $scope.toggleMin = function () {
        $scope.inlineOptions.minDate = $scope.depositMinDate;
        $scope.dateOptions.minDate = $scope.depositMinDate;
    };

    $scope.toggleMin();

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };

    $scope.setDate = function (year, month, day) {
        $scope.dt = new Date(year, month, day);
    };

    $scope.format = 'MM/dd/yyyy';
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };

    $scope.popup2 = {
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