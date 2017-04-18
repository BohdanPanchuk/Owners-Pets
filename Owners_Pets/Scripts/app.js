'use strict';
var app = angular.module('app', ['ngRoute', 'ui.bootstrap']);

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/Content/Index.html',
            controller: 'usersController',
            resolve: {
                usersData: ['$http', function ($http) {
                    return $http.get('http://localhost:54146/api/User').then(function (userResponse) {
                        return userResponse.data;
                    })
                }]
            }
        })
        .when('/User/:Id', {
            templateUrl: '/Content/UserInfo.html',
            controller: 'userInfoController',
            resolve: {
                userName: ['$http', '$route', function ($http, $route) {
                    $route.current.params.key
                    console.log("$route.current.param.Id: " + $route.current.params.Id);
                    return $http.get('http://localhost:54146/api/User/' + $route.current.params.Id)
                        .then(function (response) {
                            return response.data.Name;
                        });
                }],
                userInfoList: ['$http', '$route', function ($http, $route) {
                return $http.get('http://localhost:54146/api/UserInfo/' + $route.current.params.Id)
                    .then(function (userInfoResponse) {
                        return userInfoResponse.data;
                    });
                }]
            }
        })
        .otherwise({ redirectTo: '/' });
    
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});