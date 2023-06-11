(function (app) {
    app.controller('footerEditController', footerEditController);

    footerEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function footerEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
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
        $scope.UpdateFooter = UpdateFooter;
       
        function loadFooterDetail() {
            apiService.get('api/footer/getbyid/' + $stateParams.id, null, function (result) {
                $scope.footer = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateFooter() {
            apiService.put('api/footer/update', $scope.footer, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('footers');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
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
        loadFooterDetail();
    }

})(angular.module('pharma.footers'));