using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public Todo() { }
        public Todo(Todo todo)
        {
            this.ID = todo.ID;
            this.Description = todo.Description;
            this.DueDate = todo.DueDate;
            this.CategoryID = todo.CategoryID;
        }
    }
}