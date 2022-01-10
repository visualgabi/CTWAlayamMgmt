'use strict';
app.controller('contributionRptPrtController', ['$scope', '$routeParams', 'sharedService', 'authService', 
    '$window', 'PDFConveterService', 'ngAuthSettings', 'familyService', 'offeringService', 'sponsorService', 'organizationService',
    function ($scope, $routeParams, sharedService, authService, $window,
        PDFConveterService, ngAuthSettings, familyService, offeringService, sponsorService, organizationService) {

        $scope.orgBasicData = {};
        $scope.familyBasicData = {};

        $scope.offeringSearchFamily = {
            familyId: "",
            startDate: "",
            endDate: "",
            fiscalYearId: ""
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

        $scope.totalOffering = 0;

        $scope.reportType = $routeParams.reportType;

        if ($scope.reportType == 1) {
            $scope.offeringSearchFamily.familyId = $routeParams.id;
            $scope.offeringSearchFamily.startDate = $routeParams.startDate;
            $scope.offeringSearchFamily.endDate = $routeParams.endDate;
            $scope.offeringSearchFamily.fiscalYearId = $routeParams.fiscalYearId;
        }
        else if ($scope.reportType == 2)
        {
            $scope.offeringSearchFamilyMember.familyMemberId = $routeParams.id;
            $scope.offeringSearchFamilyMember.startDate = $routeParams.startDate;
            $scope.offeringSearchFamilyMember.endDate = $routeParams.endDate;
            $scope.offeringSearchFamilyMember.fiscalYearId = $routeParams.fiscalYearId;
        }
        else if ($scope.reportType == 3) {
            $scope.offeringSearchSponsor.sponsorId = $routeParams.id;
            $scope.offeringSearchSponsor.startDate = $routeParams.startDate;
            $scope.offeringSearchSponsor.endDate = $routeParams.endDate;
            $scope.offeringSearchSponsor.fiscalYearId = $routeParams.fiscalYearId;
        }
        
        

        

        $scope.orgId = $routeParams.orgId;
        $scope.familyMembers = $routeParams.familyMembers;
      
        $scope.authentication = authService.authentication;

        $scope.initialize = function () {

            $scope.formLoad();
            $scope.load();
        };

        $scope.missionCompled = function () {
            $window.status = "Done";
        };

        $scope.today = new Date();

        $scope.formLoad = function () {

            organizationService.getOrganizationBasicDataById($scope.orgId).then(function (results) {
                $scope.orgBasicData = results.data;
            }, function (error) {
                showErroMessage(error)
            });

            if ($scope.reportType == 1 || $scope.reportType == 2) {
                familyService.getFamilyBasicDataById($routeParams.id).then(function (results) {
                    $scope.familyBasicData = results.data;
                }, function (error) {
                    showErroMessage(error)
                });
            }          

            if ($scope.reportType == 3) {
                sponsorService.getBasicDataById($routeParams.id).then(function (results) {
                    $scope.sponsor = results.data;
                }, function (error) {
                    showErroMessage(error)
                });
            }

           

        }

        $scope.load = function () {

            if ($scope.reportType == 1)
            {
                offeringService.familyOfferingReport($scope.offeringSearchFamily).then(function (results) {
                    $scope.familyOfferings = results.data;                                        

                    $scope.totalOffering = sharedService.sum($scope.familyOfferings, 'amount');
                   

                }, function (error) {
                    alert(error.data.message);
                });
            }
            else if($scope.reportType == 2)
            {
                offeringService.familyMemberOfferingReport($scope.offeringSearchFamilyMember).then(function (results) {
                    $scope.familyMemberOfferings = results.data;
                    $scope.totalOffering = sharedService.sum($scope.familyMemberOfferings, 'amount');

                }, function (error) {
                    alert(error.data.message);
                });
            }
            else if ($scope.reportType == 3) {
                offeringService.sponsorOfferingReport($scope.offeringSearchSponsor).then(function (results) {
                    $scope.sponsorOfferings = results.data;
                    $scope.totalOffering = sharedService.sum($scope.sponsorOfferings, 'amount');

                }, function (error) {
                    alert(error.data.message);
                });
            }
        }

    }])