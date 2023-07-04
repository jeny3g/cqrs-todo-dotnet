using Todo.Service.Application.TodoItems.Commands.Delete;

namespace Todo.Service.UnitTest._Builders.Commands.TodoItems;

public class DeleteTodoItemCommandBuilder : BaseBuilder<DeleteTodoItemCommand, DeleteTodoItemCommand>
{
    private Guid Id;

    public static new DeleteTodoItemCommandBuilder New()
    {
        return new DeleteTodoItemCommandBuilder()
        {
            Id = Guid.NewGuid()
        };
    }

    public override DeleteTodoItemCommand Build()
    {
        return new DeleteTodoItemCommand()
        {
            Id = Id
        };
    }

    public DeleteTodoItemCommandBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
}
