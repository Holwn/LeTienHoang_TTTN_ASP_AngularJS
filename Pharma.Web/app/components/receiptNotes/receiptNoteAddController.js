(function (app) {
    app.controller('receiptNoteAddController', receiptNoteAddController);

    receiptNoteAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$http','$filter'];

    function receiptNoteAddController(apiService, $scope, notificationService, $state, commonService, $http, $filter) {
        $scope.receiptNote = {
            SupplierID: 1,
            Amount: 0,
            VAT: 0,
            Date: 0,
            PaymentAmount: 0,
            Description: '',
            StoreID: 1,
            CreateDate: new Date(),
            Status: true,
        };
        $scope.subjects = [];
        $scope.products = [];
        $scope.productItems = {};
        $scope.receiptItems = [];
        var stt = 0;
        var today = new Date();
        var codeFromDate = (today.getDate() + today.getSeconds()).toString() + (today.getMonth() + today.getMinutes()).toString() + (today.getFullYear() + today.getHours()).toString();
        $scope.receiptNote.Code = 'R' + codeFromDate;
        $scope.receiptNote.Number = codeFromDate;
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
        $scope.receiptNote.PaymentMethod = $scope.TypePayments[0].Alias;
        $scope.receiptNote.Date = new Date();
        $scope.formattedDate = $filter('date')($scope.receiptNote.Date, 'dd-MM-yyyy');
        $scope.AddReceiptNote = AddReceiptNote;

        $scope.$watch('receiptNote.Date', function (newValue) {
            $scope.formattedDate = $filter('date')(newValue, 'dd-MM-yyyy');
        });

        $scope.$watch("receiptItems", function (n, o) {
            if ($scope.receiptItems[0] == null) {
                stt = 0;
            }
        }, true);

        $scope.AddProductToTable = function (item) {
            stt = stt + 1;
            angular.forEach($scope.units, function (unit) {
                if (unit.ID == item.UnitID) {
                    $scope.productItems.UnitName = unit.Name;
                    $scope.productItems.Contain = unit.Contain;
                }
            })
            $scope.productItems.Stt = stt;
            $scope.productItems.Name = item.Name;
            $scope.productItems.ID = item.ID;
            $scope.productItems.UnitID = item.UnitID;
            $scope.productItems.Quantity = 0;
            $scope.productItems.RetailPrice = 0;
            $scope.productItems.VAT = 0;
            $scope.productItems.Discount = 0;
            $scope.productItems.BatchNumber = '';
            $scope.productItems.ExpiredDate = 0;
            $scope.productItems.Amount = 0;


            $scope.receiptItems.push($scope.productItems);
            $scope.productItems = {};
        }

        $scope.RemoveItem = function (item) {
            var index = $scope.receiptItems.indexOf(item);
            var template = item;
            $scope.receiptItems.splice(index, 1);
            for (i = index; i < $scope.receiptItems.length; i++) {
                $scope.receiptItems[i].Stt = template.Stt;
                template.Stt++;
            }
            stt = template.Stt - 1;
            updateAmountNote();
        }

        $scope.ChangeReceiptNote = function () {
            $scope.receiptNote.PaymentAmount = $scope.receiptNote.Amount + Math.round($scope.receiptNote.Amount * $scope.receiptNote.VAT / 100)
        }

        $scope.ChangeItem = function (item) {
            item.RetailPrice = new Number(item.RetailPrice);
            item.Quantity = new Number(item.Quantity);
            if (item.Quantity != NaN && item.RetailPrice != NaN) {
                var amountItem = Math.round(item.Quantity * item.RetailPrice);
                item.Amount = amountItem + Math.round(item.VAT * amountItem / 100) + Math.round(item.Discount * amountItem / 100);
                updateAmountNote();
            }
        }

        $scope.OnSubjectChange = function () {
            $scope.receiptNote.SupplierID = $scope.subject.ID;
        }

        $scope.OnProdctChange = function () {
            $scope.receiptNote.SupplierID = $scope.product.ID;
        }

        function updateAmountNote() {
            var totalAmount = 0;
            angular.forEach($scope.receiptItems, function (receiptItem) {
                totalAmount += receiptItem.Amount;
            });
            $scope.receiptNote.Amount = totalAmount;
            $scope.ChangeReceiptNote();
        }

        function AddReceiptNote() {

            apiService.post('api/receiptnote/create', $scope.receiptNote, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                console.log(result);
                AddReceiptNoteItemInReceiptNote(result.data);
            }, function (error) {
                notificationService.displayError('Tạo phiếu nhập mới thất bại');
            });

            /*$state.go('receiptNotes');*/
        }

        function AddReceiptNoteItemInReceiptNote(resultData) {
            angular.forEach($scope.receiptItems, function (receiptItem) {
                var receiptNoteItem = {};
                receiptNoteItem.NoteID = resultData.ID;
                receiptNoteItem.ProductID = receiptItem.ID;
                receiptNoteItem.BatchPrice = receiptItem.Contain == 0 ? receiptItem.RetailPrice : receiptItem.RetailPrice * receiptItem.Contain;
                receiptNoteItem.RetailPrice = receiptItem.RetailPrice;
                receiptNoteItem.VAT = receiptItem.VAT;
                receiptNoteItem.Discount = receiptItem.Discount;
                receiptNoteItem.BatchNumber = receiptItem.BatchNumber;
                receiptNoteItem.ExpiredDate = receiptItem.ExpiredDate;
                receiptNoteItem.Quantity = parseInt(receiptItem.Quantity);
                receiptNoteItem.UnitID = receiptItem.UnitID;

                AddReceiptNoteItem(receiptNoteItem);
            });
        }

        function AddReceiptNoteItem(receiptNoteItem) {
            apiService.post('api/receiptnoteitem/create', receiptNoteItem, function (result) {
                console.log(result);
                AddProductMapping(receiptNoteItem);
            }, function (error) {
                notificationService.displayError('Tạo sản phẩm trong phiếu nhập mới thất bại');
            });
        }

        function AddProductMapping(receiptNoteItem) {
            var productMapping = {};
            productMapping.ProductID = receiptNoteItem.ProductID;
            productMapping.BatchInPrice = receiptNoteItem.BatchPrice;
            productMapping.BatchInDate = $scope.receiptNote.Date;
            productMapping.RetailInPrice = receiptNoteItem.RetailPrice;
            productMapping.RetailInDate = $scope.receiptNote.Date;
            productMapping.BatchNumber = receiptNoteItem.BatchNumber;
            productMapping.ExpiredDate = receiptNoteItem.ExpiredDate;
            productMapping.Quantity = receiptNoteItem.Quantity;
            productMapping.UnitID = receiptNoteItem.UnitID;

            apiService.post('api/productmapping/create', productMapping, function (result) {
                console.log(result);
            }, function (error) {
                notificationService.displayError('Tạo kho lưu trữ mới thất bại');
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

        getProducts();
        getSubjects();
        getUnits();
    }
})(angular.module('pharma.receiptNotes'));