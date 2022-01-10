'use strict';
app.controller('expenseRptPrtController', ['$scope', 'expenseService', '$routeParams', '$filter', 'sharedService', 'authService', 'ngTableParams', '$window', 'reportDataService',
    function ($scope, expenseService, $routeParams, $filter, sharedService, authService, ngTableParams, $window, reportDataService) {

    $scope.expenses = [];

    
    $scope.expenseTypes = [];
    $scope.paymentTypes = [];
    $scope.accounts = [];

    $scope.expenseTypesWithAmount = [];
    $scope.paymentTypesWithAmount = [];
    $scope.accountsWithAmount = [];

    $scope.expenseSearch = {
        organizationId: "",
        expenseStartDate: "",
        expenseEndDate: "",
        expenseTypeId: "",
        accountId: "",
        orderById: ""
    };

    $scope.expenseTypeName = "";

    $scope.expenseType = $routeParams.expenseType;

    $scope.expenseSearch.organizationId = $routeParams.organizationId;
    $scope.expenseSearch.expenseTypeId = $routeParams.expenseTypeId;
    $scope.expenseSearch.expenseEndDate = $filter('date')(new Date($routeParams.expenseEndDate), 'MM/dd/yyyy');
    $scope.expenseSearch.expenseStartDate = $filter('date')(new Date($routeParams.expenseStartDate), 'MM/dd/yyyy');
    $scope.expenseSearch.orderById = $routeParams.orderById;
    $scope.expenseSearch.accountId = $routeParams.accountId;
    $scope.expenseTypeName = $routeParams.expenseTypeName;

    //$scope.expenses = reportDataService.expenseData;

    $scope.expense = {
        id: 0,
        organizationId: "",
        expenseTypeId: "",
        subExpenseTypeId: "",
        paymentTypeId: "",
        amount: "",
        expenseDate: "",
        transacitonNumber: "",
        notes: "",
        active: true,
        rowTimeStamp: ""
    }

    $scope.authentication = authService.authentication;

    $scope.initialize = function () {

        $scope.formLoad();
        $scope.load();
    };


    $scope.missionCompled = function () {
        $window.status = "Done";
    };

    $scope.formLoad = function () {
        $scope.tableParams = new ngTableParams({
            page: 1,   // show first page
            count: 5  // count per page
        }, {
            counts: [], // hide page counts control
            total: 1,  // value less than count hide pagination
            getData: function ($defer, params) {
                $defer.resolve($scope.expenses);
            }
        });
    }



    $scope.load = function () {        
        expenseService.report($scope.expenseSearch).then(function (results) {
            $scope.expenses = results.data;
            //$scope.loadSummary();
            $scope.tableParams.reload();

        }, function (error) {
            alert(error.data.message);
        });
    }


    $scope.loadSummary()
    {
        $scope.totalExpense = sharedService.sum($scope.expenses, 'amount');

        $scope.paymentTypesWithAmount = [];
        angular.forEach($scope.paymentTypes, function (item) {
            var filteredData = $filter('filter')($scope.expenses, { paymentTypeId: item.id });
            var total = sharedService.sum(filteredData, 'amount');
            $scope.paymentTypesWithAmount.push({ "id": item.id, "name": item.name, "total": total });
        });

        $scope.expenseTypesWithAmount = [];
        angular.forEach($scope.expenseTypes, function (item) {
            if ($scope.expenseSearch.expenseTypeId == null || $scope.expenseSearch.expenseTypeId == item.id) {
                var filteredData = $filter('filter')($scope.expenses, { expenseTypeId: item.id });
                var total = sharedService.sum(filteredData, 'amount');
                $scope.expenseTypesWithAmount.push({ "id": item.id, "name": item.name, "total": total });
            }
        });

        $scope.accountsWithAmount = [];
        angular.forEach($scope.accounts, function (item) {
            if ($scope.expenseSearch.accountId == null || $scope.expenseSearch.accountId == item.id) {
                var filteredData = $filter('filter')($scope.expenses, { accountId: item.id });
                var total = sharedService.sum(filteredData, 'amount');
                $scope.accountsWithAmount.push({ "id": item.id, "name": item.name, "total": total });
            }
        });
    }

}]);