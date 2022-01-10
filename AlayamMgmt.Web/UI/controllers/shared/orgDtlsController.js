'use strict';
app.controller('orgDtlsController', ['$scope', 'organizationService', '$routeParams', 'authService',
    function ($scope, organizationService, $routeParams, authService) {

    $scope.showAlert = false;
    $scope.alertClass;
    $scope.alertMsg;


    $scope.orgData = {
        id: 0,
        name: "",
        address1: "",
        address2: "",
        address3: "",
        city: "",
        country: "",
        denomination: "",
        discription: "",
        emailId1: "",
        emailId2: "",
        ethnicOrigin: "",
        phone1: "",
        phone2: "",
        primaryLanguage: "",
        secondaryLanguage: "",
        state: "",
        website: "",
        zipCode: "",
        active: true,
        rowTimeStamp: "",
        parentId: null,
        currency: "",
        taxId: ""
    };
    
    $scope.formLoad = function () {     

        $scope.authentication = authService.authentication;

        if ($routeParams.id !== undefined) {

            if ($scope.authentication.role == "1") {
                organizationService.getOrganizationById($routeParams.id).then(function (results) {
                    $scope.orgData = results.data;
                })
            }
            else
            {
                organizationService.getOrganization().then(function (results) {
                    $scope.orgData = results.data;
                })
            }
        }    
    };
   
}]);

