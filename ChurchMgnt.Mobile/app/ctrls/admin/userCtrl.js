'use strict';
app.controller('userCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'userService', 'roleService', 'organizationService','$stateParams',
    function ($scope, $filter, sharedService, authService, ngTableParams, userService, roleService, organizationService, $stateParams) {

        $scope.users = [];      

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

    $scope.action = "";

    $scope.authentication = authService.authentication;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.user = {
                id: 0,
                userName: "",
                password: "",
                roleId: "",
                organizationId: "",
                firstName: "",
                lastName: "",
                active: true,
                rowTimeStamp: ""
            }           
        }
    }

    $scope.isUnique = function (userId, value, ctrl) {

        var results = $filter('filter')($scope.users, { userName: value }, true);

        if ((userId == 0 && results.length > 0) || (userId > 0 && results.length > 1))
            ctrl.$setValidity('isunique', false);
        else
            ctrl.$setValidity('isunique', true);
    }
   

    $scope.initialize = function (id) {        

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
        }

        if ($stateParams.id !== undefined) {

            userService.getById($stateParams.id).then(function (results) {
                $scope.user = results.data;                
            })
        }
      

        if ($scope.authentication.role == "1")
        {
            organizationService.getOrganizations(null).then(function (results) {
                $scope.organizations = results.data;

            }, function (error) {
                showErroMessage(error)
            });          
        }
        else
        {
            organizationService.getOrganization(null).then(function (results) {
                $scope.organizations.push(results.data);               

            }, function (error) {
                showErroMessage(error)
            });           
        }

        roleService.get(null).then(function (results) {
            $scope.roles = results.data;

        }, function (error) {
            showErroMessage(error)
        });       
        
    }

    $scope.shouldShow = function (role) {
        if ($scope.authentication.role == "1")
            return true;
        else
            return role.id != 1;
    }

    $scope.load = function () {
        if ($scope.authentication.role == "1") {
            userService.get(null).then(function (results) {
                $scope.users = results.data;
                $scope.tableParams.reload();
            });
        }
        else
        {
            userService.get($scope.authentication.orgId, null).then(function (results) {
                $scope.users = results.data;
                $scope.tableParams.reload();
            });
        }
    }

    $scope.loadAfterEdit = function (id) {
        if ($scope.authentication.role == "1") {
            userService.get(null).then(function (results) {
                $scope.users = results.data;
                $scope.initialize(id);
            });
        }
        else {
            userService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.users = results.data;
                $scope.initialize(id);
            });
        }
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

    $scope.save = function (userForm) {       
     

        if ($scope.authentication.role != 1)
            $scope.user.organizationId = $scope.authentication.orgId;


        if (userForm.$valid) {
            if ($scope.user.id != 0)
                $scope.edit(userForm);
            else {  
                    userService.save($scope.user).then(function (results) {
                        $scope.alertClass = "alert-success";
                        $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                        //$scope.loadUsers();
                        userForm.$setPristine();
                        $scope.clear();

                    }, function (error) {
                        $scope.alertClass = "alert-danger";
                        $scope.showAlert = true;

                        if (error.data === 3) {
                            $scope.loadAfterEdit($scope.user.id);
                            $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                        }
                        else if (error.data === 2) {
                            $scope.loadAfterEdit($scope.user.id);
                            $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                        }
                        else
                            $scope.alertMsg = sharedService.getShortErrorMsg();
                    });

                    $scope.showAlert = true;
                }
        }
    };

    $scope.edit = function (userForm) {

        userService.edit($scope.user).then(function (results) {
            $scope.alertClass = "alert-success";
            $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
            $scope.loadAfterEdit($scope.user.id);
            $scope.showAlert = true;

        }, function (error) {
            $scope.alertClass = "alert-danger";
            $scope.showAlert = true;

            if (error.data === 3) {
                $scope.loadAfterEdit($scope.user.id);
                $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
            }
            else if (error.data === 2) {
                $scope.loadAfterEdit($scope.user.id);
                $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
            }
            else
                $scope.alertMsg = sharedService.getShortErrorMsg();

            $scope.showAlert = true;
        });

    };   

}]);