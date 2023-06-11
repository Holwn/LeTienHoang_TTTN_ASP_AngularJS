(function (app) {
    app.controller('pageListController', pageListController);

    pageListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function pageListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.pages = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.isAll = false;

        $scope.getPages = getPages;
        $scope.search = search;
        $scope.deletePage = deletePage;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var lisId = [];
            $.each($scope.selected, function (i, item) {
                lisId.push(item.ID);
            });
            var config = {
                params: {
                    checkedPages: JSON.stringify(lisId)
                }
            }
            apiService.del('api/page/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.pages, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.pages, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("pages", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deletePage(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('api/page/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                });
            });
        }

        function search() {
            getPages();
        }

        function getPages(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('api/page/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.pages = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load page failed.');
            });
        }

        $scope.getPages();
    }
})(angular.module('pharma.pages'));