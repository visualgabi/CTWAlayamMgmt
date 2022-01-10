'use strict';
app.controller('familyOfferingRptController', ['$scope', 'lookupService', '$filter', 'sharedService', 'authService', '$window', 'PDFConveterService', 'familyService','offeringService','familyMemberService','sponsorService',
    function ($scope, lookupService, $filter, sharedService, authService, $window, PDFConveterService, familyService, offeringService, familyMemberService, sponsorService) {

        //$scope.familes = [];
        //$scope.familyMembers = [];
        //$scope.offeringTypes = [];
        //$scope.fundTypes = [];
        $scope.paymentTypes = [];

        $scope.offeringTypeSummaryFamily = [];
        $scope.fundTypeSummaryFamily = [];
        $scope.paymentTypeSummaryFamily = [];

        $scope.totalFamilyOffering = 0;
        $scope.totalFamilyMemberOffering = 0;
        $scope.totalSponsorOffering = 0;

        $scope.offeringSearchFamily = {
            familyId: "",
            startDate: "",
            endDate: "",                        
            fundTypeId: "",
            offeringTypeId: ""
        }

        $scope.offeringSearchFamilyMember = {
            familyMemberId: "",
            startDate: "",
            endDate: "",
            fundTypeId: "",
            offeringTypeId: ""
        }

        $scope.offeringSearchSponsor = {
            familyId: "",
            startDate: "",
            endDate: "",
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

        $scope.familyMaxDate = function () {
            var d = new Date($scope.offeringSearchFamily.startDate);
            var startDate = new Date($scope.offeringSearchFamily.startDate);

            d.setMonth(startDate.getMonth() + 6);

            return d;
        };

        $scope.familyMemberMaxDate = function () {
            var d = new Date($scope.offeringSearchFamilyMember.startDate);
            var startDate = new Date($scope.offeringSearchFamilyMember.startDate);

            d.setMonth(startDate.getMonth() + 6);

            return d;
        };

        $scope.sponsorMemberMaxDate = function () {
            var d = new Date($scope.offeringSearchSponsor.startDate);
            var startDate = new Date($scope.offeringSearchSponsor.startDate);

            d.setMonth(startDate.getMonth() + 6);

            return d;
        };

        $scope.isGreaterThanZero = function () {
            return function (item) {

                return item.total > 0;

            };
        };

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

    $scope.setMaxDate = function () {
        var maxDate = new Date($scope.offeringSearchFamily.startDate);
        maxDate.setMonth(maxDate.getMonth() + 12);        
        $('txtOfferingEndDate').attr('max', maxDate);
    }

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
    }

    $scope.getFamilyMemberOfferingReport = function (sponsorForm) {
        if (sponsorForm.$valid) {
            offeringService.familyMemberOfferingReport($scope.offeringSearchFamilyMember).then(function (results) {
                $scope.familyMemberOfferings = results.data;

                if ($scope.familyMemberOfferings.length > 0) {

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
    }

    $scope.getSponsorOfferingReport = function (reportForm) {

        if (reportForm.$valid) {
            offeringService.sponsorOfferingReport($scope.offeringSearchSponsor).then(function (results) {
                $scope.sponsorOfferings = results.data;

                if ($scope.sponsorOfferings.length > 0) {

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
    }

    $scope.familySummaryReport = function ()
    {
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

    $scope.familyMemberSummaryReport = function ()
    {
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