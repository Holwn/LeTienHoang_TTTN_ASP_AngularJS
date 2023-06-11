(function (app) {
    app.controller('contactdetailEditController', contactdetailEditController);

    contactdetailEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function contactdetailEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.contactdetail = {
        }
        $scope.UpdateContactdetail = UpdateContactdetail;

        function loadcontactdetailDetail() {
            apiService.get('api/contactdetail/getbyid/' + $stateParams.id, null, function (result) {
                $scope.contactdetail = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateContactdetail() {
            apiService.put('api/contactdetail/update', $scope.contactdetail, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('contactdetails');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        loadcontactdetailDetail();
    }

})(angular.module('pharma.contactdetails'));