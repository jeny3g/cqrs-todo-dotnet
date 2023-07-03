namespace Todo.Service.Application.TodoItems.Commands.Edit;

public class EditTodoItemCommandHandler : IRequestHandler<EditTodoItemCommand>
{
    private readonly ITodoContext _context;

    public EditTodoItemCommandHandler(ITodoContext context)
    {
        _context = context;
    }

    public async Task Handle(EditTodoItemCommand request, CancellationToken cancellationToken)
    {
        request ??= new EditTodoItemCommand();

        var entity = await _context
            .TodoItems
            .Where(e => e.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity is null)
            return;

        try
        {
            request.Adapt(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new PersistenceException(ex);
        }
    }
}