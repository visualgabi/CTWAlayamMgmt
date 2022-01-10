//var app = angular.module('alayamMgmtApp', ['common.core', 'common.ui','ui.bootstrap', 'ngRoute', 'LocalStorageModule', 'ngTouch', 'angular-loading-bar', 'ngTable', 'ngAnimate', 'ngSanitize', 'mgcrea.ngStrap', 'ngFileUpload', "chart.js", ]);
//var app = angular.module('alayamMgmtApp', ['common.core', 'common.ui', 'LocalStorageModule', 'ngTouch', 'ngTable', 'ngAnimate', 'ngSanitize', 'mgcrea.ngStrap', 'ngFileUpload', 'chart.js','angular.filter']);
var app = angular.module('alayamMgmtApp', ['common.core', 'common.ui', 'LocalStorageModule', 'ngTouch', 'ngTable', 'ngAnimate', 'ngSanitize', 'mgcrea.ngStrap', 'chart.js', 'angular.filter', 'localytics.directives']);

app.config(function ($routeProvider) {
   
   $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/UI/views/login.html"
    });

   $routeProvider.when("/home", {
       controller: "homeController",
       templateUrl: "/UI/views/home.html"
   });

   $routeProvider.when("/shared/orglst", {
        controller: "orglstController",
        templateUrl: "/UI/views/shared/orglst.html"
    });

   $routeProvider.when("/shared/org", {
        controller: "orgController",
        templateUrl: "/UI/views/shared/org.html"
    });

   $routeProvider.when("/shared/org/:id", {
        controller: "orgController",
        templateUrl: "/UI/views/shared/org.html"
    });

   $routeProvider.when("/shared/orgDtls/:id", {
        controller: "orgDtlsController",
        templateUrl: "/UI/views/shared/orgDtls.html"
    });       

    /*$routeProvider.when("/orgProfileDtls", {
        controller: "orgProfileDtlsController",
        templateUrl: "/UI/views/shared/orgProfileDtls.html"
    });

    $routeProvider.when("/orgProfile", {
        controller: "orgProfileController",
        templateUrl: "/UI/views/shared/orgProfile.html"
    });
    */
    
    $routeProvider.when("/shared/offering", {
        controller: "offeringController",
        templateUrl: "/UI/views/shared/offering.html"
    });

    $routeProvider.when("/shared/expense", {
        controller: "expenseController",
        templateUrl: "/UI/views/shared/expense.html"
    });

    $routeProvider.when("/shared/deposit", {
        controller: "depositController",
        templateUrl: "/UI/views/shared/deposit.html"
    });

    $routeProvider.when("/shared/event", {
        controller: "eventController",
        templateUrl: "/UI/views/shared/event.html"
    });      

    $routeProvider.when("/memberCare/familylst", {
        controller: "familylstController",
        templateUrl: "/UI/views/memberCare/familylst.html"
    });

    $routeProvider.when("/memberCare/familyDtls/:id", {
        controller: "familyDtlsController",
        templateUrl: "/UI/views/memberCare/familyDtls.html"
    });

    $routeProvider.when("/memberCare/family/:id", {
        controller: "familyController",
        templateUrl: "/UI/views/memberCare/family.html"
    });

    $routeProvider.when("/memberCare/family", {
        controller: "familyController",
        templateUrl: "/UI/views/memberCare/family.html"
    });
   
    $routeProvider.when("/memberCare/sponsor", {
        controller: "sponsorController",
        templateUrl: "/UI/views/memberCare/sponsor.html"
    });
    

    /*
    $routeProvider.when("/expenseRptPrt/:organizationId/:expenseTypeId/:expenseStartDate/:expenseEndDate/:orderById/:expenseTypeName", {
        controller: "expenseRptPrtController",
        templateUrl: "/UI/views/report/expenseRptPrt.html"
    });

    $routeProvider.when("/offeringRptPrt/:organizationId/:startDate/:endDate/:orderById/:sourceId/:sourceName", {
        controller: "offeringRptPrtController",
        templateUrl: "/UI/views/report/offeringRptPrt.html"
    });

    $routeProvider.when("/depositRptPrt/:organizationId/:userId/:accountId/:depositStartDate/:depositEndDate/:orderById/:accountName", {
        controller: "depositRptPrtController",
        templateUrl: "/UI/views/report/depositRptPrt.html"
    });

    $routeProvider.when("/eventRptPrt/:organizationId/:eventTypeId/:splEventTypeId/:eventStartDate/:eventEndDate/:orderById/:eventTypeName/:subEventTypeName", {
        controller: "eventRptPrtController",
        templateUrl: "/UI/views/report/eventRptPrt.html"
    });

     $routeProvider.when("/offeringRptPrtView", {
        controller: "offeringController",
        templateUrl: "/UI/views/report/offeringRptPrtView.html"
    });

    */



    $routeProvider.when("/report/offeringRpt", {
        controller: "offeringRptController",
        templateUrl: "/UI/views/report/offeringRpt.html"
    });

    $routeProvider.when("/report/expenseRpt", {
        controller: "expenseRptController",
        templateUrl: "/UI/views/report/expenseRpt.html"
    });

    $routeProvider.when("/report/depositRpt", {
        controller: "depositRptController",
        templateUrl: "/UI/views/report/depositRpt.html"
    });

    $routeProvider.when("/report/eventRpt", {
        controller: "eventRptController",
        templateUrl: "/UI/views/report/eventRpt.html"
    });  
 
    $routeProvider.when("/report/familyOfferingRpt", {
        controller: "familyOfferingRptController",
        templateUrl: "/UI/views/report/familyOfferingRpt.html"
    });

    $routeProvider.when("/report/transactionRpt", {
        controller: "transactionRptController",
        templateUrl: "/UI/views/report/transactionRpt.html"
    });  

  

    $routeProvider.when("/masterData/fundType", {
        controller: "fundTypeController",
        templateUrl: "/UI/views/masterData/fundType.html"
    });


    $routeProvider.when("/masterData/offeringType", {
        controller: "offeringTypeController",
        templateUrl: "/UI/views/masterData/offeringType.html"
    });

    $routeProvider.when("/masterData/expenseType", {
        controller: "expenseTypeController",
        templateUrl: "/UI/views/masterData/expenseType.html"
    });

    $routeProvider.when("/masterData/subExpenseType", {
        controller: "subExpenseTypeController",
        templateUrl: "/UI/views/masterData/subExpenseType.html"
    });  

    $routeProvider.when("/masterData/eventType", {
        controller: "eventTypeController",
        templateUrl: "/UI/views/masterData/eventType.html"
    });

  
    $routeProvider.when("/yearBeginActivity/orgFiscalYear", {
        controller: "orgFiscalYearController",
        templateUrl: "/UI/views/yearBeginActivity/fiscalYear.html"
    });

    $routeProvider.when("/yearBeginActivity/openingBalance", {
        controller: "openingBalanceController",
        templateUrl: "/UI/views/yearBeginActivity/openingBalance.html"
    });

    $routeProvider.when("/yearBeginActivity/fiscalYearBudget", {
        controller: "fiscalYearBudgetController",
        templateUrl: "/UI/views/yearBeginActivity/fiscalYearBudget.html"
    });

    $routeProvider.when("/yearBeginActivity/familyPledge", {
        controller: "familyPledgeController",
        templateUrl: "/UI/views/yearBeginActivity/familyPledge.html"
    });


    $routeProvider.when("/yearEndActivity/contributionRpt", {
        controller: "contributionRptController",
        templateUrl: "/UI/views/yearEndActivity/contributionRpt.html"
    });

    $routeProvider.when("/yearEndActivity/contributionRptPrt/:fiscalYearId/:id/:startDate/:endDate/:orgId/:familyMembers/:reportType", {
        controller: "contributionRptPrtController",
        templateUrl: "/UI/views/yearEndActivity/contributionRptPrt.html"
    });

    $routeProvider.when("/yearEndActivity/balanceSheet", {
        controller: "balanceSheetController",
        templateUrl: "/UI/views/yearEndActivity/balanceSheet.html"
    }); 


    $routeProvider.when("/admin/branch", {
        controller: "branchController",
        templateUrl: "/UI/views/admin/branch.html"
    });

    $routeProvider.when("/admin/branchlst", {
        controller: "branchlstController",
        templateUrl: "/UI/views/admin/branchlst.html"
    });

    $routeProvider.when("/admin/branch/:id", {
        controller: "branchController",
        templateUrl: "/UI/views/admin/branch.html"
    });

    $routeProvider.when("/admin/branchDtls/:id", {
        controller: "branchDtlsController",
        templateUrl: "/UI/views/admin/branchDtls.html"
    });

    $routeProvider.when("/admin/bank", {
        controller: "bankController",
        templateUrl: "/UI/views/admin/bank.html"
    });

    $routeProvider.when("/admin/account", {
        controller: "accountController",
        templateUrl: "/UI/views/admin/account.html"
    });

    $routeProvider.when("/admin/user", {
        controller: "userController",
        templateUrl: "/UI/views/admin/user.html"
    });

    $routeProvider.when("/error", {
        controller: "errorController",
        templateUrl: "/UI/views/error.html"
    });

    $routeProvider.otherwise({ redirectTo: "/login" });

});

