using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("ProductMappings")]
    public class ProductMapping
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        [Key]
        [Column(Order = 2)]
        public int ProductID { set; get; }
        public int? RetailID { set; get; }
        public int? ReceiptQuantity { set; get; }
        public decimal? ReceiptPrice  { set; get; }
        public int? DeliveryQuantity { set; get; }
        public decimal? DeliveryPrice { set; get; }

        [MaxLength(256)]
        public string BatchNumber { set; get; }

        public DateTime? ExpiredDate { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }
    }
}