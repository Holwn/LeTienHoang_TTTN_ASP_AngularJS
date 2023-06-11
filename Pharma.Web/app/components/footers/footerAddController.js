(function (app) {
    app.controller('footerAddController', footerAddController);

    footerAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function footerAddController(apiService, $scope, notificationService, $state) {
        $scope.footer = {
        }
        $scope.typeFooters = [
            {
                ID: "title",
                Name: "Tiêu đề"
            },
            {
                ID: "page",
                Name: "Trang đơn"
            }
        ]
        $scope.AddFooter = AddFooter;


        function AddFooter() {
            apiService.post('api/footer/create', $scope.footer, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('footers');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }

        function loadPages() {
            apiService.get('api/page/getallparents', null, function (result) {
                $scope.pages = result.data;
            }, function () {
                console.log('Cannot get list page');
            });
        }

        function loadParentFooters() {
            apiService.get('api/footer/getallparents', null, function (result) {
                $scope.parentFooters = result.data;
            }, function () {
                console.log('Cannot get list page');
            });
        }

        loadPages();
        loadParentFooters();
    }

})(angular.module('pharma.footers'));