'use strict';
app.controller('offeringRptController', ['$scope', 'offeringService', '$filter', 'authService', '$window', 'PDFConveterService','sharedService','lookupService',
function ($scope, offeringService, $filter, authService, $window, PDFConveterService, sharedService, lookupService) {
       
    $scope.showPrint = false;

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

        d.setMonth(startDate.getMonth() + 6);

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


    $scope.print = function () {
        // your code here
        // PDFConveterService.print("#/expenseRptPrt/" + $scope.expenseSearch.organizationId + "/" + $scope.expenseSearch.expenseTypeId + "/" + $scope.expenseSearch.expenseStartDate + "/" + $scope.expenseSearch.expenseEndDate);
        
        var orderById = "";
        if ($scope.offeringSearch.orderById == "")
            orderById = null;
        else
            orderById = $scope.offeringSearch.orderById;

        var sourceById = null;
        var sourceName = "All";

        if ( ! (angular.isUndefined($scope.offeringSearch.sourceById) || $scope.offeringSearch.sourceById == null) )
         {
            sourceById = $scope.offeringSearch.sourceById;            
            sourceName = $filter('filter')($scope.sources, { id: $scope.offeringSearch.sourceById })[0].name;
        }       
            

        PDFConveterService.print("#/offeringRptPrt/" + $scope.offeringSearch.organizationId + "/" + $scope.offeringSearch.startDate + "/" + $scope.offeringSearch.endDate + "/" + orderById + "/" + sourceById + "/" + sourceName).then(function (results) {
            var file = new Blob([results.data], { type: 'application/pdf' });
            var fileURL = URL.createObjectURL(file);
            $window.open(fileURL);
        }, function (error) {
            alert(error)
        });

       // $window.open("#/offeringRptPrt/" + $scope.offeringSearch.organizationId + "/" + $scope.offeringSearch.offeringStartDate + "/" + $scope.offeringSearch.offeringEndDate);
        //$location.path("/expenseRptPrt/" + $scope.expenseSearch.organizationId + "/" + $scope.expenseSearch.expenseTypeId+ "/" + $scope.expenseSearch.expenseStartDate + "/" + $scope.expenseSearch.expenseEndDate);
        // $location.path("/expenseRptPrt/" + $scope.expenseSearch.organizationId); //+ "/" + $filter('date')($scope.expenseSearch.expenseStartDate, 'MM/dd/yyyy') + "/" + $filter('date')($scope.expenseSearch.expenseEndDate, 'MM/d/yyyy'));

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
                    $scope.alertClassForEmptyList = "";
                    $scope.showAlertForEmptyList = false;
                    $scope.alertMsgForEmptyList = "";

                    $scope.total = sharedService.sum($scope.offerings, 'amount');

                    $scope.paymentTypesSummary = [];
                    angular.forEach($scope.paymentTypes, function (item) {
                        var filteredData = $filter('filter')($scope.offerings, { paymentTypeId: item.id });
                        var total = sharedService.sum(filteredData, 'amount');
                        $scope.paymentTypesSummary.push({ "id": item.id, "name": item.name, "total": total });
                    });

                    $scope.sourceTypeSummary = [];
                    angular.forEach($scope.sources, function (item) {
                        if ($scope.offeringSearch.sourceId == "" || $scope.offeringSearch.sourceId == item.id) {
                            var filteredData

                            if (item.id == 1)
                                filteredData = $filter('filter')($scope.offerings, { familyMemberId: '!null' });
                            if (item.id == 2)
                                filteredData = $filter('filter')($scope.offerings, { sponsorId: '!null' });

                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.sourceTypeSummary.push({ "id": item.id, "name": item.name, "total": total });
                        }

                    });

                    $scope.offeringTypeSummary = [];
                    angular.forEach($scope.offeringTypes, function (item) {
                        if ($scope.offeringSearch.offeringTypeId == "" || $scope.offeringSearch.offeringTypeId == item.id) {
                            var filteredData = $filter('filter')($scope.offerings, { offeringTypeId: item.id });
                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.offeringTypeSummary.push({ "id": item.id, "name": item.name, "total": total });
                        }
                    });

                    $scope.fundTypeSummary = [];
                    angular.forEach($scope.fundTypes, function (item) {
                        if ($scope.offeringSearch.fundTypeId == "" || $scope.offeringSearch.fundTypeId == item.id) {
                            var filteredData = $filter('filter')($scope.offerings, { fundTypeId: item.id });
                            var total = sharedService.sum(filteredData, 'amount');
                            $scope.fundTypeSummary.push({ "id": item.id, "name": item.name, "total": total });
                        }
                    });
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