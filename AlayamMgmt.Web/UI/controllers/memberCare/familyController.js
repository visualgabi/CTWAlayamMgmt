'use strict';
app.controller('familyController', ['$scope', 'lookupService', 'familyService', '$routeParams', '$filter', 'sharedService', 'authService', 'organizationService', 'notificationService', '$location',
    function ($scope, lookupService, familyService, $routeParams, $filter, sharedService, authService, organizationService, notificationService,$location) {

    
    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.authentication = authService.authentication;

    $scope.todayDate = new Date();
    $scope.previousMonth = new Date();

    $scope.previousMonth.setMonth($scope.previousMonth.getMonth() - 1);

    $scope.familyData = {
        id: 0,
        name: "",
        address1: "",
        address2: "",
        address3: "",
        city: "",
        countryId: "",        
        notes: "",
        emailId1: "",
        emailId2: "",
        ethnicOriginId: "",
        phone1: "",
        phone2: "",
        primaryLanguageId: "",
        secondaryLanguageId: "",
        stateId: "",        
        zipCode: "",
        active: true,
        firstVisitedDate: "",
        membershipStartDate: "",        
        branchId: "",
        membershipStatusId: "",        
        rowTimeStamp: "",
        mariageDate :""
    };


    //$scope.countries = [];
    $scope.states = [];
    //$scope.languages = [];    
    //$scope.ethnicOrigins = [];
    //$scope.membershipStatuses = [];
    //$scope.branches = [];

    

    $scope.todayDate = function () {
        return new Date();
    };

    $scope.rptMinDate = function () {
        var d = new Date();

        d.setFullYear(d.getFullYear() - 5);

        return d;
    };

    $scope.mariageMinDate = function () {
        var d = new Date();

        d.setFullYear(d.getFullYear() - 100);

        return d;
    };


    $scope.formLoad = function () {

        $scope.clear();

        if ($routeParams.id !== undefined) {
            familyService.getFamilyById($routeParams.id).then(function (results) {
                $scope.familyData = results.data;
                $scope.familyData.firstVisitedDate = new Date($filter('date')($scope.familyData.firstVisitedDate, 'MM/d/yyyy'));

                if( angular.isDefined($scope.familyData.membershipStartDate) && ($scope.familyData.membershipStartDate != null ) )
                    $scope.familyData.membershipStartDate = new Date($filter('date')($scope.familyData.membershipStartDate, 'MM/d/yyyy'));

                if (angular.isDefined($scope.familyData.mariageDate) && ($scope.familyData.mariageDate != null))
                    $scope.familyData.mariageDate = new Date($filter('date')($scope.familyData.mariageDate, 'MM/d/yyyy'));

            }, function (error) {
                showErroMessage(error)
            });
        }

        lookupService.getCountries(null).then(function (results) {            
            $scope.countries = results.data;

        }, function (error) {
            notificationService.displayError(sharedService.getShortErrorMsg())
        });

        lookupService.getStates(null).then(function (results) {
            $scope.states = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getLanguages(null).then(function (results) {
            $scope.languages = results.data;

        }, function (error) {
            showErroMessage(error)
        });     

        lookupService.getEthnicOrigins(null).then(function (results) {
            $scope.ethnicOrigins = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getMembershipStatuses(null).then(function (results) {
            $scope.membershipStatuses = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        organizationService.getBranchesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.branches = results.data;            
        },
          function (error) {
              showErroMessage(error)
          });
    };
  
    $scope.initialize = function () {
        $scope.formLoad();      
    };
    
    $scope.clear = function () {
        $scope.familyData = {
            id: 0,
            name: "",
            address1: "",
            address2: "",
            address3: "",
            city: "",
            countryId: "",
            notes: "",
            emailId1: "",
            emailId2: "",
            ethnicOriginId: "",
            phone1: "",
            phone2: "",
            primaryLanguageId: "",
            secondaryLanguageId: "",
            stateId: "",
            zipCode: "",
            active: true,
            firstVisitedDate: "",
            membershipStartDate: "",
            branchId: "",
            membershipStatusId: "",
            rowTimeStamp: "",
            mariageDate: ""
        };
    }

    $scope.clearMsg = function () {
        $scope.showAlert = false;
        $scope.alertClass;
        $scope.alertMsg;
    };

    function showErroMessage() {
        $scope.alertClass = "alert-danger";
        $scope.alertMsg = sharedService.getErrorMsg(error.data, $scope.familyData.name);
        $scope.showAlert = true;
    }

    $scope.close = function () {        
        $location.path('/memberCare/familylst');
    }    

    $scope.savefamily = function (familyForm) {

        if (familyForm.$valid) {

            $scope.familyData.countryId = $scope.familyData.countryId;
            $scope.familyData.stateId = $scope.familyData.stateId;
            $scope.familyData.primaryLanguageId = $scope.familyData.primaryLanguageId;
            $scope.familyData.secondaryLanguageId = $scope.familyData.secondaryLanguageId;            
            $scope.familyData.ethnicOriginId = $scope.familyData.ethnicOriginId;
            $scope.familyData.branchId = $scope.familyData.branchId;
            $scope.familyData.membershipStatusId = $scope.familyData.membershipStatusId;
            $scope.familyData.membershipStatusId = $scope.familyData.membershipStatusId;

            familyService.saveFamily($scope.familyData).then(function (results) {

                if ($scope.familyData.id == 0) {
                    familyForm.$setPristine();
                    $scope.clear();
                }
                else
                    $scope.formLoad();               

                $scope.alertClass = "alert-success";
                $scope.alertMsg = sharedService.getSaveSuccessMsg("family");
                $scope.showAlert = true;

            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;

                if (error.data === 3) {
                    $scope.formLoad();
                    $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                }
                else if (error.data === 2) {
                    $scope.formLoad();
                    $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                }
                else
                    $scope.alertMsg = sharedService.getShortErrorMsg();
            });            
        }
    };   

}]);

