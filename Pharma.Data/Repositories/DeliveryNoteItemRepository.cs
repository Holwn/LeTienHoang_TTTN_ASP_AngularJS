using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using Pharma.Model.ServiceModel;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IDeliveryNoteItemRepository : IRepository<DeliveryNoteItem> {
        IEnumerable<DNIGroupByExpiredDateModel> GroupByExpiredDateModels();
        IEnumerable<DeliveryNoteItem> GetAllByNoteId(int noteId);
    }
    public class DeliveryNoteItemRepository:RepositoryBase<DeliveryNoteItem>,IDeliveryNoteItemRepository
    {
        public DeliveryNoteItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<DeliveryNoteItem> GetAllByNoteId(int noteId)
        {
            var query = from p in DbContext.DeliveryNoteItems
                        where p.NoteID == noteId
                        select p;
            return query;
        }

        public IEnumerable<DNIGroupByExpiredDateModel> GroupByExpiredDateModels()
        {
            var query = from p in DbContext.DeliveryNoteItems
                        group p by new { p.ProductID, p.ExpiredDate } into g
                        select new DNIGroupByExpiredDateModel
                        {
                            ProductID = g.Key.ProductID,
                            ExpiredDate = g.Key.ExpiredDate,
                            TotalQuantity = g.Sum(p => p.Quantity),
                            TotalPrice = g.Sum(p => p.Price * p.Quantity)
                                          + g.Sum(p => p.Price * p.Quantity * p.VAT / 100)
                                          - g.Sum(p => p.Price * p.Quantity * p.Discount / 100)
                        };

            return query.ToList();
        }
    }
}
