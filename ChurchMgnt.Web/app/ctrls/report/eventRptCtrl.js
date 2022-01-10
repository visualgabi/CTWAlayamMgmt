'use strict';
app.controller('eventRptCtrl', ['$scope', 'lookupService', 'eventService', '$filter', 'sharedService', 'authService', '$window', 'ngAuthSettings',
    function ($scope, lookupService, eventService, $filter, sharedService, authService, $window, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    $scope.showPrint = false;

    $scope.showAlertForEmptyList = false;
    $scope.alertClassForEmptyList = "";
    $scope.alertMsgForEmptyList = "";


    $scope.events = [];
    //$scope.eventTypes = [];
    $scope.orderBys = [{ id: 1, name: "Branch" }, { id: 2, name: "Event Type" }, { id: 3, name: "Offering" }, { id: 4, name: "Expense" }];
    $scope.splEventTypes = [{ id: 1, name: "Regular Event" }, { id: 2, name: "Spl Event" }];

    $scope.eventStartDate = "";
    $scope.eventEndDate = "";

    $scope.eventSearch = {
        organizationId: "",
        eventStartDate: "",
        eventEndDate: "",
        eventTypeId: "",
        splEventTypeId: "",
        orderById : ""
    }

    $scope.todayDate = function () {
        return new Date();
    };

    $scope.rptMinDate = function () {
        var d = new Date();

        d.setFullYear(d.getFullYear() - 5);

        return d;
    };

    $scope.maxDate = function () {
        var d = new Date($scope.eventSearch.eventStartDate);
        var startDate = new Date($scope.eventSearch.eventStartDate);

        d.setMonth(startDate.getMonth() + 12);

        return d;
    };


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

    $scope.authentication = authService.authentication;

    $scope.total = 0;
    $scope.splTotal = 0;
    $scope.regularTotal = 0;

    $scope.avgAdultCount = 0
    $scope.splAvgAdultCount = 0;
    $scope.regulAvgAdultCount = 0;

    $scope.eventSummary = [];


    $scope.print = function () {
        //// your code here
        //// PDFConveterService.print("#/expenseRptPrt/" + $scope.expenseSearch.organizationId + "/" + $scope.expenseSearch.expenseTypeId + "/" + $scope.expenseSearch.expenseStartDate + "/" + $scope.expenseSearch.expenseEndDate);
        //var orderById = "", splEventTypeId;
        //if ($scope.eventSearch.orderById == "")
        //    orderById = null;
        //else
        //    orderById = $scope.eventSearch.orderById;

        //if ($scope.eventSearch.splEventTypeId == "")
        //    splEventTypeId = null;
        //else
        //    splEventTypeId = $scope.eventSearch.splEventTypeId;

        //var eventTypeName = "All";
        //var subEventTypeName = "All";

        //if ($scope.eventSearch.splEventTypeId != "")
        //    eventTypeName = $filter('filter')($scope.splEventTypes, { id: $scope.eventSearch.splEventTypeId }, true)[0].name;

        //if ($scope.eventSearch.eventTypeId != null)
        //    subEventTypeName = $filter('filter')($scope.eventTypes, { id: $scope.eventSearch.eventTypeId }, true)[0].name;

        //PDFConveterService.print(serviceBase + "#/eventRptPrt/" + $scope.eventSearch.organizationId + "/" + $scope.eventSearch.eventTypeId + "/" + splEventTypeId + "/" + $scope.eventSearch.eventStartDate + "/" + $scope.eventSearch.eventEndDate + "/" + orderById + "/" + eventTypeName + "/" + subEventTypeName).then(function (results) {
        //    var file = new Blob([results.data], { type: 'application/pdf' });
        //    var fileURL = URL.createObjectURL(file);
        //    $window.open(fileURL, '_blank');
        //}, function (error) {
        //    alert(error)
        //});

        //$window.open("#/expenseRptPrt/" + $scope.expenseSearch.organizationId + "/" + $scope.expenseSearch.expenseTypeId + "/" + $scope.expenseSearch.expenseStartDate + "/" + $scope.expenseSearch.expenseEndDate);
        //$location.path("/expenseRptPrt/" + $scope.expenseSearch.organizationId + "/" + $scope.expenseSearch.expenseTypeId+ "/" + $scope.expenseSearch.expenseStartDate + "/" + $scope.expenseSearch.expenseEndDate);
        // $location.path("/expenseRptPrt/" + $scope.expenseSearch.organizationId); //+ "/" + $filter('date')($scope.expenseSearch.expenseStartDate, 'MM/dd/yyyy') + "/" + $filter('date')($scope.expenseSearch.expenseEndDate, 'MM/d/yyyy'));

    };

    $scope.initialize = function () {

        lookupService.getEventTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.eventTypes = results.data;
        }, function (error) {
            showErroMessage(error)
        });       

    };

   

    $scope.search = function (reportForm) {

        if (reportForm.$valid) {
            $scope.eventSearch.organizationId = $scope.authentication.orgId;
            $scope.eventSearch.eventTypeId = $scope.eventSearch.eventTypeId == "" ? null : $scope.eventSearch.eventTypeId;

            eventService.report($scope.eventSearch).then(function (results) {
                $scope.events = results.data;                

                if ($scope.events.length > 0) {
                    $scope.showPrint = true;
                    $scope.alertClassForEmptyList = "";
                    $scope.showAlertForEmptyList = false;
                    $scope.alertMsgForEmptyList = "";

                    $scope.eventTotal = $scope.events.length;
                    $scope.splEventTotal = $filter('filter')($scope.events, { isSpecialEvent: true }, true).length;
                    $scope.regEventTotal = $filter('filter')($scope.events, { isSpecialEvent: false }, true).length;

                    var splEvents = $filter('filter')($scope.events, { isSpecialEvent: true }, true)
                    var regEvents = $filter('filter')($scope.events, { isSpecialEvent: false }, true)

                    $scope.eventSummary = [];

                    if ($scope.eventSearch.splEventTypeId == "") {
                        $scope.avgAdultCount = (sharedService.sum($scope.events, 'noOfAdultAttended')) / $scope.events.length;
                        $scope.avgMenCount = sharedService.sum($scope.events, 'noOfMenAttended') / $scope.events.length;
                        $scope.avgWomenCount = sharedService.sum($scope.events, 'noOfWomenAttended') / $scope.events.length;
                        $scope.avgKidsCount = sharedService.sum($scope.events, 'noOfKidsParticipated') / $scope.events.length;
                        $scope.offeringTotal = sharedService.sum($scope.events, 'offering');
                        $scope.expenseTotal = sharedService.sum($scope.events, 'expense');
                        $scope.offeringAvg = sharedService.sum($scope.events, 'offering') / $scope.events.length;
                        $scope.expenseAvg = sharedService.sum($scope.events, 'expense') / $scope.events.length;

                        $scope.eventSummary.push({
                            "type": "All Events", "noOfEvent": $scope.eventTotal, "avgAdultCount": $scope.avgAdultCount, "avgMenCount": $scope.avgMenCount, "avgWomenCount": $scope.avgWomenCount,
                            "avgKidsCount": $scope.avgKidsCount, "offeringTotal": $scope.offeringTotal, "expenseTotal": $scope.expenseTotal, "offeringAvg": $scope.offeringAvg, "expenseAvg": $scope.expenseAvg
                        })
                    }

                    if ($scope.eventSearch.splEventTypeId == "" || $scope.eventSearch.splEventTypeId == 1) {
                        $scope.regAvgAdultCount = (sharedService.sum(regEvents, 'noOfAdultAttended')) / regEvents.length;
                        $scope.regAvgMenCount = sharedService.sum(regEvents, 'noOfMenAttended') / regEvents.length;
                        $scope.regAvgWomenCount = sharedService.sum(regEvents, 'noOfWomenAttended') / regEvents.length;
                        $scope.regAvgKidsCount = sharedService.sum(regEvents, 'noOfKidsParticipated') / regEvents.length;
                        $scope.regOfferingTotal = sharedService.sum(regEvents, 'offering')
                        $scope.regExpenseTotal = sharedService.sum(regEvents, 'expense')
                        $scope.regOfferingAvg = sharedService.sum(regEvents, 'offering') / regEvents.length;
                        $scope.regExpenseAvg = sharedService.sum(regEvents, 'expense') / regEvents.length;

                        $scope.eventSummary.push({
                            "type": "RegEvent", "noOfEvent": $scope.regEventTotal, "avgAdultCount": $scope.regAvgAdultCount, "avgMenCount": $scope.regAvgMenCount, "avgWomenCount": $scope.regAvgWomenCount,
                            "avgKidsCount": $scope.regAvgKidsCount, "offeringTotal": $scope.regOfferingTotal, "expenseTotal": $scope.regExpenseTotal, "offeringAvg": $scope.regOfferingAvg, "expenseAvg": $scope.regExpenseAvg
                        })
                    }

                    if ($scope.eventSearch.splEventTypeId == "" || $scope.eventSearch.splEventTypeId == 2) {
                        $scope.splAvgAdultCount = (sharedService.sum(splEvents, 'noOfAdultAttended')) / splEvents.length;
                        $scope.splAvgMenCount = sharedService.sum(splEvents, 'noOfMenAttended') / splEvents.length;
                        $scope.splAvgWomenCount = sharedService.sum(splEvents, 'noOfWomenAttended') / splEvents.length;
                        $scope.splAvgKidsCount = sharedService.sum(splEvents, 'noOfKidsParticipated') / splEvents.length;
                        $scope.splOfferingTotal = sharedService.sum(splEvents, 'offering');
                        $scope.splExpenseTotal = sharedService.sum(splEvents, 'expense');
                        $scope.splOfferingAvg = sharedService.sum(splEvents, 'offering') / splEvents.length;
                        $scope.splExpenseAvg = sharedService.sum(splEvents, 'expense') / splEvents.length;

                        $scope.eventSummary.push({
                            "type": "SplEvent", "noOfEvent": $scope.splEventTotal, "avgAdultCount": $scope.splAvgAdultCount, "avgMenCount": $scope.splAvgMenCount, "avgWomenCount": $scope.splAvgWomenCount,
                            "avgKidsCount": $scope.splAvgKidsCount, "offeringTotal": $scope.splOfferingTotal, "expenseTotal": $scope.splExpenseTotal, "offeringAvg": $scope.splOfferingAvg, "expenseAvg": $scope.splExpenseAvg
                        })
                    }



                    //$scope.eventSummary = [
                    //                    {
                    //                        "type": "All Events", "noOfEvent": $scope.eventTotal, "avgAdultCount": $scope.avgAdultCount, "avgMenCount": $scope.avgMenCount, "avgWomenCount": $scope.avgWomenCount,
                    //                        "avgKidsCount": $scope.avgKidsCount, "offeringTotal": $scope.offeringTotal, "expenseTotal": $scope.expenseTotal, "offeringAvg": $scope.offeringAvg, "expenseAvg": $scope.expenseAvg
                    //                    },
                    //                    {
                    //                        "type": "SplEvent", "noOfEvent": $scope.splEventTotal, "avgAdultCount": $scope.splAvgAdultCount, "avgMenCount": $scope.splAvgMenCount, "avgWomenCount": $scope.splAvgWomenCount,
                    //                        "splAvgKidsCount": $scope.splAvgKidsCount, "splOfferingTotal": $scope.splOfferingTotal, "splExpenseTotal": $scope.splExpenseTotal, "splOfferingAvg": $scope.splOfferingAvg, "splExpenseAvg": $scope.splExpenseAvg
                    //                    },
                    //                    {
                    //                        "type": "RegEvent", "noOfEvent": $scope.regEventTotal, "avgAdultCount": $scope.regAvgAdultCount, "avgMenCount": $scope.regAvgMenCount, "avgWomenCount": $scope.regAvgWomenCount,
                    //                        "regAvgKidsCount": $scope.regAvgKidsCount, "regOfferingTotal": $scope.regOfferingTotal, "regExpenseTotal": $scope.regExpenseTotal, "regOfferingAvg": $scope.regOfferingAvg, "regExpenseAvg": $scope.regExpenseAvg
                    //                    }
                    //]


                }
                else {
                    $scope.showPrint = false;
                    $scope.alertMsgForEmptyList = sharedService.getShortErrorMsgForEmptyList();
                    $scope.alertClassForEmptyList = "alert-danger";
                    $scope.showAlertForEmptyList = true;
                }

            }, function (error) {
                alert(error.data.message);
            });
        }

        
    }

}]);