'use strict';
app.controller('contributionRptController', ['$scope', '$filter', 'sharedService', '$window', 'PDFConveterService',
    'familyService', 'offeringService', 'familyMemberService', 'sponsorService', 'orgFiscalYearService', 'organizationService',
    function ($scope, $filter, sharedService, $window,PDFConveterService, familyService, offeringService, familyMemberService, sponsorService, orgFiscalYearService, organizationService) {

        //$scope.familes = [];
        $scope.familyMembers = [];
        $scope.sponsors = [];
      //  $scope.fiscalYears = [];
        $scope.selectedfamilyMember;
       
       
        $scope.totalFamilyOffering = 0;
        $scope.totalFamilyMemberOffering = 0;
        $scope.totalSponsorOffering = 0;

        $scope.offeringSearchFamily = {
            familyId: "",
            startDate: "",
            endDate: "",
            fiscalYearId : ""
        }

        $scope.offeringSearchFamilyMember = {
            familyMemberId: "",
            startDate: "",
            endDate: "",
            fiscalYearId: ""
        }

        $scope.offeringSearchSponsor = {
            sponsorId: "",
            startDate: "",
            endDate: "",
            fiscalYearId: ""
        }

        $scope.today = new Date();

        $scope.showPrintFamily = false;
        $scope.showPrintFamilyMember = false;
        $scope.showPrintSponsor = false;

        $scope.showAlertForEmptyListFamily = false;
        $scope.alertClassForEmptyListFamily = "";
        $scope.alertMsgForEmptyListFamily = "";

        $scope.showAlertForEmptyListFamilyMember = false;
        $scope.alertClassForEmptyListFamilyMember = "";
        $scope.alertMsgForEmptyListFamilyMember = "";

        $scope.showAlertForEmptyListSponsor = false;
        $scope.alertClassForEmptyListSponsor = "";
        $scope.alertMsgForEmptyListSponsor = "";




        $('.nav-tabs a').click(function (e) {
            e.preventDefault();
            $(this).tab('show');
        });
       

        $scope.initialize = function () {

            familyService.getFamiliesByOrgId($scope.authentication.orgId, true).then(function (results) {
                $scope.familes = results.data;
            }, function (error) {
                showErroMessage(error)
            });

            familyMemberService.getTaxPayerByOrgId($scope.authentication.orgId, true).then(function (results) {
                $scope.familyMembers = results.data;
            }, function (error) {
                showErroMessage(error)
            });

            sponsorService.getByOrgId($scope.authentication.orgId, true).then(function (results) {
                $scope.sponsors = results.data;
            }, function (error) {
                showErroMessage(error)
            });

            orgFiscalYearService.getOrgFiscalYearsByOrgId($scope.authentication.orgId, true).then(function (results) {
                $scope.fiscalYears = results.data;
            }, function (error) {
                showErroMessage(error)
            });

            organizationService.getOrganization().then(function (results) {
                $scope.org = results.data;
            }, function (error) {
                showErroMessage(error)
            });

        };

        $scope.print = function (reportType, contributorName) {

            var contributorName = contributorName
            var startDate = "", endDate = "", id = "", fiscalYearId= "";

            if (reportType == 1)
            {
                fiscalYearId = $scope.offeringSearchFamily.fiscalYearId;
                id = $scope.offeringSearchFamily.familyId;
                startDate = $scope.offeringSearchFamily.startDate;
                endDate = $scope.offeringSearchFamily.endDate;
            }
            else if (reportType == 2)
            {
                fiscalYearId = $scope.offeringSearchFamilyMember.fiscalYearId;
                id = $scope.offeringSearchFamilyMember.familyMemberId;
                startDate = $scope.offeringSearchFamilyMember.startDate;
                endDate = $scope.offeringSearchFamilyMember.endDate;
            }
            else if (reportType == 3) {
                fiscalYearId = $scope.offeringSearchSponsor.fiscalYearId;
                id = $scope.offeringSearchSponsor.sponsorId;
                startDate = $scope.offeringSearchSponsor.startDate;
                endDate = $scope.offeringSearchSponsor.endDate;
            }

            PDFConveterService.print(serviceBase + "#/yearEndActivity/contributionRptPrt/" + fiscalYearId + "/" + id + "/" + startDate + "/" + endDate + "/" + $scope.authentication.orgId + "/" + contributorName + "/" + reportType).then(function (results) {
                var file = new Blob([results.data], { type: 'application/pdf' });
                var fileURL = URL.createObjectURL(file);
                $window.open(fileURL, '_blank');
            }, function (error) {
                alert(error)
            });           

        };

        var getFamilyMembers = function (obj) {

            var array = [];

            angular.forEach(obj, function (value) {                
                array.push(value.fullName);
            });          

            return array.join(',');
      };

        $scope.getFamilyOfferingReport = function () {

            // if (familyForm.$valid) {
            var results = $filter('filter')($scope.fiscalYears, { id: $scope.offeringSearchFamily.fiscalYearId });

            

            $scope.offeringSearchFamily.startDate = results[0].fiscalYearStart;
            $scope.offeringSearchFamily.endDate = results[0].fiscalYearEnd;

            offeringService.familyOfferingReport($scope.offeringSearchFamily).then(function (results) {
                $scope.familyOfferings = results.data;                
            

                if ($scope.familyOfferings.length > 0) {

                    $scope.selectedFamily = $filter('filter')($scope.familes, { id: $scope.offeringSearchFamily.familyId })[0];
                    $scope.selectedFamilyMembers = getFamilyMembers( $filter('filter')($scope.selectedFamily.familyMembers, { isTaxPayer: true }) );
                    $scope.totalFamilyOffering = sharedService.sum($scope.familyOfferings, 'amount');

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
            //}
        }

        $scope.getFamilyMemberOfferingReport = function () {

            var results = $filter('filter')($scope.fiscalYears, { id: $scope.offeringSearchFamilyMember.fiscalYearId });

            $scope.offeringSearchFamilyMember.startDate = results[0].fiscalYearStart;
            $scope.offeringSearchFamilyMember.endDate = results[0].fiscalYearEnd;

            offeringService.familyMemberOfferingReport($scope.offeringSearchFamilyMember).then(function (results) {
                $scope.familyMemberOfferings = results.data;               

                if ($scope.familyMemberOfferings.length > 0) {

                    $scope.familyOfferings = results.data;

                    $scope.selectedFamilyMember = $filter('filter')($scope.familyMembers, { id: $scope.offeringSearchFamilyMember.familyMemberId })[0];
                    $scope.selectedFamilyForMember = $filter('filter')($scope.familes, { id: $scope.selectedFamilyMember.familyId })[0];
                    $scope.totalFamilyMemberOffering = sharedService.sum($scope.familyMemberOfferings, 'amount');

                    $scope.showPrintFamilyMember = true;
                    $scope.alertClassForEmptyListFamilyMember = "";
                    $scope.showAlertForEmptyListFamilyMember = false;
                    $scope.alertMsgForEmptyListFamilyMember = "";

                    $scope.familyMemberSummaryReport();
                }
                else {
                    $scope.showPrintFamilyMember = false;
                    $scope.alertMsgForEmptyListFamilyMember = sharedService.getShortErrorMsgForEmptyList();
                    $scope.alertClassForEmptyListFamilyMember = "alert-danger";
                    $scope.showAlertForEmptyListFamilyMember = true;
                }

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.getSponsorOfferingReport = function () {


            var results = $filter('filter')($scope.fiscalYears, { id: $scope.offeringSearchSponsor.fiscalYearId });

            $scope.offeringSearchSponsor.startDate = results[0].fiscalYearStart;
            $scope.offeringSearchSponsor.endDate = results[0].fiscalYearEnd;

            offeringService.sponsorOfferingReport($scope.offeringSearchSponsor).then(function (results) {
                $scope.sponsorOfferings = results.data;

                if ($scope.sponsorOfferings.length > 0) {

                    $scope.selectedSponsor = $filter('filter')($scope.sponsors, { id: $scope.offeringSearchSponsor.sponsorId })[0];
                    $scope.totalSponsorOffering = sharedService.sum($scope.sponsorOfferings, 'amount');

                    $scope.showPrintSponsor = true;
                    $scope.alertClassForEmptyListSponsor = "";
                    $scope.showAlertForEmptyListSponsor = false;
                    $scope.alertMsgForEmptyListSponsor = "";
                    $scope.sponsorSummaryReport();
                }
                else {
                    $scope.showPrintSponsor = false;
                    $scope.alertMsgForEmptyListSponsor = sharedService.getShortErrorMsgForEmptyList();
                    $scope.alertClassForEmptyListSponsor = "alert-danger";
                    $scope.showAlertForEmptyListSponsor = true;
                }

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.familySummaryReport = function () {
            $scope.totalFamilyOffering = sharedService.sum($scope.familyOfferings, 'amount');

            $scope.offeringTypeSummaryFamily = [];
            angular.forEach($scope.offeringTypes, function (item) {
                if ($scope.offeringSearchFamily.offeringTypeId == "" || $scope.offeringSearchFamily.offeringTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.familyOfferings, { offeringTypeId: item.id });
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.offeringTypeSummaryFamily.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.fundTypeSummaryFamily = [];
            angular.forEach($scope.fundTypes, function (item) {
                if ($scope.offeringSearchFamily.fundTypeId == "" || $scope.offeringSearchFamily.fundTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.familyOfferings, { fundTypeId: item.id });
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.fundTypeSummaryFamily.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.paymentTypesSummaryFamily = [];
            angular.forEach($scope.paymentTypes, function (item) {
                var filteredData = $filter('filter')($scope.familyOfferings, { paymentTypeId: item.id });
                var total = sharedService.sum(filteredData, 'amount');
                $scope.paymentTypesSummaryFamily.push({ "id": item.id, "name": item.name, "total": total });
            });
        }

        $scope.familyMemberSummaryReport = function () {
            $scope.totalFamilyMemberOffering = sharedService.sum($scope.familyMemberOfferings, 'amount');

            $scope.offeringTypeSummaryFamilyMember = [];
            angular.forEach($scope.offeringTypes, function (item) {
                if ($scope.offeringSearchFamilyMember.offeringTypeId == "" || $scope.offeringSearchFamilyMember.offeringTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.familyMemberOfferings, { offeringTypeId: item.id });
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.offeringTypeSummaryFamilyMember.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.fundTypeSummaryFamilyMember = [];
            angular.forEach($scope.fundTypes, function (item) {
                if ($scope.offeringSearchFamilyMember.fundTypeId == "" || $scope.offeringSearchFamilyMember.fundTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.familyMemberOfferings, { fundTypeId: item.id });
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.fundTypeSummaryFamilyMember.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.paymentTypesSummaryFamilyMember = [];
            angular.forEach($scope.paymentTypes, function (item) {
                var filteredData = $filter('filter')($scope.familyMemberOfferings, { paymentTypeId: item.id });
                var total = sharedService.sum(filteredData, 'amount');
                $scope.paymentTypesSummaryFamilyMember.push({ "id": item.id, "name": item.name, "total": total });
            });
        }

        $scope.sponsorSummaryReport = function () {
            $scope.totalSponsorOffering = sharedService.sum($scope.sponsorOfferings, 'amount');

            $scope.offeringTypeSummarySponsor = [];
            angular.forEach($scope.offeringTypes, function (item) {
                if ($scope.offeringSearchSponsor.offeringTypeId == "" || $scope.offeringSearchSponsor.offeringTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.sponsorOfferings, { offeringTypeId: item.id });
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.offeringTypeSummarySponsor.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.fundTypeSummarySponsor = [];
            angular.forEach($scope.fundTypes, function (item) {
                if ($scope.offeringSearchSponsor.fundTypeId == "" || $scope.offeringSearchSponsor.fundTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.sponsorOfferings, { fundTypeId: item.id });
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.fundTypeSummarySponsor.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.paymentTypesSummarySponsor = [];
            angular.forEach($scope.paymentTypes, function (item) {
                var filteredData = $filter('filter')($scope.sponsorOfferings, { paymentTypeId: item.id });
                var total = sharedService.sum(filteredData, 'amount');
                $scope.paymentTypesSummarySponsor.push({ "id": item.id, "name": item.name, "total": total });
            });
        }

    }]);