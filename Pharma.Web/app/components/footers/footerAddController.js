(function (app) {
    app.controller('footerAddController', footerAddController);

    footerAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function footerAddController(apiService, $scope, notificationService, $state) {
        $scope.footer = {
        }

        $scope.AddFooter = AddFooter;

        function AddFooter() {
            apiService.post('api/footer/create', $scope.footer, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('footers');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }

        function loadParentfooters() {
            apiService.get('api/footer/getallparents', null, function (result) {
                $scope.parentfooters = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentfooters();
    }

})(angular.module('pharma.footers'));