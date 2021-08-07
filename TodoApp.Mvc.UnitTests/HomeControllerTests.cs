using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TodoApp.Mvc.Controllers;
using TodoApp.Mvc.Models;

namespace TodoApp.Mvc.UnitTests
{
    [TestClass]
    public class TodoAppTests
    {
        [TestMethod]
        public void InstantiateList_GenerateTodo_ListIsNotNull()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act
            TodoList todoList = homeController.GenerateTodo("First todo [Unit Test]", System.DateTime.Now);

            // Assert
            Assert.IsNotNull(todoList.TodoItems);
        }

        [TestMethod]
        public void InstantiateList_GenerateTodosAndDeleteTodos_ListIsEmpty()
        {
            HomeController homeController = new HomeController();
            TodoList todoList = homeController.GenerateTodo("Second test todo [Unit test]", System.DateTime.Now);
            TodoItemModel item = new TodoItemModel() { Id = todoList.TodoItems.Count, TodoContent = "Todo #3 [Unit test]", Deadline = System.DateTime.Now };
            todoList.TodoItems.Add(item);

            //Act: Set Checked property of each Todo item in List to true
            foreach (var todo in todoList.TodoItems)
            homeController.ToggleClaim(todo.Id, true);

            //Imitate DeleteTodo() method from Controller
            todoList.TodoItems.RemoveAll(p => p.Checked == true);

            Assert.AreEqual(todoList.TodoItems.Count, 0);
        }
    }
}
