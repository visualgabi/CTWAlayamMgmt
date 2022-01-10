//'use strict';
//app.factory('notificationService', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

//    var notificationFactory = {};

//    toastr.options = {
//        "debug": false,
//        "positionClass": "toast-top-right",
//        "onclick": null,
//        "fadeIn": 300,
//        "fadeOut": 1000,
//        "timeOut": 3000,
//        "extendedTimeOut": 1000
//    };

//    var _displaySuccess = function displaySuccess(message) {
//        toastr.success(message);
//    }

//    var _displayError = function displayError(error) {
//        if (Array.isArray(error)) {
//            error.forEach(function (err) {
//                toastr.error(err);
//            });
//        } else {
//            toastr.error(error);
//        }
//    }

//    var _displayWarning = function displayWarning(message) {
//        toastr.warning(message);
//    }

//    var _displayInfo = function displayInfo(message) {
//        toastr.info(message);
//    }

//    notificationFactory.displayInfo = _displayInfo;
//    notificationFactory.displayWarning = _displayWarning;
//    notificationFactory.displayError = _displayError;
//    notificationFactory.displaySuccess = _displaySuccess;
   


//    return notificationFactory;

//}]);

(function (app) {
    'use strict';

    app.factory('notificationService', notificationService);

    function notificationService() {

        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        };

        var service = {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        };

        return service;

        function displaySuccess(message) {
            toastr.success(message);
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.forEach(function (err) {
                    toastr.error(err);
                });
            } else {
                toastr.error(error);
            }
        }

        function displayWarning(message) {
            toastr.warning(message);
        }

        function displayInfo(message) {
            toastr.info(message);
        }

    }

})(angular.module('common.core'));