'use strict';
app.controller('orgProfileDtlsController', ['$scope', '$routeParams', '$filter', 'sharedService', 'authService', 'Upload', function ($scope, $routeParams, $filter, sharedService, authService, Upload) {

    $scope.orgProfileData = {
        id: 0,
        taxId: "",        
        Currency: "",
        logo: ""          
    };

    $scope.upload = function (file) {
        Upload.upload({            
            file: file
        }).progress(function (evt) {
            var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            console.log('progress: ' + progressPercentage + '% ' + evt.config.file.name);
        }).success(function (data, status, headers, config) {
            console.log('file ' + config.file.name + 'uploaded. Response: ' + data);
        }).error(function (data, status, headers, config) {
            console.log('error status: ' + status);
        })
    };

    $scope.formLoad = function () {

        $scope.orgProfileData = {
            id: 0,
            taxId: "",
            Currency: "",
            logo: ""
        };
    }

}]);