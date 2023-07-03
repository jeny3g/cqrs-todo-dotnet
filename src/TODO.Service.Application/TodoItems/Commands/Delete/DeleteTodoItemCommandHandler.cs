namespace Todo.Service.Application.TodoItems.Commands.Delete;


public class DeleteTodoItemCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly ITodoContext _context;
    private readonly Messages _messages;

    public DeleteTodoItemCommandHandler(ITodoContext context, Messages messages)
    {
        _context = context;
        _messages = messages;
    }

    public async Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        request ??= new DeleteTodoItemCommand();

        var entity = await _context
            .TodoItems
            .Where(e =>
                e.Id == request.Id &&
                e.Active)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity is null)
            throw new NotFoundException(_messages.GetMessage(Messages.Entities.TODO_ITEM), request.Id);

        try
        {
            entity.Active = false;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new PersistenceException(ex);
        }
    }
}
