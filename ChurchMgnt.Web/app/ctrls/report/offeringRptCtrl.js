'use strict';
app.controller('offeringRptCtrl', ['$scope', 'offeringService', '$filter', 'authService', '$window', 'downloadService', 'sharedService', 'lookupService','$location',
function ($scope, offeringService, $filter, authService, $window, downloadService, sharedService, lookupService,location) {
       
    $scope.showPrint = false;
    $scope.showExcel = false;
    

    $scope.showAlertForEmptyList = false;
    $scope.alertClassForEmptyList = "";
    $scope.alertMsgForEmptyList = "";
    

    $scope.offerings = [];
    $scope.orderBys = [{ id: 1, name: "Family Member" }, { id: 2, name: "Offering Type" }, { id: 3, name: "Fund Type" }, { id: 4, name: "Offering Date" }, { id: 5, name: "Amount" }];
    $scope.sources = [{ id: 1, name: "Family" }, { id: 2, name: "Sponsor" }];

    //$scope.fundTypes = [];
   // $scope.offeringTypes = [];
    $scope.paymentTypes = [];

    $scope.total = 0;
    $scope.sourceTypeSummary = [];
    $scope.fundTypeSummary = [];
    $scope.offeringTypeSummary = [];
    $scope.paymentTypeSummary = [];

    $scope.offeringSearch = {
        organizationId: "",
        startDate: "",
        endDate: "",
        orderById: "",
        sourceId: "",
        fundTypeId: "",
        offeringTypeId: ""
        
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
        var d = new Date($scope.offeringSearch.startDate);
        var startDate = new Date($scope.offeringSearch.startDate);

        d.setMonth(startDate.getMonth() + 12);

        return d;
    };


    $scope.isGreaterThanZero = function () {
        return function (item) {

            return item.total > 0;

        };
    };


    $scope.offering = {
        id: 0,
        organizationId: "",
        familyMemberId: "",
        offeringTypeId: "",
        fundTypeId: "",
        amount: "",
        offeringDate: "",
        transacitonNumber: "",
        notes: "",
        active: true,
        rowTimeStamp: "",
        sponsorId: "",
        sourceId: 1
    }

    //$scope.authentication = authService.authentication;


    $scope.downloadPdf = function () {

        $scope.filter = { startDate: $scope.offeringSearch.startDate, endDate: $scope.offeringSearch.endDate, source: "", fundType: "", offeringType: "", orderBy: "" };

        if ($scope.offeringSearch.fundTypeId == null || $scope.offeringSearch.fundTypeId == "")
            $scope.filter.fundType = "All";
        else
            $scope.filter.fundType = $filter('filter')($scope.fundTypes, { id: $scope.offeringSearch.fundTypeId }, true)[0].name;

        if ($scope.offeringSearch.offeringTypeId == "" || $scope.offeringSearch.offeringTypeId == null)
            $scope.filter.offeringType = "All";
        else
            $scope.filter.offeringType = $filter('filter')($scope.offeringTypes, { id: $scope.offeringSearch.offeringTypeId }, true)[0].name;

        if ($scope.offeringSearch.sourceId == "" || $scope.offeringSearch.sourceId == null)
            $scope.filter.source = "All";
        else
            $scope.filter.source = $filter('filter')($scope.sources, { id: $scope.offeringSearch.sourceId }, true)[0].name;

        if ($scope.offeringSearch.orderById == "" || $scope.offeringSearch.orderById === null)
            $scope.filter.orderBy = "Created date";
        else
            $scope.filter.orderBy = $filter('filter')($scope.orderBys, { id: $scope.offeringSearch.orderById }, true)[0].name;


        $scope.summaryBySourceType = $filter('filter')($scope.sourceTypeSummary, function (obj) {
            return obj.total > 0;
        });

        $scope.summaryByPayment = $filter('filter')($scope.paymentTypesSummary, function (obj) {
            return obj.total > 0;
        });

        $scope.summaryByFundType = $filter('filter')($scope.fundTypeSummary, function (obj) {
            return obj.total > 0;
        });

        $scope.summaryByOfferingType = $filter('filter')($scope.offeringTypeSummary, function (obj) {
            return obj.total > 0;
        });

        $scope.printObj = {
            currency: $scope.authentication.currency,
            totalOffering: $scope.total,
            SummaryBySourceTypes: $scope.summaryBySourceType,
            SummaryByPaymentTypes: $scope.summaryByPayment,
            SummaryByFundTypes: $scope.summaryByFundType,
            summaryByOfferingTypes: $scope.summaryByOfferingType,
            offerings: $scope.offerings,
            churchMgntURL: location.absUrl().split('#')[0],
            filter: $scope.filter,
            orgName: $scope.authentication.orgName
        }

        downloadService.printOfferingReport($scope.printObj).then(function (results) {
            var file = new Blob([results.data], { type: 'application/pdf' });
            var fileURL = URL.createObjectURL(file);
            $window.open(fileURL, '_blank');
        }, function (error) {
            alert(error)
        });

    };

    $scope.initialize = function () {        

        lookupService.getOfferingTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.offeringTypes = results.data;
        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getFundTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.fundTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getPaymentTypes(null).then(function (results) {
            $scope.paymentTypes = results.data;

        }, function (error) {
            showErroMessage(error)
        });
    }   

    $scope.search = function (reportForm) {
        if (reportForm.$valid) {
            $scope.offeringSearch.organizationId = $scope.authentication.orgId;

            offeringService.report($scope.offeringSearch).then(function (results) {
                $scope.offerings = results.data;                

                if ($scope.offerings.length > 0) {
                    $scope.showPrint = true;
                    $scope.showExcel = true;
                    $scope.alertClassForEmptyList = "";
                    $scope.showAlertForEmptyList = false;
                    $scope.alertMsgForEmptyList = "";

                    $scope.total = sharedService.sum($scope.offerings, 'amount');

                    $scope.paymentTypesSummary = [];
                    angular.forEach($scope.paymentTypes, function (item) {
                        var filteredData = $filter('filter')($scope.offerings, { paymentTypeId: item.id },true);
                        var total = sharedService.sum(filteredData, 'amount');
                        $scope.paymentTypesSummary.push({ "id": item.id, "name": item.name, "total": total });
                    });

                    $scope.sourceTypeSummary = [];
                    angular.forEach($scope.sources, function (item) {
                        if ($scope.offeringSearch.sourceId == "" || $scope.offeringSearch.sourceId == item.id) {
                            var filteredData

                            if (item.id == 1)
                                filteredData = $filter('filter')($scope.offerings, { sponsorId : null },true);
                            if (item.id == 2)
                                filteredData = $filter('filter')($scope.offerings, { familyMemberId: null },true);

                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.sourceTypeSummary.push({ "id": item.id, "name": item.name, "total": total });
                        }

                    });

                    $scope.offeringTypeSummary = [];
                    angular.forEach($scope.offeringTypes, function (item) {
                        if ($scope.offeringSearch.offeringTypeId == "" || $scope.offeringSearch.offeringTypeId == item.id) {
                            var filteredData = $filter('filter')($scope.offerings, { offeringTypeId: item.id },true);
                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.offeringTypeSummary.push({ "id": item.id, "name": item.name, "total": total });
                        }
                    });

                    $scope.fundTypeSummary = [];
                    angular.forEach($scope.fundTypes, function (item) {
                        if ($scope.offeringSearch.fundTypeId == "" || $scope.offeringSearch.fundTypeId == item.id) {
                            var filteredData = $filter('filter')($scope.offerings, { fundTypeId: item.id },true);
                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.fundTypeSummary.push({ "id": item.id, "name": item.name, "total": total });
                        }
                    });
                }
                else {
                    $scope.showPrint = false;
                    $scope.showExcel = false;
                    $scope.alertMsgForEmptyList = sharedService.getShortErrorMsgForEmptyList();
                    $scope.alertClassForEmptyList = "alert-danger";
                    $scope.showAlertForEmptyList = true;
                }

            }, function (error) {
                alert(error.data.message);
            });
        }
    }

    $scope.downloadExcel = function () {
        $scope.offeringExcel = [];

        angular.forEach($scope.offerings, function (item) {
            $scope.offeringExcel.push({ "Sponsor": item.sponsor, "FamilyMember": item.familyMember, "OfferingDate": item.offeringDate, "Amount": item.amount, "TransactionNumber": item.transactionNumber, "fundType": item.fundType, "offeringType": item.offeringType, "paymentType": item.paymentType });
        });

        sharedService.jsonToCSVConvertor($scope.offeringExcel, "Offering Report", true);
    }

    

}]);