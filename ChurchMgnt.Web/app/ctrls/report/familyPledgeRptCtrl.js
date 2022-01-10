'use strict';
app.controller('familyPledgeRptCtrl', ['$scope', 'lookupService', '$filter', 'sharedService', 'authService', '$window', 'downloadService', 'familyService', 'offeringService', 'familyPledgeService', 'orgFiscalYearService','$location',
function ($scope, lookupService, $filter, sharedService, authService, $window, downloadService, familyService, offeringService, familyPledgeService, orgFiscalYearService,location) {

        //$scope.familes = [];
        //$scope.familyMembers = [];
        //$scope.offeringTypes = [];
    //$scope.fundTypes = [];
        $scope.familiesLoaded = 0;
        $scope.paymentTypes = [];

        $scope.offeringTypeSummaryFamily = [];
        $scope.fundTypeSummaryFamily = [];
        $scope.paymentTypeSummaryFamily = [];

        $scope.totalFamilyOffering = 0;
        $scope.totalFamilyMemberOffering = 0;
        $scope.totalSponsorOffering = 0;

        $scope.family = {};
        $scope.family.selected = undefined;

        $scope.offeringSearchFamily = {
            familyId: "",
            startDate: "",
            endDate: "",
            fundTypeId: "",
            offeringTypeId: "",
            fiscalYearId: ""
        }
       
        $scope.todayDate = function () {
            return new Date();
        };

        $scope.rptStartMinDate = function () {

            if ($scope.offeringSearchFamily.fiscalYearId != "") {
                var filteredData = $filter('filter')($scope.fiscalYears, { id: $scope.offeringSearchFamily.fiscalYearId },true);

                var d = new Date();
                d = new Date($filter('date')(filteredData[0].fiscalYearStart, 'MM/d/yyyy'));

                return d;
            }
            else
            {
                var d = new Date();

                d.setFullYear(d.getFullYear() - 5);

                return d;
            }
        };


        $scope.rptStartMaxDate = function () {

            if ($scope.offeringSearchFamily.fiscalYearId != "") {
                var filteredData = $filter('filter')($scope.fiscalYears, { id: $scope.offeringSearchFamily.fiscalYearId },true);

                var d = new Date();
                d = new Date($filter('date')(filteredData[0].fiscalYearEnd, 'MM/d/yyyy'));

                return d;
            }
            else
                return new Date();
        };

        $scope.rptEndMaxDate = function () {
            if ($scope.offeringSearchFamily.fiscalYearId != "") {
                var filteredData = $filter('filter')($scope.fiscalYears, { id: $scope.offeringSearchFamily.fiscalYearId },true);

                var d = new Date();
                d = new Date($filter('date')(filteredData[0].fiscalYearEnd, 'MM/d/yyyy'));

                return d;
            }
            else
                return new Date();
        };

        $scope.familyMaxDate = function () {
            var d = new Date($scope.offeringSearchFamily.startDate);
            var startDate = new Date($scope.offeringSearchFamily.startDate);

            d.setMonth(startDate.getMonth() + 12);

            return d;
        };
      

        $scope.isGreaterThanZero = function () {
            return function (item) {

                return item.total > 0;

            };
        };

        $scope.isGreaterThanZeroForCommitment = function () {
            return function (item) {

                return item.total > 0 || item.committed > 0;

            };
        };

        $scope.showPrintFamily = false;
        $scope.showPrintFamilyMember = false;
        $scope.showPrintSponsor = false;

        $scope.showAlertForEmptyListFamily = false;
        $scope.alertClassForEmptyListFamily = "";
        $scope.alertMsgForEmptyListFamily = "";       

        $scope.initialize = function () {

            familyService.getFamiliesByOrgId($scope.authentication.orgId, true).then(function (results) {
                $scope.familiesLoaded = 1;
                $scope.familes = results.data;
            }, function (error) {
                showErroMessage(error)
            });          

            orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, true).then(function (results) {
                $scope.fiscalYears = results.data;
            }, function (error) {
                showErroMessage(error)
            });


           
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

        };

        $scope.getFamilyOfferingReport = function (familyForm) {

            if (familyForm.$valid) {

                $scope.offeringSearchFamily.familyId = $scope.family.selected.id;

                offeringService.familyOfferingReport($scope.offeringSearchFamily).then(function (results) {
                    $scope.familyOfferings = results.data;

                    if ($scope.familyOfferings.length > 0) {

                        $scope.showPrintFamily = true;
                        $scope.alertClassForEmptyListFamily = "";
                        $scope.showAlertForEmptyListFamily = false;
                        $scope.alertMsgForEmptyListFamily = "";
                        $scope.familySummaryReport();
                    }
                    else {
                        $scope.showPrintFamily = false;
                        $scope.alertMsgForEmptyListFamily = sharedService.getShortErrorMsgForEmptyList();
                        $scope.alertClassForEmptyListFamily = "alert-danger";
                        $scope.showAlertForEmptyListFamily = true;
                    }

                }, function (error) {
                    alert(error.data.message);
                });              

            }
            else {
                if (familyForm.ddFamily.$error.required == true) {
                    familyForm.ddFamily.$dirty = true;
                }
            }
        }
        

        $scope.familySummaryReport = function () {
            $scope.totalFamilyOffering = sharedService.sum($scope.familyOfferings, 'amount');

            $scope.offeringTypeSummaryFamily = [];
            angular.forEach($scope.offeringTypes, function (item) {
                if ($scope.offeringSearchFamily.offeringTypeId == "" || $scope.offeringSearchFamily.offeringTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.familyOfferings, { offeringTypeId: item.id },true);
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.offeringTypeSummaryFamily.push({ "id": item.id, "name": item.name, "total": total });
                }
            });


            familyPledgeService.getFamilyPledgeByFiscalYearIdId($scope.offeringSearchFamily.familyId, $scope.offeringSearchFamily.fiscalYearId).then(function (results) {
                $scope.familyPledges = results.data;

                $scope.fundTypeSummaryFamily = [];
                angular.forEach($scope.fundTypes, function (item) {
                    if ($scope.offeringSearchFamily.fundTypeId == "" || $scope.offeringSearchFamily.fundTypeId == item.id) {
                        var filteredData = $filter('filter')($scope.familyOfferings, { fundTypeId: item.id },true);
                        var total = sharedService.sum(filteredData, 'amount');

                        var familyPledge = $filter('filter')($scope.familyPledges, { fundTypeId: item.id },true);
                        var committed = familyPledge.length > 0 ? familyPledge[0].amount : 0.00

                        $scope.fundTypeSummaryFamily.push({ "id": item.id, "name": item.name, "total": total, "committed": committed });
                    }
                });

            }, function (error) {
                showErroMessage(error)
            });

           

            $scope.paymentTypesSummaryFamily = [];
            angular.forEach($scope.paymentTypes, function (item) {
                var filteredData = $filter('filter')($scope.familyOfferings, { paymentTypeId: item.id },true);
                var total = sharedService.sum(filteredData, 'amount');
                $scope.paymentTypesSummaryFamily.push({ "id": item.id, "name": item.name, "total": total });
            });
        }

        $scope.downloadPdf = function () {
            
            $scope.filter = { startDate: $scope.offeringSearchFamily.startDate, endDate: $scope.offeringSearchFamily.endDate };

            $scope.summaryByPayment = $filter('filter')($scope.paymentTypesSummaryFamily, function (obj) {
                return obj.total > 0;
            });

            $scope.summaryByFundType = $filter('filter')($scope.fundTypeSummaryFamily, function (obj) {
                return obj.total > 0;
            });

            $scope.summaryByOfferingType = $filter('filter')($scope.offeringTypeSummaryFamily, function (obj) {
                return obj.total > 0;
            });

            $scope.printObj = {
                currency: $scope.authentication.currency,
                totalOffering: $scope.totalFamilyOffering,
                SummaryBySourceTypes: $scope.summaryBySourceType,
                SummaryByPaymentTypes: $scope.summaryByPayment,
                SummaryByFundTypes: $scope.summaryByFundType,
                summaryByOfferingTypes: $scope.summaryByOfferingType,
                offerings: $scope.familyOfferings,
                churchMgntURL: location.absUrl().split('#')[0],
                filter: $scope.filter,
                orgName: $scope.authentication.orgName
            }

            downloadService.printFamilyPledgeReport($scope.printObj).then(function (results) {
                var file = new Blob([results.data], { type: 'application/pdf' });
                var fileURL = URL.createObjectURL(file);
                $window.open(fileURL, '_blank');
            }, function (error) {
                alert(error)
            });

        };

    }]);