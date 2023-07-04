using Todo.Service.Application.TodoItems.Queries.Get;
using Todo.Service.Persistence;
using Todo.Service.UnitTest._Builders.Queries.TodoItems;

namespace Todo.Service.UnitTest.Application.TodoItems.Queries.List;

public class ListTodoItemQueryHandlerTest
{
    private ITodoContext _context;
    private ListTodoItemQueryHandler _sut;

    private ListTodoItemQueryBuilder _listTodoItemQueryBuilder;
    private TodoItemBuilder _todoItemBuilder;

    public ListTodoItemQueryHandlerTest()
    {
        SetupDependencies();
        InitializeTestData();
    }

    private void SetupDependencies()
    {
        _context = Substitute.For<ITodoContext>();
        _sut = new ListTodoItemQueryHandler(_context);
    }

    private void InitializeTestData()
    {
        _listTodoItemQueryBuilder = ListTodoItemQueryBuilder.New();
        _todoItemBuilder = TodoItemBuilder.New();

        var set = new List<TodoItem>() { _todoItemBuilder.Build() }.AsDbSet();

        _context.TodoItems.Returns(set);
    }


    [Fact]
    public async Task Handle_ShouldReturnActiveTodoItems()
    {
        var activeTodoItem = _todoItemBuilder.WithActive(true).Build();
        var inactiveTodoItem = _todoItemBuilder.WithActive(false).Build();

        var setTodoItems = new List<TodoItem>
        {
            activeTodoItem,
            inactiveTodoItem
        }.AsDbSet();

        _context.TodoItems.Returns(setTodoItems);

        var request = _listTodoItemQueryBuilder.Build();

        var result = await _sut.Handle(request, default);

        Assert.Single(result);
        Assert.Equal(activeTodoItem.Id, result[0].Id);
        Assert.True(result[0].Active);
    }
}