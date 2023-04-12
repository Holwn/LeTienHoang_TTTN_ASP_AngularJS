using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;


namespace Pharma.Data.Repositories
{
    public interface IMenuGroupRepository : IRepository<MenuGroup> { }
    public class MenuGroupRepository:RepositoryBase<MenuGroup>,IMenuGroupRepository
    {
        public MenuGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
