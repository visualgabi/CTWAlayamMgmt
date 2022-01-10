'use strict';
app.controller('userDtlsCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'userService', 'roleService', 'organizationService', '$stateParams',
    function ($scope, $filter, sharedService, authService, ngTableParams, userService, roleService, organizationService, $stateParams) {
        

    $scope.user = {
        id: $stateParams.id,
        userName: "",
        password: "",
        roleId: "",
        organizationId: "",
        firstName: "",
        lastName: "",
        active: true,
        rowTimeStamp: ""
    }  


    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";       

    $scope.authentication = authService.authentication; 
   

    $scope.initialize = function () {

        if ($stateParams.id !== undefined) {

            userService.getById($stateParams.id).then(function (results) {
                $scope.user = results.data;               
            })
        }
        
    }

    $scope.shouldShow = function (role) {
        if ($scope.authentication.role == "1")
            return true;
        else
            return role.id != 1;
    }
    


    $scope.clearAlert = function () {
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";        
    }

   $scope.clear = function()
   {
        $scope.user = {
            id: 0,
            userName: "",
            roleId: "",
            organizationId: "",
            password: "",
            firstName: "",
            lastName: "",
            active: true,
            rowTimeStamp: ""
        };
    }
    
   $scope.enable = function (id, status) {

        userService.enable($scope.user.id, status).then(function (results) {

            $scope.user.active = status;
            $scope.user.rowTimeStamp = results.data;

            $scope.alertClass = "alert-success";
            if (status == true)
                $scope.alertMsg = sharedService.getShortEnableSuccessMsg();
            else
                $scope.alertMsg = sharedService.getShortDisableSuccessMsg();

            $scope.showAlert = true;
            

        }, function (error) {

            $scope.alertClass = "alert-danger";
            $scope.alertMsg = sharedService.getShortErrorMsg();

            $scope.showAlert = true;            
        });


    };

}]);