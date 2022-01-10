'use strict';
app.controller('homeCtrl', ['$scope', '$location', '$state', 'contactService', function ($scope, $location, $state, contactService) {

    $scope.contactData = {
        name: "",
        phone: "",
        emailId: "",
        message: ""
    };

    $scope.notity = function (contactForm) {

        if (contactForm.$valid) {
            contactService.sendMessage($scope.contactData).then(function (results) {
              
                toastr.success('We received your request, our support team will contact you soon')
                //alert("We received your request, our support team will contact you soon")

            }, function (error) {
                toastr.error('Something went wrong, Please call us in our office number or try again')                
            });
        };
    }

}]);