using Pharma.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("DeliveryNotes")]
    public class DeliveryNote : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(128)]
        public string Code { set; get; }

        [MaxLength(128)]
        public string Number { set; get; }

        public DateTime? Date { set; get; }
        [Required]
        public int CustomerID { set; get; }

        [MaxLength(256)]
        public string Description { set; get; }

        public int? VAT { set; get; }
        public decimal? Amount { set; get; }
        public decimal? PaymentAmount { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }
        public int? StoreID { set; get; }
        public virtual IEnumerable<DeliveryNoteItem> DeliveryNoteItems { set; get; }

        [ForeignKey("StoreID")]
        public virtual Store Store { set; get; }

        [ForeignKey("CustomerID")]
        public virtual Subject Subject { set; get; }
    }
}