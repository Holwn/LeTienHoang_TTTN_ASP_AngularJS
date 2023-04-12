using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IReceiptNoteRepository : IRepository<ReceiptNote> { }
    class ReceiptNoteRepository:RepositoryBase<ReceiptNote>,IReceiptNoteRepository
    {
        public ReceiptNoteRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
