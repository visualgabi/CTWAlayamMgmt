'use strict';
app.controller('familyDtlsCtrl', ['$scope', 'familyService', '$stateParams', function ($scope, familyService, $stateParams) {

    $scope.family;

    $scope.formLoad = function () {

        if ($stateParams.id !== undefined) {

            familyService.getFamilyById($stateParams.id).then(function (results) {
                $scope.family = results.data;
            })
        }
    };

}]);