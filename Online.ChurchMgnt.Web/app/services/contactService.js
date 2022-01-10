'use strict';
app.factory('contactService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var contactFactory = {};
  
    var _sendMessage = function (data) {

        var deferred = $q.defer();

        $http.post(serviceBase + 'api/contact/SendMessage', JSON.stringify(data), { headers: { 'Content-Type': 'application/json' } }).then(function (successResponse) {
            deferred.resolve(successResponse);
        }, function (failerResponse) {
            deferred.reject(failerResponse);
        });

        return deferred.promise;
    };

    
    contactFactory.sendMessage = _sendMessage;

    return contactFactory;
}]);