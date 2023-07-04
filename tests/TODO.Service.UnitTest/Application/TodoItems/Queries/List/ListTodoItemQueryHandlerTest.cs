using Todo.Service.Application.TodoItems.Queries.Get;
using Todo.Service.Persistence;
using Todo.Service.UnitTest._Builders.Queries.TodoItems;

namespace Todo.Service.UnitTest.Application.TodoItems.Queries.List;

public class ListTodoItemQueryHandlerTest
{
    private ITodoContext _context;
    private ListTodoItemQueryHandler _sut;

    private Faker _faker;

    private ListTodoItemQueryBuilder _listTodoItemQueryBuilder;
    private TodoItemBuilder _todoItemBuilder;

    public ListTodoItemQueryHandlerTest()
    {
        Setup();
        InitializeBuilders();
        InitializeTestData();
        InitializeDependencies();
    }

    private void Setup()
    {
        _faker = new Faker();
        _context = Substitute.For<ITodoContext>();
        _sut = new ListTodoItemQueryHandler(_context);
    }

    private void InitializeBuilders()
    {
        _listTodoItemQueryBuilder = ListTodoItemQueryBuilder.New();
        _todoItemBuilder = TodoItemBuilder.New();
    }

    private void InitializeTestData()
    {
        // if there's data that needs to be initialized, add it here
    }

    private void InitializeDependencies()
    {
        var setTodoItems = new List<TodoItem>().AsDbSet();
        _context.TodoItems.Returns(setTodoItems);
    }

    [Fact]
    public async Task Handle_ShouldReturnActiveTodoItems()
    {
        var activeTodoItem = _todoItemBuilder.WithActive(true).Build();
        var inactiveTodoItem = _todoItemBuilder.WithActive(false).Build();

        SetupTodoItems(new List<TodoItem> { activeTodoItem, inactiveTodoItem });

        var request = _listTodoItemQueryBuilder.Build();

        var result = await _sut.Handle(request, default);

        Assert.Single(result);
        Assert.Equal(activeTodoItem.Id, result[0].Id);
        Assert.True(result[0].Active);
    }

    private void SetupTodoItems(List<TodoItem> todoItems)
    {
        var setTodoItems = todoItems.AsDbSet();
        _context.TodoItems.Returns(setTodoItems);
    }
}
