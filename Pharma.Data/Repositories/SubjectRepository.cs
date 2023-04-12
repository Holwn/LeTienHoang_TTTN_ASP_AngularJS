using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface ISubjectRepository : IRepository<Subject> { }
    public class SubjectRepository:RepositoryBase<Subject>,ISubjectRepository
    {
        public SubjectRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
