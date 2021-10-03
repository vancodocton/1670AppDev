using System;

namespace WebApplication.Models
{
    public class Todo
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public Todo()
        {
        }

        public Todo(int id, string description, DateTime dueDate)
        {
            this.ID = id;
            this.Description = description;
            this.DueDate = dueDate;
        }
        public Todo(Todo todo) : this(todo.ID, todo.Description, todo.DueDate) { }
    }
}