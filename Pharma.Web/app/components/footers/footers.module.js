/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.footers', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('footers', {
                url: "/footers",
                parent: 'base',
                templateUrl: "app/components/footers/footerListView.html",
                controller: "footerListController"
            })
            .state('add_footer', {
                url: "/add_footer",
                parent: 'base',
                templateUrl: "app/components/footers/footerAddView.html",
                controller: "footerAddController"
            })
            .state('edit_footer', {
                url: "/edit_footer/:id",
                parent: 'base',
                templateUrl: "app/components/footers/footerEditView.html",
                controller: "footerEditController"
            });
    }
})();