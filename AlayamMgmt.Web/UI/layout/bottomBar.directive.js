(function (app) {
    'use strict';

    app.directive('bottomBar', bottomBar);

    function bottomBar() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/UI/layout/bottomBar.html'
        }
    }

})(angular.module('common.ui'));