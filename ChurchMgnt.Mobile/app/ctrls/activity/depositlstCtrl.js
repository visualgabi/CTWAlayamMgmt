'use strict';
app.controller('depositlstCtrl', ['$scope', 'lookupService', 'depositService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'userService',
    function ($scope, lookupService, depositService, $filter, sharedService, authService, ngTableParams, userService) {

        $scope.deposits = [];        
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
       

        $scope.authentication = authService.authentication;
        $scope.amountTitle = $scope.authentication.currency;

     
        $scope.formLoad = function () {
            depositService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.deposits = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.deposits.length, // length of data
                    getData: function ($defer, params) {

                        $scope.filteredData = $filter('filter')($scope.deposits, $scope.filterText);

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