/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.receiptNotes', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('receiptNotes', {
                url: "/receiptNotes",
                parent: 'base',
                templateUrl: "app/components/receiptNotes/receiptNoteListView.html",
                controller: "receiptNoteListController"
            }).state('add_receiptNote', {
                url: "/add_receiptNote",
                parent: 'base',
                templateUrl: "app/components/receiptNotes/receiptNoteAddView.html",
                controller: "receiptNoteAddController"
            }).state('edit_receiptNote', {
                url: "/edit_receiptNote/:id",
                parent: 'base',
                templateUrl: "app/components/receiptNotes/receiptNoteEditView.html",
                controller: "receiptNoteEditController"
            });
    }
})();