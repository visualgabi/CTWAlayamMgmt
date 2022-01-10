'use strict';
app.factory('PDFConveterService', ['$http', '$q', 'ngAuthSettings', '$window', function ($http, $q, ngAuthSettings,$window) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var PDFConveterFactory = {};

    var _print = function (data) {

        //return $http.get(serviceBase + 'api/PDFConveter/Convert?id=' + data).then(function (results) {
        //    return results;
        //});

        var deferred = $q.defer();
        //, responseType: 'arraybuffer'

        // $http.post(serviceBase + 'api/PDFConveter/DownloadPDF', JSON.stringify(data), { headers: { 'Content-Type': 'application/json', responseType: 'arraybuffer' } }).then(function (successResponse) {
        $http.post(serviceBase + 'api/PDFConveter/DownloadPDF', JSON.stringify(data), { 'responseType': 'arraybuffer' } ).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    PDFConveterFactory.print = _print;

    return PDFConveterFactory;

}]);