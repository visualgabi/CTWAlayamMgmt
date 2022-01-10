'use strict';
app.controller('depositDtlsCtrl', ['$scope', 'lookupService', 'depositService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'userService', '$stateParams',
    function ($scope, lookupService, depositService, $filter, sharedService, authService, ngTableParams, userService, $stateParams) {

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
     

        $scope.initialize = function (id) {

            //$scope.clear();

            if ($stateParams.id !== undefined) {

                depositService.getById($stateParams.id).then(function (results) {
                    $scope.deposit = results.data;
                    $scope.deposit.depositDate = new Date($filter('date')($scope.deposit.depositDate, 'MM/d/yyyy'));
                    $scope.deposit.offeringDate = new Date($filter('date')($scope.deposit.offeringDate, 'MM/d/yyyy'));
                })
            }

        };

        
        $scope.clearAlert = function () {
            $scope.showAlert = false;
            $scope.alertClass = "";
            $scope.alertMsg = "";
           
        }    
        
        $scope.enable = function (id, status) {            

            depositService.enable($scope.deposit.id, status).then(function (results) {

                $scope.deposit.active = status;
                $scope.deposit.rowTimeStamp = results.data;

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