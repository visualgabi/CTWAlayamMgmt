'use strict';
app.controller('samplePrintCtrl', ['$scope', '$stateParams', 'sharedService', 'authService', '$window', 'PDFConveterService', 'ngAuthSettings', 'localStorageService',
    function ($scope, $stateParams, sharedService, authService, $window, PDFConveterService, ngAuthSettings, localStorageService) {

        $scope.initialize = function () {
            $scope.familyOfferingDetails = localStorageService.get('familyOfferingDetails');
        }

    }]);