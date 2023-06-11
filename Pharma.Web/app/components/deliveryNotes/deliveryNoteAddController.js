/// <reference path="../../../assets/admin/libs/angularjs/angular.min.js" />

(function (app) {
    app.controller('deliveryNoteAddController', deliveryNoteAddController);

    deliveryNoteAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$http','$filter'];

    function deliveryNoteAddController(apiService, $scope, notificationService, $state, commonService, $http, $filter) {
        $scope.deliveryNote = {
            CustomerID: 5,
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
        $scope.deliveryItems = [];
        var stt = 0;
        var today = new Date();
        var codeFromDate = (today.getDate() + today.getSeconds()).toString() + (today.getMonth() + today.getMinutes()).toString() + (today.getFullYear() + today.getHours()).toString();
        $scope.deliveryNote.Code = 'R' + codeFromDate;
        $scope.deliveryNote.Number = codeFromDate;
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
        $scope.deliveryNote.PaymentMethod = $scope.TypePayments[0].Alias;
        $scope.deliveryNote.Date = new Date();
        $scope.formattedDate = $filter('date')($scope.deliveryNote.Date, 'dd-MM-yyyy');
        $scope.AddDeliveryNote = AddDeliveryNote;

        $scope.$watch('deliveryNote.Date', function (newValue) {
            $scope.formattedDate = $filter('date')(newValue, 'dd-MM-yyyy');
        });

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

        $scope.ChangeDeliveryNote = function () {
            $scope.deliveryNote.PaymentAmount = $scope.deliveryNote.Amount + Math.round($scope.deliveryNote.Amount * $scope.deliveryNote.VAT / 100)
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

        $scope.OnSubjectChange = function () {
            $scope.deliveryNote.CustomerID = $scope.subject.ID;
        }

        $scope.OnProdctChange = function () {
            $scope.deliveryNote.CustomerID = $scope.product.ID;
        }

        function updateAmountNote() {
            var totalAmount = 0;
            angular.forEach($scope.deliveryItems, function (deliveryItem) {
                totalAmount += deliveryItem.Amount;
            });
            $scope.deliveryNote.Amount = totalAmount;
            $scope.ChangeDeliveryNote();
        }

        function AddDeliveryNote() {

            apiService.post('api/deliverynote/create', $scope.deliveryNote, function (result) {
                notificationService.displaySuccess(result.data.Code + ' đã được thêm mới');

                AddDeliveryNoteItemIndeliveryNote(result.data);
            }, function (error) {
                notificationService.displayError('Tạo phiếu nhập mới thất bại');
            });

            $state.go('deliveryNotes');
        }

        function AddDeliveryNoteItemIndeliveryNote(resultData) {
            angular.forEach($scope.deliveryItems, function (deliveryItem) {
                var deliveryNoteItem = {};
                deliveryNoteItem.NoteID = resultData.ID;
                deliveryNoteItem.ProductID = deliveryItem.ID;
                deliveryNoteItem.Price = deliveryItem.Price;
                deliveryNoteItem.VAT = deliveryItem.VAT;
                deliveryNoteItem.Discount = deliveryItem.Discount;
                deliveryNoteItem.BatchNumber = deliveryItem.BatchNumber;
                deliveryNoteItem.ExpiredDate = deliveryItem.ExpiredDate;
                deliveryNoteItem.Quantity = parseInt(deliveryItem.Quantity);
                deliveryNoteItem.UnitID = deliveryItem.UnitID;

                AddDeliveryNoteItem(deliveryNoteItem);
            });
        }

        function AddDeliveryNoteItem(deliveryNoteItem) {
            apiService.post('api/deliverynoteitem/create', deliveryNoteItem, function (result) {
                $scope.LoadProductMapping();
            }, function (error) {
                notificationService.displayError('Tạo sản phẩm trong phiếu nhập mới thất bại');
            });
            $scope.LoadProductMapping();
        }

        $scope.LoadProductMapping = function() {
            apiService.get('api/productmapping/loadproductmapping', null, function (result) {
                notificationService.displaySuccess('Chạy kho thành công');
            }, function (error) {
                notificationService.displayError('Chạy kho thất bại');
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
                    if (item.Status == true && item.TypeSub == 'customer') {
                        $scope.subjects.push(item);
                    }
                })
                if ($scope.subjects.length > 0) {
                    for (i = 0; i < $scope.subjects.length; i++) {
                        if ($scope.subjects[i].ID == $scope.deliveryNote.CustomerID) {
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
                    if (item.Status == true && item.Quantity>0) {
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
})(angular.module('pharma.deliveryNotes'));