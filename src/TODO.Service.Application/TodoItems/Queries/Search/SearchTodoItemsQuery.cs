using Todo.Service.Application.TodoItems.Models;

namespace Todo.Service.Application.TodoItems.Queries.Search;

public class SearchTodoItems
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool? IsDone { get; set; }
    public DateTime? DueDate { get; set; }
    public bool? Active { get; set; }
}

public class SearchTodoItemsQuery : IRequest<PageResponse<TodoItemSearchViewModel>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool? IsDone { get; set; }
    public DateTime? DueDate { get; set; }
    public bool? Active { get; set; }

    public PageRequest PageRequest { get; set; }
    public Ordination Ordination { get; set; }
}
