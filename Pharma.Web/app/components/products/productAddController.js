(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','commonService'];

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            StoreID: 1,
            CreateDate: new Date(),
            IsHaveExpiredDate: false,
            HomeFlag: true,
            Quantity: 0,
            Prescription: false,
            ContainMidule: 1,
            Status: true,
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        $scope.AddProduct = AddProduct;
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

        function AddProduct() {
            $scope.product.MoreImage = JSON.stringify($scope.moreImages);
            apiService.post('api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
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
    }
})(angular.module('pharma.products'));