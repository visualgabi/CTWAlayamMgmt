var app = angular.module('nellaiTechApp',
    [       
        'ui.router',
        'ngSanitize',
        'ui.bootstrap'
    ]
);

//var serviceBase = 'http://localhost:63671/';
//var serviceBase = 'http://localhost:8090/';
var serviceBase = 'http://localhost:15312/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase
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



