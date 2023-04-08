using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Model.Models
{
    [Table("StoreRoles")]
    public class StoreRole
    {
        [Key]
        public int ID { set; get; }

        [Required]
        [MaxLength(128)]
        public string Name { set; get; }

        [MaxLength(256)]
        public string Description { set; get; }

        public virtual IEnumerable<Store> Stores { set; get; }
    }
}