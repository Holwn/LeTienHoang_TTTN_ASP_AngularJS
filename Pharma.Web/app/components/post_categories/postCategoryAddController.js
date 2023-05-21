(function (app) {
    app.controller('postCategoryAddController', postCategoryAddController);

    postCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function postCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.postCategory = {
            CreateDate: new Date(),
            Status: true,
        }
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddPostCategory = AddPostCategory;

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

        function AddPostCategory() {
            apiService.post('api/postcategory/create', $scope.postCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('post_categories');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
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
    }

})(angular.module('pharma.post_categories'));