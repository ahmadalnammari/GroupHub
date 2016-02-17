'use strict';


angular.module('myApp', [
    'ngTouch',
    'ngRoute',
    'ngAnimate',
    'checklist-model',
    'myApp.controllers',
]).
config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/login', {templateUrl: 'partials/login.html', controller: 'LoginCtrl'});
    $routeProvider.when('/mygroups/:userToken', {templateUrl: 'partials/my-groups.html', controller: 'MyGroupsCtrl'});
    $routeProvider.when('/groupDetails', {templateUrl: 'partials/group-detail.html', controller: 'GroupDetailCtrl'});
    $routeProvider.otherwise({redirectTo: '/login'});
}]);