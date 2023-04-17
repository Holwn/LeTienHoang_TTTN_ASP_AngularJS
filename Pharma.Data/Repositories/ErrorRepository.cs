using Pharma.Data.Infrastructure;
using Pharma.Model.Models;

namespace Pharma.Data.Repositories
{
    public interface IErrorRepository : IRepository<Error>
    { }

    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}