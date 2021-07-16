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
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        static HomeController()
        {
            _TodoList = new TodoList();
            _TodoItems = new List<TodoItemModel>();
            _TodoId = 0;
        }

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

        /*  [HttpPost]
          public IActionResult DeleteTodo()
          {
              TodoList CheckedTodosInList = new TodoList();

              foreach (var todo in _TodoList.TodoItems)
              {
                  if (todo.Checked)
                  {
                      CheckedTodosInList.TodoItems.Add(todo);
                      _TodoList.TodoItems.Remove(todo);
                      Debug.WriteLine("Removed todo from static List. Total items left in _TodoList: " + _TodoList.TodoItems.Count);
                  }
              }

              return View("~/Views/Home/Index.cshtml", _TodoList);
          }*/

        [HttpGet]
        public IActionResult DeleteTodo()
        {
            var TodoItemsCheckedList = _TodoItems.Where(z => z.Checked == true).ToList();

            TodoItemsCheckedList.RemoveAll(p => p.Checked == true);

            _TodoList.TodoItems.RemoveAll(p => p.Checked == true);

            Debug.WriteLine("Deleted selected todo's.");

            //foreach (var q in checkedStatus)
            //{
            //    var checkedTodo = _TodoList.TodoItems.Where(x => x.Id == q).FirstOrDefault();
            //    checkedTodo.Checked = true;
            //    _TodoList.TodoItems.Remove(checkedTodo);
            //}
            //other logic
            return View("~/Views/Home/Index.cshtml", _TodoList);
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
