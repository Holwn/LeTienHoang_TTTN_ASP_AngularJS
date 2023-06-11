/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.noteBooks', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('noteBooks', {
                url: "/noteBooks",
                parent: 'base',
                templateUrl: "app/components/noteBooks/noteBookListView.html",
                controller: "noteBookListController"
            }).state('add_noteBook', {
                url: "/add_noteBook",
                parent: 'base',
                templateUrl: "app/components/noteBooks/noteBookAddView.html",
                controller: "noteBookAddController"
            }).state('edit_noteBook', {
                url: "/edit_noteBook/:id",
                parent: 'base',
                templateUrl: "app/components/noteBooks/noteBookEditView.html",
                controller: "noteBookEditController"
            });
    }
})();