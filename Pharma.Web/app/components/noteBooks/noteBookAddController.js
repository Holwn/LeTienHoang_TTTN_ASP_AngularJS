(function (app) {
    app.controller('noteBookAddController', noteBookAddController);

    noteBookAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function noteBookAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.noteBook = {
            CreateDate: new Date(),
            Status: true,
        }
        $scope.linkNoteBooks = [
            { Link: 'products',Name:'Danh mục thuốc' },
            { Link: 'product_categories',Name:'Danh mục thuốc' },
            { Link: 'units',Name:'Đơn vị' },
            { Link: 'subjects', Name:'Chủ thể' },
            { Link: 'contactdetails', Name:'Liên hệ' },
            { Link: 'receiptNotes', Name:'Phiếu nhập' },
            { Link: 'deliveryNotes', Name:'Phiếu xuất' },
            { Link: 'post_categories', Name:'Chủ đề bài viết' },
            { Link: 'posts', Name:'Danh sách bài viết' },
            { Link: 'slides', Name:'Slide' },
            { Link: 'footers', Name:'Cuối trang' },
        ];
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '500px'
        }
        $scope.AddNoteBook = AddNoteBook;

        function AddNoteBook() {
            apiService.post('api/notebook/create', $scope.noteBook, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('noteBooks');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }
    }
})(angular.module('pharma.noteBooks'));