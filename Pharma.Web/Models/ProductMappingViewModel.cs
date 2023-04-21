using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class ProductMappingViewModel
    {
        public int ID { set; get; }

        public int ProductID { set; get; }
        public decimal? BatchInPrice { set; get; }

        public DateTime? BatchInDate { set; get; }
        public decimal? RetailInPrice { set; get; }

        public DateTime? RetailInDate { set; get; }
        public string BatchNumber { set; get; }

        public DateTime? ExpiredDate { set; get; }
        public int? Quantity { set; get; }
        public int? UnitID { set; get; }
        public virtual UnitViewModel Unit { set; get; }
        public virtual ProductViewModel Product { set; get; }
    }
}