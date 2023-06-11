(function (app) {
    app.controller('contactdetailAddController', contactdetailAddController);

    contactdetailAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function contactdetailAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.contactdetail = {
            Status: true,
        }
        $scope.AddContactdetail = AddContactdetail;

        function AddContactdetail() {
            apiService.post('api/contactdetail/create', $scope.contactdetail, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('contactdetails');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }
    }
})(angular.module('pharma.contactdetails'));