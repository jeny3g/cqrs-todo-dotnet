using Todo.Service.Application.TodoItems.Queries.Get;
using Todo.Service.Persistence;
using Todo.Service.UnitTest._Builders.Queries.TodoItems;

namespace Todo.Service.UnitTest.Application.TodoItems.Queries.Get;

public class GetTodoItemQueryHandlerTest
{
    private ITodoContext _context;
    private Messages _messages;
    private GetTodoItemQueryHandler _sut;

    private Faker _faker;

    private GetTodoItemQueryBuilder _getTodoItemQueryBuilder;
    private TodoItemBuilder _todoItemBuilder;

    private Guid _id;

    public GetTodoItemQueryHandlerTest()
    {
        Setup();
        InitializeTestData();
        InitializeBuilders();
        InitializeDependencies();
    }

    private void Setup()
    {
        _faker = new Faker();
        _context = Substitute.For<ITodoContext>();
        _messages = MessagesConfig.Build();
        _sut = new GetTodoItemQueryHandler(_context, _messages);
    }

    private void InitializeBuilders()
    {
        _getTodoItemQueryBuilder = GetTodoItemQueryBuilder.New();
        _todoItemBuilder = TodoItemBuilder.New();
    }

    private void InitializeTestData()
    {
        _id = _faker.Random.Guid();
    }

    private void InitializeDependencies()
    {
        var setTodoItems = new List<TodoItem>().AsDbSet();
        _context.TodoItems.Returns(setTodoItems);

    }

    [Fact]
    public async Task Must_Throw_NotFoundException_When_Not_Found_Entity()
    {
        var request = _getTodoItemQueryBuilder.Build();

        var ex = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(request, CancellationToken.None));

        Assert.Equal(_messages.GetMessage(Messages.Entities.TODO_ITEM), ex.Name);
        Assert.Equal(request.Id, ex.Key);
    }

    [Fact]
    public async Task Must_Return_Entity()
    {
        var request = _getTodoItemQueryBuilder.WithId(_id).Build();
        SetupTodoItems(_id);

        var result = await _sut.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(request.Id, result.Id);
    }


    private void SetupTodoItems(Guid id)
    {
        var todoItem = _todoItemBuilder.WithId(id).Build();

        var setTodoItems = new List<TodoItem>() { todoItem }.AsDbSet();

        _context.TodoItems.Returns(setTodoItems);
    }
}
