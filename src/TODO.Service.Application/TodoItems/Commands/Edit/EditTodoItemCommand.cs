namespace Todo.Service.Application.TodoItems.Commands.Edit;

public class EditTodoItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? DueDate { get; set; }
}

public class EditTodoItemCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? DueDate { get; set; }
}