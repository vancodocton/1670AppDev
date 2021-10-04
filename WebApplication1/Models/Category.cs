using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }
    }
}