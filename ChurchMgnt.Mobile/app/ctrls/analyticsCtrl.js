'use strict';
app.controller('analyticsCtrl', ['$scope', 'authService', 'dashboardService',
    function ($scope, authService, dashboardService) {   

        $scope.authentication = authService.authentication;
        
        $scope.loadingExpenseRpt = true;
        $scope.loadingBudgetRpt = true;
        $scope.loadingOfferingRpt = true;
        $scope.loadingMemVisRpt = true;

        if (authService.authentication == null) {
            $location.path('/login');
        }

        $scope.getMemberVisitorCountGraph = function () {

            dashboardService.getMemberVisitorCountGraph($scope.authentication.orgId).then(function (results) {
                $scope.memberVisitorCountGraphModel = results.data;                

                $scope.setupMemberVisitorCountGraph();

                $scope.loadingMemVisRpt = false;

            }, function (error) {
                showErroMessage(error)
            });
        }

        $scope.getFundTypeOfferingGraph = function () {

            dashboardService.getFundTypeOfferingGraph($scope.authentication.orgId).then(function (results) {
                $scope.fundTypeOfferingGraphModel = results.data;

                $scope.setupFundTypeOfferingGraph();

                $scope.loadingOfferingRpt = false;

            }, function (error) {
                showErroMessage(error)
            });
        }


        $scope.getBudgetGraph = function () {

            dashboardService.getBudgetGraph($scope.authentication.orgId).then(function (results) {

                $scope.budgetGraphModel = results.data.budgets;                

                $scope.setupBudgetGraphModel();

                $scope.loadingBudgetRpt = false;

            }, function (error) {
                showErroMessage(error)
            });
        }

        $scope.getExpenseGraph = function () {

            dashboardService.getExpenseGraph($scope.authentication.orgId).then(function (results) {

                $scope.expenseGraphModel = results.data.expenses;

                $scope.setupExpenseGraphGraph();

                $scope.loadingExpenseRpt = false;

            }, function (error) {
                showErroMessage(error)
            });
        }

        $scope.formLoad = function () {

            $scope.getExpenseGraph();
            $scope.getFundTypeOfferingGraph();
            $scope.getMemberVisitorCountGraph();
            $scope.getBudgetGraph();
        }        
        

        $scope.setupMemberVisitorCountGraph = function () {

            $scope.lineOptions = {
                scaleShowGridLines: true,
                scaleGridLineColor: "rgba(0,0,0,.05)",
                scaleGridLineWidth: 1,
                bezierCurve: true,
                bezierCurveTension: 0.4,
                pointDot: true,
                pointDotRadius: 4,
                pointDotStrokeWidth: 1,
                pointHitDetectionRadius: 20,
                datasetStroke: true,
                datasetStrokeWidth: 1,
                datasetFill: true
            };


            $scope.lineData = {
                labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                datasets: [
                    {
                        label: "Members",
                        fillColor: "rgba(220,220,220,0.5)",
                        strokeColor: "rgba(220,220,220,1)",
                        pointColor: "rgba(220,220,220,1)",
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: "rgba(220,220,220,1)",
                        data: $scope.memberVisitorCountGraphModel.memberListCount
                    },
                    {
                        label: "Visitors",
                        fillColor: "rgba(98,203,49,0.5)",
                        strokeColor: "rgba(98,203,49,0.7)",
                        pointColor: "rgba(98,203,49,1)",
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: "rgba(26,179,148,1)",
                        data: $scope.memberVisitorCountGraphModel.visitorListCount
                    }
                ]
            };
        }

        $scope.setupFundTypeOfferingGraph = function () {

            $scope.barOptions = {
                scaleBeginAtZero: true,
                scaleShowGridLines: true,
                scaleGridLineColor: "rgba(0,0,0,.05)",
                scaleGridLineWidth: 1,
                barShowStroke: true,
                barStrokeWidth: 1,
                barValueSpacing: 5,
                barDatasetSpacing: 1,
                legend: { textStyle: { fontSize: 10 } }
            };

            $scope.barData = {
                labels: $scope.fundTypeOfferingGraphModel.fundTypes,
                datasets: [
                    {
                        label: "Target",
                        fillColor: "rgba(220,220,220,0.5)",
                        strokeColor: "rgba(220,220,220,0.8)",
                        highlightFill: "rgba(220,220,220,0.75)",
                        highlightStroke: "rgba(220,220,220,1)",
                        data: $scope.fundTypeOfferingGraphModel.budgets
                    },
                    {
                        label: "Received",
                        fillColor: "rgba(98,203,49,0.5)",
                        strokeColor: "rgba(98,203,49,0.8)",
                        highlightFill: "rgba(98,203,49,0.75)",
                        highlightStroke: "rgba(98,203,49,1)",
                        data: $scope.fundTypeOfferingGraphModel.offerings
                    }
                ]
            };
        }

        $scope.setupBudgetGraphModel = function () {

            $scope.pieOptions = {
                segmentShowStroke: true,
                segmentStrokeColor: "#fff",
                segmentStrokeWidth: 1,
                percentageInnerCutout: 0, // This is 0 for Pie charts
                animationSteps: 100,
                animationEasing: "easeOutBounce",
                animateRotate: true,
                animateScale: false,
            };
        }

        $scope.setupExpenseGraphGraph = function () {

            $scope.doughnutOptions = {
                segmentShowStroke: true,
                segmentStrokeColor: "#fff",
                segmentStrokeWidth: 1,
                percentageInnerCutout: 45, // This is 0 for Pie charts
                animationSteps: 100,
                animationEasing: "easeOutBounce",
                animateRotate: true,
                animateScale: false,
            };
        }      
      
    }]);