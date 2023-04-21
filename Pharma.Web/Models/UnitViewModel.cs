using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class UnitViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int? StoreID { set; get; }
        public int? ParentID { set; get; }
        public int? Contain { set; get; }
        public bool Status { set; get; }

        public virtual IEnumerable<ProductViewModel> Products { set; get; }
        public virtual IEnumerable<ProductMappingViewModel> ProductMappings { set; get; }
        public virtual StoreViewModel Store { set; get; }
    }
}