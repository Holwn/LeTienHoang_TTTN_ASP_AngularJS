using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class ProductViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public int CategoryID { set; get; }
        public string Image { set; get; }
        public string MoreImage { set; get; }
        public decimal? BatchPrice { set; get; }
        public decimal? RetailPrice { set; get; }
        public int BatchUnitID { set; get; }
        public int RetailUnitID { set; get; }
        public int? Contain { set; get; }
        public int? MiduleUnitID { set; get; }
        public int? ContainMidule { set; get; }
        public int? Quantity { set; get; }
        public string BatchNumber { set; get; }
        public DateTime? ExpiredDate { set; get; }
        public bool? IsHaveExpiredDate { set; get; }
        public string Barcode { set; get; }
        public string Description { set; get; }
        public string Contents { set; get; }
        public string Manufacturer { set; get; }
        public string AddressManufacturer { set; get; }
        public int? StoreID { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
        public bool? Prescription { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public string Tags { set; get; }
        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public string UpdatedBy { set; get; }
        public bool Status { set; get; }
        public virtual IEnumerable<ProductTagViewModel> ProductTags { set; get; }
        public virtual IEnumerable<ReceiptNoteItemViewModel> ReceiptNoteItems { set; get; }
        public virtual IEnumerable<DeliveryNoteItemViewModel> DeliveryNoteItems { set; get; }
        public virtual ProductCategoryViewModel ProductCategory { set; get; }
        public virtual StoreViewModel Store { set; get; }
    }
}