//var serviceBase = 'http://localhost:63671/';
//var serviceBase = 'http://localhost:8090/';
var serviceBase = '';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'alayamWebApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.config(function ($popoverProvider) {
    angular.extend($popoverProvider.defaults, {
        html: true
    });
});

app.directive('afterRender', ['$timeout', function ($timeout) {
    var def = {
        restrict: 'A',
        terminal: true,
        transclude: false,
        link: function (scope, element, attrs) {
            $timeout(scope.$eval(attrs.afterRender), 0);  //Calling a scoped method
        }
    };
    return def;
}]);




app.run(['authService', '$rootScope', '$location', function (authService, $rootScope, $location) {
    authService.fillAuthData();    
    var authentication = authService.authentication;

    $('[data-toggle=offcanvas]').click(function () {
        $('.row-offcanvas').toggleClass('active');
    });

    $rootScope.$on('$routeChangeStart', function (event, next, current) {

        if (authentication.isAuth)
        {
            if ($location.path().indexOf('admin') > 0 && (authentication.role == 3 || authentication.role == 4))
                $location.path('/error');
            else if ($location.path().indexOf('YearEndActivity') > 0 && authentication.role == 4)
                $location.path('/error');
            else if ($location.path().indexOf('YearBeginActivity') > 0 && authentication.role == 4)
                $location.path('/error');
            else if ($location.path().indexOf('Report') > 0 && authentication.role == 4)
                $location.path('/error');

        }
        else
        {
            if($location.path().indexOf('RptPrt') <= 0 )
                $location.path('/login');
        }
            
        
        
    });
}]);

