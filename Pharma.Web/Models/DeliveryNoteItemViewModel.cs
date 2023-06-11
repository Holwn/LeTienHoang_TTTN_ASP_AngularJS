using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class DeliveryNoteItemViewModel
    {
        public int NoteID { set; get; }
        public int ProductID { set; get; }
        public int? UnitID { set; get; }
        public decimal? Price { set; get; }
        public int? Quantity { set; get; }
        public int? VAT { set; get; }
        public decimal? Discount { set; get; }
        public string BatchNumber { set; get; }
        public DateTime? ExpiredDate { set; get; }
        public virtual ProductViewModel Product { set; get; }
        public virtual DeliveryNoteViewModel DeliveryNote { set; get; }
    }
}