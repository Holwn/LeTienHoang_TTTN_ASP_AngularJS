using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class SubjectViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Description { set; get; }
        public string Barcode { set; get; }
        public string TypeSub { set; get; }
        public int? StoreID { set; get; }
        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public string UpdatedBy { set; get; }
        public bool Status { set; get; }
        public virtual IEnumerable<ReceiptNoteViewModel> ReceiptNotes { set; get; }
        public virtual IEnumerable<DeliveryNoteViewModel> DeliveryNotes { set; get; }
        public virtual StoreViewModel Store { set; get; }
    }
}