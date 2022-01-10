'use strict';
app.controller('contributionPrintCtrl', ['$scope', '$stateParams', 'sharedService', 'authService',
    '$window', 'PDFConveterService', 'ngAuthSettings', 'familyService', 'offeringService', 'sponsorService', 'organizationService','$timeout','imageUploadService','userService','localStorageService',
    function ($scope, $stateParams, sharedService, authService, $window,
        PDFConveterService, ngAuthSettings, familyService, offeringService, sponsorService, organizationService, $timeout, imageUploadService, userService, localStorageService) {

        

        $scope.orgBasicData = {};
        $scope.familyBasicData = {};
      
        $scope.signatureImage = null;
        $scope.logoImage = null;
        $scope.contributionMsg = "";


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

        $scope.reportType = $stateParams.reportType;

        if ($scope.reportType == 1) {
            $scope.offeringSearchFamily.familyId = $stateParams.id;
            $scope.offeringSearchFamily.startDate = $stateParams.startDate;
            $scope.offeringSearchFamily.endDate = $stateParams.endDate;
            $scope.offeringSearchFamily.fiscalYearId = $stateParams.fiscalYearId;
        }
        else if ($scope.reportType == 2)
        {
            $scope.offeringSearchFamilyMember.familyMemberId = $stateParams.id;
            $scope.offeringSearchFamilyMember.startDate = $stateParams.startDate;
            $scope.offeringSearchFamilyMember.endDate = $stateParams.endDate;
            $scope.offeringSearchFamilyMember.fiscalYearId = $stateParams.fiscalYearId;
        }
        else if ($scope.reportType == 3) {
            $scope.offeringSearchSponsor.sponsorId = $stateParams.id;
            $scope.offeringSearchSponsor.startDate = $stateParams.startDate;
            $scope.offeringSearchSponsor.endDate = $stateParams.endDate;
            $scope.offeringSearchSponsor.fiscalYearId = $stateParams.fiscalYearId;
        }      
      
        var includeLogo = $stateParams.includeLogo;
        var includeSignature = $stateParams.includeSignature;
        $scope.pastorName = $stateParams.pastorName;

        $scope.orgId = $stateParams.orgId;
        $scope.familyMembers = $stateParams.familyMembers;
      
        $scope.authentication = authService.authentication;
        var serviceBase = ngAuthSettings.apiServiceBaseUri;


        //$(document).on('click', '.toggle-button', function () {
        //    $(this).toggleClass('toggle-button-selected');
        //});

        $scope.initialize = function () {

            $scope.familyOfferingDetails = localStorageService.get('familyOfferingDetails');

            alert($scope.familyOfferingDetails.pastorName);

            $scope.formLoad();
            $scope.load();

            $timeout(function () {
                $window.status = "Done";
            });
        };      

        $scope.today = new Date();

        //$scope.missionCompled = function () {
        //    $window.status = "Done";
        //};


        $scope.formLoad = function () {                       
            organizationService.getOrganizationBasicDataById($scope.orgId).then(function (results) {                
                $scope.orgBasicData = results.data;
                var orgName = $scope.orgBasicData.name.replace(/\s/g, "");
                if (includeLogo == 1)
                    $scope.logoImage = "http://localhost:15312/Images/" + orgName + "/logo.PNG";
                if (includeSignature == 1)
                    $scope.signatureImage = "http://localhost:15312/Images/" + orgName + "/signature.PNG";
            }, function (error) {
                showErroMessage(error)
            });

            organizationService.getContributionMsgByOrgId($scope.orgId).then(function (results) {
                if (results.data != null)
                    $scope.contributionMsg = results.data.message;
                else
                    $scope.contributionMsg = "";

            }, function (error) {
                showErroMessage(error)
            });

            if ($scope.reportType == 1 || $scope.reportType == 2) {
                familyService.getFamilyBasicDataById($stateParams.id).then(function (results) {
                    $scope.familyBasicData = results.data;
                }, function (error) {
                    showErroMessage(error)
                });
            }          

            if ($scope.reportType == 3) {
                sponsorService.getBasicDataById($stateParams.id).then(function (results) {
                    $scope.sponsor = results.data;
                }, function (error) {
                    showErroMessage(error)
                });
            }

           

        }

        $scope.filteredFamilyOfferings = [];

        var filterFamilyOfferings = function () {
            var totalPagesPagesExcludeFirstPage = 0;

            if ($scope.familyOfferings.length > 25) {
                var totalPagesPagesExcludeFirstPage = ($scope.familyOfferings.length - 25) / 30;
            }

            if (totalPagesPagesExcludeFirstPage > 0) {
                for (var iPos = 0; iPos < totalPagesPagesExcludeFirstPage; iPos++) {
                    var start = 25;
                    if (iPos > 0) {
                        start = 25 + (30 * iPos);
                    }

                    var end = start + 30;

                    $scope.filteredFamilyOfferings.push({ "index": iPos, "offeringData": $scope.familyOfferings.slice(start, end) })
                }
            }
        };


        $scope.filteredFamilyMemberOfferings = [];

        var filterFamilyMemberOfferings = function () {
            var totalPagesPagesExcludeFirstPage = 0;

            if ($scope.familyMemberOfferings.length > 25) {
                var totalPagesPagesExcludeFirstPage = ($scope.familyMemberOfferings.length - 25) / 30;
            }

            if (totalPagesPagesExcludeFirstPage > 0) {
                for (var iPos = 0; iPos < totalPagesPagesExcludeFirstPage; iPos++) {
                    var start = 25;
                    if (iPos > 0) {
                        start = 25 + (30 * iPos);
                    }

                    var end = start + 30;

                    $scope.filteredFamilyMemberOfferings.push({ "index": iPos, "offeringData": $scope.familyMemberOfferings.slice(start, end) })
                }
            }
        };

        $scope.filteredSponsorOfferings = [];

        var filterSponsorOfferings = function () {
            var totalPagesPagesExcludeFirstPage = 0;

            if ($scope.sponsorOfferings.length > 25) {
                var totalPagesPagesExcludeFirstPage = ($scope.sponsorOfferings.length - 25) / 30;
            }

            if (totalPagesPagesExcludeFirstPage > 0) {
                for (var iPos = 0; iPos < totalPagesPagesExcludeFirstPage; iPos++) {
                    var start = 25;
                    if (iPos > 0) {
                        start = 25 + (30 * iPos);
                    }

                    var end = start + 30;

                    $scope.filteredSponsorOfferings.push({ "index": iPos, "offeringData": $scope.sponsorOfferings.slice(start, end) })
                }
            }
        };


        $scope.load = function () {

            if ($scope.reportType == 1)
            {
                offeringService.familyOfferingReport($scope.offeringSearchFamily).then(function (results) {
                    $scope.familyOfferings = results.data;
                    filterFamilyOfferings();

                    $scope.totalOffering = sharedService.sum($scope.familyOfferings, 'amount');
                   

                }, function (error) {
                    alert(error.data.message);
                });
            }
            else if($scope.reportType == 2)
            {
                offeringService.familyMemberOfferingReport($scope.offeringSearchFamilyMember).then(function (results) {
                    $scope.familyMemberOfferings = results.data;
                    filterFamilyMemberOfferings();
                    $scope.totalOffering = sharedService.sum($scope.familyMemberOfferings, 'amount');

                }, function (error) {
                    alert(error.data.message);
                });
            }
            else if ($scope.reportType == 3) {
                offeringService.sponsorOfferingReport($scope.offeringSearchSponsor).then(function (results) {
                    $scope.sponsorOfferings = results.data;
                    filterSponsorOfferings();
                    $scope.totalOffering = sharedService.sum($scope.sponsorOfferings, 'amount');

                }, function (error) {
                    alert(error.data.message);
                });
            }

           
        }

    }])