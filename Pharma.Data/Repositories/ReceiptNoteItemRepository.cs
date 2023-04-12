using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IReceiptNoteItemRepository : IRepository<ReceiptNoteItem> { }
    class ReceiptNoteItemRepository:RepositoryBase<ReceiptNoteItem>,IReceiptNoteItemRepository
    {
        public ReceiptNoteItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
