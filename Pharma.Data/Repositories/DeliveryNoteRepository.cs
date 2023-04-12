using Pharma.Data.Infrastructure;
using Pharma.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pharma.Data.Repositories
{
    public interface IDeliveryNoteRepository : IRepository<DeliveryNote> { }
    public class DeliveryNoteRepository : RepositoryBase<DeliveryNote>,IDeliveryNoteRepository
    {
        public DeliveryNoteRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
