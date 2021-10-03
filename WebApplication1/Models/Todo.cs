using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Todo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}