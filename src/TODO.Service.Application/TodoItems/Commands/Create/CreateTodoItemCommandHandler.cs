namespace Todo.Service.Application.TodoItems.Commands.Create;

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, CreateSuccess>
{
    private readonly ITodoContext _context;

    public CreateTodoItemCommandHandler(ITodoContext context)
    {
        _context = context;
    }

    public async Task<CreateSuccess> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        request ??= new CreateTodoItemCommand();

        var newItem = request.Adapt<TodoItem>();

        try
        {
            newItem.Active = true;

            _context.TodoItems.Add(newItem);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new PersistenceException(ex);
        }

        return new CreateSuccess(newItem.Id);
    }
}
