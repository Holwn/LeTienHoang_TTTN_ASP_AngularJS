﻿using Pharma.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("Stores")]
    public class Store : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string Alias { set; get; }

        [MaxLength(50)]
        public string Phone { set; get; }

        [MaxLength(256)]
        public string Email { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        [MaxLength(1024)]
        public string Barcode { set; get; }

        public int? RefStoreID { set; get; }
        public int? OwnerID { set; get; }
        [Required]
        public int RoleID { set; get; }
        public virtual IEnumerable<Product> Products { set; get; }
        public virtual IEnumerable<Unit> Units { set; get; }
        public virtual IEnumerable<Subject> Subjects { set; get; }
        public virtual IEnumerable<ReceiptNote> ReceiptNotes { set; get; }
        public virtual IEnumerable<DeliveryNote> DeliveryNotes { set; get; }

        [ForeignKey("RoleID")]
        public virtual StoreRole StoreRole { set; get; }
    }
}