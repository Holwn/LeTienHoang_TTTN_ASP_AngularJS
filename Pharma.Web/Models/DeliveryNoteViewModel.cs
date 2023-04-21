using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class DeliveryNoteViewModel
    {
        public int ID { set; get; }
        public string Code { set; get; }
        public string Number { set; get; }

        public DateTime? Date { set; get; }
        public int CustomerID { set; get; }
        public string Description { set; get; }
        public int? VAT { set; get; }
        public decimal? Amount { set; get; }
        public decimal? PaymentAmount { set; get; }
        public string PaymentMethod { set; get; }
        public int? StoreID { set; get; }
        public DateTime? CreatedDate { set; get; }
        public int? CreatedBy { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public int? UpdatedBy { set; get; }
        public bool Status { set; get; }
        public virtual IEnumerable<DeliveryNoteItemViewModel> DeliveryNoteItems { set; get; }
        public virtual StoreViewModel Store { set; get; }
        public virtual SubjectViewModel Subject { set; get; }
    }
}