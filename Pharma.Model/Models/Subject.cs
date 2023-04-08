using Pharma.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("Subjects")]
    public class Subject : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

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

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string TypeSub { set; get; }

        public int StoreID { set; get; }
        public virtual IEnumerable<ReceiptNote> ReceiptNotes { set; get; }
        public virtual IEnumerable<DeliveryNote> DeliveryNotes { set; get; }

        [ForeignKey("StoreID")]
        public virtual Store Store { set; get; }
    }
}