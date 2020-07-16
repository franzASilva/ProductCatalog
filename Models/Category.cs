using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MaxLength(120, ErrorMessage = "The length for this field is between 3 and 120 characters")]
        [MinLength(3, ErrorMessage = "The length for this field is between 3 and 120 characters")]
        public string Title { get; set; }
    }
}