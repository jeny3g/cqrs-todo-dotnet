using Todo.Service.Application.TodoItems.Queries.Get;

namespace Todo.Service.UnitTest._Builders.Queries.TodoItems;

public class ListTodoItemQueryBuilder : BaseBuilder<ListTodoItemQueryBuilder, ListTodoItemQuery>
{
    public static new ListTodoItemQueryBuilder New()
    {
        return new ListTodoItemQueryBuilder()
        {
        };
    }

    public override ListTodoItemQuery Build()
    {
        return new ListTodoItemQuery()
        {
        };
    }
}