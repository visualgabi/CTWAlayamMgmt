'use strict';
app.controller('remainderCtrl', ['$scope', 'lookupService', 'familyService', '$stateParams', '$filter', 'sharedService', 'authService', 'organizationService', 'notificationService', '$location', '$state','remainderService',
function ($scope, lookupService, familyService, $stateParams, $filter, sharedService, authService, organizationService, notificationService, $location, $state, remainderService) {

    $scope.birthdayRemainder = {
        memberId: 0,
        familyMember: "",
        birthDate: "",
        emailId: "",
        Phone: "",
        address1: "",
        address2: "",
        city: "",
        zipCode: "",
        wishedStatus: false,
        user: ""
    };

    $scope.authentication = authService.authentication;

    $scope.formLoad = function () {
        remainderService.getBirthdayListByOrgId($scope.authentication.orgId).then(function (results) {
            $scope.birthdayRemainders = results.data;          

        }, function (error) {
            alert(error.data.message);
        });

        remainderService.getTodayMarriageAnniversaryFamiliesByOrgId($scope.authentication.orgId).then(function (results) {
            $scope.marriageAnniversaryRemainders = results.data;

        }, function (error) {
            alert(error.data.message);
        });
    }

    

}]);