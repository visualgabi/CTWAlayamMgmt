'use strict';
app.controller('homeController', ['$scope', 'authService', 'dashboardService', 'familyService', 'offeringService', 'expenseService', 'depositService', 'eventService',
    function ($scope, authService, dashboardService, familyService, offeringService, expenseService, depositService, eventService) {

        $scope.recentMembers = [];
        $scope.recentVisitors = [];
        $scope.recentOfferings = [];
        $scope.recentDeposits = [];
        $scope.recentExpenses = [];
        $scope.recentEvents = [];

        $scope.authentication = authService.authentication;
        $scope.loadingRecentMembers = true;
        $scope.loadingRecentVisitors = true;
        $scope.loadingExpenseRpt = true;
        $scope.loadingBudgetRpt = true;
        $scope.loadingOfferingRpt = true;
        $scope.loadingMemVisRpt = true;


        $scope.loadingRecentOfferings = true;
        $scope.loadingRecentExpenses = true;
        $scope.loadingRecentDeposits = true;
        $scope.loadingRecentEvents = true;

        if (authService.authentication == null) {
            $location.path('/login');
        }

        $('.nav-tabs a').click(function (e) {
            e.preventDefault();
            $(this).tab('show');
        });

        dashboardService.getByOrgId($scope.authentication.orgId, true).then(function (results) {
            $scope.dashboradData = results.data;
            $scope.memVisCntChartData = [$scope.dashboradData.memberListCount, $scope.dashboradData.visitorListCount];
            $scope.budgetChartLabels = $scope.dashboradData.fundTypes;
            $scope.budgetChartData = $scope.dashboradData.budgets;
            $scope.expenseChartLabels = $scope.dashboradData.expenseTypes;
            $scope.expenseChartData = $scope.dashboradData.expenses;
            $scope.receivedChartData = [$scope.dashboradData.budgets, $scope.dashboradData.offerings];
            $scope.receivedChartLabels = $scope.dashboradData.fundTypes;


            $scope.loadingExpenseRpt = false;
            $scope.loadingBudgetRpt = false;
            $scope.loadingOfferingRpt = false;
            $scope.loadingMemVisRpt = false;

        }, function (error) {
            showErroMessage(error)
        });

        $scope.memVisCntChartLabels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dev"];
        $scope.memVisCntChartSeries = ['Members', 'Visitors'];
        $scope.onMemVisCntChartClick = function (points, evt) {
            console.log(points, evt);
        };

        
        $scope.receivedChartSeries = ['Target', 'Received'];

        $scope.formLoad = function () {
            familyService.getRecentVisitorsByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentVisitors = results.data;
                $scope.loadingRecentVisitors = false;

            }, function (error) {
                alert(error.data.message);
            });


            familyService.getRecentMembersByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentMembers = results.data;
                $scope.loadingRecentMembers = false;

            }, function (error) {
                alert(error.data.message);
            });

            offeringService.getRecentOfferingsByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentOfferings = results.data;
                $scope.loadingRecentOfferings = false;

            }, function (error) {
                alert(error.data.message);
            });

            depositService.getRecentDepositsByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentDeposits = results.data;
                $scope.loadingRecentDeposits = false;

            }, function (error) {
                alert(error.data.message);
            });

            expenseService.getRecentExpensesByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentExpenses = results.data;

                $scope.loadingRecentExpenses = false;

            }, function (error) {
                alert(error.data.message);
            });

            eventService.getRecentEventsByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.recentEvents = results.data;

                $scope.loadingRecentEvents = false;

            }, function (error) {
                alert(error.data.message);
            });
        }


    }]);