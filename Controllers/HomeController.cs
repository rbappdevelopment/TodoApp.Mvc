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
        private static int TodoId = 0;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            TodoList todoList = new TodoList();
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GenerateTodo(string todoContent, DateTime deadline)
        {
            TodoItemModel TodoModel = new TodoItemModel();

            TodoModel.Id = TodoId;
            TodoModel.TodoContent = todoContent;
            TodoModel.Deadline = deadline;

            return View(TodoModel);
        }
    }
}
