using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("ReceiptNoteItems")]
    public class ReceiptNoteItem
    {
        [Key]
        [Column(Order = 1)]
        public int NoteID { set; get; }

        [Key]
        [Column(Order = 2)]
        public int ProductID { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BatchPrice { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RetailPrice { set; get; }

        public int? VAT { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { set; get; }

        [MaxLength(256)]
        public string BatchNumber { set; get; }

        public DateTime? ExpiredDate { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }

        [ForeignKey("NoteID")]
        public virtual ReceiptNote ReceiptNote { set; get; }
    }
}