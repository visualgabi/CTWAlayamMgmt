'use strict';
app.controller('expenseCtrl', ['$scope', 'lookupService', 'expenseService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'organizationService', '$stateParams',
    function ($scope, lookupService, expenseService, $filter, sharedService, authService, ngTableParams, organizationService, $stateParams) {
    
    $scope.targets = [{ id: 1, name: "Branch" }, { id: 2, name: "Organization" }];

    $scope.expenses = [];    
    $scope.subExpenseTypes = [];            

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";    

    $scope.action = "";
  
    $scope.expense = {
        id: $stateParams.id,
        organizationId: "",
        branchId: "",
        expenseTypeId: "",
        subExpenseTypeId: "",
        paymentTypeId: "",
        amount: "",
        expenseDate: "",
        transacitonNumber: "",
        notes: "",
        active: true,
        rowTimeStamp: "",
        targetId: 1,
        branch: "",
        accountId: "",
        account:""
}
    $scope.authentication = authService.authentication;

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

        $scope.expenseMinDate = d.setDate(d.getDate() + 1);
    };

    $scope.rptMinDate();

    $scope.rptMaxDate = function () {
        $scope.expenseMaxDate = new Date();
    };

    $scope.rptMaxDate();

    $scope.disableEditAction = function (recordDate) {
        return new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy')) > new Date($filter('date')(recordDate, 'MM/d/yyyy'))
    };

   
    $scope.amountTitle = $scope.authentication.currency;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }

    $scope.clear = function () {
        $scope.expense = {
            id: 0,
            organizationId: "",
            branchId: "",
            expenseTypeId: "",
            subExpenseTypeId: "",
            paymentTypeId: "",
            amount: "",
            expenseDate: "",
            transacitonNumber: "",
            notes: "",
            active: true,
            rowTimeStamp: "",
            targetId: 1,
            branch: "",
            accountId: "",
            account: ""
        }
    }  

    $scope.initialize = function () {

        $scope.clear();

        if ($stateParams.id !== undefined) {

            expenseService.getById($stateParams.id).then(function (results) {
                $scope.expense = results.data;
                $scope.expense.expenseDate = new Date($filter('date')($scope.expense.expenseDate, 'MM/d/yyyy'));
                $scope.expense.targetId = $scope.expense.branch != null ? 1 : 2;
            })

        }
       
        lookupService.getPaymentTypes().then(function (results) {
            $scope.paymentTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.expenseTypes = results.data;
        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getSubExpenseTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.subExpenseTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getAccountsByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.accounts = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        organizationService.getBranchesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.branches = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    };

    $scope.availableExpenseTypes = function (id) {
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

    $scope.availableSubExpenseTypes = function (id, expenseTypeId) {
        return function (item) {

            if (id == 0) {
                return item.active == true && item.expenseTypeId == expenseTypeId;
            }
            else {
                return item.expenseTypeId == expenseTypeId;
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
        expenseService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.expenses = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {

        var orgId = $scope.authentication.orgId;

        expenseService.getByOrgId(orgId, null).then(function (results) {
            $scope.expenses = results.data;
            $scope.initialize(id);
        });
    }

    $scope.save = function (expenseForm) {

        if (expenseForm.$valid) {

            if ($scope.expense.targetId == 2) {
                $scope.expense.organizationId = $scope.authentication.orgId;
                $scope.expense.branchId = null;
            }
            else
            {
                $scope.expense.organizationId = null;
            }

            expenseService.save($scope.expense).then(function (results) {

                if ($scope.expense.id == 0) {
                    expenseForm.$setPristine();
                    $scope.clear();
                }
                else
                    $scope.loadAfterEdit($scope.expense.id);

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
        $scope.expense.expenseDate = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.expense.expenseDate = null;
    };

    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: $scope.expenseMinDate,
        showWeeks: true
    };

    $scope.dateOptions = {
        dateDisabled: disabled,
        formatYear: 'yy',
        maxDate: $scope.expenseMaxDate,
        minDate: $scope.expenseMinDate,
        startingDay: 1
    };

        // Disable weekend selection
    function disabled(data) {
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }

    $scope.toggleMin = function () {
        $scope.inlineOptions.minDate = $scope.expenseMinDate;
        $scope.dateOptions.minDate = $scope.expenseMinDate;
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