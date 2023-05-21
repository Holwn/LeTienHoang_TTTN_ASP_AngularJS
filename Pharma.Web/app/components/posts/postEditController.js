(function (app) {
    app.controller('postEditController', postEditController);

    postEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function postEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.post = {
            Status: true,
        }

        $scope.UpdatePost = UpdatePost;
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.postCategories = [];
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.post.Image = fileUrl;
                });
            }
            finder.popup();
        }

        function GetSeoTitle() {
            $scope.post.Alias = commonService.getSeoTitle($scope.post.Name);
        }

        function loadPostDetail() {
            apiService.get('api/post/getbyid/' + $stateParams.id, null, function (result) {
                $scope.post = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdatePost() {
            apiService.put('api/post/update', $scope.post, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('posts');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        function getPostCategories() {
            apiService.get('api/postcategory/getallparents', null, function (result) {
                angular.forEach(result.data, function (item) {
                    if (item.Status == true) {
                        $scope.postCategories.push(item);
                    }
                })
            }, function () {
                console.log('Get Post category failed.');
            });
        }

        getPostCategories();
        loadPostDetail();
    }

})(angular.module('pharma.posts'));