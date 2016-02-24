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
}]).run(['$rootScope', '$window', 
  function($rootScope, $window) {
    
      
      $.ajaxSetup({
    beforeSend: function(xhr) {
        var accessToken = $window.localStorage.getItem("fb-access-token");
        var loginType = $window.localStorage.getItem("login-type");
        if(accessToken && loginType){
        xhr.setRequestHeader("fb-access-token", accessToken);
        xhr.setRequestHeader("login-type", loginType);
    }}
});
      
      
/*console.log("dsfs");
  $rootScope.user = {};

  $window.fbAsyncInit = function() {
    // Executed when the SDK is loaded

    FB.init({ 



      appId: '1570241279927313', 

      status: true, 
      cookie: true, 
      xfbml: true,
      version: 'v2.4'
    });


  };

  // Are you familiar to IIFE ( http://bit.ly/iifewdb ) ?

  (function(d){
    // load the Facebook javascript SDK

    var js, 
    id = 'facebook-jssdk', 
    ref = d.getElementsByTagName('script')[0];

    if (d.getElementById(id)) {
      return;
    }

    js = d.createElement('script'); 
    js.id = id; 
    js.async = true;
    js.src = "//connect.facebook.net/en_US/sdk.js";

    ref.parentNode.insertBefore(js, ref);

  }(document));*/

}]);