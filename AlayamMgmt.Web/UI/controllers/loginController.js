'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.loginData = {
        userName: "",
        password: "",
        useRefreshTokens: true,
        rememberMe: false
    };

    $scope.message = "";

    if (authService.authentication.isAuth == true) {
        if ($scope.authentication.role == 1)
            $location.path('/orglst');
        else
            $location.path('/home');
    }
  

    $scope.login = function (loginForm) {

        if (loginForm.$valid) {
            authService.login($scope.loginData).then(function (response) {

                $scope.authentication = authService.authentication;

                if ($scope.authentication.role == 1)
                    $location.path('/shared/orglst');
                else
                    $location.path('/home');

            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };
    }

    

}]);