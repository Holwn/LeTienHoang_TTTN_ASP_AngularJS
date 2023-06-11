(function (app) {
    app.controller('receiptNoteListController', receiptNoteListController);

    receiptNoteListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function receiptNoteListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.receiptNotes = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.isAll = false;

        $scope.getReceiptNotes = getReceiptNotes;
        $scope.search = search;
        $scope.deleteReceiptNote = deleteReceiptNote;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var lisId = [];
            $.each($scope.selected, function (i, item) {
                lisId.push(item.ID);
            });
            var config = {
                params: {
                    checkedReceiptNotes: JSON.stringify(lisId)
                }
            }
            apiService.del('api/receiptnote/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                $scope.LoadProductMapping();
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.receiptNotes, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.receiptNotes, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("receiptNotes", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteReceiptNote(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('api/receiptnote/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    $scope.LoadProductMapping();
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                });
            });
        }

        $scope.LoadProductMapping = function () {
            apiService.get('api/productmapping/loadproductmapping', null, function (result) {
                notificationService.displaySuccess('Chạy kho thành công');
            }, function (error) {
                notificationService.displayError('Chạy kho thất bại');
            });
        }

        function search() {
            getReceiptNotes();
        }

        function getReceiptNotes(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('api/receiptnote/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.receiptNotes = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load receiptNote failed.');
            });
        }

        $scope.getReceiptNotes();
    }
})(angular.module('pharma.receiptNotes'));