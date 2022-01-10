'use strict';
app.controller('branchDtlsCtrl', ['$scope', 'organizationService', '$stateParams',
    function ($scope, organizationService, $stateParams) {
      

    $scope.showAlert = false;
    $scope.alertClass;
    $scope.alertMsg;


    $scope.branch = {
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
        parentId: null
    };

    $scope.formLoad = function () {

        if ($stateParams.id !== undefined) {

            organizationService.getBranch($stateParams.id).then(function (results) {
                $scope.branch = results.data;
            })
        }
    };

}]);

