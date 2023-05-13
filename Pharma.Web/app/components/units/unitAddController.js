(function (app) {
    app.controller('unitAddController', unitAddController);

    unitAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function unitAddController(apiService, $scope, notificationService, $state) {
        $scope.unit = {
            StoreID: 1,
            Status: true,
        }

        $scope.AddUnit = AddUnit;

        function AddUnit() {
            apiService.post('api/unit/create', $scope.unit, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('units');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
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
    }

})(angular.module('pharma.units'));