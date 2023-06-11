using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using Pharma.Model.ServiceModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IProductMappingRepository : IRepository<ProductMapping>
    {
        ProductMapping GetByExpiredDate(DateTime expiredDate, int productId);

        int? SumByProductIds(int productId);
    }
    class ProductMappingRepository : RepositoryBase<ProductMapping>, IProductMappingRepository
    {
        public ProductMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public ProductMapping GetByExpiredDate(DateTime expiredDate, int productId)
        {
            var query = from p in DbContext.ProductMappings
                        where p.ProductID == productId && p.ExpiredDate == expiredDate
                        select p;
            return query.FirstOrDefault();
        }

        public int? SumByProductIds(int productId)
        {
            var query = (from p in DbContext.ProductMappings
                        where p.ProductID == productId
                        select p.ReceiptQuantity - p.DeliveryQuantity).Sum();
            return query;
        }
    }
}
