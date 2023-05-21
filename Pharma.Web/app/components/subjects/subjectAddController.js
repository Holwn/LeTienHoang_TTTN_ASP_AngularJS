(function (app) {
    app.controller('subjectAddController', subjectAddController);

    subjectAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function subjectAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.subject = {
            StoreID: 1,
            CreateDate: new Date(),
            Status: true,
        }
        $scope.TypeSubjects = [
            {
                Alias: 'supplier',
                Name:'Nhà cung cấp'
            },
            {
                Alias: 'customer',
                Name: 'Khách hàng'
            }
        ];
        $scope.AddSubject = AddSubject;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.subject.Alias = commonService.getSeoTitle($scope.subject.Name);
        }

        function AddSubject() {
            apiService.post('api/subject/create', $scope.subject, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('subjects');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }
    }
})(angular.module('pharma.subjects'));