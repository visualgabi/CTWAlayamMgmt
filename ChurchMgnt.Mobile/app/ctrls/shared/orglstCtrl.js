'use strict';

app.controller('orglstCtrl', ['$scope', 'organizationService', 'ngTableParams', '$filter', 'sharedService',
    function ($scope, organizationService, ngTableParams, $filter, sharedService) {
 
    $scope.orgs = [];
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";    

    $scope.formLoad = function () {
        organizationService.getOrganizations(null).then(function (results) {         
            $scope.orgs = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.orgs.length, // length of data
                getData: function ($defer, params) {

                    //  var fitleredData = $filter('filter')(data, filterText);

                    $scope.filteredData = $filter('filter')($scope.orgs, $scope.filterText);

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

    $scope.enableOrg = function (id, status) {        

        var org = $filter('filter')($scope.orgs, { id: id });
        
        organizationService.enableOrganization(org[0].id, status).then(function (results) {

            org[0].active = status;
            org[0].rowTimeStamp = results.data;

            $scope.alertClass = "alert-success";
            if (status == true)
                $scope.alertMsg = sharedService.getEnableSuccessMsg("Organization");
            else
                $scope.alertMsg = sharedService.getDisableSuccessMsg("Organization");

            $scope.showAlert = true;

            $scope.tableParams.reload();           

        }, function (error) {

            $scope.alertClass = "alert-danger";
            $scope.alertMsg = sharedService.getErrorMsg(error.data, org.name);

            $scope.showAlert = true;

            $scope.tableParams.reload();            
        });      
       
    };
   

}]);

