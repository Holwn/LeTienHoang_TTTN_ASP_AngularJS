(function (app) {
    app.controller('unitEditController', unitEditController);

    unitEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function unitEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.unit = {
        }

        $scope.UpdateUnit = UpdateUnit;

        function loadUnitDetail() {
            apiService.get('api/unit/getbyid/' + $stateParams.id, null, function (result) {
                $scope.unit = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateUnit() {
            apiService.put('api/unit/update', $scope.unit, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('units');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        function loadParentUnits() {
            apiService.get('api/unit/getallparents', null, function (result) {
                $scope.parentUnits = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentUnits();
        loadUnitDetail();
    }

})(angular.module('pharma.units'));