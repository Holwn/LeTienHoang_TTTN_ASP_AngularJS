using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IStoreRoleRepository : IRepository<StoreRole> { }
    class StoreRoleRepository:RepositoryBase<StoreRole>,IStoreRoleRepository
    {
        public StoreRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
