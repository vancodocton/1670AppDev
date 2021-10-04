using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TodoController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Todo
        public ActionResult Index()
        {
            List<Todo> todos = _context.Todos.Include(t => t.Category).ToList();
            return View(todos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return View(todo);
            }

            _context.Todos.Add(todo);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var todo = _context.Todos.SingleOrDefault(t => t.ID == id);

            if (todo == null)
            {
                return HttpNotFound();
            }

            return View(todo);
        }

        [HttpPost]
        public ActionResult Edit(Todo todo)
        {
            var todoInDb = _context.Todos.SingleOrDefault(t => t.ID == todo.ID);

            if (todoInDb == null)
                return HttpNotFound();
            else
            {
                todoInDb.Description = todo.Description;
                todoInDb.DueDate = todo.DueDate;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var todo = _context.Todos.SingleOrDefault(t => t.ID == id);

            if (todo == null)
            {
                return HttpNotFound();
            }

            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var todo = _context.Todos.SingleOrDefault(t => t.ID == id);

            if (todo == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Todos.Remove(todo);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Details(int id)
        {
            var todo = _context.Todos.SingleOrDefault(t => t.ID == id);

            if (todo == null)
            {
                return HttpNotFound();
            }

            return View(todo);
        }
    }
}