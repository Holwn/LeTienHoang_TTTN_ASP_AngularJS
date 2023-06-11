(function (app) {
    app.controller('deliveryNoteEditController', deliveryNoteEditController);

    deliveryNoteEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService', '$filter'];

    function deliveryNoteEditController(apiService, $scope, notificationService, $state, $stateParams, commonService, $filter) {
        $scope.deliveryNote = {
        };
        $scope.formattedDate = $filter('date')($scope.deliveryNote.Date, 'dd-MM-yyyy');
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
        $scope.deliveryItems = [];
        var stt = 0;
        $scope.UpdateDeliveryNote = UpdateDeliveryNote;

        $scope.$watch("deliveryItems", function (n, o) {
            if ($scope.deliveryItems[0] == null) {
                stt = 0;
            }
        }, true);

        $scope.AddProductToTable = function (item) {
            stt = stt + 1;
            angular.forEach($scope.units, function (unit) {
                if (unit.ID == item.RetailUnitID) {
                    $scope.productItems.UnitName = unit.Name;
                }
            })
            $scope.productItems.Stt = stt;
            $scope.productItems.Name = item.Name;
            $scope.productItems.ID = item.ID;
            $scope.productItems.UnitID = item.RetailUnitID;
            $scope.productItems.Quantity = 0;
            $scope.productItems.QuantityMapping = item.Quantity;
            $scope.productItems.Price = 0;
            $scope.productItems.PriceMapping = item.BatchPrice;
            $scope.productItems.VAT = 0;
            $scope.productItems.Discount = 0;
            $scope.productItems.BatchNumber = '';
            $scope.productItems.ExpiredDate = item.ExpiredDate;
            $scope.productItems.Amount = 0;


            $scope.deliveryItems.push($scope.productItems);
            $scope.productItems = {};
        }

        $scope.RemoveItem = function (item) {
            var index = $scope.deliveryItems.indexOf(item);
            var template = item;
            $scope.deliveryItems.splice(index, 1);
            for (i = index; i < $scope.deliveryItems.length; i++) {
                $scope.deliveryItems[i].Stt = template.Stt;
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

        $scope.ChangeDeliveryNote = function () {
            $scope.deliveryNote.PaymentAmount = $scope.deliveryNote.Amount + Math.round($scope.deliveryNote.Amount * $scope.deliveryNote.VAT / 100)
        }

        function updateAmountNote() {
            var totalAmount = 0;
            angular.forEach($scope.deliveryItems, function (deliveryItem) {
                totalAmount += deliveryItem.Amount;
            });
            $scope.deliveryNote.Amount = totalAmount;
            $scope.ChangeDeliveryNote();
        }

        $scope.$watch('deliveryNote.Date', function (newValue) {
            $scope.formattedDate = $filter('date')(newValue, 'dd-MM-yyyy');
        });

        $scope.OnSubjectChange = function () {
            $scope.deliveryNote.SupplierID = $scope.subject.ID;
        }

        $scope.OnProdctChange = function () {
            $scope.deliveryNote.SupplierID = $scope.product.ID;
        }

        function loadDeliveryNoteDetail() {
            apiService.get('api/deliverynote/getbyid/' + $stateParams.id, null, function (result) {
                var resultNote = result.data;
                resultNote.Date = new Date(resultNote.Date);

                $scope.deliveryNote = resultNote;
                loadDeliveryNoteItem(resultNote.ID);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }



        function loadDeliveryNoteItem(deliveryNoteId) {
            var config = {
                params: {
                    noteId: deliveryNoteId
                }
            }

            apiService.get('api/deliverynoteitem/getall', config, function (result) {
                var resultNoteItems = result.data;
                angular.forEach(resultNoteItems, function (resultNoteItem) {

                    $scope.productItems.ProductID = resultNoteItem.ProductID;
                    $scope.productItems.UnitID = resultNoteItem.UnitID;
                    $scope.productItems.Quantity = resultNoteItem.Quantity;
                    $scope.productItems.Price = resultNoteItem.Price;
                    $scope.productItems.VAT = resultNoteItem.VAT;
                    $scope.productItems.Discount = resultNoteItem.Discount;
                    $scope.productItems.BatchNumber = resultNoteItem.BatchNumber;
                    $scope.productItems.ExpiredDate = $filter('date')(resultNoteItem.ExpiredDate, 'dd/MM/yyyy');
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
                                    $scope.deliveryItems.push($scope.productItems);
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

        function UpdateDeliveryNote() {
            apiService.put('api/deliverynote/update', $scope.deliveryNote, function (result) {
                notificationService.displaySuccess(result.data.Code + ' đã được cập nhật');

                UpdateDeliveryNoteItemIndeliveryNote(result.data);

            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
            $state.go('deliveryNotes');
        }

        function UpdateDeliveryNoteItemIndeliveryNote(resultData) {
            angular.forEach($scope.deliveryItems, function (deliveryItem) {
                var deliveryNoteItem = {};
                deliveryNoteItem.NoteID = parseInt(resultData.ID);
                deliveryNoteItem.ProductID = parseInt(deliveryItem.ProductID);
                deliveryNoteItem.BatchPrice = deliveryItem.Contain == 0 ? parseInt(deliveryItem.Price) : parseInt(deliveryItem.Price) * deliveryItem.Contain;
                deliveryNoteItem.Price = parseInt(deliveryItem.Price);
                deliveryNoteItem.VAT = deliveryItem.VAT;
                deliveryNoteItem.Discount = deliveryItem.Discount;
                deliveryNoteItem.BatchNumber = deliveryItem.BatchNumber;
                deliveryNoteItem.Quantity = parseInt(deliveryItem.Quantity);
                deliveryNoteItem.UnitID = deliveryItem.UnitID;

                deliveryNoteItem.ExpiredDate = deliveryItem.ExpiredDate;

                UpdateDeliveryNoteItem(deliveryNoteItem);
            });
        }
        function UpdateDeliveryNoteItem(deliveryNoteItem) {
            apiService.put('api/deliverynoteitem/update', deliveryNoteItem, function (result) {
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
                loadDeliveryNoteDetail();
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
                        if ($scope.subjects[i].ID == $scope.deliveryNote.SupplierID) {
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
                loadDeliveryNoteDetail();
            }, function () {
                console.log('Get product failed.');
            });
        }

        getProducts();
        getSubjects();
        getUnits();
    }

})(angular.module('pharma.deliveryNotes'));