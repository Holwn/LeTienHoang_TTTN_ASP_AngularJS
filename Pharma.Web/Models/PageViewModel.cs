using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class PageViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }

        public string Contents { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public string UpdatedBy { set; get; }
        public bool Status { set; get; }
    }
}