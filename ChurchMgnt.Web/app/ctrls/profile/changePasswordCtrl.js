'use strict';
app.controller('changePasswordCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService','smtpSettingService','profileService','$window',
function ($scope, $filter, sharedService, authService, ngTableParams, lookupService, smtpSettingService, profileService, $window) {
        
    $scope.authentication = authService.authentication;


    $scope.changePassword = {
        userId: $scope.authentication.userId,
        password: "",
        confirmPassword: "",
        userEmail: $scope.authentication.userName
    }

    $scope.alertClass = "";
    $scope.alertMsg = "";
    $scope.showAlert = false;   

    $scope.change = function (changePasswordForm) {

        if (changePasswordForm.$valid) {

            profileService.changePassword($scope.changePassword).then(function (results) {              
                authService.logOut();
               
                $scope.alertClass = "alert-success";
                $scope.alertMsg = "You login password got changed and system emailed the new password also, Please logout and login back in the system";
                $scope.showAlert = true;

            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;

                if (error.data === 3) {
                    $scope.loadAfterEdit($scope.offering.id);
                    $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                }
                else if (error.data === 2) {
                    $scope.loadAfterEdit($scope.offering.id);
                    $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                }
                else
                    $scope.alertMsg = sharedService.getShortErrorMsg();
            });
        }
    };

    $scope.reset = function () {
        $window.location.reload();
    }

}]);