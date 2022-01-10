'use strict';
app.controller('offeringCtrl', ['$scope', 'lookupService', 'offeringService', 'familyMemberService', '$filter', 'sharedService', 'authService', 'ngTableParams', 'sponsorService','$stateParams',
    function ($scope, lookupService, offeringService, familyMemberService, $filter, sharedService, authService, ngTableParams, sponsorService, $stateParams) {

        $scope.sources = [{ id: 1, name: "Family" }, { id: 2, name: "Sponsor" }];

        $scope.offerings = [];        
        $scope.filterText;

        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";        

        $scope.action = "";

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

        $scope.todayDate = function () {
            return new Date();
        };

        //$scope.rptMinDate = function () {
        //    var d = new Date();
        //    d = new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy'));

        //    d.setDate(d.getDate() + 1);
        //    return d;
        //};


        $scope.rptMinDate = function () {

            var d = new Date();
            d = new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy'));

            $scope.offeringMinDate = d.setDate(d.getDate() + 1);
        };

        $scope.rptMinDate();

        $scope.rptMaxDate = function () {
            $scope.offeringMaxDate = new Date();
        };

        $scope.rptMaxDate();


        $scope.disableEditAction = function (recordDate) {
            return new Date($filter('date')($scope.authentication.lastTaxFiledFiscalEndDate, 'MM/d/yyyy')) > new Date($filter('date')(recordDate, 'MM/d/yyyy'))
        };


        $scope.clear = function () {
            $scope.offering = {
                id: 0,
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
        }

        $scope.authentication = authService.authentication;
        $scope.amountTitle = $scope.authentication.currency;


        $scope.setAction = function (value) {
            $scope.action = value;

            if ($scope.action == 'add') {
                $scope.clear();
            }
        }  

        $scope.$watch("filterText", function () {
            $scope.tableParams.reload();
            $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
        });

        $scope.initialize = function () {

            $scope.clear();


            if ($stateParams.id !== undefined) {
                offeringService.getById($stateParams.id).then(function (results) {
                    $scope.offering = results.data;
                    $scope.offering.offeringDate = new Date($filter('date')($scope.offering.offeringDate, 'MM/d/yyyy'));
                    $scope.offering.sourceId = $scope.offering.familyMemberId != null ? 1 : 2;
                })
            }
          

            familyMemberService.getTaxPayerByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.familyMembers = results.data;

            }, function (error) {
                showErroMessage(error)
            });

            sponsorService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.sponsors = results.data;

            }, function (error) {
                showErroMessage(error)
            });


            lookupService.getPaymentTypes(true).then(function (results) {
                $scope.paymentTypes = results.data;

            }, function (error) {
                showErroMessage(error)
            });

            lookupService.getOfferingTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.offeringTypes = results.data;
            }, function (error) {
                showErroMessage(error)
            });

            lookupService.getFundTypesByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.fundTypes = results.data;

            }, function (error) {
                showErroMessage(error)
            });

        };

        $scope.availableFundTypes = function (id) {
            return function (item) {

                if (id == 0) {
                    return item.active == true;
                }
                else {
                    return true;
                }

            };
        };

        $scope.availableOfferingTypes = function (id) {
            return function (item) {

                if (id == 0) {
                    return item.active == true;
                }
                else {
                    return true;
                }

            };
        };

        $scope.availableFamilyMembers = function (id) {
            return function (item) {

                if (id == 0) {
                    return item.active == true;
                }
                else {
                    return true;
                }

            };
        };

        $scope.availableSponsors = function (id) {
            return function (item) {

                if (id == 0) {
                    return item.active == true;
                }
                else {
                    return true;
                }

            };
        };

        $scope.clearAlert = function () {
            $scope.showAlert = false;
            $scope.alertClass = "";
            $scope.alertMsg = "";

            $scope.showAlertForDelete = false;
            $scope.alertClassForDelete = "";
            $scope.alertMsgForDelete = "";
        }

        
        $scope.loadAfterEdit = function (id) {

            var orgId = $scope.authentication.orgId;

            offeringService.getByOrgId(orgId, null).then(function (results) {
                $scope.offerings = results.data;
                $scope.initialize(id);
            });
        }

        $scope.save = function (offeringForm) {

            if (offeringForm.$valid) {

                $scope.offering.organizationId = $scope.authentication.orgId;

                offeringService.save($scope.offering).then(function (results) {

                    if ($scope.offering.id == 0) {
                        offeringForm.$setPristine();
                        $scope.clear();
                    }
                    else
                        $scope.loadAfterEdit($scope.offering.id);

                    $scope.alertClass = "alert-success";
                    $scope.alertMsg = sharedService.getShortSaveSuccessMsg();
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

                $scope.showAlert = true;
            }
        };



        $scope.today = function () {
            $scope.offering.offeringDate = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.offering.offeringDate = null;
        };

        $scope.inlineOptions = {
            customClass: getDayClass,
            minDate: $scope.offeringMinDate,
            showWeeks: true
        };

        $scope.dateOptions = {
            dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: $scope.offeringMaxDate,
            minDate: $scope.offeringMinDate,
            startingDay: 1
        };

        // Disable weekend selection
        function disabled(data) {
            var date = data.date,
              mode = data.mode;
            return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
        }

        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.offeringMinDate;
            $scope.dateOptions.minDate = $scope.offeringMinDate;
        };

        $scope.toggleMin();

        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };

        $scope.setDate = function (year, month, day) {
            $scope.dt = new Date(year, month, day);
        };

        $scope.format = 'MM/dd/yyyy';
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.popup1 = {
            opened: false
        };



        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);
        $scope.events = [
          {
              date: tomorrow,
              status: 'full'
          },
          {
              date: afterTomorrow,
              status: 'partially'
          }
        ];

        function getDayClass(data) {
            var date = data.date,
              mode = data.mode;
            if (mode === 'day') {
                var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                for (var i = 0; i < $scope.events.length; i++) {
                    var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                    if (dayToCheck === currentDay) {
                        return $scope.events[i].status;
                    }
                }
            }

            return '';
        }


        $scope.today = function () {
            $scope.offering.offeringDate = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.offering.offeringDate = null;
        };

        $scope.inlineOptions = {
            customClass: getDayClass,
            minDate: $scope.offeringMinDate,
            showWeeks: true
        };

        $scope.dateOptions = {
            dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: $scope.offeringMaxDate,
            minDate: $scope.offeringMinDate,
            startingDay: 1
        };

        // Disable weekend selection
        function disabled(data) {
            var date = data.date,
              mode = data.mode;
            return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
        }

        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.offeringMinDate;
            $scope.dateOptions.minDate = $scope.offeringMinDate;
        };

        $scope.toggleMin();

        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };

        $scope.setDate = function (year, month, day) {
            $scope.dt = new Date(year, month, day);
        };

        $scope.format = 'MM/dd/yyyy';
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.popup1 = {
            opened: false
        };



        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);
        $scope.events = [
          {
              date: tomorrow,
              status: 'full'
          },
          {
              date: afterTomorrow,
              status: 'partially'
          }
        ];

        function getDayClass(data) {
            var date = data.date,
              mode = data.mode;
            if (mode === 'day') {
                var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                for (var i = 0; i < $scope.events.length; i++) {
                    var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                    if (dayToCheck === currentDay) {
                        return $scope.events[i].status;
                    }
                }
            }

            return '';
        }



    }]);