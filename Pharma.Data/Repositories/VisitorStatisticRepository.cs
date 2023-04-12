using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IVisitorStatisticRepository : IRepository<VisitorStatistic> { }
    class VisitorStatisticRepository:RepositoryBase<VisitorStatistic>,IVisitorStatisticRepository
    {
        public VisitorStatisticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
