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

    var _jsonToCSVConvertor = function (JSONData, ReportTitle, ShowLabel) {  
        //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
        var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

        var CSV = '';
        //Set Report title in first row or line

        CSV += ReportTitle + '\r\n\n';

        //This condition will generate the Label/Header
        if (ShowLabel) {
            var row = "";

            //This loop will extract the label from 1st index of on array
            for (var index in arrData[0]) {

                //Now convert each value to string and comma-seprated
                row += index + ',';
            }

            row = row.slice(0, -1);

            //append Label row with line break
            CSV += row + '\r\n';
        }

        //1st loop is to extract each row
        for (var i = 0; i < arrData.length; i++) {
            var row = "";

            //2nd loop will extract each column and convert it in string comma-seprated
            for (var index in arrData[i]) {
                row += '"' + arrData[i][index] + '",';
            }

            row.slice(0, row.length - 1);

            //add a line break after each row
            CSV += row + '\r\n';
        }

        if (CSV == '') {
            alert("Invalid data");
            return;
        }

        //Generate a file name
        var fileName = "ChurchMgnt_";
        //this will remove the blank-spaces from the title and replace it with an underscore
        fileName += ReportTitle.replace(/ /g, "_");

        //Initialize file format you want csv or xls
        var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

        // Now the little tricky part.
        // you can use either>> window.open(uri);
        // but this will not work in some browsers
        // or you will not get the correct file extension    

        //this trick will generate a temp <a /> tag
        var link = document.createElement("a");
        link.href = uri;

        //set the visibility hidden so it will not effect on your web-layout
        link.style = "visibility:hidden";
        link.download = fileName + ".csv";

        //this part will append the anchor tag and remove it after automatic click
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }

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
    sharedServiceFactory.jsonToCSVConvertor = _jsonToCSVConvertor;
    

    return sharedServiceFactory;

}]);