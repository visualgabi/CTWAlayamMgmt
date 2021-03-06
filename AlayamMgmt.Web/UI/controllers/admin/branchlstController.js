'use strict';

app.controller('branchlstController', ['$scope', 'organizationService', 'ngTableParams', '$filter', 'sharedService', 'authService',
    function ($scope, organizationService, ngTableParams, $filter, sharedService, authService) {
   
    $scope.branches = [];
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.authentication = authService.authentication;

    $scope.formLoad = function () {
        organizationService.getBranchesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.branches = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.branches.length, // length of data
                getData: function ($defer, params) {

                    //  var fitleredData = $filter('filter')(data, filterText);

                    var filteredData = $filter('filter')($scope.branches, $scope.filterText);

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

    $scope.$watch("filterText", function () {
        $scope.tableParams.reload();
        $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
    });

    $scope.enableBranch = function (id, status) {

        var org = $filter('filter')($scope.branches, { id: id });

        organizationService.enableOrganization(org[0].id, status).then(function (results) {

            org[0].active = status;
            org[0].rowTimeStamp = results.data;

            $scope.alertClass = "alert-success";
            if (status == true)
                $scope.alertMsg = sharedService.getShortEnableSuccessMsg();
            else
                $scope.alertMsg = sharedService.getShortDisableSuccessMsg();

            $scope.showAlert = true;

            $scope.tableParams.reload();

        }, function (error) {

            $scope.alertClass = "alert-danger";
            $scope.alertMsg = sharedService.getShortErrorMsg();

            $scope.showAlert = true;

            $scope.tableParams.reload();
        });

    };


}]);

