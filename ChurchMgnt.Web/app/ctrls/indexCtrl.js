'use strict';
app.controller('indexCtrl', ['$scope', '$location', 'authService','$state', function ($scope, $location, authService,$state) {

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/login');
    }

    $scope.authentication = authService.authentication;
   $scope.location = $location;
   



}]);