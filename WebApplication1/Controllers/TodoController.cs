using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            //var userID = User.Identity.GetUserId();
            var todos = _context.Todos.Include(t => t.Category).ToList();
            return View(todos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new TodoCategoriesViewModel
            {
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(TodoCategoriesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var todo = new Todo(viewModel.Todo);
                _context.Todos.Add(todo);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                viewModel.Categories = _context.Categories.ToList();
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Todo todo = _context.Todos.SingleOrDefault<Todo>(t => t.ID == id);

            if (todo == null)
            {
                return new HttpNotFoundResult();
            }

            var viewModel = new TodoCategoriesViewModel
            {
                Todo = todo,
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(TodoCategoriesViewModel viewModel)
        {
            var todoInDb = _context.Todos.SingleOrDefault(t => t.ID == viewModel.Todo.ID);

            if (todoInDb == null)
            {
                return HttpNotFound();
            }

            try
            {
                todoInDb.Description = viewModel.Todo.Description;
                todoInDb.DueDate = viewModel.Todo.DueDate;
                todoInDb.CategoryID = viewModel.Todo.CategoryID;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                viewModel.Categories = _context.Categories.ToList();
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var todo = _context.Todos.Include(t => t.Category).SingleOrDefault(t => t.ID == id);

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

        [HttpGet]
        public ActionResult Details(int id)
        {
            var todo = _context.Todos.Include(t => t.Category).SingleOrDefault(t => t.ID == id);

            if (todo == null)
            {
                return HttpNotFound();
            }

            return View(todo);
        }
    }
}