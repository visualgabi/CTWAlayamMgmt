'use strict';
app.controller('familyMemberController', ['$scope', 'lookupService', 'familyMemberService', '$routeParams', 'sharedService', 'authService', 
    function ($scope, lookupService, familyMemberService, $routeParams, sharedService, authService) {
      
    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.authentication = authService.authentication;

    $scope.familyMemberData = {
        id: 0,
        firstName: "",
        lastName: "",
        middleName: "",
        initial: "",
        gender: "",
        DOB: "",        
        familyId : "",        
        emailId1: "",                
        phone1: "",                
        active: true,
        rowTimeStamp: "",
        isTaxPayer : false
    };

    $scope.membershipStatuses = [];   

    $scope.formLoad = function () {
        $scope.familyMemberData = {
            id: 0,
            firstName: "",
            lastName: "",
            middleName: "",
            initial: "",
            gender: "",
            DOB: "",            
            familyId : "",
            active: true,
            rowTimeStamp: "",
            isTaxPayer: false
        };

        if ($routeParams.id !== undefined) {
            familyMemberService.getfamilyMemberById($routeParams.id).then(function (results) {
                $scope.familyMemberData = results.data;              
            }, function (error) {
                showErroMessage(error)
            });
        }

        lookupService.getMembershipStatuses().then(function (results) {
            $scope.membershipStatuses = results.data;

        }, function (error) {
            showErroMessage(error)
        });      
    };

    $scope.formReset = function () {
        $scope.initialize();
        $scope.clearMsg();
    };

    $scope.initialize = function () {
        $scope.formLoad();
        //  $scope.familyMemberForm.$setUntouched();      
    };

    $scope.clearMsg = function () {
        $scope.showAlert = false;
        $scope.alertClass;
        $scope.alertMsg;
    };

    function showErroMessage() {
        $scope.alertClass = "alert-danger";
        $scope.alertMsg = sharedService.getErrorMsg(error.data, $scope.familyMemberData.name);
        $scope.showAlert = true;
    }

    $scope.savefamilyMember = function (familyMemberForm) {

        if (familyMemberForm.$valid) {                       

            familyMemberService.savefamilyMember($scope.familyMemberData).then(function (results) {

                $scope.initialize();

                $scope.alertClass = "alert-success";
                $scope.alertMsg = sharedService.getSaveSuccessMsg("familyMember");

            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.alertMsg = sharedService.getErrorMsg(error.data, $scope.familyMemberData.name);

                if (error.data === 3)
                    $scope.initialize();
            });

            $scope.showAlert = true;
        }
    };

}]);

