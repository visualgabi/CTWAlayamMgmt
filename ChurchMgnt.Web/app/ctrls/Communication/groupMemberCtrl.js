'use strict';
app.controller('groupMemberCtrl', ['$scope', '$filter', 'sharedService', 'authService', 'ngTableParams', 'lookupService', 'groupService', 'organizationService', 'groupMemberService','familyMemberService','$window',
function ($scope, $filter, sharedService, authService, ngTableParams, lookupService, groupService, organizationService, groupMemberService, familyMemberService, $window) {
    
    $scope.groups = [];
    $scope.members = [];
   // $scope.alreadyAvailableMembers = [];

    $scope.selectedMembers = [];
    $scope.selectedGroup = "";    

    $scope.groupMembers = [];   

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.formLoad = function () {

        groupService.getByOrgId($scope.authentication.orgId, true).then(function (results) {
            $scope.groups = results.data;                       
        }, function (error) {
            showErroMessage(error)
        });

        familyMemberService.getGroupFamilyMembersByOrgId($scope.authentication.orgId).then(function (results) {
            $scope.members = results.data;
        }, function (error) {
            showErroMessage(error)
        });

    }

    $scope.clearAlert = function () {
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";        
    }

    $scope.reset = function () {
        $window.location.reload();
    }

    var settingupGroupMembers = function () {

        for (var iPos = 0; iPos < $scope.groupMembers.length; iPos++) {            
            $scope.groupMembers[iPos].active = false;
        }

        for (var iPos = 0; iPos < $scope.selectedMembers.length; iPos++)
        {
            var existingList = $filter('filter')($scope.groupMembers, { familyMemberId: $scope.selectedMembers[iPos] }, true);

            if (existingList.length > 0)
                existingList[0].active = true;
            else
                $scope.groupMembers.push({ "groupId": $scope.selectedGroup, "familyMemberId": $scope.selectedMembers[iPos], "active": true })
          
        }        
    }


    $scope.loadAfterSave = function () {

        groupMemberService.getByGroupId($scope.selectedGroup, null).then(function (results) {
            $scope.groupMembers = results.data;
            $scope.selectedMembers = [];

            for (var iPos = 0; iPos < $scope.groupMembers.length; iPos++) {
                if ($scope.groupMembers[iPos].active == true)
                    $scope.selectedMembers.push($scope.groupMembers[iPos].familyMemberId);
            }

            var demo2 = $('.demo2').bootstrapDualListbox();
            demo2.bootstrapDualListbox('refresh', true);

        }, function (error) {
            showErroMessage(error)
        });
    }

    $scope.onChange = function () {

        groupMemberService.getByGroupId($scope.selectedGroup, null).then(function (results) {
            $scope.groupMembers = results.data;
            $scope.selectedMembers = [];
           
            for (var iPos = 0; iPos < $scope.groupMembers.length; iPos++) {
                if ($scope.groupMembers[iPos].active == true)
                    $scope.selectedMembers.push($scope.groupMembers[iPos].familyMemberId);
            }

            var demo2 = $('.demo2').bootstrapDualListbox();
            demo2.bootstrapDualListbox('refresh', true);

        }, function (error) {
            showErroMessage(error)
        });
    }


    $scope.SaveGroupMember = function (groupMemberForm) {

        if (groupMemberForm.$valid) {
            //var unique = $scope.isUnique($scope.group);

           // if (unique == true) {
                settingupGroupMembers();
                groupMemberService.save($scope.groupMembers).then(function (results) {

                    //if ($scope.group.id == 0) {
                    //    groupMemberForm.$setPristine();
                    //    $scope.clear();
                    //}
                    //else
                    $scope.loadAfterSave();

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                    $scope.showAlert = true;

                }, function (error) {
                    $scope.alertClass = "alert-danger";
                    $scope.showAlert = true;

                    if (error.data === 3) {
                        $scope.loadAfterEdit($scope.group.id);
                        $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                    }
                    else if (error.data === 2) {
                        $scope.loadAfterEdit($scope.group.id);
                        $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                    }
                    else
                        $scope.alertMsg = sharedService.getShortErrorMsg();
                });

                $scope.showAlert = true;
            }
            else {
                $scope.alertMsg = "No family member got selected";
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;
            }
        //}
    };

}]);