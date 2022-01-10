'use strict';
app.controller('offeringRptPrtController', ['$scope', 'offeringService', '$routeParams', '$filter', 'ngTableParams', '$window',
    function ($scope, offeringService, $routeParams, $filter, ngTableParams, $window) {
           
    $scope.offerings = [];

    $scope.sourceName = "";

    $scope.offeringSearch = {
        organizationId: "",
        startDate: "",
        endDate: "",        
        orderById: "",
        sourceId : ""
    };   

    $scope.offeringSearch.organizationId = $routeParams.organizationId;    
    $scope.offeringSearch.endDate = $filter('date')(new Date($routeParams.endDate), 'MM/dd/yyyy');
    $scope.offeringSearch.startDate = $filter('date')(new Date($routeParams.startDate), 'MM/dd/yyyy');
    $scope.offeringSearch.orderById = $routeParams.orderById;
    $scope.offeringSearch.sourceId = $routeParams.sourceId;
    $scope.sourceName = $routeParams.sourceName;    

    
    $scope.initialize = function () {
        $scope.formLoad();
        $scope.load();
    };

    $scope.formLoad = function () {
        $scope.tableParams = new ngTableParams({
            page: 1,   // show first page
            count: 5  // count per page
        }, {
            counts: [], // hide page counts control
            total: 1,  // value less than count hide pagination
            getData: function ($defer, params) {
                $defer.resolve($scope.offerings);
            }
        });
    }

    $scope.load = function () {
        offeringService.report($scope.offeringSearch).then(function (results) {
            $scope.offerings = results.data;
            $scope.tableParams.reload();

        }, function (error) {
            alert(error.data.message);
        });
    }

}]);