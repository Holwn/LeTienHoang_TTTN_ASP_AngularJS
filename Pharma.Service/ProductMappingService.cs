using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Service
{
    public interface IProductMappingService
    {
        ProductMapping Add(ProductMapping ProductMapping);

        void Update(ProductMapping ProductMapping);
        void LoadingProductMapping();

        ProductMapping Delete(int id);

        IEnumerable<ProductMapping> GetAll();

        ProductMapping GetById(int id);

        void Save();
    }

    public class ProductMappingService : IProductMappingService
    {
        private IProductMappingRepository _productMappingRepository;
        private IProductRepository _productRepository;
        private IReceiptNoteItemRepository _receiptNoteItemRepository;
        private IDeliveryNoteItemRepository _deliveryNoteItemRepository;
        private IUnitOfWork _unitOfWork;

        public ProductMappingService(IProductMappingRepository productMappingRepository,IProductRepository productRepository,IReceiptNoteItemRepository receiptNoteItemRepository,IDeliveryNoteItemRepository deliveryNoteItemRepository, IUnitOfWork unitOfWork)
        {
            this._productMappingRepository = productMappingRepository;
            this._productRepository = productRepository;
            this._receiptNoteItemRepository = receiptNoteItemRepository;
            this._deliveryNoteItemRepository = deliveryNoteItemRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductMapping Add(ProductMapping productMapping)
        {
            return _productMappingRepository.Add(productMapping);
        }

        public ProductMapping Delete(int id)
        {
            return _productMappingRepository.Delete(id);
        }

        public IEnumerable<ProductMapping> GetAll()
        {
            return _productMappingRepository.GetAll();
        }

        public ProductMapping GetById(int id)
        {
            return _productMappingRepository.GetSingleById(id);
        }

        public void LoadingProductMapping()
        {
            var receiptNoteItemGroups = _receiptNoteItemRepository.GroupByExpiredDateModels();
            var deliveryNoteItemGroups = _deliveryNoteItemRepository.GroupByExpiredDateModels();
            var productMapings = _productMappingRepository.GetAll();
            if(productMapings.Count(x => x.ID > 0) != 0)
            {
                foreach(var productMaping in productMapings)
                {
                    _productMappingRepository.Delete(productMaping);
                }
                _unitOfWork.Commit();
            }
            productMapings = _productMappingRepository.GetAll();
            if (productMapings.Count(x => x.ID > 0) == 0)
            {
                if (receiptNoteItemGroups.Count(x => x.ProductID > 0) !=0 )
                {
                    foreach (var receiptNoteItemGroup in receiptNoteItemGroups)
                    {
                        var product = _productRepository.GetSingleById(receiptNoteItemGroup.ProductID);
                        var contain = product.Contain * product.ContainMidule;

                        ProductMapping productMappingNew = new ProductMapping();
                        productMappingNew.ProductID = receiptNoteItemGroup.ProductID;
                        productMappingNew.ReceiptPrice = receiptNoteItemGroup.TotalPrice;
                        productMappingNew.ReceiptQuantity = receiptNoteItemGroup.TotalQuantity * contain;
                        productMappingNew.DeliveryQuantity = 0;
                        productMappingNew.DeliveryPrice = 0;
                        productMappingNew.RetailID = product.RetailUnitID;
                        productMappingNew.ExpiredDate = receiptNoteItemGroup.ExpiredDate;

                        _productMappingRepository.Add(productMappingNew);
                    }
                    _unitOfWork.Commit();
                    var productMapingNews = _productMappingRepository.GetAll();

                    if (deliveryNoteItemGroups.Count(x => x.ProductID > 0) != 0)
                    {
                        foreach(var deliveryNoteItemGroup in deliveryNoteItemGroups)
                        {
                            foreach(var productMapingNew in productMapingNews)
                            {
                                if(productMapingNew.ProductID==deliveryNoteItemGroup.ProductID && productMapingNew.ExpiredDate.ToString().Substring(0,10) == deliveryNoteItemGroup.ExpiredDate.ToString().Substring(0, 10))
                                {
                                    productMapingNew.DeliveryQuantity = deliveryNoteItemGroup.TotalQuantity;
                                    productMapingNew.DeliveryPrice = deliveryNoteItemGroup.TotalPrice;

                                    _productMappingRepository.Update(productMapingNew);
                                }    
                            }
                            _unitOfWork.Commit();
                        }
                    }

                    var productMapingForProducts = _productMappingRepository.GetAll();
                    foreach (var productMapingForProduct in productMapingForProducts.OrderByDescending(p => p.ExpiredDate))
                    {
                        Product productGetQuantity = _productRepository.GetSingleById(productMapingForProduct.ProductID);
                        productGetQuantity.Quantity = _productMappingRepository.SumByProductIds(productMapingForProduct.ProductID);
                        productGetQuantity.ExpiredDate = productMapingForProduct.ExpiredDate;
                        productGetQuantity.IsHaveExpiredDate = true;
                        productGetQuantity.UpdatedDate = System.DateTime.Now;

                        _productRepository.Update(productGetQuantity);
                    }
                    _unitOfWork.Commit();
                }
            }
            
            
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductMapping productMapping)
        {
            _productMappingRepository.Update(productMapping);
        }
    }
}