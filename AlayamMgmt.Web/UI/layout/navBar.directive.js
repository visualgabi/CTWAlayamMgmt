(function (app) {
    'use strict';

    app.directive('navBar', navBar);

    function navBar() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/UI/layout/navBar.html'
        }
    }

})(angular.module('common.ui'));