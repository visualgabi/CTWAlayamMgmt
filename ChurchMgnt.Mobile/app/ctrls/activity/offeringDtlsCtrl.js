'use strict';
app.controller('offeringDtlsCtrl', ['$scope', 'lookupService', 'offeringService', 'familyMemberService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'sponsorService','$stateParams',
    function ($scope, lookupService, offeringService, familyMemberService, $filter, sharedService, authService, ngTableParams, sponsorService, $stateParams) {
        
        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";
        
        $scope.offering = {
            id: $stateParams.id,
            organizationId: "",
            familyMemberId: "",
            offeringTypeId: "",
            fundTypeId: "",
            amount: "",
            offeringDate: "",
            transacitonNumber: "",
            notes: "",
            active: true,
            rowTimeStamp: "",
            sponsorId: "",
            sourceId: 1
        }

        $scope.authentication = authService.authentication;

        $scope.disableEditAction = function (recordDate) {
            return new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy')) > new Date($filter('date')(recordDate, 'MM/d/yyyy'))
        };


        $scope.formLoad = function () {

            if ($stateParams.id !== undefined) {

                offeringService.getById($stateParams.id).then(function (results) {
                    $scope.offering = results.data;
                    $scope.offering.offeringDate = new Date($filter('date')($scope.offering.offeringDate, 'MM/d/yyyy'));
                    $scope.offering.sourceId = $scope.offering.familyMemberId != null ? 1 : 2;
                })
            }
        };

        $scope.clearAlert = function () {
            $scope.showAlert = false;
            $scope.alertClass = "";
            $scope.alertMsg = "";
        }

        $scope.enable = function (id, status) {            

            offeringService.enable($scope.offering.id, status).then(function (results) {

                $scope.offering.active = status;
                $scope.offering.rowTimeStamp = results.data;

                $scope.alertClass = "alert-success";
                if (status == true)
                    $scope.alertMsg = sharedService.getShortEnableSuccessMsg();
                else
                    $scope.alertMsg = sharedService.getShortDisableSuccessMsg();

                $scope.showAlert = true;

                $scope.tableParams.reload();

            }, function (error) {

                $scope.alertClass = "alert-danger";
                $scope.alertMsg = sharedService.getShortErrorMsg();

                $scope.showAlert = true;

                $scope.tableParams.reload();
            });

        };

    }]);