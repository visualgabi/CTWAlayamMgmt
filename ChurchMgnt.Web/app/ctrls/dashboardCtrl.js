'use strict';
app.controller('dashboardCtrl', ['$scope', 'authService', 'dashboardService', 'familyService', 'offeringService', 'expenseService', 'depositService', 'eventService',
    function ($scope, authService, dashboardService, familyService, offeringService, expenseService, depositService, eventService) {

        $scope.recentMembers = [];
        $scope.recentVisitors = [];
        $scope.recentOfferings = [];
        $scope.recentDeposits = [];
        $scope.recentExpenses = [];
        $scope.recentEvents = [];
        
        $scope.loadingMember = true;
        $scope.loadingVisitor = true;
        $scope.loadingOffering = true;
        $scope.loadingDeposit = true;
        $scope.loadingExpense = true;
        $scope.loadingEvent = true;

        $scope.authentication = authService.authentication;
               

        if (authService.authentication == null) {
            $location.path('/login');
        }              

        $scope.formLoad = function () {
            familyService.getRecentVisitorsByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentVisitors = results.data;
                $scope.loadingVisitor = false;

            }, function (error) {
                alert(error.data.message);
            });


            familyService.getRecentMembersByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentMembers = results.data;
                $scope.loadingMember = false;

            }, function (error) {
                alert(error.data.message);
            });

            offeringService.getRecentOfferingsByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentOfferings = results.data;
                $scope.loadingOffering = false;

            }, function (error) {
                alert(error.data.message);
            });

            depositService.getRecentDepositsByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentDeposits = results.data;
                $scope.loadingDeposit = false;

            }, function (error) {
                alert(error.data.message);
            });

            expenseService.getRecentExpensesByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentExpenses = results.data;

                $scope.loadingExpense = false;

            }, function (error) {
                alert(error.data.message);
            });

            eventService.getRecentEventsByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentEvents = results.data;

                $scope.loadingEvent = false;

            }, function (error) {
                alert(error.data.message);
            });
        }


    }]);