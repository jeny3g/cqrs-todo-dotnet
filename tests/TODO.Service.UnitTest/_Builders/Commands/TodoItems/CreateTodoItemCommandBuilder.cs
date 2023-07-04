using Todo.Service.Application.TodoItems.Commands.Create;

namespace Todo.Service.UnitTest._Builders.Commands.TodoItems;

public class CreateTodoItemCommandBuilder : BaseBuilder<CreateTodoItemCommand, CreateTodoItemCommand>
{
    public static new CreateTodoItemCommandBuilder New()
    {
        return new CreateTodoItemCommandBuilder()
        {
        };
    }

    public override CreateTodoItemCommand Build()
    {
        return new CreateTodoItemCommand()
        {
        };
    }
}
