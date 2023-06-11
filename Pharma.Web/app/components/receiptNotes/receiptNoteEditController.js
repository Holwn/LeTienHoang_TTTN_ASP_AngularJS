(function (app) {
    app.controller('receiptNoteEditController', receiptNoteEditController);

    receiptNoteEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService', '$filter'];

    function receiptNoteEditController(apiService, $scope, notificationService, $state, $stateParams, commonService, $filter) {
        $scope.receiptNote = {
        };
        $scope.formattedDate = $filter('date')($scope.receiptNote.Date, 'dd-MM-yyyy');
        $scope.dateFormat = 'dd/MM/yyyy';
        $scope.baseDate = new Date();
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
        $scope.units = [];
        $scope.productItems = {};
        $scope.receiptItems = [];
        var stt = 0;
        $scope.UpdateReceiptNote = UpdateReceiptNote;

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
                }
            })
            $scope.productItems.Stt = stt;
            $scope.productItems.Name = item.Name;
            $scope.productItems.ProductID = item.ID;
            $scope.productItems.UnitID = item.UnitID;
            $scope.productItems.Quantity = 0;
            $scope.productItems.Price = 0;
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

        $scope.ChangeItem = function (item) {
            item.Price = new Number(item.Price);
            item.Quantity = new Number(item.Quantity);
            if (item.Quantity != NaN && item.Price != NaN) {
                var amountItem = Math.round(item.Quantity * item.Price);
                item.Amount = amountItem + Math.round(item.VAT * amountItem / 100) - Math.round(item.Discount * amountItem / 100);
                updateAmountNote();
            }
        }

        $scope.ChangeReceiptNote = function () {
            $scope.receiptNote.PaymentAmount = $scope.receiptNote.Amount + Math.round($scope.receiptNote.Amount * $scope.receiptNote.VAT / 100)
        }

        function updateAmountNote() {
            var totalAmount = 0;
            angular.forEach($scope.receiptItems, function (receiptItem) {
                totalAmount += receiptItem.Amount;
            });
            $scope.receiptNote.Amount = totalAmount;
            $scope.ChangeReceiptNote();
        }

        $scope.$watch('receiptNote.Date', function (newValue) {
            $scope.formattedDate = $filter('date')(newValue, 'dd-MM-yyyy');
        });

        $scope.OnSubjectChange = function () {
            $scope.receiptNote.SupplierID = $scope.subject.ID;
        }

        $scope.OnProdctChange = function () {
            $scope.receiptNote.SupplierID = $scope.product.ID;
        }

        function loadReceiptNoteDetail() {
            apiService.get('api/receiptNote/getbyid/' + $stateParams.id, null, function (result) {
                var resultNote = result.data;
                resultNote.Date = new Date(resultNote.Date);

                $scope.receiptNote = resultNote;
                loadReceiptNoteItem(resultNote.ID);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }



        function loadReceiptNoteItem(receiptNoteId) {
            var config = {
                params: {
                    noteId: receiptNoteId
                }
            }

            apiService.get('api/receiptnoteitem/getall', config, function (result) {
                var resultNoteItems = result.data;
                angular.forEach(resultNoteItems, function (resultNoteItem) {

                    $scope.productItems.ProductID = resultNoteItem.ProductID;
                    $scope.productItems.UnitID = resultNoteItem.UnitID;
                    $scope.productItems.Quantity = resultNoteItem.Quantity;
                    $scope.productItems.Price = resultNoteItem.Price;
                    $scope.productItems.VAT = resultNoteItem.VAT;
                    $scope.productItems.Discount = resultNoteItem.Discount;
                    $scope.productItems.BatchNumber = resultNoteItem.BatchNumber;
                    $scope.productItems.ExpiredDate = $filter('date')(new Date(resultNoteItem.ExpiredDate), 'dd/MM/yyyy');
                    var amountItem = Math.round(resultNoteItem.Quantity * resultNoteItem.Price);
                    $scope.productItems.Amount = amountItem + Math.round(resultNoteItem.VAT * amountItem / 100) + Math.round(resultNoteItem.Discount * amountItem / 100);

                    angular.forEach($scope.products, function (product) {
                        if (product.ID == resultNoteItem.ProductID) {
                            $scope.productItems.Name = product.Name;
                            angular.forEach($scope.units, function (unit) {
                                if (unit.ID == resultNoteItem.UnitID) {
                                    stt = stt + 1;

                                    $scope.productItems.Stt = stt;
                                    $scope.productItems.UnitName = unit.Name;
                                    $scope.productItems.Contain = unit.Contain;

                                    $scope.receiptItems.push($scope.productItems);
                                    $scope.productItems = {};
                                }
                            })
                        }
                    })
                });


                updateAmountNote();
            }, function (error) {
                notificationService.displayError('Không lấy được dữ liệu chi tiết phiếu.');
            });
        }

        $scope.formatDate = function (dateString) {
            return new Date(dateString);
        };

        function UpdateReceiptNote() {
            apiService.put('api/receiptnote/update', $scope.receiptNote, function (result) {
                notificationService.displaySuccess(result.data.Code + ' đã được cập nhật');

                UpdateReceiptNoteItemInReceiptNote(result.data);

            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
            $state.go('receiptNotes');
        }

        function UpdateReceiptNoteItemInReceiptNote(resultData) {
            angular.forEach($scope.receiptItems, function (receiptItem) {
                var receiptNoteItem = {};
                receiptNoteItem.NoteID = parseInt(resultData.ID);
                receiptNoteItem.ProductID = parseInt(receiptItem.ProductID);
                receiptNoteItem.BatchPrice = receiptItem.Contain == 0 ? parseInt(receiptItem.Price) : parseInt(receiptItem.Price) * receiptItem.Contain;
                receiptNoteItem.Price = parseInt(receiptItem.Price);
                receiptNoteItem.VAT = receiptItem.VAT;
                receiptNoteItem.Discount = receiptItem.Discount;
                receiptNoteItem.BatchNumber = receiptItem.BatchNumber;
                receiptNoteItem.Quantity = parseInt(receiptItem.Quantity);
                receiptNoteItem.UnitID = receiptItem.UnitID;

                var dateString = receiptItem.ExpiredDate;
                if (dateString.includes('/')) {
                    var parts = dateString.split("/"); // Split the date string by "/"
                    var day = parseInt(parts[0], 10); // Parse the day part as an integer
                    var month = parseInt(parts[1], 10 - 1); // Parse the month part as an integer (subtract 1 as months are zero-based in JavaScript)
                    var year = parseInt(parts[2], 10); // Parse the year part as an integer
                    var isoDateString = `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
                    var date = new Date(isoDateString + 'T00:00:00');
                    receiptNoteItem.ExpiredDate = date.toISOString();
                }
                else {
                    receiptNoteItem.ExpiredDate = dateString;
                }
                

                UpdateReceiptNoteItem(receiptNoteItem);
            });
        }
        function UpdateReceiptNoteItem(receiptNoteItem) {
            apiService.put('api/receiptnoteitem/update', receiptNoteItem, function (result) {
                notificationService.displaySuccess('Chi tiết phiếu nhập đã được cập nhật');
                
            }, function (error) {
                notificationService.displayError('Cập nhật chi tiết phiếu nhập thất bại.');
            });
            $scope.LoadProductMapping();
        }

        $scope.LoadProductMapping = function () {
            apiService.get('api/productmapping/loadproductmapping', null, function (result) {
                notificationService.displaySuccess('Chạy kho thành công');
            }, function (error) {
                notificationService.displayError('Chạy kho thất bại');
            });
        }

        function getUnits() {
            apiService.get('api/unit/getallparents', null, function (result) {
                $scope.units = result.data;
                loadReceiptNoteDetail();
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
                loadReceiptNoteDetail();
            }, function () {
                console.log('Get product failed.');
            });
        }

        getProducts();
        getSubjects();
        getUnits();
    }

})(angular.module('pharma.receiptNotes'));