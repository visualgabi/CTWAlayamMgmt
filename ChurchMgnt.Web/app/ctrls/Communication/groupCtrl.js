'use strict';
app.controller('groupCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService', 'groupService', 'organizationService',
function ($scope, $filter, sharedService, authService, ngTableParams, lookupService, groupService, organizationService) {

    $scope.groups = [];
    $scope.filterText;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

    $scope.action = "";

    $scope.group = {
        id: 0,
        organizationId: "",
        name: "",
        description: "",
        active: true,
        rowTimeStamp: ""
    }

    $scope.clear = function () {
        $scope.group = {
            id: 0,
            organizationId: "",
            name: "",
            description: "",
            active: true,
            rowTimeStamp: ""
        }
    }

    $scope.authentication = authService.authentication;

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }

    $scope.formLoad = function () {
        groupService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.groups = results.data;

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.groups.length, // length of data
                getData: function ($defer, params) {

                    $scope.filteredData = $filter('filter')($scope.groups, $scope.filterText);

                    var orderedData = params.sorting() ?
                   $filter('orderBy')($scope.filteredData, params.orderBy()) :
                   $scope.filteredData;

                    params.total(orderedData.length);
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            })

        }, function (error) {
            alert(error.data.message);
        });
    }

    $scope.$watch("filterText", function () {
        $scope.tableParams.reload();
        $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
    });

    $scope.initialize = function (id) {

        $scope.clear();

        if (id != 0) {
            var results = $filter('filter')($scope.groups, { id: id }, true);
            $scope.group = results[0];
        }       
    };  

    $scope.clearAlert = function () {
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";
    }

    $scope.load = function () {
        groupService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.groups = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.loadAfterEdit = function (id) {

        groupService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.groups = results.data;
            $scope.initialize(id);
        });
    }

    $scope.save = function (groupForm) {

        if (groupForm.$valid) {
            $scope.group.organizationId = $scope.authentication.orgId;
            var unique = $scope.isUnique($scope.group);

            if (unique == true) {

                groupService.save($scope.group).then(function (results) {

                    if ($scope.group.id == 0) {
                        groupForm.$setPristine();
                        $scope.clear();
                    }
                    else
                        $scope.loadAfterEdit($scope.group.id);

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                    $scope.showAlert = true;

                }, function (error) {
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;

                    if (error.data === 3) {
                        $scope.loadAfterEdit($scope.group.id);
                        $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                    }
                    else if (error.data === 2) {
                        $scope.loadAfterEdit($scope.group.id);
                        $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                    }
                    else
                        $scope.alertMsg = sharedService.getShortErrorMsg();
                });

                $scope.showAlert = true;
            }
            else {
                $scope.alertMsg = sharedService.getShortUniqueErrorMsg();
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;
            }
        }
    };

    $scope.enable = function (id, status) {

        var orgGroups = $filter('filter')($scope.groups, { id: id }, true);

        var unique = $scope.isUnique(orgGroups[0]);

        if (unique == true) {
            groupService.enable(orgGroups[0].id, status).then(function (results) {

                orgGroups[0].active = status;
                orgGroups[0].rowTimeStamp = results.data;

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
        }
        else {
            $scope.alertMsg = sharedService.getShortUniqueErrorMsg();
            $scope.alertClass = "alert-danger";
            $scope.showAlert = true;
        }

    };


    $scope.isUnique = function (selectedObj) {

        var returnValue = true;

        angular.forEach($scope.smtpSettings, function (item) {
            if (item.id != selectedObj.id && item.organizationId == selectedObj.organizationId && item.name == selectedObj.name) {
                returnValue = false;
                return returnValue;
            }
        })

        return returnValue;
    };

}]);