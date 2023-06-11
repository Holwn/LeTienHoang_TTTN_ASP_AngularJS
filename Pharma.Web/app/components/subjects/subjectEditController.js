(function (app) {
    app.controller('subjectEditController', subjectEditController);

    subjectEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function subjectEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.subject = {
        }
        $scope.TypeSubjects = [
            {
                Alias: 'supplier',
                Name: 'Nhà cung cấp'
            },
            {
                Alias: 'customer',
                Name: 'Khách hàng'
            }
        ];
        $scope.UpdateSubject = UpdateSubject;
        $scope.GetSeoTitle = GetSeoTitle;
        
        function GetSeoTitle() {
            $scope.subject.Alias = commonService.getSeoTitle($scope.subject.Name);
        }

        function loadSubjectDetail() {
            apiService.get('api/subject/getbyid/' + $stateParams.id, null, function (result) {
                $scope.subject = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateSubject() {
            apiService.put('api/subject/update', $scope.subject, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('subjects');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        loadSubjectDetail();
    }

})(angular.module('pharma.subjects'));