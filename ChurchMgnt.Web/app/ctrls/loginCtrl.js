'use strict';
app.controller('loginCtrl', ['$scope', '$location', 'authService', '$state', function ($scope, $location, authService, $state) {

    $scope.loginData = {
        userName: "",
        password: "",
        useRefreshTokens: true,
        rememberMe: true
    };

    $scope.message = "";

    if (authService.authentication.isAuth == true) {
        if ($scope.authentication.role == 1)
            $state.go('orglst');
            //$location.path('/orglst');
        else
            $state.go('analytics');
            //$location.path('/dashboard');
    }
  

    $scope.login = function (loginForm) {

        if (loginForm.$valid) {
            authService.login($scope.loginData).then(function (response) {

                $scope.authentication = authService.authentication;

                if ($scope.authentication.role == 1)
                    $state.go('orglst');
                    //$location.path('/shared/orglst');
                else
                    $state.go('analytics');
                    //$location.path('/dashboard');

            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };
    }

    

}]);