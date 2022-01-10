'use strict';
app.controller('contributionRptCtrl', ['$scope', '$filter', 'sharedService', '$window', 'downloadService',
    'familyService', 'offeringService', 'familyMemberService', 'sponsorService', 'orgFiscalYearService', 'organizationService','$location','imageUploadService','ngAuthSettings','userService','localStorageService','emailComposeService',
function ($scope, $filter, sharedService, $window, downloadService, familyService, offeringService, familyMemberService, sponsorService, orgFiscalYearService, organizationService, location, imageUploadService, ngAuthSettings, userService, localStorageService, emailComposeService) {

    localStorageService.remove('familyOfferingDetails');

    $scope.emailSending = false;
    $scope.pdfDownloading = false;

    $scope.familiesLoaded = 0;
    $scope.familyMembersLoaded = 0;
    
    $scope.contributionMsg = "";

    $scope.family = {};
    $scope.family.selected = undefined;

    $scope.familyMember = {};
    $scope.familyMember.selected = undefined;

    $scope.includeLogo = 0, $scope.includeSignature = 0;

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
        //$scope.familes = [];
        $scope.familyMembers = [];
        $scope.sponsors = [];
      //  $scope.fiscalYears = [];
        $scope.selectedfamilyMember;
        $scope.pastorInfo = null;
        $("[name='chkIncludeSignature']").bootstrapSwitch();
        $("[name='chkIncludeSignature']").bootstrapSwitch('size', 'small');
        $("[name='chkIncludeSignature']").bootstrapSwitch('onColor', 'success');
        $("[name='chkIncludeSignature']").bootstrapSwitch('offColor', 'warning'); 

        $("[name='chkIncludeSignature']").on('switchChange.bootstrapSwitch', function (event, state) {
            if ($scope.signatureImage != null)
            {
                if ($("[name='chkIncludeSignature']").bootstrapSwitch('state') == true)
                    $scope.includeSignature = 1;
                else
                    $scope.includeSignature = 0;
            }
        else
                $scope.includeSignature = 0;
        });
      
     //   $('.selectpicker').selectpicker();


        $scope.signatureImage = null;
        $scope.logoImage = null;
       
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
                $scope.familiesLoaded = 1;
            }, function (error) {
                $scope.familiesLoaded = 1;
                showErroMessage(error)
            });

            familyMemberService.getTaxPayerByOrgId($scope.authentication.orgId, true).then(function (results) {
                $scope.familyMembers = results.data;
                $scope.familyMembersLoaded = 1;
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


            imageUploadService.getSignature().then(function (results) {                
                if (results.data.length > 0) {
                    $scope.signatureImage = serviceBase + results.data[0];

                    if ($("[name='chkIncludeSignature']").bootstrapSwitch('state') == true)
                        $scope.includeSignature = 1;
                    else
                        $scope.includeSignature = 0;
                }
                else
                    $scope.includeSignature = 0;

            });

            imageUploadService.getLogo().then(function (results) {                
                if (results.data.length > 0) {
                    $scope.includeLogo = 1;
                    $scope.logoImage = serviceBase + results.data[0];                    
                }
            });

            userService.getPastorInfoByOrgId($scope.authentication.orgId).then(function (results) {
                $scope.pastorInfo = results.data;
            });

            organizationService.getContributionMsgByOrgId($scope.authentication.orgId).then(function (results) {
                if (results.data != null)
                    $scope.contributionMsg = results.data.message;
                else
                    $scope.contributionMsg = "";

            }, function (error) {
                showErroMessage(error)
            });

        };

        $scope.getFamilyTaxPayerNames = function () {

            var taPayerNames = "";

            if ($scope.selectedFamilyMembers !== undefined) {
                for (var iPos = 0; iPos < $scope.selectedFamilyMembers.length; iPos++) {
                    taPayerNames = taPayerNames + $scope.selectedFamilyMembers[iPos].fullName + ", ";
                }
            }

            return taPayerNames;
        }

        $scope.printOrEmail = function (reportType, contributorName, action) {          

          //  storeFamilyOfferingDetails();

            var contributorName = contributorName
            var startDate = "", endDate = "", id = "", fiscalYearId = "";           
            var pastorName = $scope.pastorInfo.firstName + " " + $scope.pastorInfo.lastName;
            
            var donorObj = {name: "", address1: "", address2: "", city: "", state:"", zipCode:"", country:"", email:"", type:""}
            var searchObj = {startDate: "", endDate: ""}


            var printObj = null;

            if (reportType == 1)
            {
                fiscalYearId = $scope.offeringSearchFamily.fiscalYearId;
                id = $scope.offeringSearchFamily.familyId;
                
                searchObj.startDate = $scope.offeringSearchFamily.startDate;
                searchObj.endDate = $scope.offeringSearchFamily.endDate;

                donorObj.name = $scope.getFamilyTaxPayerNames();
                donorObj.address1 = $scope.selectedFamily.address1;
                donorObj.address2 = $scope.selectedFamily.address2;
                donorObj.city = $scope.selectedFamily.city;
                donorObj.state = $scope.selectedFamily.state;
                donorObj.country = $scope.selectedFamily.country;
                donorObj.zipCode = $scope.selectedFamily.zipCode;
                donorObj.email = $scope.selectedFamily.emailId1;
                donorObj.type = "family";

                printObj = getPrintOfferingDetails(donorObj, searchObj, $scope.totalFamilyOffering, $scope.familyOfferings)
            }
            else if (reportType == 2)
            {
                fiscalYearId = $scope.offeringSearchFamilyMember.fiscalYearId;
                id = $scope.offeringSearchFamilyMember.familyMemberId;

                searchObj.startDate = $scope.offeringSearchFamilyMember.startDate;
                searchObj.endDate = $scope.offeringSearchFamilyMember.endDate;

                donorObj.name = $scope.selectedFamilyMember.fullName;
                donorObj.address1 = $scope.selectedFamilyForMember.address1;
                donorObj.address2 = $scope.selectedFamilyForMember.address2;
                donorObj.city = $scope.selectedFamilyForMember.city;
                donorObj.state = $scope.selectedFamilyForMember.state;
                donorObj.country = $scope.selectedFamilyForMember.country;
                donorObj.zipCode = $scope.selectedFamilyForMember.zipCode;
                donorObj.type = "family member";

                if ($scope.selectedFamilyMember.emailId1 != null && $scope.selectedFamilyMember.emailId1 != "")
                    donorObj.email = $scope.selectedFamilyMember.emailId1;
                else
                    donorObj.email = $scope.selectedFamilyForMember.family.emailId1;

                printObj = getPrintOfferingDetails(donorObj, searchObj, $scope.totalFamilyMemberOffering, $scope.familyMemberOfferings)
            }
            else if (reportType == 3) {
                fiscalYearId = $scope.offeringSearchSponsor.fiscalYearId;
                id = $scope.offeringSearchSponsor.sponsorId;

                searchObj.startDate = $scope.offeringSearchSponsor.startDate;
                searchObj.endDate = $scope.offeringSearchSponsor.endDate;

                donorObj.name = $scope.selectedSponsor.name;
                donorObj.address1 = $scope.selectedSponsor.address1;
                donorObj.address2 = $scope.selectedSponsor.address2;
                donorObj.city = $scope.selectedSponsor.city;
                donorObj.state = $scope.selectedSponsor.state;
                donorObj.country = $scope.selectedSponsor.country;
                donorObj.zipCode = $scope.selectedSponsor.zipCode;
                donorObj.email = $scope.selectedSponsor.emailId1;
                donorObj.type = "sponsor";

                printObj = getPrintOfferingDetails(donorObj, searchObj, $scope.totalSponsorOffering, $scope.sponsorOfferings)
            }

            if (action == 'print') {
                $scope.pdfDownloading = true;
                downloadService.printContributionLeter(printObj).then(function (results) {
                    $scope.pdfDownloading = false;
                    var file = new Blob([results.data], { type: 'application/pdf' });
                    var fileURL = URL.createObjectURL(file);
                    $window.open(fileURL, '_blank');
                }, function (error) {
                    $scope.pdfDownloading = false;
                    alert(error)
                });
            }
            else if(action == 'email')
            {
                $scope.emailSending = true;
                emailComposeService.contributionEmail(printObj).then(function (results) {
                    $scope.emailSending = false;
                    showEmailStatus(reportType, 1);                                        

                }, function (error) {
                    $scope.emailSending = false;
                    showEmailStatus(reportType, 0);
                  
                });

                $scope.showAlert = true;

            }

            

            //PDFConveterService.print(location.absUrl().split('#')[0] + "#/print/contributionPrint/" + fiscalYearId + "/" + id + "/" + startDate + "/" + endDate + "/" + $scope.authentication.orgId + "/" + contributorName + "/" + reportType + "/" + $scope.includeLogo + "/" + $scope.includeSignature + "/" + pastorName).then(function (results) {
            //    var file = new Blob([results.data], { type: 'application/pdf' });
            //    var fileURL = URL.createObjectURL(file);
            //    $window.open(fileURL, '_blank');
            //}, function (error) {
            //    alert(error)
            //});           

        };

        var showEmailStatus = function (reportType, status)
        {
            if (status == 1) {

                if (reportType == 1) {
                    $scope.alertClassForEmptyListFamily = "alert-success";
                    $scope.alertMsgForEmptyListFamily = "System sent the contribution report to the family in the email";
                    $scope.showAlertForEmptyListFamily = true;
                }

                if (reportType == 2) {
                    $scope.alertClassForEmptyListFamilyMember = "alert-success";
                    $scope.alertMsgForEmptyListFamilyMember = "System sent the contribution report to the family member in the email";
                    $scope.showAlertForEmptyListFamilyMember = true;
                }

                if (reportType == 3) {
                    $scope.alertClassForEmptyListSponsor = "alert-success";
                    $scope.alertMsgForEmptyListSponsor = "System sent the contribution report to the sponsor in the email";
                    $scope.showAlertForEmptyListSponsor = true;
                 
                }
            }
            else if (status == 0) {

                if (reportType == 1) {
                    $scope.alertClassForEmptyListFamily = "alert-danger";                    
                    $scope.alertMsgForEmptyListFamily = sharedService.getShortErrorMsg();
                    $scope.showAlertForEmptyListFamily = true;
                }

                if (reportType == 2) {
                    $scope.alertClassForEmptyListFamilyMember = "alert-danger";                    
                    $scope.alertMsgForEmptyListFamilyMember = sharedService.getShortErrorMsg();
                    $scope.showAlertForEmptyListFamilyMember = true;
                }

                if (reportType == 3) {
                    $scope.alertClassForEmptyListSponsor = "alert-danger";                    
                    $scope.alertMsgForEmptyListSponsor = sharedService.getShortErrorMsg();
                    $scope.showAlertForEmptyListSponsor = true;
                }
            }

        }
        

        var getFamilyMembers = function (obj) {

            var array = [];

            angular.forEach(obj, function (value) {                
                array.push(value.fullName);
            });          

            return array.join(', ');
        };       

        $scope.getFamilyOfferingReport = function (familyForm) {

            if (familyForm.$valid) {

            $scope.offeringSearchFamily.familyId = $scope.family.selected.id;
            var results = $filter('filter')($scope.fiscalYears, { id: $scope.offeringSearchFamily.fiscalYearId },true);
            
            familyMemberService.getTaxPayerByFamilyId($scope.offeringSearchFamily.familyId, true).then(function (results) {
                $scope.selectedFamilyMembers = results.data;                
            }, function (error) {
                showErroMessage(error)
            });


            $scope.offeringSearchFamily.startDate = results[0].fiscalYearStart;
            $scope.offeringSearchFamily.endDate = results[0].fiscalYearEnd;

            offeringService.familyOfferingReport($scope.offeringSearchFamily).then(function (results) {
                $scope.familyOfferings = results.data;                                

                if ($scope.familyOfferings.length > 0) {

                    $scope.selectedFamily = $filter('filter')($scope.familes, { id: $scope.offeringSearchFamily.familyId },true)[0];
                   // $scope.selectedFamilyMembers = getFamilyMembers( $filter('filter')($scope.selectedFamily.familyMembers, { isTaxPayer: true },true) );
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
           }
           else
           {
               if (familyForm.ddFamily.$error.required == true)
               {
                   familyForm.ddFamily.$dirty = true;
               }
           }
        }

        $scope.getFamilyMemberOfferingReport = function (familyMemberForm) {
            if (familyMemberForm.$valid) {

            $scope.offeringSearchFamilyMember.familyMemberId = $scope.familyMember.selected.id;
            var results = $filter('filter')($scope.fiscalYears, { id: $scope.offeringSearchFamilyMember.fiscalYearId }, true);

            $scope.offeringSearchFamilyMember.startDate = results[0].fiscalYearStart;
            $scope.offeringSearchFamilyMember.endDate = results[0].fiscalYearEnd;

            offeringService.familyMemberOfferingReport($scope.offeringSearchFamilyMember).then(function (results) {
                $scope.familyMemberOfferings = results.data;               

                if ($scope.familyMemberOfferings.length > 0) {

                    $scope.familyOfferings = results.data;

                    $scope.selectedFamilyMember = $filter('filter')($scope.familyMembers, { id: $scope.offeringSearchFamilyMember.familyMemberId },true)[0];
                    $scope.selectedFamilyForMember = $filter('filter')($scope.familes, { id: $scope.selectedFamilyMember.familyId },true)[0];
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
            else {
                if (familyMemberForm.ddFamilyMember.$error.required == true) {
                    familyMemberForm.ddFamilyMember.$dirty = true;
                }
            }
        }

        $scope.getSponsorOfferingReport = function () {


            var results = $filter('filter')($scope.fiscalYears, { id: $scope.offeringSearchSponsor.fiscalYearId },true);

            $scope.offeringSearchSponsor.startDate = results[0].fiscalYearStart;
            $scope.offeringSearchSponsor.endDate = results[0].fiscalYearEnd;

            offeringService.sponsorOfferingReport($scope.offeringSearchSponsor).then(function (results) {
                $scope.sponsorOfferings = results.data;

                if ($scope.sponsorOfferings.length > 0) {

                    $scope.selectedSponsor = $filter('filter')($scope.sponsors, { id: $scope.offeringSearchSponsor.sponsorId },true)[0];
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
                    var filteredData = $filter('filter')($scope.familyOfferings, { offeringTypeId: item.id },true);
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.offeringTypeSummaryFamily.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.fundTypeSummaryFamily = [];
            angular.forEach($scope.fundTypes, function (item) {
                if ($scope.offeringSearchFamily.fundTypeId == "" || $scope.offeringSearchFamily.fundTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.familyOfferings, { fundTypeId: item.id },true);
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.fundTypeSummaryFamily.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.paymentTypesSummaryFamily = [];
            angular.forEach($scope.paymentTypes, function (item) {
                var filteredData = $filter('filter')($scope.familyOfferings, { paymentTypeId: item.id },true);
                var total = sharedService.sum(filteredData, 'amount');
                $scope.paymentTypesSummaryFamily.push({ "id": item.id, "name": item.name, "total": total });
            });
        }

        $scope.familyMemberSummaryReport = function () {
            $scope.totalFamilyMemberOffering = sharedService.sum($scope.familyMemberOfferings, 'amount');

            $scope.offeringTypeSummaryFamilyMember = [];
            angular.forEach($scope.offeringTypes, function (item) {
                if ($scope.offeringSearchFamilyMember.offeringTypeId == "" || $scope.offeringSearchFamilyMember.offeringTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.familyMemberOfferings, { offeringTypeId: item.id },true);
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.offeringTypeSummaryFamilyMember.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.fundTypeSummaryFamilyMember = [];
            angular.forEach($scope.fundTypes, function (item) {
                if ($scope.offeringSearchFamilyMember.fundTypeId == "" || $scope.offeringSearchFamilyMember.fundTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.familyMemberOfferings, { fundTypeId: item.id },true);
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.fundTypeSummaryFamilyMember.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.paymentTypesSummaryFamilyMember = [];
            angular.forEach($scope.paymentTypes, function (item) {
                var filteredData = $filter('filter')($scope.familyMemberOfferings, { paymentTypeId: item.id },true);
                var total = sharedService.sum(filteredData, 'amount');
                $scope.paymentTypesSummaryFamilyMember.push({ "id": item.id, "name": item.name, "total": total });
            });
        }

        $scope.sponsorSummaryReport = function () {
            $scope.totalSponsorOffering = sharedService.sum($scope.sponsorOfferings, 'amount');

            $scope.offeringTypeSummarySponsor = [];
            angular.forEach($scope.offeringTypes, function (item) {
                if ($scope.offeringSearchSponsor.offeringTypeId == "" || $scope.offeringSearchSponsor.offeringTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.sponsorOfferings, { offeringTypeId: item.id },true);
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.offeringTypeSummarySponsor.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.fundTypeSummarySponsor = [];
            angular.forEach($scope.fundTypes, function (item) {
                if ($scope.offeringSearchSponsor.fundTypeId == "" || $scope.offeringSearchSponsor.fundTypeId == item.id) {
                    var filteredData = $filter('filter')($scope.sponsorOfferings, { fundTypeId: item.id },true);
                    var total = sharedService.sum(filteredData, 'amount');
                    $scope.fundTypeSummarySponsor.push({ "id": item.id, "name": item.name, "total": total });
                }
            });

            $scope.paymentTypesSummarySponsor = [];
            angular.forEach($scope.paymentTypes, function (item) {
                var filteredData = $filter('filter')($scope.sponsorOfferings, { paymentTypeId: item.id },true);
                var total = sharedService.sum(filteredData, 'amount');
                $scope.paymentTypesSummarySponsor.push({ "id": item.id, "name": item.name, "total": total });
            });
        }
    
        var getPrintOfferingDetails = function (donorObj, searchObj, totalOffering, offeringData)
        {
            var contributionPrintModel = null;
            var msg = "The IRS requires that individual contributions of $250.00 or more be listed separately to ensure tax deductibility. Contributions to " + $scope.org.name +
                " are tax deductible. Statements that are not questioned within 90 days will be considered to be accurate. No goods or services were provided by " + $scope.org.name + " in connection with any contribution, or their value were " +
            "insignificant or consisted entirely of intangible religious benefits.";
            if($scope.contributionMsg != '')
            {
                msg = $scope.contributionMsg
            }

          return contributionPrintModel =
            {
                orgName: $scope.org.name,
                orgAddress1: $scope.org.address1,
                orgAddress2: $scope.org.address2,
                orgCity: $scope.org.city,
                orgState: $scope.org.state,
                orgCountry: $scope.org.country,
                orgZipCode: $scope.org.zipCode,
                orgTaxId: $scope.org.taxId,
                donorName: donorObj.name,
                donorAddress1: donorObj.address1,
                donorAddress2: donorObj.address2,
                donorCity: donorObj.city,
                donorState: donorObj.state,
                donorCountry: donorObj.country,
                donorZipCode: donorObj.zipCode,
                donorEmailId: donorObj.email,
                donorType: donorObj.type,
                startDate: searchObj.startDate,
                endDate: searchObj.endDate,
                totalContribution: totalOffering,
                orgContributionMsg: msg,
                orgPastorName: $scope.pastorInfo.firstName + " " + $scope.pastorInfo.lastName,
                orgIncludeSignature: $scope.includeSignature,
                orgSignatureLink: $scope.signatureImage,
                orgLogoLink: $scope.logoImage,
                orgWebsite: $scope.org.website,
                orgPhone: $scope.org.phone1,
                orgEmail: $scope.org.emailId1,
                churchMgntURL: location.absUrl().split('#')[0],
                offerings: offeringData,
                orgCurrency: $scope.authentication.currency
            }


        }
    
    }]);