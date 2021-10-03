using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
namespace WebApplication.Controllers
{
    public class TodoController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Todo
        public ActionResult Index()
        {
            List<Todo> todos = _context.Todos.ToList();
            return View(todos);
        }
    }
}