'use strict';
app.controller('eventlstCtrl', ['$scope', 'lookupService', 'eventService', 'organizationService', '$filter', 'sharedService', 'authService', 'ngTableParams',
    function ($scope, lookupService, eventService, organizationService, $filter, sharedService, authService, ngTableParams) {


        $scope.events = [];          
        $scope.filterText;                        
        

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

       

        $scope.expenseTitle = 'Expense in ' + $scope.authentication.currency;
        $scope.offeringTitle = 'Offering in ' + $scope.authentication.currency;

        $scope.authentication = authService.authentication;
        $scope.expenseTitle = 'Expense in ' + $scope.authentication.currency;
        $scope.offeringTitle = 'Offering in ' + $scope.authentication.currency;


        $scope.formLoad = function () {
            eventService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.events = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.events.length, // length of data
                    getData: function ($defer, params) {

                        $scope.filteredData = $filter('filter')($scope.events, $scope.filterText);

                        var orderedData = params.sorting() ?
                       $filter('orderBy')($scope.filteredData, params.orderBy()) :
                       $scope.filteredData;

                        params.total(orderedData.length);
                        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                    }
                })

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.$watch("filterText", function () {
            $scope.tableParams.reload();
            $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
        });

    }]);