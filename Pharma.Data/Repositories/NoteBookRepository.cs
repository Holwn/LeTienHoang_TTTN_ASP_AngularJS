using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace Pharma.Data.Repositories
{
    public interface INoteBookRepository : IRepository<NoteBook>
    { }

    public class NoteBookRepository : RepositoryBase<NoteBook>, INoteBookRepository
    {
        public NoteBookRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}