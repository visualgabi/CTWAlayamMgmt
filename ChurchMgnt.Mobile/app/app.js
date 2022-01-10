var app = angular.module('churchMgntApp',
    [       
        'ui.router',
        'ngSanitize',
        'ui.bootstrap',       
        'LocalStorageModule',
        'xeditable',
        'ngTouch',
        'ngTable',
        'ngAnimate',        
        'mgcrea.ngStrap',        
        'angular.filter',                
        'angularValidator',
        'chieffancypants.loadingBar'        
    ]
);

//var serviceBase = 'http://localhost:63671/';
//var serviceBase = 'http://localhost:8090/';
var serviceBase = 'http://localhost:15312/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'alayamMobileWebApp',
    clientSecret: '28E921CA-9A0C-432F-AF28-48799CD3F541'
});

app.config(function ($httpProvider, $provide) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.config(function ($popoverProvider) {
    angular.extend($popoverProvider.defaults, {
        html: true
    });
});

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


app.run(['authService', '$rootScope', '$location', '$state', function (authService, $rootScope, $location, $state) {
    authService.fillAuthData();
    var authentication = authService.authentication;

    $('[data-toggle=offcanvas]').click(function () {
        $('.row-offcanvas').toggleClass('active');
    });    

    $rootScope.$on("$stateChangeStart", function (e, toState, toParams, fromState, fromParams) {
        console.log("Start:   " + toState.name);

        if (toState.name.indexOf('contributionPrint') < 0) {
                if (authentication.isAuth) {
                    if (toState.name.indexOf('admin') >= 0 && (authentication.role == 3 || authentication.role == 4))
                        $location.path('/error');
                    else if (toState.name.indexOf('YearEndActivity') >= 0 && authentication.role == 4)
                        $location.path('/error');
                    else if (toState.name.indexOf('YearBeginActivity') >= 0 && authentication.role == 4)
                        $location.path('/error');
                    else if (toState.name.indexOf('Report') >= 0 && authentication.role == 4)
                        $location.path('/error');

                }
                else
                    $location.path('/login');
        }
        

    });

    //$rootScope.$on('$statechangestart', function (event, next, current) {

    //    if (!$location.path().indexOf('print')) {
    //        if (authentication.isAuth) {
    //            if ($location.path().indexOf('admin') > 0 && (authentication.role == 3 || authentication.role == 4))
    //                $location.path('/error');
    //            else if ($location.path().indexOf('YearEndActivity') > 0 && authentication.role == 4)
    //                $location.path('/error');
    //            else if ($location.path().indexOf('YearBeginActivity') > 0 && authentication.role == 4)
    //                $location.path('/error');
    //            else if ($location.path().indexOf('Report') > 0 && authentication.role == 4)
    //                $location.path('/error');

    //        }
    //        else
    //            $location.path('/login');
    //    }


    //});
}]);
