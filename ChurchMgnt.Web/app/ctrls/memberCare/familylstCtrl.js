'use strict';

app.controller('familylstCtrl', ['$scope', 'familyService', 'ngTableParams', '$filter', 'sharedService', 'familyMemberService', 'authService', 'lookupService', '$document', 'groupMemberService', 'groupService',
function ($scope, familyService, ngTableParams, $filter, sharedService, familyMemberService, authService, lookupService, $document, groupMemberService, groupService) {


    $scope.familyMemberEditing = false;

    $scope.families = [];
    $scope.familyMembers = [];
    $scope.filterText;
    $scope.relationships = [];

    $scope.groups = [];
    $scope.groupMembers = [];
    $scope.selectedGroups = [];

    $scope.familyMemberId = 0;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

    $scope.showAlertForMoveFamily = false;
    $scope.alertClassForMoveFamily = "";
    $scope.alertMsgForMoveFamily = "";

    $scope.loadingFamilyList = true;

    $scope.checkBoxEmailGroups = [];

    var lastEventObj = null;

    $scope.todayDate = function () {
        return new Date();
    };

    $scope.rptMinDate = function () {
        var d = new Date();

        d.setFullYear(d.getFullYear() - 200);

        return d;
    };

    $scope.showEmailGroup = function () {
        var result = true;

        if ($scope.familyMemberData.dob !== undefined && $scope.familyMemberData.dob != "") {
            // var birthdate = new Date("1987/08/01");
            var birthdate = new Date($filter('date')($scope.familyMemberData.dob, 'MM/d/yyyy'));;
            var cur = new Date();
            var diff = cur - birthdate;
            if (Math.floor(diff / 31536000000) < 13)
                result = false;
        }

        return result;

    };

    $scope.familyMemberData = {
        id: 0,
        firstName: "",
        lastName: "",
        middleName: "",
        initial: "",
        gender: "",
        dob: "",
        familyId: "",
        active: true,
        rowTimeStamp: "",
        phone1: "",
        emailId1: "",
        notes: "",
        relationshipId: "",
        groupMembers : []
    };

    $scope.moveMemberData = {
        familyId: "",
        familyMemberId: "",
        familyMemberFirstName:"",
        relationshipId: "",       
        active: true,
        rowTimeStamp: ""        
    };

    $scope.groupMember = {        
        familyMemberId: "",
        groupId: "",
        active: true,
        rowTimeStamp: ""
    };
    
    $scope.authentication = authService.authentication;

    $scope.setFamilyMemberId = function (id) {
        $scope.familyMemberId = id;        
    }   

    $scope.formLoad = function () {
        familyService.getFamiliesIncludeMembersAndGroupByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.families = results.data;
            $scope.loadingFamilyList = false;
            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,           // count per page
                sorting: {
                    name: 'asc'     // initial sorting
                }
            }, {
                total: $scope.families.length, // length of data
                getData: function ($defer, params) {

                    //  var fitleredData = $filter('filter')(data, filterText);

                    $scope.filteredData = $filter('filter')($scope.families, $scope.filterText);

                    var orderedData = params.sorting() ?
                   $filter('orderBy')($scope.filteredData, params.orderBy()) :
                   $scope.filteredData;


                    params.total(orderedData.length);
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            })

        }, function (error) {
            //alert(error.data.message);
        });

        lookupService.getMembershipStatuses().then(function (results) {
            $scope.membershipStatuses = results.data;

        }, function (error) {
            showErroMessage(error)
        });

        groupService.getByOrgId($scope.authentication.orgId, true).then(function (results) {
            $scope.groups = results.data;
        }, function (error) {
            showErroMessage(error)
        });
    }

    $scope.loadFamily = function () {
        familyService.getFamiliesIncludeMembersAndGroupByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.families = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.$watch("filterText", function () {
        $scope.tableParams.reload();
        $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
    });

   
    $scope.enableFamily = function (id, status) {

        var family = $filter('filter')($scope.families, { id: id });

        familyService.changeStatus(family[0].id, status).then(function (results) {

            family[0].active = status;
            family[0].rowTimeStamp = results.data;

            $scope.alertClassForDelete = "alert-success";
            if (status == true)
                $scope.alertMsgForDelete = sharedService.getEnableSuccessMsg("family");
            else
                $scope.alertMsgForDelete = sharedService.getDisableSuccessMsg("family");

            $scope.showAlertForDelete = true;

            $scope.tableParams.reload();

        }, function (error) {

            $scope.alertClassForDelete = "alert-danger";
            $scope.alertMsgForDelete = sharedService.getErrorMsg(error.family, org.name);

            $scope.showAlertForDelete = true;

            $scope.tableParams.reload();
        });

    }; 

   

    $scope.clear = function () {
        $scope.familyMemberData = {
            id: 0,
            firstName: "",
            lastName: "",
            middleName: "",
            initial: "",
            gender: "",
            dob: "",
            familyId: "",
            active: true,
            rowTimeStamp: "",
            phone1: "",
            emailId1: "",
            notes: "",
            relationshipId: "",
            groupMembers: []
        };

        $scope.selectedGroups = [];
        $scope.checkBoxEmailGroups = [];

        for (var iPos = 0; iPos < $scope.groups.length; iPos++) {
            var checked = false;
            $scope.checkBoxEmailGroups.push({ "groupId": $scope.groups[iPos].id, "familyMemberId": $scope.familyMemberData.id, "name": $scope.groups[iPos].name, "checked": checked })
        }

    }

    $scope.setAction = function (value) {
        $scope.action = value;

        if ($scope.action == 'add') {
            $scope.clear();
        }
    }


    $scope.initialize = function (familyId, familyMemberId) {
        
        //$scope.clear();

        if (familyMemberId != 0)
        {
            $scope.checkBoxEmailGroups = [];

            var family = $filter('filter')($scope.families, { id: familyId }, true);

            var familyMember = $filter('filter')(family[0].familyMembers, { id: familyMemberId }, true);

            $scope.familyMemberData = familyMember[0];
            $scope.familyMemberData.dob = new Date($filter('date')($scope.familyMemberData.dob, 'MM/d/yyyy'));

            var selectedGroupMembers = $filter('filter')($scope.familyMemberData.groupMembers, { active: true }, true);

            $scope.selectedGroups = [];
            
            for(var iPos=0; iPos < $scope.groups.length; iPos++ )
            {
                var familyMemberGroup = $filter('filter')(selectedGroupMembers, { groupId: $scope.groups[iPos].id }, true);
                var checked = familyMemberGroup.length > 0;

                $scope.checkBoxEmailGroups.push({ "groupId": $scope.groups[iPos].id, "familyMemberId": $scope.familyMemberData.id, "name": $scope.groups[iPos].name, "checked": checked })
            }

        }
        else
        {
            $scope.clear();
        }

        lookupService.getRelationships(true).then(function (results) {
            $scope.relationships = results.data;
        }, function (error) {
            showErroMessage(error)
        });
       
    };

    $scope.beginFamilyMemberEdit = function () {
        $scope.familyMemberEditing = true;
    }

    $scope.endFamilyMemberEdit = function () {
        $scope.familyMemberEditing = false;
    }

    $scope.groupCheckStaus = function (selectedGroupId) {
        for (var iPos = 0; iPos < $scope.selectedGroups.length; iPos++) {
            if ($scope.selectedGroups[iPos] == selectedGroupId)
                return true;
        }
        return false;
    }

    $scope.updateGroupCheckBoxModel = function (checked, selectedGroupId) {

        if (checked == true)
            $scope.selectedGroups.push(selectedGroupId);
        else {
            for (var i = 0 ; i < $scope.selectedGroups.length; i++) {
                if ($scope.selectedGroups[i] == selectedGroupId) {
                    $scope.selectedGroups.splice(i, 1);
                }
            }
        }
    }   
    

    $scope.loadAfterEdit = function (familyId, familyMemberId) {

        familyService.getFamiliesIncludeMembersAndGroupByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.families = results.data;
            $scope.initialize(familyId, familyMemberId);
        });
    }

    $scope.loadAfterMoveMember = function () {
        familyService.getFamiliesIncludeMembersAndGroupByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.families = results.data;
            $scope.tableParams.reload();
        });
    }

    $scope.clearAlert = function ()
    {
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.showAlertForDelete = false;
        $scope.alertClassForDelete = "";
        $scope.alertMsgForDelete = "";

        $scope.showAlertForMoveFamily = false;
        $scope.alertClassForMoveFamily = "";
        $scope.alertMsgForMoveFamily = "";
    }

    $scope.initializeMoveFamilyMember = function (familyMemberId, firstName) {
        $scope.moveMemberData.familyMemberId = familyMemberId;
        $scope.moveMemberData.familyMemberFirstName = firstName;
        

        lookupService.getRelationships(true).then(function (results) {
            $scope.relationships = results.data;
        }, function (error) {
            showErroMessage(error)
        });
    }

    $scope.movefamilyMember = function (moveMemberForm) {

        if (moveMemberForm.$valid) {
            
            familyMemberService.movefamilyMember($scope.moveMemberData).then(function (results) {
                moveMemberForm.$setPristine();
                    $scope.clear();

                    $scope.alertClassForDelete = "alert-success";
                    $scope.alertMsgForDelete = "Selected family member moved to target family successfully";
                    $scope.showAlertForDelete = true;
                    $scope.loadAfterMoveMember();


            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;

                if (error.data === 3) {
                    $scope.loadAfterEdit(familyId, $scope.familyMemberData.id);
                    $scope.alertMsgForMoveFamily = sharedService.getShortConcurrencyErrorMsg()
                }
                else if (error.data === 2) {
                    $scope.loadAfterEdit(familyId, $scope.familyMemberData.id);
                    $scope.alertMsgForMoveFamily = sharedService.getShortUniqueErrorMsg()
                }
                else
                    $scope.alertMsgForMoveFamily = sharedService.getShortErrorMsg();
            });
        }
    }

    var settingGroupMembers = function()
    {
        
        for (var iPos = 0; iPos < $scope.familyMemberData.groupMembers.length; iPos++) {
            $scope.familyMemberData.groupMembers[iPos].active = false;            
        }

        $scope.selectedGroups = $filter('filter')($scope.checkBoxEmailGroups, { checked: true }, true);

        for (var iPos = 0; iPos < $scope.selectedGroups.length; iPos++) {

            if ($scope.familyMemberData.id == 0) {
                $scope.familyMemberData.groupMembers.push({ "groupId": $scope.selectedGroups[iPos].groupId, "familyMemberId": 0, "active": true })
            }
            else {
                var selectedGroupMembers = $filter('filter')($scope.familyMemberData.groupMembers, { groupId: $scope.selectedGroups[iPos].groupId }, true);
                if (selectedGroupMembers.length > 0)
                    selectedGroupMembers[0].active = true;
                else
                    $scope.familyMemberData.groupMembers.push({ "groupId": $scope.selectedGroups[iPos].groupId, "familyMemberId": $scope.familyMemberData.id, "active": true })
            }

        }

        /*
        for (var iPos = 0; iPos < $scope.selectedGroups.length; iPos++) {

            if ($scope.familyMemberData.id == 0)
            {
                $scope.familyMemberData.groupMembers.push({ "groupId": $scope.selectedGroups[iPos].groupId, "familyMemberId": 0, "active": true })
            }
            else
            {
                var selectedGroupMembers = $filter('filter')($scope.familyMemberData.groupMembers, { groupId: $scope.selectedGroups[iPos] }, true);
                if (selectedGroupMembers.length > 0)
                    selectedGroupMembers[0].active = true;
                else
                    $scope.familyMemberData.groupMembers.push({ "groupId": $scope.selectedGroups[iPos], "familyMemberId": $scope.familyMemberData.id, "active": true })
            }
            
        }
        */
           
    }
    

    $scope.savefamilyMember = function (familyId, familyMemberForm) {

        if (familyMemberForm.$valid) {           

            $scope.familyMemberData.familyId = familyId;
            settingGroupMembers();
            familyMemberService.savefamilyMember($scope.familyMemberData).then(function (results) {

                if ($scope.familyMemberData.id == 0) {
                    familyMemberForm.$setPristine();
                    $scope.clear();
                }
                else
                    $scope.loadAfterEdit(familyId, $scope.familyMemberData.id);

                $scope.alertClass = "alert-success";
                $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
                $scope.showAlert = true;
                

            }, function (error) {
                $scope.alertClass = "alert-danger";
                $scope.showAlert = true;

                if (error.data === 3) {
                    $scope.loadAfterEdit(familyId, $scope.familyMemberData.id);
                    $scope.alertMsg = sharedService.getShortConcurrencyErrorMsg()
                }
                else if (error.data === 2) {
                    $scope.loadAfterEdit(familyId, $scope.familyMemberData.id);
                    $scope.alertMsg = sharedService.getShortUniqueErrorMsg()
                }
                else
                    $scope.alertMsg = sharedService.getShortErrorMsg();
            });           
        }
    }

    $scope.enableFamilyMember = function (familyId, familyMemberId, status) {

        var family = $filter('filter')($scope.families, { id: familyId });

        var familyMember = $filter('filter')(family[0].familyMembers, { id: familyMemberId });
        $scope.familyMemberId = 0;

        familyMemberService.enablefamilyMember(familyMember[0].id, status).then(function (results) {

            familyMember[0].active = status;
            familyMember[0].rowTimeStamp = results.data;

            $scope.alertClassForDelete = "alert-success";
            if (status == true)
                $scope.alertMsgForDelete = sharedService.getShortEnableSuccessMsg();
            else
                $scope.alertMsgForDelete = sharedService.getShortDisableSuccessMsg();

            $scope.showAlertForDelete = true;

            $scope.tableParams.reload();

        }, function (error) {

            $scope.alertClassForDelete = "alert-danger";
            $scope.alertMsgForDelete = sharedService.getShortErrorMsg();

            $scope.showAlertForDelete = true;

            $scope.tableParams.reload();
        });

    };


    $scope.tableRowExpanded = false;
    $scope.tableRowIndexExpandedCurr = "";
    $scope.tableRowIndexExpandedPrev = "";
    $scope.familyIdExpanded = "";

    $scope.dayDataCollapseFn = function () {
        $scope.dayDataCollapse = [];
        for (var i = 0; i < $scope.families.length; i += 1) {
            $scope.dayDataCollapse.push(false);
        }
    };

    $scope.showFamilyMembers = function (index, obj) {       
        
        $scope.dayDataCollapseFn();

        if (lastEventObj == null)
            lastEventObj = obj.target;
        else if (lastEventObj === obj.target)
        {

        }
        else
        {
            lastEventObj.setAttribute('class', 'btn btn-info btn-sm fa fa-angle-down')
            lastEventObj = obj.target;
        }        


        if (obj.target.getAttribute('class') == "btn btn-info btn-sm fa fa-angle-down") {
            $scope.dayDataCollapse[index] = true;
            obj.target.setAttribute('class', 'btn btn-info btn-sm fa fa-angle-up')            
        }
        else
        {
            $scope.dayDataCollapse[index] = false;
            obj.target.setAttribute('class', 'btn btn-info btn-sm fa fa-angle-down')
        }

    }

}]);