'use strict';
app.controller('forgotPwdController', ['$scope', '$routeParams', '$filter', 'sharedService', 'authService', 'ngTableParams', 'userService', 'roleService', 'organizationService',
    function ($scope, $routeParams, $filter, sharedService, authService, ngTableParams, userService, roleService, organizationService) {

        $scope.forgotPwd = {            
            email: ""           
        }

        $scope.ClearControls = function () {
            $scope.forgotPwd = {
                email: ""
            }
        }

        $scope.save = function (forgotPwdForm) {

            if (forgotPwdForm.$valid) {
                userService.forgotPwd($scope.forgotPwd).then(function (results) {

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = "System successfully sent your password to your email account";
                    
                    userForm.$setPristine();
                    $scope.ClearControls();

                }, function (error) {
                    $scope.alertClass = "alert-danger";
                    $scope.alertMsg = sharedService.getShortErrorMsg();
                  
                });

                $scope.showAlert = true;
            }
        }

    }]);