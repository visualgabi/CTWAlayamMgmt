'use strict';
app.controller('emailComposeCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService', 'groupService', 'organizationService', 'emailComposeService','$sce','emailTemplateService','familyMemberService',
function ($scope, $filter, sharedService, authService, ngTableParams, lookupService, groupService, organizationService, emailComposeService, $sce, emailTemplateService, familyMemberService) {
    
    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.emailSending = false;

    $scope.showAlertForTemplate = false;
    $scope.alertClassForTemplate = "";
    $scope.alertMsgForTemplate = "";

    $scope.groups = [];
    $scope.familyMembers = [];
    $scope.familyMembersLoaded = 0;
    $scope.authentication = authService.authentication;

    var fileInput = ""//document.getElementById("fileInput");

    $scope.multiSelected = {};
    $scope.multiSelected.groups = [];    
    $scope.multiSelected.familyMembers = [];
    $scope.templates = [];
        
    $scope.selectedGroupNames = "";
    $scope.selectedMemberNames = "";
    $scope.selectedFileNames = "";
    $scope.selectedFiles = [];
    $scope.formdata = new FormData();

    // Initial step
    $scope.step = 1;

    // Wizard functions
    $scope.wizard = {
        show: function (number) {
            $scope.step = number;
        },
        next: function () {
            goToReview();
        },
        prev: function () {
            $scope.step--;
        }
    };

    var goToReview = function (obj) {
        var errorMsg = emailFormValidate();

        if (errorMsg == "") {
            $scope.alertMsg = "";
            $scope.alertClass = "";
            $scope.showAlert = false;

            getSelectedGroupNames();
            $scope.step++;            
        }
        else {
            $scope.alertMsg = errorMsg;
            $scope.alertClass = "alert-danger";
            $scope.showAlert = true;
        }
    }

    var getSelectedGroupNames = function (obj) {

        var groupArray = [];
        var memberArray = [];
        var fileArray = [];
        $scope.formdata = new FormData();        
        $scope.emailCompose.groups = [];
        $scope.emailCompose.files = [];
        $scope.emailCompose.familyMembers = [];

        angular.forEach($scope.multiSelected.groups, function (value) {
            groupArray.push(value.name);
            if (value.type == "1")
                $scope.emailCompose.organizationLevelGroups.push(value.id);
            else if(value.type == "2")
                $scope.emailCompose.branchLevelGroups.push(value.id);
            else if (value.type == "3")
                $scope.emailCompose.customizedGroups.push(value.id);
        });

        angular.forEach($scope.multiSelected.familyMembers, function (value) {
            memberArray.push(value.fullName);

            $scope.emailCompose.familyMembers.push(value.id)
        });

        angular.forEach($scope.selectedFiles, function (file) {
            $scope.formdata.append('file', file);
            fileArray.push(file.name);
        });       

        $scope.selectedFileNames = fileArray.join(', ');
        $scope.selectedGroupNames = groupArray.join(', ');
        $scope.selectedMemberNames = memberArray.join(', ');
    };

    $scope.loadTemplate = function(){
       
        if ($scope.emailCompose.content != "" ) {
            $scope.showAlertForTemplate = true;
            $scope.alertClassForTemplate = "alert-danger";
            $scope.alertMsgForTemplate = "Are you sure?, you want to load the email template. Existing email content will be cleared";
        }
        else
        {
            $scope.assignContent();
        }
    }


    $scope.assignContent = function () {

        var results = $filter('filter')($scope.templates, { id: $scope.emailCompose.templateId }, true);        

        if (results.length > 0 ) {
            $scope.emailCompose.content = results[0].content;            
        }

        $scope.emailCompose.templateId = "";
    }
    
    var emailFormValidate = function () {

        var errorMsg = "";
        var errorMsg = "Please enter to email address or select family member or email group";
        
        if ($scope.multiSelected.groups === undefined)
            $scope.multiSelected.groups = [];
        if ($scope.multiSelected.familyMembers === undefined)
            $scope.multiSelected.familyMembers = [];

        if ($scope.multiSelected.groups.length > 0 || $scope.multiSelected.familyMembers.length > 0 || $scope.emailCompose.to != "")
            errorMsg = ""            
        else
            errorMsg = "Please enter to email address or select family member or email group";

        if ($scope.emailCompose.subject == "")
        {
            if( errorMsg == "")
                errorMsg = "Please enter email subject";
            else
                errorMsg = errorMsg + ", email subject also required";
        }

        if ($scope.emailCompose.content == "") {
            if (errorMsg == "")
                errorMsg = "Please enter email content";
            else
                errorMsg = errorMsg + ", email content also required";
        }

        if (errorMsg == "" && $scope.emailCompose.to != "")
        {
            errorMsg = checkEmails();
        }

        if (errorMsg == "") {
            errorMsg = checkAttachmentSize();
        }

        return errorMsg;

    };

    $scope.emailCompose = {
        from: $scope.authentication.userName,
        to: "",
        cc: "",
        subject: "",
        content: "",
        templateId: "",
        customizedGroups: [],
        organizationLevelGroups: [],
        branchLevelGroups: [],
        familyMembers: [],
        files: []
    }

    $scope.formLoad = function () {       

        groupService.getCustomAndBuildInGroupByOrgId($scope.authentication.orgId, true).then(function (results) {
            $scope.groups = results.data;            
        }, function (error) {
            showErroMessage(error)
        });

        emailTemplateService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.templates = results.data;
        }, function (error) {
            showErroMessage(error)
        });

        familyMemberService.getGroupFamilyMembersByOrgId($scope.authentication.orgId).then(function (results) {
            $scope.familyMembersLoaded = 1;
            $scope.familyMembers = results.data;          

        }, function (error) {
            showErroMessage(error)
        });
    }


    $scope.trustAsHtml = function(string) {
    return $sce.trustAsHtml(string);
    };

    var checkEmail = function (email) {
        var regExp = /(^[a-z]([a-z_\.]*)@([a-z_\.]*)([.][a-z]{3})$)|(^[a-z]([a-z_\.]*)@([a-z_\.]*)(\.[a-z]{3})(\.[a-z]{2})*$)/i;
        return regExp.test(email);
    }

    var checkAttachmentSize = function () {
        var errorMsg = "";
        var fileSize = 0

        angular.forEach($scope.selectedFiles, function (file) {
            fileSize += file.size           
        });

        if (fileSize > 7340032)
            errorMsg = "Attachment files size is too big, System supports only 7 MBs"

        return errorMsg;
    }

    var checkEmails = function () {
        var errorMsg = "";

        if ($scope.emailCompose.to != "") {           
            var emails = $scope.emailCompose.to;
            var emailArray = emails.split(",");
            var invEmails = "";
            for (var i = 0; i <= (emailArray.length - 1) ; i++) {
                if (!checkEmail(emailArray[i])) {
                    invEmails += emailArray[i] + "\n";
                }
            }
            if (invEmails != "") {
                errorMsg = "Invalid emails:\n" + invEmails;
            }
        }

        return errorMsg;
    }

    $scope.sendEmail = function () {    

        $scope.emailSending = true;

        $scope.formdata.append('orgName', $scope.authentication.orgName);
        $scope.formdata.append('from', $scope.authentication.userName);
        $scope.formdata.append('to', $scope.emailCompose.to);
        $scope.formdata.append('cc', $scope.emailCompose.cc);
        $scope.formdata.append('subject', $scope.emailCompose.subject);
        $scope.formdata.append('content', $scope.emailCompose.content);
        
        $scope.formdata.append('customizedGroups', $scope.emailCompose.customizedGroups);
        $scope.formdata.append('organizationLevelGroups', $scope.emailCompose.organizationLevelGroups);
        $scope.formdata.append('branchLevelGroups', $scope.emailCompose.branchLevelGroups);
        $scope.formdata.append('familyMembers', $scope.emailCompose.familyMembers);

        emailComposeService.sendEmail($scope.formdata).then(function (results) {
            $scope.emailSending = false;
            clearForm();

            $scope.alertMsg = "System sent the email(s) successfully";
            $scope.alertClass = "alert-success";
            $scope.showAlert = true;
        }, function (error) {
            $scope.emailSending = false;
            clearForm();

            $scope.alertMsg = "System unable to send the emails, Please try after sometime";
            $scope.alertClass = "alert-danger";
            $scope.showAlert = true;
        });

       
    }

    var clearForm = function ()
    {
        $scope.step = 1;

        $scope.formdata = new FormData();

        $scope.emailCompose = {
            from: $scope.authentication.userName,
            to: "",
            cc: "",
            subject: "",
            content: "",
            templateId: "",
            customizedGroups: [],
            organizationLevelGroups: [],
            branchLevelGroups: [],
            familyMembers: [],
            files: []
        }

        $scope.multiSelected = {};        
        $scope.selectedFiles = [];

        $('#file-upload').val('');
    }

    $scope.fileClear = function () {       
        $('#file-upload').val('');
    }   

}]);