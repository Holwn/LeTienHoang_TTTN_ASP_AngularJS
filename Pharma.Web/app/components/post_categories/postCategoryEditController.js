(function (app) {
    app.controller('postCategoryEditController', postCategoryEditController);

    postCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function postCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.postCategory = {
            Status: true,
        }

        $scope.UpdatePostCategory = UpdatePostCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.postCategory.Image = fileUrl;
                });
            }
            finder.popup();
        }

        function GetSeoTitle() {
            $scope.postCategory.Alias = commonService.getSeoTitle($scope.postCategory.Name);
        }

        function loadPostCategoryDetail() {
            apiService.get('api/postcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.postCategory = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdatePostCategory() {
            apiService.put('api/postcategory/update', $scope.postCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('post_categories');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        function loadParentCategories() {
            apiService.get('api/postcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentCategories();
        loadPostCategoryDetail();
    }

})(angular.module('pharma.post_categories'));