'use strict';
app.controller('reportedIssueCtrl', ['$scope', 'reportedIssueService', 'sharedService', 'authService', function ($scope, reportedIssueService, sharedService, authService) {

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";
    $scope.authentication = authService.authentication;

    $scope.reportedIssue = {
        id: 0,
        organizationId: "",
        IssueStatusId: 1,
        title: "",
        description: "",        
        active: true,
        rowTimeStamp: ""
    }

    $scope.Save = function () {
        alert('hello')
    } 

    $scope.clearAlert = function () {
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

    }

    $scope.clear = function () {
        $scope.reportedIssue = {
            id: 0,
            organizationId: "",
            IssueStatusId: 1,
            title: "",
            description: "",
            active: true,
            rowTimeStamp: ""
        }
    }

    $scope.save = function (reportedIssueForm) {

        if (reportedIssueForm.$valid) {
            $scope.reportedIssue.organizationId = $scope.authentication.orgId;
            reportedIssueService.save($scope.reportedIssue).then(function (results) {               

                if ($scope.reportedIssue.id == 0) {
                    reportedIssueForm.$setPristine();
                    $scope.clear();
                }


                $scope.alertClass = "alert-success";
                $scope.alertMsg = "You request submitted successfully, ChurchMgnt support team will look into this, Thanks for reporting this issue";
                $scope.showAlert = true;

            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;

                if (error.data === 3) {
                    $scope.loadAfterEdit($scope.offering.id);
                    $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                }
                else if (error.data === 2) {
                    $scope.loadAfterEdit($scope.offering.id);
                    $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                }
                else
                    $scope.alertMsg = sharedService.getShortErrorMsg();
            });
        }          
        
    };

}]);