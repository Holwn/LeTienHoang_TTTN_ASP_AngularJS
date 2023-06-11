using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("DeliveryNoteItems")]
    public class DeliveryNoteItem
    {
        [Key]
        [Column(Order = 1)]
        public int NoteID { set; get; }

        [Key]
        [Column(Order = 2)]
        public int ProductID { set; get; }
        public int? UnitID { set; get; }
        public decimal? Price { set; get; }
        public int? Quantity { set; get; }
        public int? VAT { set; get; }
        public decimal? Discount { set; get; }

        [MaxLength(256)]
        public string BatchNumber { set; get; }

        public DateTime? ExpiredDate { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }

        [ForeignKey("NoteID")]
        public virtual DeliveryNote DeliveryNote { set; get; }
    }
}