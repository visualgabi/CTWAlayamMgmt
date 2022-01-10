'use strict';
app.controller('smtpSettingCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService','smtpSettingService','organizationService',
function ($scope, $filter, sharedService, authService, ngTableParams, lookupService, smtpSettingService, organizationService) {

        $scope.smtpSettings = [];
        $scope.filterText;

        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";

        $scope.action = "";

        $scope.smtpSetting = {
            id: 0,
            organizationId: "",
            smtpServer: "",
            smtpServerPort: "",
            smtpServerUserName: "",
            smtpServerPassword: "",
            fromEmailId: "",
            fromEmailLabel: "",            
            active: true,
            rowTimeStamp: ""            
        }

        $scope.clear = function () {
            $scope.smtpSetting = {
                id: 0,
                organizationId: "",
                smtpServer: "",
                smtpServerPort: "",
                smtpServerUserName: "",
                smtpServerPassword: "",
                fromEmailId: "",
                fromEmailLabel: "",
                active: true,
                rowTimeStamp: ""
            }
        }

        $scope.setAction = function (value) {
            $scope.action = value;

            if ($scope.action == 'add') {
                $scope.clear();
            }
        }

        $scope.formLoad = function () {
            smtpSettingService.get(null).then(function (results) {
                $scope.smtpSettings = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.smtpSettings.length, // length of data
                    getData: function ($defer, params) {

                        $scope.filteredData = $filter('filter')($scope.smtpSettings, $scope.filterText);

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
                var results = $filter('filter')($scope.smtpSettings, { id: id }, true);
                $scope.smtpSetting = results[0];               
            }

            organizationService.getOrganizations(null).then(function (results) {
                $scope.organizations = results.data;

            }, function (error) {
                showErroMessage(error)
            });           
        };

        $scope.availableOrganizations = function (id) {
            return function (item) {

                if (id == 0) {
                    return item.active == true;
                }
                else {
                    return true;
                }

            };
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
            smtpSettingService.get(null).then(function (results) {
                $scope.smtpSettings = results.data;
                $scope.tableParams.reload();
            });
        }

        $scope.loadAfterEdit = function (id) {

            smtpSettingService.get(null).then(function (results) {
                $scope.smtpSettings = results.data;
                $scope.initialize(id);
            });
        }

        $scope.save = function (smtpSettingForm) {

            if (smtpSettingForm.$valid) {      
                
                var unique = $scope.isUnique($scope.smtpSetting);               

                if (unique == true) {

                    smtpSettingService.save($scope.smtpSetting).then(function (results) {

                        if ($scope.smtpSetting.id == 0) {
                            smtpSettingForm.$setPristine();
                            $scope.clear();
                        }
                        else
                            $scope.loadAfterEdit($scope.smtpSetting.id);

                        $scope.alertClass = "alert-success";
                        $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                        $scope.showAlert = true;

                    }, function (error) {
                        $scope.alertClass = "alert-danger";
                        $scope.showAlert = true;

                        if (error.data === 3) {
                            $scope.loadAfterEdit($scope.smtpSetting.id);
                            $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                        }
                        else if (error.data === 2) {
                            $scope.loadAfterEdit($scope.smtpSetting.id);
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

            var orgSMTPSettings = $filter('filter')($scope.smtpSettings, { id: id }, true);

            var unique = $scope.isUnique(orgSMTPSettings[0]);

            if (unique == true) {
                smtpSettingService.enable(orgSMTPSettings[0].id, status).then(function (results) {

                    orgSMTPSettings[0].active = status;
                    orgSMTPSettings[0].rowTimeStamp = results.data;

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
                if (item.id != selectedObj.id && item.organizationId == selectedObj.organizationId) {
                    returnValue = false;
                    return returnValue;
                }
            })

            return returnValue;
        };

    }]);