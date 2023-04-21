using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class StoreViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Description { set; get; }
        public string Barcode { set; get; }

        public int? RefStoreID { set; get; }
        public int? OwnerID { set; get; }
        public int RoleID { set; get; }
        public DateTime? CreatedDate { set; get; }
        public int? CreatedBy { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public int? UpdatedBy { set; get; }
        public bool Status { set; get; }
        public virtual IEnumerable<ProductViewModel> Products { set; get; }
        public virtual IEnumerable<UnitViewModel> Units { set; get; }
        public virtual IEnumerable<SubjectViewModel> Subjects { set; get; }
        public virtual IEnumerable<ReceiptNoteViewModel> ReceiptNotes { set; get; }
        public virtual IEnumerable<DeliveryNoteViewModel> DeliveryNotes { set; get; }
        public virtual StoreRoleViewModel StoreRole { set; get; }
    }
}