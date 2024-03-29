﻿/// <reference path="../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma',
        [
            'pharma.application_groups',
            'pharma.application_roles',
            'pharma.application_users',
            'pharma.deliveryNotes',
            'pharma.contactdetails',
            'pharma.footers',
            'pharma.noteBooks',
            'pharma.pages',
            'pharma.post_categories',
            'pharma.posts',
            'pharma.product_categories',
            'pharma.products',
            'pharma.receiptNotes',
            'pharma.slides',
            'pharma.subjects',
            'pharma.units',
            'pharma.common'
        ])
        .config(config)
        .config(configAuthentication)
        .config(['$qProvider', function ($qProvider) {
            $qProvider.errorOnUnhandledRejections(false);
        }])

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: "",
                templateUrl: "app/shared/views/baseView.html",
                abstract: true
            })
            .state('login', {
                url: "/login",
                templateUrl: "app/components/login/loginView.html",
                controller: "loginController"
            })
            .state('home', {
                url: "/admin",
                parent: 'base',
                templateUrl: "app/components/home/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/login');
    }

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }

})();