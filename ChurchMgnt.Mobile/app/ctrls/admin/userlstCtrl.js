'use strict';
app.controller('userlstCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'userService', 'roleService', 'organizationService',
    function ($scope, $filter, sharedService, authService, ngTableParams, userService, roleService, organizationService) {

        $scope.users = [];        

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";        

    $scope.authentication = authService.authentication;
  

    $scope.formLoad = function () {

        if ($scope.authentication.role == 1) {
            userService.get(null).then(function (results) {
                $scope.users = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.users.length, // length of data
                    getData: function ($defer, params) {

                        $scope.filteredData = $filter('filter')($scope.users, $scope.filterText);

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
        else
        {
            userService.get($scope.authentication.orgId, null).then(function (results) {
                $scope.users = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.users.length, // length of data
                    getData: function ($defer, params) {

                        var filteredData = $filter('filter')($scope.users, $scope.filterText);

                        var orderedData = params.sorting() ?
                       $filter('orderBy')(filteredData, params.orderBy()) :
                       filteredData;

                        params.total(orderedData.length);
                        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                    }
                })

            }, function (error) {
                alert(error.data.message);
            });
        }
    }

    $scope.$watch("filterText", function () {
        $scope.tableParams.reload();
        $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
    });    
}]);