'use strict';
app.controller('templateCtrl', ['$scope', 'authService', 'emailTemplateService','$stateParams','sharedService','$state',
    function ($scope, authService, emailTemplateService, $stateParams, sharedService, $state) {
        
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";
        $scope.emailTemplates = [];

        $scope.authentication = authService.authentication;

        $scope.emailTemplate = {
            id: $stateParams.id,
            organizationId: "",
            name: "",
            description: "",
            template: "",
            active: true,
            rowTimeStamp: "",
            content: ""
        };

        $scope.close = function () {
            $state.go('email.templateLst');
        }

        $scope.clear = function () {
            $scope.emailTemplate = {
                id: 0,
                organizationId: "",
                name: "",
                description: "",
                template: "",
                active: true,
                rowTimeStamp: "",
                content: ""
            };
        }

        $scope.formLoad = function () {

            $scope.clear();
            
            emailTemplateService.getByOrgId($scope.authentication.orgId, true).then(function (results) {
                $scope.emailTemplates = results.data;

            }, function (error) {
                showErroMessage(error)
            });

            if ($stateParams.id !== undefined) {
                emailTemplateService.getById($stateParams.id).then(function (results) {
                    $scope.emailTemplate = results.data;
                },
                function (error) {
                    showErroMessage(error)
                });
            }
        };       

        $scope.initialize = function () {
            $scope.formLoad();
        };
    
        function showErroMessage() {
            $scope.alertClass = "alert-danger";
            $scope.alertMsg = sharedService.getErrorMsg(error.data, $scope.branchData.name);
            $scope.showAlert = true;
        }

        $scope.clearAlert = function () {
            $scope.showAlert = false;
            $scope.alertClass = "";
            $scope.alertMsg = "";           
        }       


        $scope.isUnique = function (selectedObj) {

            var returnValue = true;

            angular.forEach($scope.emailTemplates, function (item) {

                if (selectedObj.id != item.id && item.name == selectedObj.name) {
                    returnValue = false;
                    return returnValue;
                }               

            })

            return returnValue;
        }

        $scope.close = function () {
            $state.go('communication.templateLst');
        }

        $scope.save = function (emailTemplateForm) {

            if (emailTemplateForm.$valid) {
                var unique = true;
                unique = $scope.isUnique($scope.emailTemplate);

                if (unique == true) {

                    if (emailTemplateForm.$valid) {
                        $scope.emailTemplate.organizationId = $scope.authentication.orgId;

                        emailTemplateService.save($scope.emailTemplate).then(function (results) {

                            if ($scope.emailTemplate.id == 0) {
                                emailTemplateForm.$setPristine();
                                $scope.clear();
                            }
                            else {
                                $scope.initialize();
                            }

                            $scope.alertClass = "alert-success";
                            $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                            $scope.showAlert = true;

                        }, function (error) {
                            $scope.alertClass = "alert-danger";
                            $scope.showAlert = true;

                            if (error.data === 3) {
                                $scope.initialize();
                                $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                            }
                            else if (error.data === 2) {
                                $scope.initialize();
                                $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                            }
                            else
                                $scope.alertMsg = sharedService.getShortErrorMsg();
                        });

                    }
                }
                else {
                    $scope.alertMsg = sharedService.getShortUniqueErrorMsg();
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;
                }

            }
            else
            {
                if ($scope.emailTemplate.content == "" || $scope.emailTemplate.content === null)
                {
                    $scope.alertMsg = "Please enter email template";
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;
                }
            }       
        
    }

    }]);