using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IDeliveryNoteItemRepository : IRepository<DeliveryNoteItem> { }
    public class DeliveryNoteItemRepository:RepositoryBase<DeliveryNoteItem>,IDeliveryNoteItemRepository
    {
        public DeliveryNoteItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
