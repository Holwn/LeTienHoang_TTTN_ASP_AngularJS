using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BatchPrice { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RetailPrice { set; get; }

        public int? Quantity { set; get; }
        public int? VAT { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { set; get; }

        [MaxLength(256)]
        public string BatchNumber { set; get; }

        public DateTime? ExpiredDate { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }

        [ForeignKey("NoteID")]
        public virtual DeliveryNote DeliveryNote { set; get; }
    }
}