/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.contactdetails', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('contactdetails', {
                url: "/contactdetails",
                parent: 'base',
                templateUrl: "app/components/contactdetails/contactdetailListView.html",
                controller: "contactdetailListController"
            }).state('add_contactdetail', {
                url: "/add_contactdetail",
                parent: 'base',
                templateUrl: "app/components/contactdetails/contactdetailAddView.html",
                controller: "contactdetailAddController"
            }).state('edit_contactdetail', {
                url: "/edit_contactdetail/:id",
                parent: 'base',
                templateUrl: "app/components/contactdetails/contactdetailEditView.html",
                controller: "contactdetailEditController"
            });
    }
})();