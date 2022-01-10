'use strict';
app.controller('branchCtrl', ['$scope', 'lookupService', 'organizationService', 'sharedService', 'authService', '$location','$stateParams','$state',
    function ($scope, lookupService, organizationService, sharedService, authService, location, $stateParams, $state) {

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.authentication = authService.authentication;

    $scope.branchData = {
        id: $stateParams.id,
        name: "",
        address1: "",
        address2: "",
        address3: "",
        city: "",
        countryId: "",
        denominationId: "",
        discription: "",
        emailId1: "",
        emailId2: "",
        ethnicOriginId: "",
        phone1: "",
        phone2: "",
        primaryLanguageId: "",
        secondaryLanguageId: "",
        stateId: "",
        website: "",
        zipCode: "",
        active: true,
        rowTimeStamp: "",
        parentId: $scope.authentication.orgId        
    };

    //$scope.countries = [];
    $scope.states = [];
    //$scope.languages = [];
    //$scope.denominations = [];
    //$scope.ethnicOrigins = [];

    $scope.close = function () {
        $state.go('admin.branchlst');           
    }

    $scope.ClearControls = function () {
        $scope.branchData = {
            id: 0,
            name: "",
            address1: "",
            address2: "",
            address3: "",
            city: "",
            countryId: "",
            denominationId: "",
            discription: "",
            emailId1: "",
            emailId2: "",
            ethnicOriginId: "",
            phone1: "",
            phone2: "",
            primaryLanguageId: "",
            secondaryLanguageId: "",
            stateId: "",
            website: "",
            zipCode: "",
            active: true,
            rowTimeStamp: "",
            parentId: $scope.authentication.orgId            
        };
    }

    $scope.formLoad = function () {

        $scope.ClearControls();

        if ($stateParams.id !== undefined) {
            organizationService.getBranch($stateParams.id).then(function (results) {
                $scope.branchData = results.data;               
            },
            function (error) {
                showErroMessage(error)
            });
        }   


        lookupService.getCountries().then(function (results) {
            $scope.countries = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getStates().then(function (results) {
            $scope.states = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getLanguages().then(function (results) {
            $scope.languages = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getDenominations().then(function (results) {
            $scope.denominations = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getEthnicOrigins().then(function (results) {
            $scope.ethnicOrigins = results.data;

        }, function (error) {
            showErroMessage(error)
        });
    };

    $scope.formReset = function () {
        $scope.initialize();
        $scope.clearMsg();
    };

    $scope.initialize = function () {
        $scope.formLoad();        
    };

    $scope.clearMsg = function () {
        $scope.showAlert = false;
        $scope.alertClass;
        $scope.alertMsg;
    };

    function showErroMessage() {
        $scope.alertClass = "alert-danger";
        $scope.alertMsg = sharedService.getErrorMsg(error.data, $scope.branchData.name);
        $scope.showAlert = true;
    }

    $scope.saveOrg = function (orgForm) {

        if (orgForm.$valid) {

            $scope.branchData.countryId = $scope.branchData.countryId;
            $scope.branchData.stateId = $scope.branchData.stateId;
            $scope.branchData.primaryLanguageId = $scope.branchData.primaryLanguageId;
            $scope.branchData.secondaryLanguageId = $scope.branchData.secondaryLanguageId;
            $scope.branchData.denominationId = $scope.branchData.denominationId;
            $scope.branchData.ethnicOriginId = $scope.branchData.ethnicOriginId;

            organizationService.saveOrganization($scope.branchData).then(function (results) {

                if ($scope.branchData.id == 0)
                {
                    orgForm.$setPristine();
                    $scope.ClearControls();
                }
                else
                {
                    $scope.initialize();
                }

                $scope.alertClass = "alert-success";
                $scope.alertMsg = sharedService.getShortSaveSuccessMsg();

            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.alertMsg = sharedService.getShortErrorMsg();

                if (error.data === 3)
                    $scope.initialize();
            });

            $scope.showAlert = true;
        }
    };
}]);

