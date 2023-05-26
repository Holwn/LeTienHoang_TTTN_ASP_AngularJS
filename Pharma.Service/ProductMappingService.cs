using Pharma.Data.Infrastructure;
using Pharma.Data.Repositories;
using Pharma.Model.Models;
using System.Collections.Generic;

namespace Pharma.Service
{
    public interface IProductMappingService
    {
        ProductMapping Add(ProductMapping ProductMapping);

        void Update(ProductMapping ProductMapping);

        ProductMapping Delete(int id);

        IEnumerable<ProductMapping> GetAll();

        ProductMapping GetById(int id);

        void Save();
    }

    public class ProductMappingService : IProductMappingService
    {
        private IProductMappingRepository _productMappingRepository;
        private IUnitOfWork _unitOfWork;

        public ProductMappingService(IProductMappingRepository ProductMappingRepository, IUnitOfWork unitOfWork)
        {
            this._productMappingRepository = ProductMappingRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductMapping Add(ProductMapping ProductMapping)
        {
            ProductMapping mapping;
            if (_productMappingRepository.Count(x=>
            x.ProductID== ProductMapping.ProductID 
            && x.BatchInPrice== ProductMapping.BatchInPrice
            && x.RetailInPrice== ProductMapping.RetailInPrice
            && x.ExpiredDate== ProductMapping.ExpiredDate)!=0)
            {
                var ProductMappingData = _productMappingRepository.GetAll();
                foreach (var mappingdata in ProductMappingData)
                {
                    if(mappingdata.ProductID == ProductMapping.ProductID
                        && mappingdata.BatchInPrice == ProductMapping.BatchInPrice
                        && mappingdata.RetailInPrice == ProductMapping.RetailInPrice
                        && mappingdata.ExpiredDate == ProductMapping.ExpiredDate)
                    {
                        mappingdata.Quantity += ProductMapping.Quantity;

                        mapping = mappingdata;
                        _productMappingRepository.Update(mapping);
                    }    
                }
                return new ProductMapping();
            }
            else
            {
                return _productMappingRepository.Add(ProductMapping);
            }
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

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductMapping ProductMapping)
        {
            _productMappingRepository.Update(ProductMapping);
        }
    }
}