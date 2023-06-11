(function (app) {
    app.controller('deliveryNoteListController', deliveryNoteListController);

    deliveryNoteListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function deliveryNoteListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.deliveryNotes = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.isAll = false;

        $scope.getDeliveryNotes = getDeliveryNotes;
        $scope.search = search;
        $scope.deleteDeliveryNote = deleteDeliveryNote;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var lisId = [];
            $.each($scope.selected, function (i, item) {
                lisId.push(item.ID);
            });
            var config = {
                params: {
                    checkedDeliveryNotes: JSON.stringify(lisId)
                }
            }
            apiService.del('api/deliverynote/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.deliveryNotes, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.deliveryNotes, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("deliveryNotes", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteDeliveryNote(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('api/deliverynote/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                });
            });
        }

        function search() {
            getDeliveryNotes();
        }

        function getDeliveryNotes(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('api/deliverynote/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.deliveryNotes = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load deliveryNote failed.');
            });
        }

        $scope.getDeliveryNotes();
    }
})(angular.module('pharma.deliveryNotes'));