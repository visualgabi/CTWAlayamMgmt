'use strict';
app.controller('expenseDtlsCtrl', ['$scope', 'lookupService', 'expenseService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'organizationService','$stateParams',
    function ($scope, lookupService, expenseService, $filter, sharedService, authService, ngTableParams, organizationService, $stateParams) {

        $scope.targets = [{ id: 1, name: "Branch" }, { id: 2, name: "Organization" }];

        $scope.expenses = [];        
        $scope.subExpenseTypes = [];        
        $scope.filterText;

        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";       

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
            account: ""
        }
        $scope.authentication = authService.authentication;

        $scope.disableEditAction = function (recordDate) {
            return new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy')) > new Date($filter('date')(recordDate, 'MM/d/yyyy'))
        };

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

        $scope.formLoad = function () {
            if ($stateParams.id !== undefined) {

                expenseService.getById($stateParams.id).then(function (results) {
                    $scope.expense = results.data;
                    $scope.expense.expenseDate = new Date($filter('date')($scope.expense.expenseDate, 'MM/d/yyyy'));
                    $scope.expense.targetId = $scope.expense.branch != null ? 1 : 2;
                })              
            }
        }        

        $scope.clearAlert = function () {
            $scope.showAlert = false;
            $scope.alertClass = "";
            $scope.alertMsg = "";          
        }            

        $scope.enable = function (id, status) {            

            expenseService.enable($scope.expense.id, status).then(function (results) {

                $scope.expense.active = status;
                $scope.expense.rowTimeStamp = results.data;

                $scope.alertClass = "alert-success";
                if (status == true)
                    $scope.alertMsg = sharedService.getShortEnableSuccessMsg();
                else
                    $scope.alertMsg= sharedService.getShortDisableSuccessMsg();

                $scope.showAlert = true;                

            }, function (error) {

                $scope.alertClass = "alert-danger";
                $scope.alertMsg = sharedService.getShortErrorMsg();

                $scope.showAlert = true;

                $scope.tableParams.reload();
            });


        };

    }]);