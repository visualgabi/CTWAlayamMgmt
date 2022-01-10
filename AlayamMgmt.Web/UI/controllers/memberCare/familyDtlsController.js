'use strict';
app.controller('familyDtlsController', ['$scope', 'familyService', '$routeParams', function ($scope, familyService, $routeParams) {

    $scope.family;

    $scope.formLoad = function () {

        if ($routeParams.id !== undefined) {

            familyService.getFamilyById($routeParams.id).then(function (results) {
                $scope.family = results.data;
            })
        }
    };

}]);