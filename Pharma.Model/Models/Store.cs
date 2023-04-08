using Pharma.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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