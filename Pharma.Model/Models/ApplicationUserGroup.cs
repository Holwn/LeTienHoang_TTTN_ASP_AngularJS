using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Model.Models
{
    [Table("ApplicationUserGroups")]
    public class ApplicationUserGroup
    {
        [Key]
        [Column(Order = 2)]
        public int GroupId { set; get; }
        [Key]
        [Column(Order = 1)]
        [StringLength(128)]
        public string UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { set; get; }
        [ForeignKey("GroupId")]
        public virtual ApplicationGroup ApplicationGroup { set; get; }
    }
}
