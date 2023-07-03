using Todo.Service.Application.TodoItems.Models;

namespace Todo.Service.Application.TodoItems.Queries.Get;

public class ListTodoItemQuery : IRequest<List<TodoItemViewModel>> { }

public class ListTodoItemQueryHandler : IRequestHandler<ListTodoItemQuery, List<TodoItemViewModel>>
{
    public readonly ITodoContext _context;

    public ListTodoItemQueryHandler(ITodoContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItemViewModel>> Handle(ListTodoItemQuery request, CancellationToken cancellationToken)
    {
        var items = new List<TodoItemViewModel>()
        {
            new TodoItemViewModel()
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Description = "Test",
                IsDone = false,
                DueDate = DateTime.Now,
                Active = true
            }
        };

        return items;
    }
}
