using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IStoreRepository : IRepository<Store> { }
    class StoreRepository:RepositoryBase<Store>,IStoreRepository
    {
        public StoreRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
