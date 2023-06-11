using Pharma.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class TagViewModel
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public string Type { set; get; }

        public virtual IEnumerable<PostTag> PostTags { set; get; }
        public virtual IEnumerable<ProductTag> ProductTags { set; get; }
    }
}