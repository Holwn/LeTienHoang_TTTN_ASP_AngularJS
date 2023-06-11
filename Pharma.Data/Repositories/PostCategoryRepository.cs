using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory> {

    }
    public class PostCategoryRepository:RepositoryBase<PostCategory>,IPostCategoryRepository
    {
        public PostCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
