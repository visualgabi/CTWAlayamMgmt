'use strict';
app.controller('offeringlstCtrl', ['$scope', 'lookupService', 'offeringService', 'familyMemberService', '$filter', 'sharedService', 'authService','ngTableParams', 'sponsorService',
    function ($scope, lookupService, offeringService, familyMemberService, $filter, sharedService, authService, ngTableParams, sponsorService) {
       
    $scope.sources = [{ id: 1, name: "Family" }, { id: 2, name: "Sponsor" }];    

    $scope.offerings = [];    
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.disableEditAction = function (recordDate) {
        return new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy')) > new Date($filter('date')(recordDate, 'MM/d/yyyy'))
    };    

    $scope.authentication = authService.authentication;
    $scope.amountTitle = $scope.authentication.currency;        

    $scope.formLoad = function () {
        offeringService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.offerings = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.offerings.length, // length of data
                getData: function ($defer, params) {                 

                    $scope.filteredData = $filter('filter')($scope.offerings, $scope.filterText);

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

    
    $scope.clearAlert = function () {
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";       
    }

    $scope.enable = function (id, status) {        

        var orgOfferings = $filter('filter')($scope.offerings, { id: id });        
        
        offeringService.enable(orgOfferings[0].id, status).then(function (results) {

            orgOfferings[0].active = status;
            orgOfferings[0].rowTimeStamp = results.data;

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