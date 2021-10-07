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
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var todos = _context.Todos.Include(t => t.Category).Where(t => t.UserID == userID).ToList();
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
                viewModel.Categories = _context.Categories.ToList();
                return View(viewModel);
            }

            try
            {
                var todo = new Todo(viewModel.Todo)
                {
                    UserID = User.Identity.GetUserId()
                };

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
            var userID = User.Identity.GetUserId();
            Todo todo = _context.Todos.SingleOrDefault<Todo>(t => t.ID == id && t.UserID == userID);

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
            var userID = User.Identity.GetUserId();
            var todoInDb = _context.Todos.SingleOrDefault(t => t.ID == viewModel.Todo.ID && t.UserID == userID);

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
            var userID = User.Identity.GetUserId();
            var todo = _context.Todos.Include(t => t.Category).SingleOrDefault(t => t.ID == id && t.UserID == userID);

            if (todo == null)
            {
                return HttpNotFound();
            }

            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var userID = User.Identity.GetUserId();
            var todo = _context.Todos.SingleOrDefault(t => t.ID == id && t.UserID == userID);

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
            var userID = User.Identity.GetUserId();
            var todo = _context.Todos.Include(t => t.Category).SingleOrDefault(t => t.ID == id && t.UserID == userID);

            if (todo == null)
            {
                return HttpNotFound();
            }

            return View(todo);
        }
    }
}