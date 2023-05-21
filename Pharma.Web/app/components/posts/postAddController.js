(function (app) {
    app.controller('postAddController', postAddController);

    postAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function postAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.post = {
            CreateDate: new Date(),
            Status: true,
        }
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddPost = AddPost;
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

        function AddPost() {
            apiService.post('api/post/create', $scope.post, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('posts');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
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
    }

})(angular.module('pharma.posts'));