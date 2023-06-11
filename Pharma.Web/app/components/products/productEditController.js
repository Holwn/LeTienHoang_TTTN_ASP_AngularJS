(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function productEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.product = {
        }

        $scope.UpdateProduct = UpdateProduct;
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.moreImages = [];
        $scope.productCategories = [];
        $scope.units = [];

        CKEDITOR.on("instanceReady", function (event) {
            event.editor.on("beforeCommandExec", function (event) {
                // Show the paste dialog for the paste buttons and right-click paste
                if (event.data.name == "paste") {
                    event.editor._.forcePasteDialog = true;
                }
                // Don't show the paste dialog for Ctrl+Shift+V
                if (event.data.name == "pastetext" && event.data.commandData.from == "keystrokeHandler") {
                    event.cancel();
                }
            })
        });

        $scope.ClearMoreImage = function () {
            $scope.moreImages = [];
        }

        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
            }
            finder.popup();
        }

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function loadProductDetail() {
            apiService.get('api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImage) ? JSON.parse($scope.product.MoreImage):[];
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateProduct() {
            $scope.product.MoreImage = JSON.stringify($scope.moreImages);
            apiService.put('api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        function getProductCategories() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                angular.forEach(result.data, function (item) {
                    if (item.Status == true) {
                        $scope.productCategories.push(item);
                    }
                })
            }, function () {
                console.log('Get product category failed.');
            });
        }

        function getUnits() {
            apiService.get('api/unit/getallparents', null, function (result) {
                angular.forEach(result.data, function (item) {
                    if (item.Status == true) {
                        $scope.units.push(item);
                    }
                })
            }, function () {
                console.log('Get product category failed.');
            });
        }

        getProductCategories();
        getUnits();
        loadProductDetail();
    }

})(angular.module('pharma.products'));