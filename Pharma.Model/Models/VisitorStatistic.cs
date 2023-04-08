using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("VisitorStatistics")]
    public class VisitorStatistic
    {
        [Key]
        public int ID { set; get; }

        public DateTime VisitedDate { set; get; }

        [MaxLength(50)]
        public string IPAddress { set; get; }
    }
}