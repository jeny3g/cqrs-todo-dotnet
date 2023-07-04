using Todo.Service.Application.TodoItems.Queries.Get;

namespace Todo.Service.UnitTest._Builders.Queries.TodoItem;

public class GetTodoItemQueryBuilder : BaseBuilder<GetTodoItemQueryBuilder, GetTodoItemQuery>
{
    private Guid Id;

    public static new GetTodoItemQueryBuilder New()
    {
        return new GetTodoItemQueryBuilder()
        {
            Id = Guid.NewGuid()
        };
    }

    public override GetTodoItemQuery Build()
    {
        return new GetTodoItemQuery()
        {
            Id = Id
        };
    }
}