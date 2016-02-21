'use strict';

angular.module('myApp.controllers', [])
    .controller('MainCtrl', ['$scope', '$rootScope', '$window', '$location', function ($scope, $rootScope, $window, $location) {
        $scope.slide = '';
        $rootScope.back = function() {
          $scope.slide = 'slide-right';
          $window.history.back();
        }
        $rootScope.go = function(path){
          $scope.slide = 'slide-left';
          $location.url(path);
        }
    }])
    .controller('LoginCtrl', ['$scope', '$rootScope', function ($scope, $rootScope) {
        
        $scope.faceBookLogin = function(){
            
            facebookConnectPlugin.login(["public_profile"],
        function (userData) 
            {
                
                facebookConnectPlugin.getAccessToken(function(token) {
                    alert("Token: " + token);
                    }, function(err) {
                    alert("Could not get access token: " + err);
                 });
                
                facebookConnectPlugin.api("/me", [], function(response) {
                    
                   alert("Good to see you, " + JSON.stringify(response));
                    
                   $rootScope.go("partials/my-groups.html");
                }, function(err) {
                    alert("Could not get my details: " + err);
                 });
                
                
                
                
                
                 alert("fbLoginSuccess");
                 alert("UserInfo: " + JSON.stringify(userData));
           },
        function (error) { alert(JSON.stringify(error)) }
         );
            
        }

    }])
    .controller('MyGroupsCtrl', ['$scope', '$rootScope','$window', '$location', '$routeParams', '$http', function ($scope, $rootScope, $window, $location, $routeParams, $http) {
        
        //Load current user groups
        fetchMyGroups($routeParams.userToken);
        
        
        
        //Start methods
        
        function fetchMyGroups(userToken){
          
             $http.get('http://192.168.1.7:2016/api/Group.LoadUserGroups?userToken='+ userToken)
                         .then(function(response){ 
                            
                            if(response.data && !response.data.isError)
                            $scope.myGroups = response.data.data; 
            });
            
        }
        
        
        $scope.goToDetail = function(group){
            
            $window.localStorage.setItem("group", JSON.stringify(group));
            $location.url('/groupDetails/');
        }
        
        
        //End methods
        
                            
    }])
    .controller('GroupDetailCtrl', ['$scope', '$routeParams', '$http', function ($scope, $routeParams, $http) {
        
        //Initialize drawer
        $('.drawer').drawer();
        
        //Load group from prev page
        $scope.group = JSON.parse(window.localStorage.getItem("group"));
        
        //load group members
        fetchGroupMembers($scope.group.token);
        
        //load items
        fetchGroupItems($scope.group.token);
        
        
        
        
        
        //Start methods
        
        function fetchGroupMembers(groupToken){
          
             $http.get('http://192.168.1.7:2016/api/Group.LoadGroupMembers?groupToken='+ groupToken)
                         .then(function(response){ 
                            
                            if(response.data && !response.data.isError)
                            $scope.members = response.data.data; 
            });
            
        }
        
        function fetchGroupItems(groupToken){
          
             $http.get('http://192.168.1.7:2016/api/Group.LoadGroupMembers?groupToken='+ groupToken)
                         .then(function(response){ 
                            
                            if(response.data && !response.data.isError)
                            $scope.members = response.data.data; 
            });
            
        }
        
        //End methods
        
    }]);







/*    .controller('EmployeeListCtrl', ['$scope', 'Employee', function ($scope, Employee) {
        $scope.employees = Employee.query();
    }])
    .controller('EmployeeDetailCtrl', ['$scope', '$routeParams', 'Employee', function ($scope, $routeParams, Employee) {
        $scope.employee = Employee.get({employeeId: $routeParams.employeeId});
    }])
    .controller('ReportListCtrl', ['$scope', '$routeParams', 'Report', function ($scope, $routeParams, Report) {
        $scope.employees = Report.query({employeeId: $routeParams.employeeId});
    }]);*/
