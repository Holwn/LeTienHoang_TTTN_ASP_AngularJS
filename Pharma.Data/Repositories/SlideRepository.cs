using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide> { }
    public class SlideRepository:RepositoryBase<Slide>,ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
