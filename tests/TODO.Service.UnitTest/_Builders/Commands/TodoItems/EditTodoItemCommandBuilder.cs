using Todo.Service.Application.TodoItems.Commands.Edit;

namespace Todo.Service.UnitTest._Builders.Commands.TodoItems;

public class EditTodoItemCommandBuilder : BaseBuilder<EditTodoItemCommand, EditTodoItemCommand>
{
    private Guid Id;

    public static new EditTodoItemCommandBuilder New()
    {
        return new EditTodoItemCommandBuilder()
        {
            Id = Guid.NewGuid(),
        };
    }

    public override EditTodoItemCommand Build()
    {
        return new EditTodoItemCommand()
        {
            Id = Id,
        };
    }

    public EditTodoItemCommandBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
}
