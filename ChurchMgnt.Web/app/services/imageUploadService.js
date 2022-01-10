'use strict';
app.factory('imageUploadService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var imageUploadServiceFactory = {};

    var _get = function () {

        return $http.get(serviceBase + 'api/Upload/get').then(function (results) {
            return results;
        });      
    };

    var _getLogo = function () {

        return $http.get(serviceBase + 'api/logo/get').then(function (results) {
            return results;
        });
    };

    var _getSignature = function () {

        return $http.get(serviceBase + 'api/signature/get').then(function (results) {
            return results;
        });
    };

    imageUploadServiceFactory.get = _get;
    imageUploadServiceFactory.getLogo = _getLogo;
    imageUploadServiceFactory.getSignature = _getSignature;

    return imageUploadServiceFactory;
}]);