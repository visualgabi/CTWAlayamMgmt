(function(app) {
    'use strict';

    app.directive('topBar', topBar);

    function topBar() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/UI/layout/topBar.html'
        }
    }

})(angular.module('common.ui'));