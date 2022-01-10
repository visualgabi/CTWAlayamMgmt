function configState($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(true);

    // Set default state
    $urlRouterProvider.otherwise("/home");
    $stateProvider

        .state('home', {
            url: "/home",
            templateUrl: "app/views/home.html",
            data: {
                pageTitle: 'home',
                specialClass: 'landing-page'
            }
        })
}

angular
    .module('nellaiTechApp')
    .config(configState)
    .run(function ($rootScope, $state) {
        $rootScope.$state = $state;
        //editableOptions.theme = 'bs3';
    });