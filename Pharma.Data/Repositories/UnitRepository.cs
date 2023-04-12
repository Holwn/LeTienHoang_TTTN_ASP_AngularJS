using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IUnitRepository : IRepository<Unit> { }
    class UnitRepository:RepositoryBase<Unit>,IUnitRepository
    {
        public UnitRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
