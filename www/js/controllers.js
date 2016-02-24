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
    .controller('LoginCtrl', ['$scope', '$rootScope', '$window', '$http', function ($scope, $rootScope, $window, $http) {
        
        
        $scope.faceBookLogin = function(){
            
                            
    
            facebookConnectPlugin.login(["public_profile"],
        function (userData) 
            {
                
                facebookConnectPlugin.getAccessToken(function(token) {
                    alert("Token: " + token);
                    
                    
                    //login in grouphub system
                    $http.post('http://192.168.1.7:2016/api/User.FacebookLogin', token )
                         .then(function(response){ 
                            
                            alert("response: " + JSON.stringify(response.data));
                        
                            if(response.data && !response.data.isError){
                                
                              $window.localStorage.setItem("user", response.data);
                              $window.localStorage.setItem("fb-access-token", token);
                              $window.localStorage.setItem("login-type", 'Facebook');
                                
                            }
                    },
                              function (error) { alert(JSON.stringify(error)) });
                    
                    
                    
                                        $http.get('http://192.168.1.7:2016/api/User.FacebookLogin?accessToken='+token )
                         .then(function(response){ 
                            
                            alert("response: " + JSON.stringify(response.data));
                        
                            if(response.data && !response.data.isError){
                                
                              $window.localStorage.setItem("user", response.data);
                              $window.localStorage.setItem("fb-access-token", token);
                              $window.localStorage.setItem("login-type", 'Facebook');
                                
                            }
                    },
                              function (error) { alert(JSON.stringify(error)) });
                    
                    
          
                    
                   
                    
                    alert("access-token: " + $window.localStorage.getItem("fb-access-token"));
                    alert("login-type: " + $window.localStorage.getItem("login-type"));
                    
                    }, function(err) {
                    
                    alert("Could not get access token: " + err);

                 });
                
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
