/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.slides', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('slides', {
                url: "/slides",
                parent: 'base',
                templateUrl: "app/components/slides/slideListView.html",
                controller: "slideListController"
            }).state('add_slide', {
                url: "/add_slide",
                parent: 'base',
                templateUrl: "app/components/slides/slideAddView.html",
                controller: "slideAddController"
            }).state('edit_slide', {
                url: "/edit_slide/:id",
                parent: 'base',
                templateUrl: "app/components/slides/slideEditView.html",
                controller: "slideEditController"
            });
    }
})();