using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Model.Abstract
{
    public abstract class Auditable
    {
        public DateTime? CreatedDate { set; get; }
        public int? CreatedBy { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public int? UpdatedBy { set; get; }
        public bool Status { set; get; }
    }
}