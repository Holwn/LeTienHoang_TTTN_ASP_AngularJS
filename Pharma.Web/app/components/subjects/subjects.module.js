/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.subjects', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('subjects', {
                url: "/subjects",
                parent: 'base',
                templateUrl: "app/components/subjects/subjectListView.html",
                controller: "subjectListController"
            }).state('add_subject', {
                url: "/add_subject",
                parent: 'base',
                templateUrl: "app/components/subjects/subjectAddView.html",
                controller: "subjectAddController"
            }).state('edit_subject', {
                url: "/edit_subject/:id",
                parent: 'base',
                templateUrl: "app/components/subjects/subjectEditView.html",
                controller: "subjectEditController"
            });
    }
})();