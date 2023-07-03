namespace Todo.Service.Application.TodoItems.Queries.Get;

public class GetTodoItemQuery : IRequest<TodoItemViewModel>
{
    public Guid Id { get; set; }
}

public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItemViewModel>
{
    private readonly ITodoContext _context;
    private readonly Messages _messages;

    public GetTodoItemQueryHandler(ITodoContext context, Messages messages)
    {
        _context = context;
        _messages = messages;
    }

    public async Task<TodoItemViewModel> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        request ??= new GetTodoItemQuery();

        var item = await _context
            .TodoItems
            .Where(e => e.Id == request.Id)
            .ProjectToType<TodoItemViewModel>()
            .FirstOrDefaultAsync(cancellationToken);

        if (item is null)
            throw new NotFoundException(_messages.GetMessage(Messages.Entities.TODO_ITEM), request.Id);

        return item;
    }
}