app.filter('setDecimal', function ($filter) {
    return function (input, places) {
        if (isNaN(input)) return input;
        // If we want 1 decimal place, we want to mult/div by 10
        // If we want 2 decimal places, we want to mult/div by 100, etc
        // So use the following to create that factor
        var factor = "1" + Array(+(places > 0 && places + 1)).join("0");
        return Math.round(input * factor) / factor;
    };
});

app.directive("passwordStrength", function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            scope.$watch(attrs.passwordStrength, function (value) {                
                if (value != null) {
                    if (angular.isDefined(value)) {
                        if (value.length > 8) {
                            scope.strength = 'strong';
                        } else if (value.length > 3) {
                            scope.strength = 'medium';
                        } else {
                            scope.strength = 'weak';
                        }
                    }
                }
            });
        }
    };
});

app.directive('popOver', function ($compile) {
    var itemsTemplate = "<ul class='unstyled'><li ng-repeat='item in items'>{{item}}</li></ul>";
    var getTemplate = function (contentType) {
        var template = '';
        switch (contentType) {
            case 'items':
                template = itemsTemplate;
                break;
        }
        return template;
    }
    return {
        restrict: "A",
        transclude: true,
        template: "<span ng-transclude></span>",
        link: function (scope, element, attrs) {
            var popOverContent;
            if (scope.items) {
                var html = getTemplate("items");
                popOverContent = $compile(html)(scope);
            }
            var options = {
                content: popOverContent,
                placement: "bottom",
                html: true,
                title: scope.title
            };
            $(element).popover(options);
        },
        scope: {
            items: '=',
            title: '@'
        }
    };
});


app.directive('customPopover', function () {
    return {
        restrict: 'A',
        template: '<span>{{label}}</span>',
        link: function (scope, el, attrs) {
            scope.label = attrs.popoverLabel;
            $(el).popover({
                trigger: 'click',
                html: true,
                content: attrs.popoverHtml,
                placement: attrs.popoverPlacement
            });
        }
    };
});

