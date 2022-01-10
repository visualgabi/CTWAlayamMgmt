'use strict';
app.controller('eventRptPrtController', ['$scope', 'eventService', '$routeParams', '$filter', 'authService', 'ngTableParams', '$window',
    function ($scope, eventService, $routeParams, $filter,  authService, ngTableParams, $window) {

    $scope.events = [];

    $scope.eventTypeName = "All";
    $scope.subEventTypeName = "All";

    $scope.eventSearch = {
        organizationId: "",
        eventStartDate: "",
        eventEndDate: "",
        eventTypeId: "",
        splEventTypeId: "",
        orderById: ""
    };

    $scope.eventType = $routeParams.eventType;

    $scope.eventSearch.organizationId = $routeParams.organizationId;
    $scope.eventSearch.eventTypeId = $routeParams.eventTypeId;
    $scope.eventSearch.splEventTypeId = $routeParams.splEventTypeId;
    $scope.eventSearch.eventEndDate = $filter('date')(new Date($routeParams.eventEndDate), 'MM/dd/yyyy');
    $scope.eventSearch.eventStartDate = $filter('date')(new Date($routeParams.eventStartDate), 'MM/dd/yyyy');
    $scope.eventSearch.orderById = $routeParams.orderById;
    $scope.eventTypeName = $routeParams.eventTypeName;
    $scope.subEventTypeName = $routeParams.subEventTypeName;

    $scope.event = {
        id: 0,
        branchId: "",
        eventTypeId: "",
        name: "",
        description: "",
        isSpecialEvent: "",
        noOfAdultAttended: "",
        noOfMenAttended: "",
        noOfWomenAttended: "",
        noOfKidsParticipated: "",
        offering: "",
        expense: "",
        eventDate: "",
        notes: "",
        active: true,
        rowTimeStamp: ""
    }

    //$scope.authentication = authService.authentication;

    $scope.initialize = function () {

        $scope.formLoad();
        $scope.load();
    };


    $scope.missionCompled = function () {
        $window.status = "Done";
    };

    $scope.formLoad = function () {
        $scope.tableParams = new ngTableParams({
            page: 1,   // show first page
            count: 5  // count per page
        }, {
            counts: [], // hide page counts control
            total: 1,  // value less than count hide pagination
            getData: function ($defer, params) {
                $defer.resolve($scope.events);
            }
        });
    }



    $scope.load = function () {
        eventService.report($scope.eventSearch).then(function (results) {
            $scope.events = results.data;
            $scope.tableParams.reload();

        }, function (error) {
            alert(error.data.message);
        });
    }

}]);
