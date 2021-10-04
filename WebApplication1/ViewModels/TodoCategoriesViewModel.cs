using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class TodoCategoriesViewModel
    {
        public Todo Todo { get; set; }
        public List<Category> Categories { get; set; }
    }
}