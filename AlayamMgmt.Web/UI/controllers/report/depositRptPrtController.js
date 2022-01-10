'use strict';
app.controller('depositRptPrtController', ['$scope', 'depositService', '$routeParams', '$filter', 'ngTableParams', '$window',
    function ($scope, depositService, $routeParams, $filter, ngTableParams, $window) {

    $scope.deposits = [];
    $scope.accountName ="All"

    $scope.depositSearch = {
        organizationId: "",
        depositStartDate: "",
        depositEndDate: "",
        userId: "",
        accountId: "",
        orderById: ""
    };

    $scope.depositType = $routeParams.depositType;

    $scope.depositSearch.organizationId = $routeParams.organizationId;
    $scope.depositSearch.userId = $routeParams.userId;
    $scope.depositSearch.accountId = $routeParams.accountId;
    $scope.depositSearch.depositEndDate = $filter('date')(new Date($routeParams.depositEndDate), 'MM/dd/yyyy');
    $scope.depositSearch.depositStartDate = $filter('date')(new Date($routeParams.depositStartDate), 'MM/dd/yyyy');
    $scope.depositSearch.orderById = $routeParams.orderById;
    $scope.accountName = $routeParams.accountName;

    $scope.deposit = {
        id: 0,
        accountId: "",
        userId: "",
        description: "",
        amount: "",
        depositDate: "",
        location: "",
        active: true,
        rowTimeStamp: ""
    }

    //$scope.authentication = authService.authentication;

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
                $defer.resolve($scope.deposits);
            }
        });
    }



    $scope.load = function () {
        depositService.report($scope.depositSearch).then(function (results) {
            $scope.deposits = results.data;
            $scope.tableParams.reload();

        }, function (error) {
            alert(error.data.message);
        });
    }

}]);
