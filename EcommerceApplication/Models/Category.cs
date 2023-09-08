using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApplication.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10, ErrorMessage = "This field must be between 1 to 10 characters.")]
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage = "Order amount must be between 1 to 100.")]
        [DisplayName("Display Order")]
        public int Order { get; set; }

    }

}
