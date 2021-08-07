using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Mvc.Models;

namespace TodoApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private static int _TodoId;
        private readonly ILogger<HomeController> _logger;

        public TodoItemModel TodoModel = new TodoItemModel();
        public static TodoList _TodoList;
        public static List<TodoItemModel> _TodoItems;
       /* public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        static HomeController()
        {
            _TodoList = new TodoList();
            _TodoItems = new List<TodoItemModel>();
            _TodoId = 0;
        }

        /// <summary>
        /// Generates a Todo and adds it to the List<TodoItemModel>.
        /// </summary>
        /// <param name="todoContent">Content of the Todo item.</param>
        /// <param name="deadline">End date.</param>
        /// <returns>TodoList</returns>
        public TodoList GenerateTodo(string todoContent, DateTime deadline)
        {
            _TodoId++;
            TodoModel.Id = _TodoId;
            TodoModel.TodoContent = todoContent;
            TodoModel.Deadline = deadline;
            
            _TodoItems.Add(TodoModel);

            _TodoList.TodoItems = _TodoItems;

            return _TodoList;
        }

        /// <summary>
        /// Deletes all todo's in List that have had their Checked value set to True (which can be done by executing ToggleClaim(int id, bool value)).
        /// </summary>
        /// <returns>RedirectToAction("Index");</returns>
        [HttpGet]
        public IActionResult DeleteTodo()
        {
            _TodoList.TodoItems.RemoveAll(p => p.Checked == true);

            Debug.WriteLine("Deleted selected todo's.");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ToggleClaim(int id, bool value)
        {
            Debug.WriteLine("Value is: " + value);

            if (value)
            {
                foreach (var q in _TodoItems.Where(q => q.Id == id))
                {
                    q.Checked = true;
                };
            }
            else
            {
                foreach (var q in _TodoItems.Where(q => q.Id == id))
                {
                    q.Checked = false;
                };
            }

            Debug.WriteLine("Triggered ToggleClaim(); with id: " + id);
            
            return Ok();
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View(_TodoList);
        }

        [HttpPost]
        public IActionResult Index(string todoContent, DateTime deadline)
        {
            return View(GenerateTodo(todoContent, deadline));
        }

        public IActionResult About()
        { 
            return View(_TodoList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
