using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IProductMappingRepository : IRepository<ProductMapping> { }
    class ProductMappingRepository:RepositoryBase<ProductMapping>,IProductMappingRepository
    {
        public ProductMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
