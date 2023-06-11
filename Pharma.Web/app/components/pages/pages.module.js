/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.pages', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('pages', {
                url: "/pages",
                parent: 'base',
                templateUrl: "app/components/pages/pageListView.html",
                controller: "pageListController"
            }).state('add_page', {
                url: "/add_page",
                parent: 'base',
                templateUrl: "app/components/pages/pageAddView.html",
                controller: "pageAddController"
            }).state('edit_page', {
                url: "/edit_page/:id",
                parent: 'base',
                templateUrl: "app/components/pages/pageEditView.html",
                controller: "pageEditController"
            });
    }
})();