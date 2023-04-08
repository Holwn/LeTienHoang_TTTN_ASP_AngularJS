﻿using Pharma.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Model.Models
{
    [Table("ReceiptNotes")]
    public class ReceiptNote : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(128)]
        public string Code { set; get; }

        [MaxLength(128)]
        public string Number { set; get; }

        public DateTime? Date { set; get; }
        public int SupplierID { set; get; }

        [MaxLength(256)]
        public string Description { set; get; }

        public int? VAT { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaymentAmount { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }

        public int StoreID { set; get; }
        public virtual IEnumerable<ReceiptNoteItem> ReceiptNoteItems { set; get; }

        [ForeignKey("StoreID")]
        public virtual Store Store { set; get; }

        [ForeignKey("SupplierID")]
        public virtual Subject Subject { set; get; }
    }
}