using Pharma.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharma.Model.Models
{
    [Table("Products")]
    public class Product : Auditable
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

        public int CategoryID { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImage { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BatchPrice { set; get; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RetailPrice { set; get; }

        public int UnitID { set; get; }

        [MaxLength(100)]
        public string Barcode { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        [MaxLength(2048)]
        public string Contents { set; get; }

        [MaxLength(1024)]
        public string Manufacturer { set; get; }

        [MaxLength(1024)]
        public string AddressManufacturer { set; get; }

        public int StoreID { set; get; }

        [MaxLength(256)]
        public string MetaKeyword { set; get; }

        [MaxLength(256)]
        public string MetaDescription { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public virtual IEnumerable<ProductTag> ProductTags { set; get; }
        public virtual IEnumerable<ProductMapping> ProductMappings { set; get; }
        public virtual IEnumerable<ReceiptNoteItem> ReceiptNoteItems { set; get; }
        public virtual IEnumerable<DeliveryNoteItem> DeliveryNoteItems { set; get; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { set; get; }

        [ForeignKey("StoreID")]
        public virtual Store Store { set; get; }

        [ForeignKey("UnitID")]
        public virtual Unit Unit { set; get; }
    }
}