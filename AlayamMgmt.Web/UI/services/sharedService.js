'use strict';
app.factory('sharedService', ['$http', function ($http) {

    var sharedServiceFactory = {};

    var _getErrorMsg = function (errorCode, fieldName) {
        
        if(errorCode === 1)          
            return 'The application has encountered an unknown error. It doesn\'t appear to have affected your data, but our technical staff have been automatically notified and will be looking into this with the utmost urgency.';
        else if(errorCode === 2)   
            return 'The application unable to save record due to unique contraint error violated, The name ' + fieldName + ' available in the application.';            
        else if(errorCode === 3)
            return 'The record you are working on has been modified by another user. The new values for this record are shown below. Changes you have made have not been saved, please resubmit.';
    };


    var _getSaveSuccessMsg = function (fieldName) {

        return fieldName + ' successfully saved in the system.';
    };

    var _getEnableSuccessMsg = function (fieldName) {

        return fieldName + ' successfully enabled in the system.';
    };

    var _getDisableSuccessMsg = function (fieldName) {

        return fieldName + ' successfully disabled in the system.';
    };

    var _getShortSaveSuccessMsg = function () {

        return 'Successfully saved';
    };

    var _getShortEnableSuccessMsg = function () {

        return ' Successfully enabled.';
    };

    var _getShortDisableSuccessMsg = function () {

        return 'Successfully disabled';
    };

    var _getShortErrorMsg = function () {
        return 'Error occurred, try again';
    };

    var _getShortErrorMsgForEmptyList = function () {
        return 'No record available';
    };

    var _getShortUniqueErrorMsg = function () {
        return 'Same kind of record available, Please try new value';
    };
    var _getShortConcurrencyErrorMsg = function () {
        return 'Same record modified by another user, Please resumit with latst data';
    };

    var _sum = function (data, key) {        
        if (angular.isUndefined(data) && angular.isUndefined(key))
            return 0;
        var sum = 0;

        angular.forEach(data, function (v, k) {
            sum = sum + parseInt(v[key]);
        });
        return sum;
    };

    sharedServiceFactory.getErrorMsg = _getErrorMsg;
    sharedServiceFactory.getSaveSuccessMsg = _getSaveSuccessMsg;
    sharedServiceFactory.getEnableSuccessMsg = _getEnableSuccessMsg;
    sharedServiceFactory.getDisableSuccessMsg = _getDisableSuccessMsg;

    sharedServiceFactory.getShortSaveSuccessMsg = _getShortSaveSuccessMsg;
    sharedServiceFactory.getShortEnableSuccessMsg = _getShortEnableSuccessMsg;
    sharedServiceFactory.getShortDisableSuccessMsg = _getShortDisableSuccessMsg;
    sharedServiceFactory.getShortErrorMsg = _getShortErrorMsg;
    sharedServiceFactory.getShortUniqueErrorMsg = _getShortUniqueErrorMsg;
    sharedServiceFactory.getShortErrorMsgForEmptyList = _getShortErrorMsgForEmptyList;
    sharedServiceFactory.sum = _sum;    
    sharedServiceFactory.getShortConcurrencyErrorMsg = _getShortConcurrencyErrorMsg;

    return sharedServiceFactory;

}]);