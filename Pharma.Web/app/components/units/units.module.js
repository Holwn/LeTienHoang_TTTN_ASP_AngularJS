﻿/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.units', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('units', {
                url: "/units",
                parent: 'base',
                templateUrl: "app/components/units/unitListView.html",
                controller: "unitListController"
            })
            .state('add_unit', {
                url: "/add_unit",
                parent: 'base',
                templateUrl: "app/components/units/unitAddView.html",
                controller: "unitAddController"
            })
            .state('edit_unit', {
                url: "/edit_unit/:id",
                parent: 'base',
                templateUrl: "app/components/units/unitEditView.html",
                controller: "unitEditController"
            });
    }
})();