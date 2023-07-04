using Todo.Service.Application.TodoItems.Queries.Get;
using Todo.Service.Persistence;
using Todo.Service.UnitTest._Builders.Queries.TodoItems;

namespace Todo.Service.UnitTest.Application.TodoItems.Queries.Get;

public class GetTodoItemQueryHandlerTest
{
    private ITodoContext _context;
    private Messages _messages;
    private GetTodoItemQueryHandler _sut;

    private GetTodoItemQueryBuilder _getTodoItemQueryBuilder;
    private TodoItemBuilder _todoItemBuilder;
    private TodoItem _item;

    public GetTodoItemQueryHandlerTest()
    {
        SetupDependencies();
        InitializeTestData();
    }

    private void SetupDependencies()
    {
        _context = Substitute.For<ITodoContext>();
        _messages = MessagesConfig.Build();
        _sut = new GetTodoItemQueryHandler(_context, _messages);
    }

    private void InitializeTestData()
    {
        _getTodoItemQueryBuilder = GetTodoItemQueryBuilder.New();
        _todoItemBuilder = TodoItemBuilder.New();

        var set = new List<TodoItem>() { _item }.AsDbSet();

        _context.TodoItems.Returns(set);
    }

    [Fact]
    public async Task Must_Throw_NotFoundException_When_Not_Found_Entity()
    {
        var request = _getTodoItemQueryBuilder.Build();

        var setTransactions = new List<TodoItem> { }.AsDbSet();

        _context.TodoItems.Returns(setTransactions);
        var ex = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(request, CancellationToken.None));

        Assert.Equal(_messages.GetMessage(Messages.Entities.TODO_ITEM), ex.Name);
        Assert.Equal(request.Id, ex.Key);
    }

    [Fact]
    public async Task Must_Return_Entity()
    {
        var request = _getTodoItemQueryBuilder.Build();

        var expectedTransaction = CreateTodoItemSet(request.Id);

        var result = await _sut.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(expectedTransaction.Id, result.Id);
    }

    private TodoItem CreateTodoItemSet(Guid guid)
    {
        var transaction = _todoItemBuilder.WithId(guid).Build();

        var setTransactions = new List<TodoItem>
        {
            transaction
        }.AsDbSet();

        _context.TodoItems.Returns(setTransactions);

        return transaction;
    }
}