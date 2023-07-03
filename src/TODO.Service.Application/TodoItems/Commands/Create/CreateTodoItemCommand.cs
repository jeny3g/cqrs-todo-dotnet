namespace Todo.Service.Application.TodoItems.Commands.Create;

public class CreateTodoItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? DueDate { get; set; }
}

public class CreateTodoItemCommand : IRequest<CreateSuccess>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? DueDate { get; set; }
}
