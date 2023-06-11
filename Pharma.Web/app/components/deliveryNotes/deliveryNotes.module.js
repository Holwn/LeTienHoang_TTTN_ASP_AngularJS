/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.deliveryNotes', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('deliveryNotes', {
                url: "/deliveryNotes",
                parent: 'base',
                templateUrl: "app/components/deliveryNotes/deliveryNoteListView.html",
                controller: "deliveryNoteListController"
            }).state('add_deliveryNote', {
                url: "/add_deliveryNote",
                parent: 'base',
                templateUrl: "app/components/deliveryNotes/deliveryNoteAddView.html",
                controller: "deliveryNoteAddController"
            }).state('edit_deliveryNote', {
                url: "/edit_deliveryNote/:id",
                parent: 'base',
                templateUrl: "app/components/deliveryNotes/deliveryNoteEditView.html",
                controller: "deliveryNoteEditController"
            });
    }
})();