using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using Pharma.Service.ServiceModel;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IReceiptNoteItemRepository : IRepository<ReceiptNoteItem>
    {
        IEnumerable<ReceiptNoteItem> GetAllByNoteId(int noteId);
        IEnumerable<RNIGroupByExpiredDateModel> GroupByExpiredDateModels();
        ReceiptNoteItem GetByNoteId(int noteId, int productId);
    }
    class ReceiptNoteItemRepository : RepositoryBase<ReceiptNoteItem>, IReceiptNoteItemRepository
    {
        public ReceiptNoteItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ReceiptNoteItem> GetAllByNoteId(int noteId)
        {
            var query = from p in DbContext.ReceiptNoteItems
                        where p.NoteID == noteId
                        select p;
            return query;
        }

        public ReceiptNoteItem GetByNoteId(int noteId, int productId)
        {
            var query = from p in DbContext.ReceiptNoteItems
                        where p.NoteID == noteId && p.ProductID == productId
                        select p;
            return query.FirstOrDefault();
        }

        public IEnumerable<RNIGroupByExpiredDateModel> GroupByExpiredDateModels()
        {
            var query = from p in DbContext.ReceiptNoteItems
                        group p by new { p.ProductID, p.ExpiredDate } into g
                        select new RNIGroupByExpiredDateModel
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
