namespace Todo.Service.Application.TodoItems.Models;

public class TodoItemSearchViewModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsDone { get; set; }

    public DateTime? DueDate { get; set; }

    public bool Active { get; set; }
}


