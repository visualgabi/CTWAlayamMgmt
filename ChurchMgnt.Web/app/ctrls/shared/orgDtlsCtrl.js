'use strict';
app.controller('orgDtlsCtrl', ['$scope', 'organizationService', '$stateParams', 'authService',
    function ($scope, organizationService, $stateParams, authService) {

    $scope.showAlert = false;
    $scope.alertClass;
    $scope.alertMsg;


    $scope.orgData = {
        id: $stateParams.id,
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

        if ($stateParams.id !== undefined) {

            if ($scope.authentication.role == "1") {
                organizationService.getOrganizationById($stateParams.id).then(function (results) {
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

