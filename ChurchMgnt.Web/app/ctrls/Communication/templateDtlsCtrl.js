'use strict';
app.controller('templateDtlsCtrl', ['$scope', 'authService', 'emailTemplateService', '$stateParams',
function ($scope, authService, emailTemplateService, $stateParams) {

    $scope.emailTemplate = {
        id: $stateParams.id,
        organizationId: "",
        name: "",
        discription: "",
        template: "",
        active: true,
        rowTimeStamp: "",
        content: ""
    };

    $scope.formLoad = function () {

        if ($stateParams.id !== undefined) {

            emailTemplateService.getById($stateParams.id).then(function (results) {
                $scope.emailTemplate = results.data;
            })
        }
    };


    }])