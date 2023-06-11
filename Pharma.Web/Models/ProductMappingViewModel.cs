using Pharma.Model.Models;
using System;

namespace Pharma.Web.Models
{
    public class ProductMappingViewModel
    {
        public int ID { set; get; }
        public int ProductID { set; get; }
        public int? RetailID { set; get; }
        public int? ReceiptQuantity { set; get; }
        public decimal? ReceiptPrice { set; get; }
        public int? DeliveryQuantity { set; get; }
        public decimal? DeliveryPrice { set; get; }
        public string BatchNumber { set; get; }
        public DateTime? ExpiredDate { set; get; }
        public virtual Product Product { set; get; }
    }
}