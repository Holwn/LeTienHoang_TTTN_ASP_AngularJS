using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("ProductMappings")]
    public class ProductMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int ProductID { set; get; }
        public decimal? BatchInPrice { set; get; }

        public DateTime? BatchInDate { set; get; }
        public decimal? RetailInPrice { set; get; }

        public DateTime? RetailInDate { set; get; }

        [MaxLength(256)]
        public string BatchNumber { set; get; }

        public DateTime? ExpiredDate { set; get; }
        public int? Quantity { set; get; }
        public int? UnitID { set; get; }

        [ForeignKey("UnitID")]
        public virtual Unit Unit { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }
    }
}