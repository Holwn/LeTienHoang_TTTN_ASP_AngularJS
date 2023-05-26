(function (app) {
    app.controller('receiptNoteEditController', receiptNoteEditController);

    receiptNoteEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService','$filter'];

    function receiptNoteEditController(apiService, $scope, notificationService, $state, $stateParams, commonService, $filter) {
        $scope.receiptNote = {
        };
        $scope.formattedDate = $filter('date')($scope.receiptNote.Date, 'dd-MM-yyyy');
        $scope.TypePayments = [
            {
                Alias: 'cash',
                Name: 'Tiền mặt'
            },
            {
                Alias: 'transfer',
                Name: 'Chuyển khoản'
            }
        ];
        $scope.subjects = [];
        $scope.products = [];
        $scope.productItems = {};
        $scope.receiptItems = [];
        $scope.UpdateReceiptNote = UpdateReceiptNote;


        $scope.$watch('receiptNote.Date', function (newValue) {
            $scope.formattedDate = $filter('date')(newValue, 'dd-MM-yyyy');
        });

        $scope.OnSubjectChange = function () {
            $scope.receiptNote.SupplierID = $scope.subject.ID;
        }

        function loadreceiptNoteDetail() {
            apiService.get('api/receiptNote/getbyid/' + $stateParams.id, null, function (result) {
                var resultNote = result.data;
                resultNote.Date = new Date(resultNote.Date);

                $scope.receiptNote = resultNote;
                console.log(result);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateReceiptNote() {
            apiService.put('api/receiptNote/update', $scope.receiptNote, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('receiptNotes');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        function getUnits() {
            apiService.get('api/unit/getallparents', null, function (result) {
                $scope.units = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function getSubjects() {
            apiService.get('api/subject/getallparents', null, function (result) {
                angular.forEach(result.data, function (item) {
                    if (item.Status == true && item.TypeSub == 'supplier') {
                        $scope.subjects.push(item);
                    }
                })
                if ($scope.subjects.length > 0) {
                    console.log($scope.subjects);
                    for (i = 0; i < $scope.subjects.length; i++) {
                        if ($scope.subjects[i].ID == $scope.receiptNote.SupplierID) {
                            $scope.subject = $scope.subjects[i];
                        }
                    }
                }
            }, function () {
                console.log('Get subject failed.');
            });
        }

        function getProducts() {
            apiService.get('api/product/getallparents', null, function (result) {
                angular.forEach(result.data, function (item) {
                    if (item.Status == true) {
                        $scope.products.push(item);
                    }
                })
            }, function () {
                console.log('Get product failed.');
            });
        }

        loadreceiptNoteDetail();
        getProducts();
        getSubjects();
        getUnits();
    }

})(angular.module('pharma.receiptNotes'));