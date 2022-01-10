'use strict';
app.controller('imageUploadCtrl', ['$scope', 'authService', 'imageUploadService', '$stateParams', 'sharedService', '$state','ngAuthSettings','FileUploader','$window','localStorageService','Lightbox',
function ($scope, authService, imageUploadService, $stateParams, sharedService, $state, ngAuthSettings, FileUploader, $window, localStorageService, Lightbox) {

    var authData = JSON.parse($window.sessionStorage.getItem('authorizationData'));
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    $scope.images = [];    

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    if (authData == null) {
        authData = localStorageService.get('authorizationData');
    }

    var uploader = $scope.uploader = new FileUploader({  
        url: serviceBase + 'api/Upload/post'
    });   
  

    // FILTERS  
  
    uploader.filters.push({  
        name: 'extensionFilter',  
        fn: function (item, options) {  
            var filename = item.name;  
            var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();  
            if (extension == "jpg" || extension == "png" || extension == "gif" )  
                return true;  
            else {
                showMsg('Invalid file format. Please select a file with jpg/png/gif format and try again.', 2)                
                return false;  
            }  
        }  
    });  
  
    uploader.filters.push({  
        name: 'sizeFilter',  
        fn: function (item, options) {  
            var fileSize = item.size;  
            fileSize = parseInt(fileSize) / (1024 * 1024);  
            if (fileSize <= 5)  
                return true;  
            else {
                showMsg('Selected file exceeds the 5MB file size limit. Please choose a new file and try again.', 2)
                //alert('Selected file exceeds the 5MB file size limit. Please choose a new file and try again.');  
                return false;  
            }  
        }  
    });  
  
    uploader.filters.push({  
        name: 'itemResetFilter',  
        fn: function (item, options) {  
            if (this.queue.length < 5)  
                return true;  
            else {
                showMsg('You have exceeded the limit of uploading files.', 2)
               // alert('You have exceeded the limit of uploading files.');  
                return false;  
            }  
        }  
    });  
   
    // CALLBACKS  

    var showMsg = function(msg, type)
    {
        if (type==1)
            $scope.alertClass = "alert-success";
        else
            $scope.alertClass = "alert-danger";

        $scope.alertMsg = msg;
        $scope.showAlert = true;       
    }

    var clear = function () {
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";
    }
   
    uploader.onWhenAddingFileFailed = function (item, filter, options) {  
        console.info('onWhenAddingFileFailed', item, filter, options);  
    };  
    uploader.onAfterAddingFile = function (fileItem) {
        console.info('Files ready for upload.');
        //alert('Files ready for upload.');
      
    };  
  
    uploader.onSuccessItem = function (fileItem, response, status, headers) {  
        $scope.uploader.queue = [];  
        $scope.uploader.progress = 0;
        $scope.formLoad();
        showMsg('Selected file has been uploaded successfully.', 1);
       //// clear();
        //loadImagesAfterUpload();
        
    };  
    uploader.onErrorItem = function (fileItem, response, status, headers) {
        showMsg('We were unable to upload your file. Please logout and login back then try again.', 2)
    };  
    uploader.onCancelItem = function (fileItem, response, status, headers) {
        showMsg('File uploading has been cancelled.', 2)        
    };  
  
    uploader.onAfterAddingAll = function(addedFileItems) {  
        console.info('onAfterAddingAll', addedFileItems);  
    };  
    uploader.onBeforeUploadItem = function (item) {        
        item.headers = {
            'Authorization': 'Bearer ' + authData.token
        };
        console.info('onBeforeUploadItem', item);  
    };  
    uploader.onProgressItem = function(fileItem, progress) {  
        console.info('onProgressItem', fileItem, progress);  
    };  
    uploader.onProgressAll = function(progress) {  
        console.info('onProgressAll', progress);  
    };  
          
    uploader.onCompleteItem = function(fileItem, response, status, headers) {  
        console.info('onCompleteItem', fileItem, response, status, headers);  
    };  
    uploader.onCompleteAll = function() {  
        console.info('onCompleteAll');  
    };  
  
    console.info('uploader', uploader);

    $scope.clearUploader = function()
    {
        $scope.uploader.queue = [];
        $scope.uploader.progress = 0;
    }


    $scope.formLoad = function () {
        clear();
        $scope.images = [];
        imageUploadService.get().then(function (results) {
            $scope.fileNames = results.data;
            var image;
            for (var iPos = 0; iPos < $scope.fileNames.length; iPos++) {
                image = { 'url': serviceBase + $scope.fileNames[iPos], 'thumbUrl': serviceBase + $scope.fileNames[iPos] };
                $scope.images.push(image);
            }            
        });
    }   
   

    $scope.openLightboxModal = function (index) {
        Lightbox.openModal($scope.images, index);
    };


    }]);