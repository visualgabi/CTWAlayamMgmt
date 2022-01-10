'use strict';
app.controller('expenselstCtrl', ['$scope', 'lookupService', 'expenseService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'organizationService',
    function ($scope, lookupService, expenseService, $filter, sharedService, authService, ngTableParams, organizationService) {

        $scope.targets = [{ id: 1, name: "Branch" }, { id: 2, name: "Organization" }];       
        $scope.filterText; 
    
        $scope.authentication = authService.authentication;        

        $scope.disableEditAction = function (recordDate) {
            return new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy')) > new Date($filter('date')(recordDate, 'MM/d/yyyy'))
        };

        $scope.amountTitle = $scope.authentication.currency;      

     
        $scope.formLoad = function () {
            expenseService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.expenses = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.expenses.length, // length of data
                    getData: function ($defer, params) {

                        //  var fitleredData = $filter('filter')(data, filterText);

                        $scope.filteredData = $filter('filter')($scope.expenses, $scope.filterText);

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