using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("Units")]
    public class Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        public int? StoreID { set; get; }
        public int? ParentID { set; get; }
        public int? Contain { set; get; }
        public bool Status { set; get; }

        public virtual IEnumerable<Product> Products { set; get; }
        public virtual IEnumerable<ProductMapping> ProductMappings { set; get; }

        [ForeignKey("StoreID")]
        public virtual Store Store { set; get; }
    }
}