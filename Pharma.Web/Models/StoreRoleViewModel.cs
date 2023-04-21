using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class StoreRoleViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }

        public virtual IEnumerable<StoreViewModel> Stores { set; get; }
    }
}