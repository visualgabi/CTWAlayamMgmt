'use strict';
app.controller('templateLstCtrl', ['$scope', 'authService', 'emailTemplateService','ngTableParams','$filter','sharedService',
function ($scope, authService, emailTemplateService, ngTableParams, $filter, sharedService) {

        $scope.templates = [];
        $scope.filterText;

        $scope.showAlert = false;
        $scope.alertClass = "";
        $scope.alertMsg = "";

        $scope.authentication = authService.authentication;

        $scope.formLoad = function () {
            emailTemplateService.getByOrgId($scope.authentication.orgId, null).then(function (results) {
                $scope.templates = results.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,           // count per page
                    sorting: {
                        name: 'asc'     // initial sorting
                    }
                }, {
                    total: $scope.templates.length, // length of data
                    getData: function ($defer, params) {

                        //  var fitleredData = $filter('filter')(data, filterText);

                    $scope.filteredData = $filter('filter')($scope.templates, $scope.filterText);

                    var orderedData = params.sorting() ?
                    $filter('orderBy')($scope.filteredData, params.orderBy()) :
                    $scope.filteredData;

                    params.total(orderedData.length);
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                    }
                })

            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.$watch("filterText", function () {
            $scope.tableParams.reload();
            $scope.tableParams.page(1); //Add this to go to the first page in the new pagging
        });

        $scope.enable = function (id, status) {

            var template = $filter('filter')($scope.templates, { id: id }, true);

            emailTemplateService.enable(template[0].id, status).then(function (results) {

                template[0].active = status;
                template[0].rowTimeStamp = results.data;

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



    }])