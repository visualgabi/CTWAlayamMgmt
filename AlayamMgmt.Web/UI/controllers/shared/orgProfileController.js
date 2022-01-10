'use strict';
app.controller('orgProfileController', ['$scope', '$routeParams', '$filter', 'sharedService', 'authService', 'Upload', 'lookupService', function ($scope, $routeParams, $filter, sharedService, authService, Upload, lookupService) {

    $scope.orgProfileData = {
        id: 0,
        organizationId: 0,
        taxId: "",
        currencyId: "",
        logo: "",
        file: "Image Not Yet uploaded"
    };

    $scope.initialize = function () {
        $scope.formLoad();        
    };

    $scope.formLoad = function () {

        $scope.orgProfileData = {
            id: 0,
            organizationId: 0,
            taxId: "",
            Currency: "",
            logo: "",
            file: "Image Not Yet uploaded"
        };

        lookupService.getCurrencies().then(function (results) {
            $scope.currencies = results.data;

        }, function (error) {
            showErroMessage(error)
        });

    }

    $scope.currencies = [];

    $scope.save = function (orgForm) {

        if (orgForm.$valid) {
            $scope.orgProfileData.organizationId = $scope.authentication.orgId;
           
        }
    };

    //$scope.upload = function (file) {
    //    Upload.upload({
    //        file: file
    //    }).progress(function (evt) {
    //        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
    //        console.log('progress: ' + progressPercentage + '% ' + evt.config.file.name);
    //    }).success(function (data, status, headers, config) {
    //        console.log('file ' + config.file.name + 'uploaded. Response: ' + data);
    //    }).error(function (data, status, headers, config) {
    //        console.log('error status: ' + status);
    //    })
    //};

    //lookupService.getCurrencies().then(function (results) {
    //    $scope.currencies = results.data;

    //}, function (error) {
    //    showErroMessage(error)
    //});  

}]);