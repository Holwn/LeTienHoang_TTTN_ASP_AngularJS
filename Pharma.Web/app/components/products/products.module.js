/// <reference path="../../../assets/admin/libs/angularjs/angular.js" />

(function () {
    angular.module('pharma.products', ['pharma.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('products', {
                url: "/products",
                parent: 'base',
                templateUrl: "app/components/products/productListView.html",
                controller: "productListController"
            }).state('add_product', {
                url: "/add_product",
                parent: 'base',
                templateUrl: "app/components/products/productAddView.html",
                controller: "productAddController"
            }).state('edit_product', {
                url: "/edit_product/:id",
                parent: 'base',
                templateUrl: "app/components/products/productEditView.html",
                controller: "productEditController"
            });
    }
})();