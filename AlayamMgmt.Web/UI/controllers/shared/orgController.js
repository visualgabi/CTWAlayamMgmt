'use strict';
app.controller('orgController', ['$scope', 'lookupService', 'organizationService', '$routeParams', 'sharedService', 'authService', '$location',
    function ($scope, lookupService, organizationService, $routeParams, sharedService, authService, location) {
        
    $scope.showAlert= false;
    $scope.alertClass="";
    $scope.alertMsg="";     
    
    $scope.authentication = authService.authentication;

    $scope.orgData = {
        id: 0,
        name: "",
        address1: "",
        address2: "",
        address3: "",
        city: "",        
        countryId : "",
        denominationId : "",
        discription : "",
        emailId1 : "",
        emailId2 : "",
        ethnicOriginId : "",        
        phone1 : "",
        phone2 : "",
        primaryLanguageId :"",
        secondaryLanguageId : "",
        stateId : "",
        website : "",
        zipCode: "",
        active:true,
        rowTimeStamp: "",
        parentId: null,
        currencyId:"",
        taxId: "",
        fiscalYearStartAndEndMonthId :""
    };

    //$scope.countries = [];
    $scope.states = [];
    //$scope.languages = [];    
    //$scope.denominations = [];
    //$scope.ethnicOrigins = [];
    $scope.currencies = [];    
    $scope.fiscalYearStartAndEndMonths = [];
        
    $scope.formLoad = function () {
       
        $scope.orgData = {
            id: 0,
            name: "",
            address1: "",
            address2: "",
            address3: "",
            city: "",
            countryId:"",
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
            parentId: null,
            currencyId: "",
            taxId: "",
            fiscalYearStartAndEndMonthId: ""
        };    

        if ($routeParams.id != null) {
            if ($scope.authentication.role == "1") {
                organizationService.getOrganizationById($routeParams.id).then(function (results) {
                    $scope.orgData = results.data;
                })
            }
            else {
                organizationService.getOrganization().then(function (results) {
                    $scope.orgData = results.data;
                })
            }
        }       

        lookupService.getCountries(true).then(function (results) {
            $scope.countries = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getFiscalYearStartAndEndMonths(true).then(function (results) {
            $scope.fiscalYearStartAndEndMonths = results.data;

        }, function (error) {
            showErroMessage(error)
        });        

        lookupService.getCurrencies(true).then(function (results) {
            $scope.currencies = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getStates(true).then(function (results) {
            $scope.states = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getLanguages(true).then(function (results) {
            $scope.languages = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getDenominations(true).then(function (results) {
            $scope.denominations = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        lookupService.getEthnicOrigins(true).then(function (results) {
            $scope.ethnicOrigins = results.data;

        }, function (error) {
            showErroMessage(error)
        });
    };

    $scope.close = function () {
        if ($scope.authentication.role == 1)
            location.path('/shared/orglst');
        else
            location.path('/shared/orgDtls/' + $scope.authentication.orgId);
    }

    $scope.initialize = function () {
        $scope.formLoad();      
    };

    $scope.clearForm = function () 
    {
        $scope.orgData = {
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
            parentId: null,
            currencyId: "",
            taxId: "",
            fiscalYearStartAndEndMonthId: ""
        };
    }

    $scope.clearMsg = function () {
        $scope.showAlert = false;
        $scope.alertClass;
        $scope.alertMsg;
    };

    function showErroMessage()
    {
        $scope.alertClass = "alert-danger";
        $scope.alertMsg = sharedService.getErrorMsg(error.data, $scope.orgData.name);
        $scope.showAlert = true;
    }

    $scope.saveOrg = function (orgForm) {

        if (orgForm.$valid) {

            $scope.orgData.countryId = $scope.orgData.countryId;
            $scope.orgData.stateId = $scope.orgData.stateId;
            $scope.orgData.primaryLanguageId = $scope.orgData.primaryLanguageId;
            $scope.orgData.secondaryLanguageId = $scope.orgData.secondaryLanguageId;
            $scope.orgData.denominationId = $scope.orgData.denominationId;
            $scope.orgData.ethnicOriginId = $scope.orgData.ethnicOriginId;           

            organizationService.saveOrganization($scope.orgData).then(function (results) {               
                if ($scope.orgData.id == 0) {
                    orgForm.$setPristine();
                    $scope.clearForm();
                }
                else
                    $scope.initialize();

                $scope.alertClass = "alert-success";
                $scope.alertMsg = sharedService.getSaveSuccessMsg("family");
                $scope.showAlert = true;              
                
            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;

                if (error.data === 3) {
                    $scope.initialize();
                    $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                }
                else if (error.data === 2) {
                    $scope.initialize();
                    $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                }
                else
                    $scope.alertMsg = sharedService.getShortErrorMsg();
            });
            
        }
    };
}]);

