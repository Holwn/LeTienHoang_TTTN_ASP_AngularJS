(function (app) {
    app.controller('noteBookEditController', noteBookEditController);

    noteBookEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function noteBookEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.noteBook = {
        }
        $scope.linkNoteBooks = [
            { Link: 'products', Name: 'Danh mục thuốc' },
            { Link: 'product_categories', Name: 'Danh mục thuốc' },
            { Link: 'units', Name: 'Đơn vị' },
            { Link: 'subjects', Name: 'Chủ thể' },
            { Link: 'contactdetails', Name: 'Liên hệ' },
            { Link: 'receiptNotes', Name: 'Phiếu nhập' },
            { Link: 'deliveryNotes', Name: 'Phiếu xuất' },
            { Link: 'post_categories', Name: 'Chủ đề bài viết' },
            { Link: 'posts', Name: 'Danh sách bài viết' },
            { Link: 'slides', Name: 'Slide' },
            { Link: 'footers', Name: 'Cuối trang' },
        ];
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '500px'
        }
        $scope.UpdateNoteBook = UpdateNoteBook;

        function loadNoteBookDetail() {
            apiService.get('api/notebook/getbyid/' + $stateParams.id, null, function (result) {
                $scope.noteBook = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateNoteBook() {
            apiService.put('api/notebook/update', $scope.noteBook, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('noteBooks');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        loadNoteBookDetail();
    }

})(angular.module('pharma.noteBooks'));