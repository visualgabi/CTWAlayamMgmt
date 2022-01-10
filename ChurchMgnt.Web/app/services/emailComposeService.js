'use strict';
app.factory('emailComposeService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var emailComposeFactory = {};


    var _sendEmail = function (data) {

        var deferred = $q.defer();

        //var payload = new FormData();
      //  payload.append("to", data.to);
       // payload.append("files", data.files);

        $http.post(serviceBase + 'api/FileUpload/upload', data, { headers: { 'Content-Type': undefined } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        

        //var payload = new FormData();
        //payload.append("to", data.to);
        //payload.append("file", data.files[0]);
        
        //$http.post(serviceBase + 'api/EmailCompose/SendEmail', payload, { headers: { 'Content-Type': undefined } }).then(function (successResponse) {
        //    deferred.resolve(successResponse);
        //}, function (failerResponse) {
        //    deferred.reject(failerResponse);
        //});
        

        return deferred.promise;
    };

    var _contributionEmail = function (data) {

        var deferred = $q.defer();
        
        $http.post(serviceBase + 'api/EmailCompose/ContributionLetterInEmail', JSON.stringify(data), { 'responseType': 'arraybuffer' }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;

    };
    
    emailComposeFactory.sendEmail = _sendEmail;
    emailComposeFactory.contributionEmail = _contributionEmail;

    return emailComposeFactory;
}]);