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
        [MaxLength(500)]
        public string Description { set; get; }
        public bool Status { set; get; }

        [ForeignKey("StoreID")]
        public virtual Store Store { set; get; }
    }
}