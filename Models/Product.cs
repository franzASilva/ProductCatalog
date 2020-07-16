using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MaxLength(120, ErrorMessage = "The length for this field is between 3 and 120 characters")]
        [MinLength(3, ErrorMessage = "he length for this field is between 3 and 120 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MaxLength(1024, ErrorMessage = "The maximum length for this field is 1024 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than zero")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Required field")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MaxLength(1024, ErrorMessage = "The maximum length for this field is 1024 characters")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Required field")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Required field")]
        public DateTime LastUpdateDate { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
