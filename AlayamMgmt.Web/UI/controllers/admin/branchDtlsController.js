'use strict';
app.controller('branchDtlsController', ['$scope', 'organizationService', '$routeParams',
    function ($scope, organizationService,$routeParams) {
      

    $scope.showAlert = false;
    $scope.alertClass;
    $scope.alertMsg;


    $scope.branch = {
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
        parentId: null
    };

    $scope.formLoad = function () {

        if ($routeParams.id !== undefined) {

            organizationService.getBranch($routeParams.id).then(function (results) {
                $scope.branch = results.data;
            })
        }
    };

}]);

