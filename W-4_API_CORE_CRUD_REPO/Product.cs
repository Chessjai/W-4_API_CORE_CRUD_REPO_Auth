using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace W_4_API_CORE_CRUD_REPO
{
    [Table("products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(75)]
        public string Name { get; set; }
        [Required]
        [StringLength(75)]
        public string Category { get; set; }
        [Required]
        [StringLength(50)]
        public string Color { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
