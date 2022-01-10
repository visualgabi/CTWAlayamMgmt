'use strict';
app.controller('userController', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'userService', 'roleService', 'organizationService',
    function ($scope, $filter, sharedService, authService, ngTableParams, userService, roleService, organizationService) {

        $scope.users = [];
        //$scope.organizations = [];
       // $scope.roles = [];

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


    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

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

    $scope.formLoad = function () {

        if ($scope.authentication.role == 1) {
            userService.get(null).then(function (results) {
                $scope.users = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.users.length, // length of data
                    getData: function ($defer, params) {

                        var filteredData = $filter('filter')($scope.users, $scope.filterText);

                        var orderedData = params.sorting() ?
                       $filter('orderBy')(filteredData, params.orderBy()) :
                       filteredData;

                        params.total(orderedData.length);
                        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                    }
                })

            }, function (error) {
                alert(error.data.message);
            });
        }
        else
        {
            userService.get($scope.authentication.orgId, null).then(function (results) {
                $scope.users = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.users.length, // length of data
                    getData: function ($defer, params) {

                        var filteredData = $filter('filter')($scope.users, $scope.filterText);

                        var orderedData = params.sorting() ?
                       $filter('orderBy')(filteredData, params.orderBy()) :
                       filteredData;

                        params.total(orderedData.length);
                        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                    }
                })

            }, function (error) {
                alert(error.data.message);
            });
        }
    }

    $scope.$watch("filterText", function () {
        $scope.tableParams.reload();
        $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
    });

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

        if (id != 0) {
            var results = $filter('filter')($scope.users, { id: id });
            $scope.user = results[0];
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

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";
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

        //if ($scope.user.roleId != 1 && $scope.authentication.roleId != 1)
        //    $scope.user.organizationId = $scope.authentication.orgId;

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
    
    $scope.enable = function (id, status) {

        var orgUsers = $filter('filter')($scope.users, { id: id });

        userService.enable(orgUsers[0].id, status).then(function (results) {

            orgUsers[0].active = status;
            orgUsers[0].rowTimeStamp = results.data;

            $scope.alertClassForDelete = "alert-success";
            if (status == true)
                $scope.alertMsgForDelete = sharedService.getShortEnableSuccessMsg();
            else
                $scope.alertMsgForDelete = sharedService.getShortDisableSuccessMsg();

            $scope.showAlertForDelete = true;

            $scope.tableParams.reload();

        }, function (error) {

            $scope.alertClassForDelete = "alert-danger";
            $scope.alertMsgForDelete = sharedService.getShortErrorMsg();

            $scope.showAlertForDelete = true;

            $scope.tableParams.reload();
        });


    };

}]);