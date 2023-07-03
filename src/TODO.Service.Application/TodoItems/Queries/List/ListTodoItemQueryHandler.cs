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
        var items = await _context.TodoItems
            .ProjectToType<TodoItemViewModel>()
            .ToListAsync(cancellationToken);

        return items;
    }
}
