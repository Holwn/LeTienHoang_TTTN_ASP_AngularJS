using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Model.ServiceModel
{
    public class DNIGroupByExpiredDateModel
    {
        public int ProductID { set; get; }
        public decimal? TotalPrice { set; get; }
        public int? TotalQuantity { set; get; }
        public DateTime? ExpiredDate { set; get; }
    }
}
