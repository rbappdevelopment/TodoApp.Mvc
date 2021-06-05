using System;
using System.Collections.Generic;

/// <summary>
/// Model for Todo items.
/// </summary>
namespace TodoApp.Mvc.Models
{
	public class TodoItemModel
	{
		public int Id { get; set; }

		public string TodoContent { get; set; }

		public DateTime Deadline { get; set; }

		public bool Checked { get; set; }

	}

	public class TodoList
	{
		public List<TodoItemModel> TodoItems { get; set; }

		public bool CheckedInList { get; set; }
	}
}