'use strict';

app.controller('familylstController', ['$scope', 'familyService', 'ngTableParams', '$filter', 'sharedService', 'familyMemberService', 'authService', 'lookupService',
    function ($scope, familyService, ngTableParams, $filter, sharedService, familyMemberService, authService, lookupService) {

    $scope.families = [];
    $scope.familyMembers = [];
    $scope.filterText;
    $scope.relationships = [];


    $scope.familyMemberId = 0;

    $scope.showAlert = false;
    $scope.alertClass = "";
    $scope.alertMsg = "";

    $scope.showAlertForDelete = false;
    $scope.alertClassForDelete = "";
    $scope.alertMsgForDelete = "";

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
        relationshipId: ""
    };
    
    $scope.authentication = authService.authentication;

    $scope.setFamilyMemberId = function (id) {
        $scope.familyMemberId = id;        
    }   

    $scope.formLoad = function () {
        familyService.getFamiliesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.families = results.data;

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

                    var filteredData = $filter('filter')($scope.families, $scope.filterText);

                    var orderedData = params.sorting() ?
                   $filter('orderBy')(filteredData, params.orderBy()) :
                   filteredData;


                    params.total(orderedData.length);
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            })

        }, function (error) {
            //alert(error.data.message);
        });
    }

    $scope.loadFamily = function () {
        familyService.getFamiliesByOrgId($scope.authentication.orgId, null).then(function (results) {
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
            relationshipId: ""
        };

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
            var family = $filter('filter')($scope.families, { id: familyId });

            var familyMember = $filter('filter')(family[0].familyMembers, { id: familyMemberId });

            $scope.familyMemberData = familyMember[0];
            $scope.familyMemberData.dob = new Date($filter('date')($scope.familyMemberData.dob, 'MM/d/yyyy'));
        }

        lookupService.getRelationships(true).then(function (results) {
            $scope.relationships = results.data;
        }, function (error) {
            showErroMessage(error)
        });
       
    };

    $scope.loadAfterEdit = function (familyId, familyMemberId) {

        familyService.getFamiliesByOrgId($scope.authentication.orgId, null).then(function (results) {
            $scope.families = results.data;
            $scope.initialize(familyId, familyMemberId);
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
    }

    $scope.savefamilyMember = function (familyId, familyMemberForm) {

        if (familyMemberForm.$valid) {           

            $scope.familyMemberData.familyId = familyId;
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

    $scope.showFamilyMembers = function (index) {

        if (typeof $scope.dayDataCollapse === 'undefined') {
            $scope.dayDataCollapseFn();
        }

        if ($scope.tableRowExpanded == false)
        {
            $scope.tableRowExpanded = true;
            $scope.dayDataCollapse[index] = true;
        }
        else
        {
            $scope.tableRowExpanded = false;
            $scope.dayDataCollapse[index] = false;
        }
    }

}]);